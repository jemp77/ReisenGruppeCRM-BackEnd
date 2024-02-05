using AutoMapper;
using SmartCardCRM.Data.Entities;
using SmartCardCRM.Model.Models;
using System.Data;
using System.Linq;

namespace SmartCardCRM.Data
{
    public class QuoterData : BaseData
    {
        private readonly UtilData UtilData;
        public QuoterData(SmartCardCRMContext context)
        {
            _context = context;
            UtilData = new UtilData(context);
        }

        public void SendQuote(QuoterDTO quoterDTO)
        {
            new ExceptionLogData<bool>(_context).Log(this.GetType().Name,
                () =>
                {
                    var roomNumber = 1;
                    var rooms = "";
                    var quoter = new Mapper(MapperConfig).Map<Quoter>(quoterDTO);
                    _context.Quoter.Add(quoter);
                    _context.SaveChanges();

                    foreach (var room in quoterDTO.Rooms)
                    {
                        rooms = $"{rooms} <br/> Habitacion {roomNumber}: <br/> &ensp; Adultos: {room.Adults} <br/> &ensp; Niños: {room.Kids} <br/> &ensp; Edades: {string.Join(",", room.KidsAges)}";
                        roomNumber++;
                    }

                    //// SmartCard quote email
                    var emailFrom = _context.ConfigurationSettings.Where(x => x.Key == "EmailFrom").FirstOrDefault().Value;
                    var emailFromDisplayName = _context.ConfigurationSettings.Where(x => x.Key == "EmailFromDisplayName").FirstOrDefault().Value;
                    var quoterEmailSubject = _context.ConfigurationSettings.Where(x => x.Key == "QuoterEmailSubject").FirstOrDefault().Value;
                    var quoterEmailBodySmartCard = _context.ConfigurationSettings.Where(x => x.Key == "QuoterEmailBodySmartCard").FirstOrDefault().Value;
                    quoterEmailBodySmartCard = string.Format(quoterEmailBodySmartCard, quoterDTO.Name, quoterDTO.Email, quoterDTO.Cellphone, quoterDTO.Destination, quoterDTO.DepartureDate.ToShortDateString(), quoterDTO.ArrivalDate.ToShortDateString(), rooms);
                    UtilData.SendEmail(emailFrom, emailFromDisplayName, emailFrom, $"{quoterEmailSubject} - {quoterDTO.Name}", quoterEmailBodySmartCard);

                    //// Customer quote email
                    var quoterEmailBody = _context.ConfigurationSettings.Where(x => x.Key == "QuoterEmailBody").FirstOrDefault().Value;
                    UtilData.SendEmail(emailFrom, emailFromDisplayName, quoterDTO.Email, quoterEmailSubject, quoterEmailBody);
                    return true;
                });
        }
    }
}
