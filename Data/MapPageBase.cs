using Microsoft.AspNetCore.Components;

namespace ScavengeRUs.Data
{
    public class MapPageBase : ComponentBase
    {
        [Parameter]
        public string secretKey { get; set; }
    }
}
