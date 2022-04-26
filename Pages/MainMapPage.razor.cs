using Microsoft.JSInterop;

namespace ScavengeRUs.Pages
{
    public partial class MainMapPage
    {
        string map;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("initMap", secretKey);

                StateHasChanged();
            }
        }

    }
}
