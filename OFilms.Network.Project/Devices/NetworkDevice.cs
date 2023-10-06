using OFilms.GreenGo.Project.JsonConverters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OFilms.Network.Project.Devices
{
    /// <summary>
    /// Representas a single device within a network design.
    /// </summary>
    /// <seealso cref="OFilms.Network.Project.Devices.IDevice" />
    public abstract class NetworkDevice: IDevice
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

        /// <summary>
        /// Gets or sets the name of the user name to access the admin portal or configuration of the device.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the encrypted password to access the admin portal of the device.
        /// </summary>
        /// <remarks>
        /// This is the human readable for of the password that appears in any backup configuration.
        /// </remarks>
        public string? EncryptedPassword { get; set; }

        /// <summary>
        /// Gets or sets the primary IP address of device on the network.
        /// </summary>
        [JsonConverter(typeof(IPAddressJsonConverter))]
        public IPAddress? Address { get; set; }

        /// <summary>
        /// Gets or sets the subnet mask of the device on the network.
        /// </summary>
        [JsonConverter(typeof(IPAddressJsonConverter))]
        public IPAddress? SubnetMask { get; set; }
    }
}
