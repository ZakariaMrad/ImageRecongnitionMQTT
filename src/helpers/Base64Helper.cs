using System;
using System.IO;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

public static class Base64Helper
{
    public static Mat ToMat(string base64)
    {
        byte[] bytes = Convert.FromBase64String(base64);
        using (MemoryStream ms = new MemoryStream(bytes))
        {
            Mat image = new Mat();
            CvInvoke.Imdecode(bytes, ImreadModes.Color, image);
            return image;
        }
    }

    public static string MatToBase64(Mat image)
    {
        // Ensure we have a valid image
        if (image == null || image.IsEmpty)
        {
            throw new ArgumentException("Invalid image", nameof(image));
        }

        // Convert the Mat image to a Bitmap
        Bitmap bitmap = image.ToBitmap();

        // Save the bitmap to a MemoryStream
        using (MemoryStream memoryStream = new MemoryStream())
        {
            bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

            // Convert the MemoryStream to a byte array
            byte[] imageBytes = memoryStream.ToArray();

            // Convert the byte array to a Base64 string
            return Convert.ToBase64String(imageBytes);
        }
    }
}
