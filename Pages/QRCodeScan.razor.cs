using BlazorBarcodeScanner.ZXing.JS;

namespace ScavengeRUs.Pages
{
    public partial class QRCodeScan
    {
        private BarcodeReader _reader;

        bool ShowScanBarcode { get; set; } = true;

        public string? BarCode { get; set; }

        private string message;

        private Task OnError(string message)
        {
            this.message = message;
            StateHasChanged();
            return Task.CompletedTask;
        }

        private async void QRCodeString(BarcodeReceivedEventArgs args)
        {
            await InvokeAsync(() =>
            {
                BarCode = args.BarcodeText;
                StateHasChanged();
            });
        }

        bool ShowCodes;

    }
}
