using System.Diagnostics;
using System.Drawing;

public static class ProcessHelper {
    public static string DrawArucoMarkerAsBase64(string markerValue){
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "python";
        startInfo.Arguments = "./src/python/markergenerator.py " + markerValue;
        startInfo.RedirectStandardOutput = true;
        startInfo.UseShellExecute = false;

        using (Process process = Process.Start(startInfo))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                return result;
            }
        }
    }
    public static string DrawBeamArucoMarkerAsBase64(string markerValue){
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "python";
        startInfo.Arguments = "./src/python/beammarkergenerator.py " + markerValue;
        startInfo.RedirectStandardOutput = true;
        startInfo.UseShellExecute = false;

        using (Process process = Process.Start(startInfo))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                return result;
            }
        }
    }
}