using Microsoft.AspNetCore.Mvc;
using ResidentApi.BusinessLogic.Models;
using ResidentApi.BusinessLogic.Repository;
using ResidentApi.BusinessLogic.UtilityService;
using System.Text;

namespace ResidentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentController : ControllerBase
    {
        private readonly IExternalService _externalService;

        private readonly IResidentRepository _residentRepository;

        public ResidentController(IExternalService externalService, IResidentRepository residentRepository)
        {
            _externalService = externalService;
            _residentRepository = residentRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> GetResidentDetailsAsync([FromQuery] string id)
        {
            var resident = await _residentRepository.GetResidentAsync(id);
            return Ok(resident);
        }

        [HttpPost]
        public async Task<IActionResult> CreateResidentAsync([FromBody] SaveResidentModel saveResidentModel)
        {
            var residentUtilityBalance = await _externalService.GetResidentUtilityBalanceByIdAsync(saveResidentModel.Email);

            var totalBalance = default(double);
            if(residentUtilityBalance != null)
            {
                totalBalance = residentUtilityBalance.ElectricityBalance + residentUtilityBalance.WaterBalance + residentUtilityBalance.TrashBalance;
            }
            var totalResidentsCount = await _residentRepository.GetTotalDocumentsCountAsync();
            var residentModel = new ResidentModel()
            {
                ResidentId = (totalResidentsCount+1).ToString(),
                FirstName = saveResidentModel.FirstName,
                LastName = saveResidentModel.LastName,
                Email = saveResidentModel.Email,
                TotalBalance = totalBalance,
                ApartmentNumber = saveResidentModel.ApartmentNumber
            };
           await _residentRepository.SaveResidentAsync(residentModel);
            return Ok(residentModel.ResidentId);
        }

        [HttpPut()]
        public async Task<IActionResult> SaveResidentAsync([FromQuery] string id, [FromBody] SaveResidentModel saveResidentModel)
        {
            var residentUtilityBalance = await _externalService.GetResidentUtilityBalanceByIdAsync(saveResidentModel.Email);

            var totalBalance = default(double);
            if (residentUtilityBalance != null)
            {
                totalBalance = residentUtilityBalance.ElectricityBalance + residentUtilityBalance.WaterBalance + residentUtilityBalance.TrashBalance;
            }
            var residentModel = new ResidentModel()
            {
                ResidentId = id,
                FirstName = saveResidentModel.FirstName,
                LastName = saveResidentModel.LastName,
                Email = saveResidentModel.Email,
                TotalBalance = totalBalance,
                ApartmentNumber = saveResidentModel.ApartmentNumber
            };
            await _residentRepository.SaveResidentAsync(residentModel);
            return Ok();
        }
    }
}
