using Microsoft.AspNetCore.Components;
using OFilms.Network.Project;

namespace OFilms.Network.Designer.Pages
{
    public partial class VLANPage
    {
        [CascadingParameter]
        public NetworkProject? Project { get; set; }
    }
}
