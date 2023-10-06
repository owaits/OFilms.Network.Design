using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFilms.Network.Project.Principals
{
    /// <summary>
    /// Information about a VLAN within a network design.
    /// </summary>
    public class VLAN
    {
        /// <summary>
        /// Gets or sets the ID of the VLAN.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name given to identify this VLAN.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the colour used to identify this VLAN.
        /// </summary>
        public string? Colour { get; set; }
    }
}
