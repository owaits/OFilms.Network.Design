using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFilms.Network.Project.Devices
{
    /// <summary>
    /// Represents a physical or virtual device related to the design or implementation of an IP network.
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Gets or sets the name of the device that can be used to identify that device.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the description to help explain the function, role or type of the device.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the model number of the device assigned by the manufacturer.
        /// </summary>
        public string? ModelNumber { get; set; }

        /// <summary>
        /// Gets or sets the physical location of the device.
        /// </summary>
        public string? Location { get; set; }
    }
}
