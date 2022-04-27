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
                if (secretKey != null)
                {
                    List<bool> taskCheck = DBTest.getUserTasks(secretKey);
                    Console.WriteLine(taskCheck);
                    string taskString = "";
                    foreach (var task in taskCheck)
                    {

                        if (task == true)
                        {
                            taskString += ("1");
                        }
                        else if (task == false)
                        {
                            taskString += ("0");
                        }
                    }
                    //Console.WriteLine(taskString);
                    //taskString=taskString.Remove(taskString.Length - 1);

                    //Console.WriteLine(taskString);
                    await JSRuntime.InvokeVoidAsync("initMap", secretKey, taskString);

                    StateHasChanged();
                }

               
                
            }

        }
    }
}
