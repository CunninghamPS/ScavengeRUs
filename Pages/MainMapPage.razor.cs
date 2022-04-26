using Microsoft.JSInterop;
using ScavengeRUs.Data;

namespace ScavengeRUs.Pages
{
    public partial class MainMapPage
    {
        string map;


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (secretKey == null)
                {

                }

                else
                {
                    List<bool> taskCheck = DBTest.getUserTasks(secretKey);
                    await JSRuntime.InvokeVoidAsync("initMap", secretKey, taskCheck);

                    StateHasChanged();
                }
                {



                }
            }

        }
    }
}
