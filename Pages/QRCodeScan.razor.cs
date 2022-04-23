using BlazorBarcodeScanner.ZXing.JS;
using ScavengeRUs.Data;

namespace ScavengeRUs.Pages
{
    public partial class QRCodeScan
    {
        private BarcodeReader? _reader;

        bool ShowScanBarcode { get; set; } = true;

        // barcode syntax -> "Game Number": 01 "Task Number": 15 -> 0115 barcode decoded syntax
        public string? BarCode { get; set; }
        public bool barCodeBox { get; set; } = false;

        private async void QRCodeString(BarcodeReceivedEventArgs args)
        {
            await InvokeAsync(() =>
            {
                BarCode = args.BarcodeText;
                RunBarCodeTyped();
                StateHasChanged();
            });
        }

        private void RunBarCodeTyped()
        {
            // Change to reflect actual barcode scanning
            Console.WriteLine("Bar Code Scanned/Typed: " + BarCode);
            bool success = DBTest.validateQR(BarCode, secretKey)
            // If valid Qr code disable the qr code manual box
            barCodeBox = false;
        }

        private void barCodeToggle()
        {
            barCodeBox = !barCodeBox;
            ShowScanBarcode = !ShowScanBarcode;
        }

    }
}
