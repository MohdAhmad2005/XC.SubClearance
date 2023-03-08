using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.CCMP.DataStorage.Models.Response
{
    public class AddFilesResponse
    {
        public string DocumentId { get; set; }
        public string ActualName { get; set; }
        public string SystemName { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public string Status { get; set; }
        public string UploadedBy { get; set; }
    }
}
