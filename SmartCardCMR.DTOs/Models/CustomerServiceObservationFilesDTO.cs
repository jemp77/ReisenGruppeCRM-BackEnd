using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCardCRM.Model.Models
{
    public class CustomerServiceObservationFilesDTO
    {
        public int Id { get; set; }
        public int CustomerServiceObservationId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }

        public string FileBase64 { get; set; }
    }
}
