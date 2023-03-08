using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.UAM.Models
{
    /// <summary>
    /// Class for user filter request to get dynamic list request.
    /// </summary>
    public class UserFilterRequest
    {
        public UserFilterRequest()
        {
            this.Attributes = new List<Attribute>();
            this.Permissions = new List<string>();
        }
        /// <summary>
        /// List of attributes.
        /// </summary>
        public List<Attribute> Attributes { get; set; }

        /// <summary>
        /// List of permissions.
        /// </summary>
        public List<string> Permissions { get; set; }
    }

    /// <summary>
    /// Attribute class to filter user list.
    /// </summary>
    public class Attribute
    {
        /// <summary>
        /// name of attribute.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of attribute.
        /// </summary>
        public List<string> Value { get; set; }
    }
}
