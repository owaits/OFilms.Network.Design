using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OFilms.Network.Project.Principals
{
    /// <summary>
    /// The type of link on a port whether its connected to another switch or an end user device.
    /// </summary>
    public enum PortLinkType
    {
        /// <summary>
        /// The port allows general access to a VLAN for a user device. This port will deliver untagged traffic.
        /// </summary>
        Access,
        /// <summary>
        /// The port connects between network devices on the network. This port will deliver tagged traffic.
        /// </summary>
        Trunk
    }

    /// <summary>
    /// THe format of connection this port supports.
    /// </summary>
    public enum PortStyle
    {
        /// <summary>
        /// The port supports RJ45 copper connections.
        /// </summary>
        RJ45,
        /// <summary>
        /// The port supports fibre connections.
        /// </summary>
        Fibre
    }

    /// <summary>
    /// Represents a port within the network design.
    /// </summary>
    public class Port
    {
        public Port(int index)
        {
            Index = index;
        }

        /// <summary>
        /// Gets or sets the index or port number starting from 1.
        /// </summary>
        public int Index { get; protected set; }

        private string? label;

        /// <summary>
        /// Gets or sets a label that identifies the purpose or role of this port.
        /// </summary>
        public string? Label 
        {
            get { return label; }
            set
            {
                if (label != value)
                {
                    label = value;
                    RaisePropertyChanged();
                }
            }
        }

        private PortStyle portStyle = PortStyle.RJ45;

        /// <summary>
        /// Gets or sets the supported port connection type.
        /// </summary>
        public PortStyle PortStyle
        {
            get { return portStyle; }
            set
            {
                if (portStyle != value)
                {
                    portStyle = value;
                    RaisePropertyChanged();
                }
            }
        }

        private PortLinkType link;

        /// <summary>
        /// Gets or sets the link type of this port whether its a trunk port or allows user devices to connect.
        /// </summary>
        public PortLinkType Link
        {
            get { return link; }
            set
            {
                if (link != value)
                {
                    link = value;

                    //As access ports only support a single VLAN membership we must remove all but the lowest VLAN membership.
                    if(link == PortLinkType.Access && Membership.Count > 1)
                    {
                        int lowestVLAN = Membership.Min();
                        Membership.Clear();
                        Membership.Add(lowestVLAN);
                    }

                    RaisePropertyChanged();                    
                }
            }
        }

        /// <summary>
        /// Gets or sets the VLAN memberships for this port.
        /// </summary>
        /// <remarks>
        /// When the port is set to access only a single VLAN membership is allowed.
        /// </remarks>
        public HashSet<int> Membership { get; set; } = new HashSet<int>();

        /// <summary>
        /// Gets or sets the POE state of the port.
        /// </summary>
        public POE? POE { get; set; }

        /// <summary>
        /// Adds the specified VLAN membership to this port.
        /// </summary>
        /// <param name="vlan">The vlan to be added to membership.</param>
        public void JoinVLAN(VLAN vlan)
        {
            if (Link == PortLinkType.Access)
                Membership.Clear();

            Membership.Add(vlan.ID);

            RaisePropertyChanged(nameof(Membership));

        }

        /// <summary>
        /// Removes the specified VLAN membership for this port.
        /// </summary>
        /// <param name="vlan">The vlan to be removed from membership.</param>
        public void LeaveVLAN(VLAN vlan)
        {
            Membership.Remove(vlan.ID);

            RaisePropertyChanged(nameof(Membership));
        }

        #region Notify Property Changed

        /// <summary>
        /// Occurs when [property changed].
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
