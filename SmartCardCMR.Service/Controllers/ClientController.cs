using Microsoft.AspNetCore.Mvc;
using SmartCardCRM.Data;
using SmartCardCRM.Data.Entities;
using SmartCardCRM.Model.Models;
using SmartCardCRM.Util;
using System;
using System.Linq;

namespace SmartCardCRM.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientData ClientData;
        private readonly UtilData UtilData;
        private readonly ConfigurationSettingsData ConfigurationSettingsData;

        public ClientController(SmartCardCRMContext context)
        {
            ClientData = new ClientData(context);
            UtilData = new UtilData(context);
            ConfigurationSettingsData = new ConfigurationSettingsData(context);
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult<dynamic> GetCardsAndBanks()
        {
            return new
            {
                data = new 
                { 
                    creditCards = ConfigurationSettingsData.GetValue("CreditCardsList").Value.Split(","),
                    debitCards = ConfigurationSettingsData.GetValue("DebitCardList").Value.Split(","),
                    banks = ConfigurationSettingsData.GetValue("BanksList").Value.Split(",")
                }
            };
        }

        [HttpGet("{id}")]
        public ActionResult<dynamic> GetClient(int id)
        {
            var client = ClientData.GetClientById(id);
            if (client == null)
            {
                return NotFound();
            }

            return new { data = client };
        }

        [HttpGet]
        [Route("[action]/{filter}")]
        public ActionResult<dynamic> GetClientByFilters(string filter)
        {
            return new 
            {
                data = new 
                {
                    items = ClientData.GetClientByFilters(filter).Select(x => new 
                    {
                        x.Id,
                        x.DocumentType,
                        x.DocumentNumber,
                        x.Name,
                        x.LastName,
                        x.Email,
                        x.PhoneNumber
                    }) 
                }
            };
        }

        [HttpGet]
        [Route("[action]/{filter}")]
        public ActionResult<dynamic> GetClientWithoutSaleByFilters(string filter)
        {
            return new
            {
                data = new
                {
                    items = ClientData.GetClientByFilters(filter).Where(x => x.WayOutTime == null).Select(x => new
                    {
                        x.Id,
                        x.DocumentType,
                        x.DocumentNumber,
                        x.Name,
                        x.LastName,
                        x.Email,
                        x.PhoneNumber
                    })
                }
            };
        }

        [HttpGet]
        [Route("[action]/{startDate}/{endDate}")]
        public ActionResult<dynamic> GetClientsForDashboard(DateTime startDate, DateTime endDate)
        {
            return new 
            { 
                data = new
                {
                    items = ClientData.GetClientsForDashboard(startDate, endDate).Select(x => new
                    { 
                        x.Id,
                        x.Name,
                        x.LastName,
                        x.CoOwnerName,
                        x.CoOwnerLastName,
                        x.ArrivalTime,
                        x.WayOutTime,
                        x.HasContract
                    })
                }
            };
        }

        [HttpPut("{id}")]
        public IActionResult PutClient(int id, ClientDTO clientDTO)
        {
            var currentClientDTO = ClientData.GetClientById(id);
            clientDTO.Observations = currentClientDTO.Observations;
            clientDTO.Voucher = currentClientDTO.Voucher;
            clientDTO.WayOutTime = currentClientDTO.WayOutTime;
            clientDTO.AttemptSell = currentClientDTO.AttemptSell;
            clientDTO.CreditDenied = currentClientDTO.CreditDenied;
            clientDTO.Reserve = currentClientDTO.Reserve;
            clientDTO.ReserveValue = currentClientDTO.ReserveValue;
            ClientData.UpdateClient(clientDTO);
            return NoContent();
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public IActionResult PutOutClient(int id, ClientDTO clientUpdates)
        {
            var clientDTO = ClientData.GetClientById(id);
            clientDTO.DocumentType =  string.IsNullOrEmpty(clientUpdates.DocumentType) ? clientDTO.DocumentType : clientUpdates.DocumentType;
            clientDTO.DocumentNumber = clientUpdates.DocumentNumber;
            clientDTO.Observations = clientUpdates.Observations;
            clientDTO.Voucher = clientUpdates.Voucher;
            clientDTO.WayOutTime = CustomDateTime.Now.TimeOfDay.ToString();
            clientDTO.AttemptSell = clientUpdates.AttemptSell;
            clientDTO.CreditDenied = clientUpdates.CreditDenied;
            clientDTO.Reserve = clientUpdates.Reserve;
            clientDTO.ReserveValue = clientUpdates.ReserveValue;
            ClientData.UpdateClientPartialData(clientDTO);
            return NoContent();
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public IActionResult PutEditConfidential(int id, ClientDTO clientUpdates)
        {
            var clientDTO = ClientData.GetClientById(id);
            clientDTO.Linner = clientUpdates.Linner;
            clientDTO.Closer = clientUpdates.Closer;
            clientDTO.TlmkCode = clientUpdates.TlmkCode;
            ClientData.UpdateClientPartialData(clientDTO);
            return NoContent();
        }

        [HttpPost]
        public ActionResult<dynamic> PostClient(ClientDTO clientDTO)
        {
            return new { data = new { id = ClientData.CreateClient(clientDTO) } };
        }

        [HttpPost]
        [Route("[action]/{clientId}")]
        public IActionResult PostSendWelcomeEmail(int clientId)
        {
            var clientDTO = ClientData.GetClientById(clientId);
            return SendWelcomeEmail(clientDTO);
        }

        [HttpPost]
        [Route("[action]/{contractNumber}")]
        public IActionResult PostSendWelcomeEmailByContractNumber(string contractNumber)
        {
            var clientDTO = ClientData.GetClientByContractNumber(contractNumber).Client;
            return SendWelcomeEmail(clientDTO);
        }

        private IActionResult SendWelcomeEmail(ClientDTO clientDTO)
        {
            UtilData.SendWelcomeEmail(clientDTO.Email,
                        clientDTO.Gender == "Mujer" ? "Señora" : "Señor",
                        string.Format("{0} {1}", clientDTO.Name, clientDTO.LastName),
                        clientDTO.Gender == "Mujer" ? "Bienvenida" : "Bienvenido");
            return NoContent();
        }
    }
}
