using System;
using System.Collections.Generic;
using WebControlCenter.CommandAdapter;
using WebControlCenter.Database.Entities;
using WebControlCenter.Services;
using Controller = WebControlCenter.Database.Entities.Controller;

namespace WebControlCenter.Database.Services
{
    public class EntityRepository : IEntityRepository
    {
        private readonly IEntityFactory _modelFactory;
        private readonly IJsonSerializerService _jsonSerializerService;

        public EntityRepository(IEntityFactory modelFactory, IJsonSerializerService jsonSerializerService)
        {
            _modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
            _jsonSerializerService = jsonSerializerService ?? throw new ArgumentNullException(nameof(jsonSerializerService));
        }

        public void AddAdapter(IMqttAdapter adapter)
        {
            using (var ctx = new SqliteContext())
            {
                var controller = _modelFactory.CreateController();
                controller.Identifier = adapter.Identifier;
                controller.Type = adapter.Type;
                controller.InitializationArguments = _jsonSerializerService.Serialize(adapter.GetInitializationArguments());

                foreach(var state in adapter.GetPossibleStates())
                {
                    var stateModel = new ControllerStateInformation();
                    stateModel.State = state.State;
                    stateModel.PowerConsumption = state.PowerConsumption;
                    
                    ctx.Add(stateModel);
                    controller.ControllerStateInformations.Add(stateModel);
                }

                ctx.Add(controller);
                ctx.SaveChanges();
            }
        }

        public void AddAdapters(List<IMqttAdapter> adapters)
        {
            using (var ctx = new SqliteContext())
            {
                foreach (var adapter in adapters)
                {
                    var controller = _modelFactory.CreateController();
                    controller.Identifier = adapter.Identifier;
                    controller.Type = adapter.Type;
                    controller.InitializationArguments = _jsonSerializerService.Serialize(adapter.GetInitializationArguments());

                    foreach (var state in adapter.GetPossibleStates())
                    {
                        var stateModel = new ControllerStateInformation();
                        stateModel.State = state.State;
                        stateModel.PowerConsumption = state.PowerConsumption;
                        stateModel.Controller = controller;

                        ctx.Add(stateModel);
                    }

                    ctx.Add(controller);
                }

                ctx.SaveChanges();
            }
        }
    }
}