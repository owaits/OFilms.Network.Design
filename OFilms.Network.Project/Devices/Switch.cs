using OFilms.Network.Project.Principals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFilms.Network.Project.Devices
{
    /// <summary>
    /// Contains information about a network switch device within a network.
    /// </summary>
    /// <seealso cref="OFilms.Network.Project.Devices.NetworkDevice" />
    public class Switch: NetworkDevice
    {
        /// <summary>
        /// Gets or sets the network ports for this switch.
        /// </summary>
        public IEnumerable<Port> Ports { get; set; } = new List<Port>();

        /// <summary>
        /// Creates a new switch device with the specified number of switch ports.
        /// </summary>
        /// <param name="ports">The number of ports to assign the switch device.</param>
        /// <returns>The newly created switch device.</returns>
        public static Switch Create(int ports)
        {
            var newSwitch = new Switch();

            var portList = new List<Port>();
            for (int n = 1; n <= ports; n++)
                portList.Add(new Port(n));

            newSwitch.Ports = portList;

            return newSwitch;
        }
    }
}
