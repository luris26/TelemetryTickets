using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
namespace TicketsRUs.ClassLib;

public static class QRCodeToolkit
{
    public static Bitmap GenerateQRCodeFromString(string s)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(s, QRCodeGenerator.ECCLevel.Q);
        QRCode qrCode = new QRCode(qrCodeData);
        return qrCode.GetGraphic(20);
    }

    public static byte[] ConvertToByteArray(Bitmap qr)
    {
        byte[] result;
        using (MemoryStream stream = new MemoryStream())
        {
#pragma warning disable CA1416 // Validate platform compatibility
            qr.Save(stream, ImageFormat.Png);
#pragma warning restore CA1416 // Validate platform compatibility
            result = stream.ToArray();
        }

        return result;
    }
}
