using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using OFilms.Network.Project;
using OFilms.Network.Project.Devices;
using System.Net;

namespace OFilms.Network.Designer.Shared
{
    public partial class MainLayout
    {
        [Inject]
        public ILocalStorageService? LocalStorage { get; set; }

        public NetworkProject Project { get; set; } = new NetworkProject();


        protected override async Task OnInitializedAsync()
        {
            if(LocalStorage != null)
            {
                var project = await LocalStorage.GetItemAsync<NetworkProject>("project");
                if(project != null)
                {
                    Project = project;
                    StateHasChanged();
                }
            }
        }

        protected async Task SaveProject()
        {
            if (LocalStorage != null)
            {
                await LocalStorage.SetItemAsync("project", Project);
            }
        }

        protected async Task NewProject()
        {
            Project = new NetworkProject();

            var newSwitch = Switch.Create(10);

            //newSwitch.Model = "TL-SG2210P";
            //newSwitch.Address = IPAddress.Parse("10.10.1.21");
            newSwitch.SubnetMask = IPAddress.Parse("255.255.255.0");
            newSwitch.UserName = "admin";
            newSwitch.EncryptedPassword = "$1$E7M0@;J6M0G0J4D:M;C/A5H8@1A4J=B3-$],$";

            Project.Switches.Add(newSwitch);

            await SaveProject();

            StateHasChanged();
        }

    }
}
