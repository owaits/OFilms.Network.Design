using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFilms.Network.Project.Principals
{
    /// <summary>
    /// The port configuration for ports that support POE power delivery.
    /// </summary>
    public class POE
    {
        /// <summary>
        /// Gets or sets whether POE is enabled on this port.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the maximum power to deliver over POE for this port.
        /// </summary>
        public double PowerLimit { get; set; }
    }
}
