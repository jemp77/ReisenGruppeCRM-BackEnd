using Microsoft.AspNetCore.Mvc;
using SmartCardCRM.Data;
using SmartCardCRM.Data.Entities;
using SmartCardCRM.Model.Models;

namespace SmartCardCRM.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingData BookingData;

        public BookingController(SmartCardCRMContext context)
        {
            BookingData = new BookingData(context);
        }

        [HttpGet]
        [Route("[action]/{contractId}")]
        public ActionResult<dynamic> GetContractBookings(int contractId)
        {
            return new
            {
                data = new
                {
                    items = BookingData.GetContractBookingObservations(contractId)
                }
            };
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult GetObservationFile(int id)
        {
            var bookingFileDTO = BookingData.GetFileById(id);
            return new PhysicalFileResult(bookingFileDTO.FilePath, "application/octet-stream")
            {
                FileDownloadName = bookingFileDTO.FileName
            };
        }

        [HttpPost]
        public IActionResult PostBooking(BookingObservationsDTO bookingDTO)
        {
            BookingData.NewBookingObservation(bookingDTO);
            return NoContent();
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public IActionResult PutUpdateBookingObservation(int id, BookingObservationsDTO bookingDTO)
        {
            BookingData.UpdateObservation(id, bookingDTO);
            return NoContent();
        }
    }
}
