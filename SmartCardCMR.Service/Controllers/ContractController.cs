using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SmartCardCRM.Data;
using SmartCardCRM.Data.Entities;
using SmartCardCRM.Model.Models;
using SmartCardCRM.Report;
using SmartCardCRM.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCardCRM.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly ContractData ContractData;
        private readonly ClientData ClientData;
        private readonly UtilData UtilData;
        private readonly ConfigurationSettingsData ConfigurationSettingsData;
        private readonly ExceptionLogData<int> ExceptionData;
        IHostApplicationLifetime applicationLifetime;

        public ContractController(SmartCardCRMContext context, IHostApplicationLifetime appLifetime)
        {
            ContractData = new ContractData(context);
            ClientData = new ClientData(context);
            UtilData = new UtilData(context);
            ConfigurationSettingsData = new ConfigurationSettingsData(context);
            ExceptionData = new ExceptionLogData<int>(context);
            applicationLifetime = appLifetime;
        }

        [HttpGet("{id}")]
        public ActionResult<dynamic> GetContract(int id)
        {
            var contract = ContractData.GetContract(id).FirstOrDefault();
            if (contract == null)
            {
                return NotFound();
            }
            
            return new { data = contract };
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult<dynamic> GetContractNumber()
        {
            return new 
            { 
                data = new 
                { 
                    ContractNumber = string.Format("RG{0}", (int.Parse(ConfigurationSettingsData.GetValue("ConsecutiveContractNumber").Value) + 1).ToString()) 
                } 
            };
        }

        [HttpGet]
        [Route("[action]/{filter}")]
        public ActionResult<dynamic> GetActiveContractsByFilters(string filter)
        {
            return new
            {
                data = new
                {
                    items = ContractData.GetActiveContractsByFilters(filter).Select(x => new
                    {
                        x.Id,
                        x.Client.DocumentType,
                        x.Client.DocumentNumber,
                        x.Client.Name,
                        x.Client.LastName,
                        x.Client.Email,
                        x.Client.PhoneNumber,
                        x.ContractNumber
                    })
                }
            };
        }

        [HttpGet]
        [Route("[action]/{filter}")]
        public ActionResult<dynamic> GetAllContractsByFilters(string filter)
        {
            return new
            {
                data = new
                {
                    items = ContractData.GetAllContractsByFilters(filter).Select(x => new
                    {
                        x.Id,
                        x.Client.DocumentType,
                        x.Client.DocumentNumber,
                        x.Client.Name,
                        x.Client.LastName,
                        x.Client.Email,
                        x.Client.PhoneNumber,
                        x.ContractNumber
                    })
                }
            };
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult GetContractReport(int id)
        {
            try
            {
                var dataSets = new Dictionary<string, dynamic>();
                var contractDTO = ContractData.GetContract(id);
                if (contractDTO != null)
                {
                    var clientDTO = new List<ClientDTO> { contractDTO.FirstOrDefault().Client };
                    var file = new DocumentGenerator().GenerateWord(clientDTO.FirstOrDefault(), contractDTO.FirstOrDefault());
                    return new FileContentResult(file, "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                    {
                        FileDownloadName = string.Format("Contrato{0}.docx", contractDTO.FirstOrDefault().ContractNumber)
                    };
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                ExceptionData.LogException(this.GetType().Name,
                    string.Format("Source: {0}, Exception: {1}, Inner Exception: {2}", 
                        ex.Source, 
                        ex.Message, 
                        (ex.InnerException == null ? "" : ex.InnerException.Message)),
                    ex.StackTrace);
                throw;
            }
        }

        [HttpGet]
        [Route("[action]/{startDate}/{endDate}")]
        public IActionResult GetManifestReport(DateTime startDate, DateTime endDate)
        {
            var dataSets = new Dictionary<string, dynamic>();
            var listContractsDTO = ContractData.GetContractsByDateRange(startDate.Date, endDate.Date.AddDays(1).AddTicks(-1));
            var listClientsDTO = ClientData.GetClientsByDateRange(startDate.Date, endDate.Date.AddDays(1).AddTicks(-1));
            if (listContractsDTO != null)
            {
                dataSets.Add("ContractDataSet", listContractsDTO);
                dataSets.Add("ClientDataSet", listClientsDTO);
                var file = new DocumentGenerator().GenerateExcel("Manifest", dataSets);
                applicationLifetime.StopApplication();
                return new FileContentResult(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = "Manifiesto.xlsx"
                };
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("[action]/{email}")]
        public IActionResult GetTestWelcomeEmail(string email)
        {
            UtilData.SendWelcomeEmail(email, "Señor(a)", "Correo de Prueba", "Bienvenido(a)");
            return NoContent();
        }

        [HttpPost]
        public ActionResult<dynamic> PostContract(ContractDTO contractDTO)
        {
            var contract = ContractData.CreateContract(contractDTO);
            ConfigurationSettingsData.UpdateValue("ConsecutiveContractNumber", contract.ContractNumber.Substring(3));
            return new { data = new { id = contract.Id } };
        }

        [HttpPut("{id}")]
        public IActionResult PutContract(int id, ContractDTO contractDTO)
        {
            ContractData.UpdateContract(id, contractDTO);
            return NoContent();
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public IActionResult PutCancelContract(int id)
        {
            if (ContractData.CancelContract(id))
            {
                return NoContent();
            }
            else
            {
                return NotFound(string.Format("Contract Id: {0} not found", id));
            }
        }
    }
}
