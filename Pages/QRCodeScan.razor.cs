using BlazorBarcodeScanner.ZXing.JS;
using ScavengeRUs.Data;

namespace ScavengeRUs.Pages
{
    public partial class QRCodeScan
    {
        private BarcodeReader? _reader;

        bool ShowScanBarcode { get; set; } = true;
        private bool IsVisible { get; set; } = false;

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

        private void Close()
        {
            IsVisible = false;
        }

        private void RunBarCodeTyped()
        {
            bool gameComplete = true;
            // Change to reflect actual barcode scanning
            Console.WriteLine("Bar Code Scanned/Typed: " + BarCode);
            bool success = DBTest.validateQR(BarCode, secretKey);

            if (!success)
            {
                IsVisible = true;
            }
            else
            {
                List<bool> status = DBTest.getUserTasks(secretKey);
                foreach (bool task in status)
                    if (!task)
                        gameComplete = false;

                if (gameComplete)
                {
                    //special message here
                    EmailSMSTest.sendCongratsEmail(secretKey);
                }
            }
            
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
