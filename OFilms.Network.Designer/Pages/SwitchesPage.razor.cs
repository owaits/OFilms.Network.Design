using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OFilms.Network.Project;
using OFilms.Network.Project.Devices;
using OFilms.Network.Project.Formats;
using System.Net;
using System.Runtime.CompilerServices;

namespace OFilms.Network.Designer.Pages
{
    public partial class SwitchesPage
    {
        [CascadingParameter]
        public NetworkProject? Project { get; set; }

        [Inject]
        public IJSRuntime? JS { get; set; }

        public Switch? SelectedSwitch { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if(Project?.Switches.Count == 0)
            {
                var newSwitch = Switch.Create(10);

                //newSwitch.Model = "TL-SG2210P";
                //newSwitch.Address = IPAddress.Parse("10.10.1.21");
                newSwitch.SubnetMask = Project.ManagementSubnetMask;
                newSwitch.UserName = Project.UserName;
                newSwitch.EncryptedPassword = Project.EncryptedPassword;

                Project.Switches.Add(newSwitch);
            }

            SelectedSwitch = Project?.Switches.First();

            await Task.CompletedTask;
        }

        protected async Task Export()
        {
            TPLinkFormatter tpLink = new TPLinkFormatter();

            if(Project != null && SelectedSwitch != null)
            {
                var stream = new MemoryStream();
                tpLink.WriteToStream(Project, SelectedSwitch, stream);
                await DownloadFileFromStream(stream, "tplink.cfg"); 
            }           
        }

        private async Task DownloadFileFromStream(Stream fileStream, string fileName)
        {
            if (JS == null)
                return;

            //Reset the stream to the start of the file.
            fileStream.Seek(0, SeekOrigin.Begin);

            using var streamRef = new DotNetStreamReference(stream: fileStream);

            await JS.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
        }
    }
}
