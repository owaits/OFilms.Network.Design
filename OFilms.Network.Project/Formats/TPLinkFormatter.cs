using OFilms.Network.Project.Devices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OFilms.Network.Project.Formats
{
    /// <summary>
    /// Writes a network switch configuration to a TPLink specific configuration file.
    /// </summary>
    /// <seealso cref="OFilms.Network.Project.Formats.IFormatter" />
    public class TPLinkFormatter:IFormatter
    {
        /// <summary>
        /// Gets the name used to identify this formatter.
        /// </summary>
        public string Name { get { return "TPLink JetStream Configuration"; } }

        /// <summary>
        /// Gets the description of this formatter.
        /// </summary>
        public string Description { get { return "Creates a TPLink JetStream configuration file that can be used to restore this switch to the network designs configuration."; } }

        /// <summary>
        /// Reads the formatted content from a stream.
        /// </summary>
        /// <param name="stream">The stream to read the data from.</param>
        /// <returns>
        /// The device that was read from the formatted stream.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IDevice ReadFromStream(Stream stream)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes to stream formatted contents to a stream.
        /// </summary>
        /// <param name="project">The project to be written to the stream.</param>
        /// <param name="device">The device information to write to the stream.</param>
        /// <param name="stream">The stream to write that formatted content to.</param>
        public void WriteToStream(NetworkProject project, IDevice device, Stream stream)
        {
            StreamWriter writer = new StreamWriter(stream);

            writer.WriteLine("!" + device.ModelNumber + "\n#");
            WriteVLANS(project, writer);
            WritePassword((Switch)device, writer);
            WriteUI((Switch)device, writer);
            WriteInterfaces((Switch)device, writer);

            writer.WriteLine("end");

            writer.Flush();
        }

        /// <summary>
        /// Writes the password.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="writer">The writer.</param>
        /// <exception cref="System.ArgumentException">
        /// The switch user name has not been set. Please set the user name.
        /// or
        /// The switch password has not been set. Please set the enctrypted password to ensure the config is valid..
        /// </exception>
        private void WritePassword(Switch device, TextWriter writer)
        {
            if (string.IsNullOrEmpty(device.UserName))
                throw new ArgumentException("The switch user name has not been set. Please set the user name.");
            if (string.IsNullOrEmpty(device.EncryptedPassword))
                throw new ArgumentException("The switch password has not been set. Please set the enctrypted password to ensure the config is valid..");

            writer.WriteLine($"user name {device.UserName} privilege admin secret 5 {device.EncryptedPassword}");
            writer.WriteLine($"#");
        }

        /// <summary>
        /// Writes the UI.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="writer">The writer.</param>
        /// <exception cref="System.ArgumentException">Please ensure you have set the switch IP address and Subnet to the correct IP you want to use to access the UI.</exception>
        private void WriteUI(Switch device, TextWriter writer)
        {
            if (device.Address == default || device.SubnetMask == default)
                throw new ArgumentException("Please ensure you have set the switch IP address and Subnet to the correct IP you want to use to access the UI.");

            writer.WriteLine($"interface vlan 1");
            writer.WriteLine($"  ip address {device.Address} {device.SubnetMask}");
            if(!string.IsNullOrEmpty(device.UserName))
            {
                writer.WriteLine($"  description \"{device.Name}\"");
            }            
            writer.WriteLine($"#");
        }

        /// <summary>
        /// Writes the vlans.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="writer">The writer.</param>
        private void WriteVLANS(NetworkProject project, TextWriter writer)
        {
            foreach(var vlan in project.VLANS)
            {
                writer.WriteLine($"vlan {vlan.ID}");
                writer.WriteLine($"name \"{vlan.Name}\"\n#");
            }
        }

        /// <summary>
        /// Writes the interfaces.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="writer">The writer.</param>
        private void WriteInterfaces(Switch device, TextWriter writer)
        {
            foreach (var port in device.Ports)
            {
                writer.WriteLine($"interface gigabitEthernet 1/0/{port.Index}");

                if(!string.IsNullOrEmpty(port.Label))
                {
                    writer.WriteLine($"  description \"{port.Label}\"");
                }                

                if(port.Membership.Count > 0)
                {
                    string access = (port.Link == Principals.PortLinkType.Access ? "general" : "trunk");
                    string untagged = (port.Link == Principals.PortLinkType.Access ? "untagged" : "tagged");
                    writer.WriteLine($"  switchport general allowed vlan {string.Join(",",port.Membership)} {untagged}");
                }

                writer.WriteLine($"#");
            }
        }
    }
}
