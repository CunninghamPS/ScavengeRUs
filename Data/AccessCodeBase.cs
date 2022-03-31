using Microsoft.AspNetCore.Components;

namespace ScavengeRUs.Data
{
    public class AccessCodeBase : ComponentBase
    {
        [Parameter]
        public string urlID { get; set; }
    }
}
