using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebControlCenter.Database.Entities;
using WebControlCenter.Database.Services;

namespace WebControlCenter.Database.Repository
{
    public class StateRepository : IStateRepository
    {
        private readonly IRepositoryService _repositoryService;
        private readonly IControllerStateService _controllerStateService;

        public StateRepository(IRepositoryService repositoryService, IControllerStateService controllerStateService)
        {
            _repositoryService = repositoryService ?? throw new System.ArgumentNullException(nameof(repositoryService));
            _controllerStateService = controllerStateService ?? throw new System.ArgumentNullException(nameof(controllerStateService));
        }

        public ControllerStateInformation GetStateInformation(string controllerType, string controllerIdentifier, string stateName)
        {
            using (var ctx = _repositoryService.GetContext())
            {
                return ctx.ControllerStateInformation
                    .Include(state => state.Controller)
                    .FirstOrDefault(state =>
                    state.Controller.Type == controllerType &&
                    state.Controller.Identifier == controllerIdentifier &&
                    state.State == stateName);
            }
        }

        public void UpdateState(ControllerStateInformation newState, DateTime timestamp, ControllerStateInformation oldState)
        {
            using(var ctx = _repositoryService.GetContext())
            {
                if(oldState != null)
                {

                }
            }
        }
    }
}
