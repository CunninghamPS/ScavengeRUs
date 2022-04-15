using BlazorBarcodeScanner.ZXing.JS;

namespace ScavengeRUs.Pages
{
    public partial class QRCodeScan
    {
        private BarcodeReader? _reader;

        bool ShowScanBarcode { get; set; } = true;

        public string? BarCode { get; set; }
        public string? BarCodeTyped { get; set; }

        public bool barCodeBox { get; set; } = false;

        private async void QRCodeString(BarcodeReceivedEventArgs args)
        {
            await InvokeAsync(() =>
            {
                BarCode = args.BarcodeText;
                StateHasChanged();
            });
        }

        private void RunBarCodeTyped()
        {
            // Change to reflect actual barcode scanning
            Console.Write("Bar Code Typed: " + BarCodeTyped + "\n\n");
            Console.Write("Bar Code Scanned: " + BarCode);
            // If valid Qr code disable the qr code manual box
            barCodeBox = false;
        }

        private void barCodeToggle()
        {
            barCodeBox = !barCodeBox;
        }

    }
}
