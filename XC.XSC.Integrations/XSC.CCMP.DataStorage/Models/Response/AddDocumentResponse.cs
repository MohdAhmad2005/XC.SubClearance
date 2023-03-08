using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.CCMP.DataStorage.Models.Response
{
    public class AddDocumentResponse
    {
        /// <summary>
        /// String DocumentId
        /// </summary>
        public string DocumentId { get; set; }
        public string TenantId { get; set; }
        public string RequestId { get; set; }
        public DateTime RequestedOn { get; set; }
        public List<AddFileGroupsResponse> FileGroups { get; set; }
    }
}
