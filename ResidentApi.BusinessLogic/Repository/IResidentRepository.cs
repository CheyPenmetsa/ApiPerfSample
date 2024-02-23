using ResidentApi.BusinessLogic.Models;

namespace ResidentApi.BusinessLogic.Repository
{
    public interface IResidentRepository
    {
        Task SaveResidentAsync(ResidentModel model);

        Task<ResidentModel> GetResidentAsync(string id);

        Task<long> GetTotalDocumentsCountAsync();
    }
}
