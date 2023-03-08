using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.CCMP.DataStorage.Models.Request
{
     public class DocumentStorageFile
    {
        public Stream StreamContent { get; set; }
        public string Path { get; set; }
        public string TenantId { get; set; }
        public string FileName { get; set; }
    }
}
