namespace CoolieMint.WebApp.Dtos.Automation
{
    public interface IConditionDto
    {
        public ConditionDtoType ConditionType { get; set; }
        public bool IsInverted { get; set; }
    }
}
