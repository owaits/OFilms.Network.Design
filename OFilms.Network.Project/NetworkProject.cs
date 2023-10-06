using OFilms.GreenGo.Project.JsonConverters;
using OFilms.Network.Project.Devices;
using OFilms.Network.Project.Principals;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace OFilms.Network.Project
{
    /// <summary>
    /// A network project contains information about the system design of a single network.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class NetworkProject : INotifyPropertyChanged
    {
        private string[] HighlightColours = new string[]
        {
            "#e6194b", "#3cb44b", "#ffe119", "#4363d8", "#f58231", "#911eb4", "#46f0f0", "#f032e6", "#bcf60c", "#fabebe", "#008080", "#e6beff", "#9a6324", "#fffac8", "#800000", "#aaffc3", "#808000", "#ffd8b1", "#000075", "#808080", "#ffffff", "#000000"
        };

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the description for the network project.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets any notes that relate to the network project.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Gets or sets the date that this network project was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the last date that this project was modified.
        /// </summary>
        public DateTime Modified { get; set; }

        /// <summary>
        /// Gets or sets the prject user name used access the administration of all devices on the network.
        /// </summary>
        public string? UserName { get; set; } = "admin";

        /// <summary>
        /// Gets or sets the encrypted password to access device configuration.
        /// </summary>
        public string? EncryptedPassword { get; set; }

        /// <summary>
        /// Gets or sets the management subnet mask.
        /// </summary>
        [JsonConverter(typeof(IPAddressJsonConverter))]
        public IPAddress ManagementSubnetMask { get; set; } = new IPAddress(new byte[] {255, 255, 255, 0});

        /// <summary>
        /// Gets or sets the management gateway.
        /// </summary>
        [JsonConverter(typeof(IPAddressJsonConverter))]
        public IPAddress? ManagementGateway { get; set; }

        /// <summary>
        /// Gets or sets the vlans.
        /// </summary>
        public List<VLAN> VLANS { get; set; } = new List<VLAN>();

        /// <summary>
        /// Gets or sets the switches.
        /// </summary>
        public List<Switch> Switches { get; set; } = new List<Switch>();

        /// <summary>
        /// Nexts the vlan colour.
        /// </summary>
        /// <returns></returns>
        private string NextVLANColour()
        {
            int colourIndex = 0;

            while(colourIndex < HighlightColours.Length && VLANS.Any(v=> v.Colour == HighlightColours[colourIndex]))
                colourIndex++;

            return HighlightColours[colourIndex];
        }

        /// <summary>
        /// Nexts the pvid.
        /// </summary>
        /// <returns></returns>
        private int NextPVID()
        {
            if (VLANS.Any() != true)
                return 101;

            return Math.Max(101, VLANS.Max(v => v.ID) + 1);
        }

        /// <summary>
        /// Creates the vlan.
        /// </summary>
        /// <returns></returns>
        public VLAN CreateVLAN()
        {
            return new VLAN()
            {
                ID = NextPVID(),
                Colour = NextVLANColour()
            };

        }

        #region Notify Property Changed


        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
