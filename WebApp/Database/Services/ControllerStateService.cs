using System;
using System.Linq;
using WebControlCenter.Database.Entities;
using WebControlCenter.Database.Repository;

namespace WebControlCenter.Database.Services
{
    public class ControllerStateService : IControllerStateService
    {
        readonly IEntityFactory _entityFactory;
        private readonly IModelFactory _modelFactory;
        private readonly IRepositoryService _repositoryService;

        public ControllerStateService(IRepositoryService repositoryService, IEntityFactory entityFactory, IModelFactory modelFactory)
        {
            _entityFactory = entityFactory ?? throw new ArgumentNullException(nameof(entityFactory));
            _modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
            _repositoryService = repositoryService ?? throw new ArgumentNullException(nameof(repositoryService));
        }

        public ControllerStateHistory ActivateState(ISqLiteContext ctx, ControllerStateInformation controllerStateInformation, DateTime startTime)
        {
            var history = _entityFactory.CreateStateHistory(controllerStateInformation, startTime);
            ctx.Add(history);

            return history;
        }

        public DateTime TerminateState(ISqLiteContext ctx, ControllerStateInformation state)
        {
            var terminateDate = DateTime.UtcNow;

            var history = ctx.ControllerStateHistory.FirstOrDefault(h => h.ControllerStateInformationId == state.Id && h.EndTime == DateTime.MinValue);
            if (history != null)
            {
                history.EndTime = terminateDate;
            }

            return terminateDate;
        }

        public ControllerStateHistory ReplaceState(ISqLiteContext ctx, Models.ControllerStateInformation newStateModel, Models.ControllerStateInformation oldStateModel)
        {
            var newState = _entityFactory.CreateControllerStateInformation(newStateModel);
            var oldState = _entityFactory.CreateControllerStateInformation(oldStateModel);

            var terminationDate = TerminateState(ctx, oldState);
            return ActivateState(ctx, newState, terminationDate);
        }

        public Models.ControllerStateHistory ReplaceState(Models.ControllerStateInformation newStateModel, Models.ControllerStateInformation oldStateModel)
        {
            using(var ctx = _repositoryService.GetContext())
            {
                var history = ReplaceState(ctx, newStateModel, oldStateModel);
                ctx.SaveChanges();

                return _modelFactory.CreateControllerStateHistory(history);
            }
        }
    }
}
