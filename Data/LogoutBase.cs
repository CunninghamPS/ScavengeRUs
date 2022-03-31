using Microsoft.AspNetCore.Components;

namespace ScavengeRUs.Data
{
    public class LogoutBase : ComponentBase
    {
        [Parameter]
        public string secretKey { get; set; }
    }
}
