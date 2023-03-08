using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.UAM.Models
{
    public class TeamResponse
    {
        /// <summary>
        /// Success status for Region listing.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Message of operation on Region listing.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Result of Region listing.
        /// </summary>
        public List<TeamModel> Result { get; set; }
    }

    /// <summary>
    /// Team model.
    /// </summary>
    public class TeamModel
    {
        /// <summary>
        /// Id of the team.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the team.
        /// </summary>
        public string Name { get; set; }
    }
}
