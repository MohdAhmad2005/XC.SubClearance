using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.CCMP.DataStorage.Models.Response
{
    public class AddFileGroupsResponse
    {
        public string Path { get; set; }
        public List<AddFilesResponse> Files { get; set; }
    }
}
