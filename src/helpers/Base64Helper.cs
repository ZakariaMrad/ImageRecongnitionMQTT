using System;
using System.IO;

public static class Base64Helper
{
    public static bool SaveFileFromBase64(string base64String, string filePath)
    {
        try
        {
            string base64 = base64String;
            if (base64.StartsWith("data:"))
            {
                base64 = base64.Substring(base64.IndexOf(",") + 1);
            }
            byte[] fileBytes = Convert.FromBase64String(base64);
            File.WriteAllBytes(filePath, fileBytes);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating file: {ex.Message}");
            return false;
        }
    }

    public static string GetFileExtensionFromBase64(string base64String)
    {
        string[] parts = base64String.Split(',');
        string data = parts[0];
        string extension = string.Empty;

        if (data.Contains("/"))
        {
            string mimeType = data.Split(':')[1].Split(';')[0];
            extension = mimeType.Split('/')[1];
        }
        else
        {
            extension = parts[0].Split('/')[1].Split(';')[0];
        }
        return extension;
    }

    public static string GetBase64FromFile(string filePath)
    {
        byte[] fileBytes = File.ReadAllBytes(filePath);
        return Convert.ToBase64String(fileBytes);
    }

    public static byte[] GetBytesFromBase64(string base64String)
    {
        string base64 = base64String;
        if (base64.StartsWith("data:"))
        {
            base64 = base64.Substring(base64.IndexOf(",") + 1);
        }
        return Convert.FromBase64String(base64);
    }

    public static byte[] GetBytesFromFile(string filePath)
    {
        return File.ReadAllBytes(filePath);
    }
    
}
