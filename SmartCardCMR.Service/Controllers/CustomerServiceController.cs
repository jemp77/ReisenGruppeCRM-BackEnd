using Microsoft.AspNetCore.Mvc;
using SmartCardCRM.Data;
using SmartCardCRM.Data.Entities;
using SmartCardCRM.Model.Models;

namespace SmartCardCRM.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerServiceController : ControllerBase
    {
        private readonly CustomerServiceData CustomerServiceData;

        public CustomerServiceController(SmartCardCRMContext context)
        {
            CustomerServiceData = new CustomerServiceData(context);
        }

        [HttpGet]
        [Route("[action]/{contractId}")]
        public ActionResult<dynamic> GetContractCustomerServices(int contractId)
        {
            return new
            {
                data = new
                {
                    items = CustomerServiceData.GetContractCustomerServiceObservations(contractId)
                }
            };
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult GetObservationFile(int id)
        {
            var customerServiceFileDTO = CustomerServiceData.GetFileById(id);
            return new PhysicalFileResult(customerServiceFileDTO.FilePath, "application/octet-stream")
            {
                FileDownloadName = customerServiceFileDTO.FileName
            };
        }

        [HttpPost]
        public IActionResult PostCustomerService(CustomerServiceObservationsDTO customerServiceDTO)
        {
            CustomerServiceData.NewCustomerServiceObservation(customerServiceDTO);
            return NoContent();
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public IActionResult PutUpdateCustomerServiceObservation(int id, CustomerServiceObservationsDTO customerServiceDTO)
        {
            CustomerServiceData.UpdateObservation(id, customerServiceDTO);
            return NoContent();
        }
    }
}
