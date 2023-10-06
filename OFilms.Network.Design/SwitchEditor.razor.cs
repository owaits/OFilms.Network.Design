using Microsoft.AspNetCore.Components;
using OFilms.Network.Project;
using OFilms.Network.Project.Devices;
using OFilms.Network.Project.Principals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFilms.Network.Design
{
    public partial class SwitchEditor
    {
        [Parameter, EditorRequired]
        public NetworkProject? Project { get; set; }

        [Parameter, EditorRequired]
        public Switch? NetworkSwitch { get; set; }

        public void ToggleLink(Port port)
        {
            if (port.Link == PortLinkType.Access)
                port.Link = PortLinkType.Trunk;
            else
                port.Link = PortLinkType.Access;
        }

        public void ToggleVLANMembership(Port port, VLAN vlan)
        {
            if (port.Membership.Contains(vlan.ID))
                port.LeaveVLAN(vlan);
            else
                port.JoinVLAN(vlan);
        }

        public string? MembershipLabel(Port port)
        {
            var vlanID = port.Membership.FirstOrDefault();
            if(port.Link == PortLinkType.Access && vlanID != default)
            {
                return Project?.VLANS.FirstOrDefault(v => v.ID == vlanID)?.Name;
            }

            return null;
        }

    }
}
