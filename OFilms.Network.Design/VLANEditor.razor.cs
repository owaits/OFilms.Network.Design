using Microsoft.AspNetCore.Components;
using OFilms.Network.Project;
using OFilms.Network.Project.Principals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFilms.Network.Design
{
    public partial class VLANEditor
    {
        [Parameter, EditorRequired]
        public NetworkProject? Project { get; set; }

        protected VLAN? EditItem { get; set; }

        private int NextPVID()
        {
            if(Project?.VLANS.Any() != true)
                return 101;

            return Math.Max(101, Project.VLANS.Max(v => v.ID) + 1);
        }

        protected void CreateVLAN()
        {
            EditItem = Project?.CreateVLAN();

            if(EditItem != null)
            {
                Project?.VLANS.Add(EditItem);
                StateHasChanged();
            }
        }

        protected void EditVLAN(VLAN? vlan)
        {
            EditItem = vlan;
            StateHasChanged();
        }
    }
}
