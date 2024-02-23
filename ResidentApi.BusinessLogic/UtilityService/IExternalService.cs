using ResidentApi.BusinessLogic.UtilityService.DTOs;

namespace ResidentApi.BusinessLogic.UtilityService
{
    public interface IExternalService
    {
        Task<ResidentUtilityDto?> GetResidentUtilityBalanceByIdAsync(string residentEmail);
    }
}
