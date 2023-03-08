using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace XC.CCMP.DataStorage.Models.Request
{
    public class AddFiles
    {
        [Required(ErrorMessage = "Path is required")]
        public string Path { get; set; }

        [Required(ErrorMessage = "FileGroup is required")]
        public List<IFormFile> FileGroup { get; set; }
    }
}
