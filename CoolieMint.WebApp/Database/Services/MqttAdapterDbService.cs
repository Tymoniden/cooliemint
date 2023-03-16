using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebControlCenter.CommandAdapter;
using WebControlCenter.Database.Entities;
using WebControlCenter.Database.Repository;
using WebControlCenter.Services;

namespace WebControlCenter.Database.Services
{

    public class MqttAdapterDbService : IMqttAdapterDbService
    {
        private readonly IJsonSerializerService _jsonSerializerService;
        private readonly IEntityFactory _entityFactory;
        private readonly IModelFactory _modelFactory;
        private readonly IRepositoryService _repositoryService;

        public MqttAdapterDbService(IJsonSerializerService jsonSerializerService, IRepositoryService repositoryService, IEntityFactory entityFactory, IModelFactory modelFactory)
        {
            _jsonSerializerService = jsonSerializerService ?? throw new System.ArgumentNullException(nameof(jsonSerializerService));
            _entityFactory = entityFactory ?? throw new System.ArgumentNullException(nameof(entityFactory));
            _modelFactory = modelFactory ?? throw new System.ArgumentNullException(nameof(modelFactory));
            _repositoryService = repositoryService ?? throw new System.ArgumentNullException(nameof(repositoryService));
        }

        public Models.Controller InitializeAdapter(IMqttAdapter mqttAdapter)
        {
            using (var ctx = _repositoryService.GetContext())
            {
                var controller = ctx.Controller.Include(c => c.ControllerStateInformations).FirstOrDefault(c => c.Identifier == mqttAdapter.Identifier);
                if (controller == null)
                {
                    controller = InsertController(ctx, mqttAdapter);
                }

                foreach (var controllerState in GetUnknownStates(ctx, controller, mqttAdapter.GetPossibleStates()))
                {
                    InsertControllerState(ctx, controller, controllerState);
                }

                ctx.SaveChanges();

                return _modelFactory.CreateController(controller);
            }
        }

        List<IControllerState> GetUnknownStates(ISqLiteContext ctx, Controller controller, List<IControllerState> states)
        {
            if (states?.Any() != true)
            {
                return new List<IControllerState>();
            }

            var existingStates = ctx.ControllerStateInformation.Where(stateInfo => stateInfo.ControllerId == controller.Id);
            return states.Where(state => !existingStates.Any(existingState => existingState.State == state.State)).ToList();
        }

        Controller InsertController(ISqLiteContext ctx, IMqttAdapter mqttAdapter)
        {
            var controller = _entityFactory.CreateController();
            controller.Identifier = mqttAdapter.Identifier;
            controller.Type = mqttAdapter.Type;
            controller.InitializationArguments = _jsonSerializerService.Serialize(mqttAdapter.GetInitializationArguments(), SerializerSettings.ApiSerializer);

            ctx.Add(controller);

            return controller;
        }

        public void InsertControllerState(Controller controller, IControllerState controllerState)
        {
            using(var ctx = _repositoryService.GetContext())
            {
                InsertControllerState(ctx, controller, controllerState);

                ctx.SaveChanges();
            }
        }

        void InsertControllerState(ISqLiteContext ctx, Controller controller, IControllerState controllerState)
        {
            var stateModel = new ControllerStateInformation();
            stateModel.State = controllerState.State;
            stateModel.PowerConsumption = controllerState.PowerConsumption;
            stateModel.Controller = controller;

            ctx.Add(stateModel);
        }

        public List<Models.ControllerStateInformation> GetStates(Models.Controller controller)
        {
            using(var ctx = _repositoryService.GetContext())
            {
                return GetStates(ctx, controller);
            }
        }

        List<Models.ControllerStateInformation> GetStates(ISqLiteContext ctx, Models.Controller controller)
        {
            return ctx.ControllerStateInformation
                .Where(stateInfo => stateInfo.ControllerId == controller.Id)
                .Select(state => _modelFactory.CreateControllerStateInformation(state))
                .ToList();
        }
    }
}
