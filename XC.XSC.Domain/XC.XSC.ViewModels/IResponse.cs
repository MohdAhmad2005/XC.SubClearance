using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.Configuration
{
    /// <summary>
    /// This interface is used to return the information about any operation that happens on database.
    /// </summary>
    public interface IResponse
    {
        [DefaultValue(false)]
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public object? Result { get; set; }
    }
}
