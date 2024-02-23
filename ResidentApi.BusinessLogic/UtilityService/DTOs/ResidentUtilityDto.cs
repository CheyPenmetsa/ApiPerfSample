namespace ResidentApi.BusinessLogic.UtilityService.DTOs
{
    public class ResidentUtilityDto
    {
        public double ElectricityBalance { get; set; }

        public double WaterBalance { get; set; }

        public double TrashBalance { get; set; }

        public DateTime PeriodStart { get; set; }

        public DateTime PeriodEnd { get; set; }

        public double TotalPastDueBalance { get; set; }
    }
}
