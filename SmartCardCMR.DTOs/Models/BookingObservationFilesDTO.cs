namespace SmartCardCRM.Model.Models
{
    public class BookingObservationFilesDTO
    {
        public int Id { get; set; }
        public int BookingObservationId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }

        public string FileBase64 { get; set; }
    }
}
