using System.Diagnostics;

namespace UnifyCov
{
  public class Integration
  {
    public string ReturnStringCommand(string pathOpenCover, string pathTestPlatform, string dllProject, string pathReportGenerator)
    {
      //nome do arquivo sem a extensão dll
      string dllFileName = dllProject.Remove(dllProject.LastIndexOf('.')).Substring(dllProject.LastIndexOf("\\") + 1);
      string str2 = "\"";
      string str3 = DateTime.Now.ToString("ddMMyyyy-HHmm");
      return "cd / \r\n      #cd " + pathOpenCover + " \r\n      #OpenCover.Console.exe -target:" + str2 + pathTestPlatform + str2 + " -targetargs:" + str2 + dllProject + str2 + " -output:C:\\ResultRunCoverageCode\\OpenCoverXMLs\\" + dllFileName + "_" + str3 + ".xml -register:user \r\n      #cd / \r\n      #cd " + pathReportGenerator + "\r\n      #ReportGenerator.exe -reports:C:\\ResultRunCoverageCode\\OpenCoverXMLs\\" + dllFileName + "_" + str3 + ".xml -targetdir:C:\\ResultRunCoverageCode\\ReportGenerator\\CodeCoverage_" + dllFileName + "\\" + dllFileName + str3 + " -assemblyfilters:+*;-MOQ\r\n      #cd / \r\n      #cd C:\\ResultRunCoverageCode\\ReportGenerator\\CodeCoverage_" + dllFileName + "\\" + dllFileName + str3 + "\r\n      #index.html#";
    }

    public Process ExecuteCmd()
    {
      return new Process()
      {
        StartInfo = new ProcessStartInfo()
        {
          WindowStyle = (ProcessWindowStyle)1,
          FileName = "cmd.exe",
          CreateNoWindow = false,
          UseShellExecute = false,
          RedirectStandardOutput = true,
          RedirectStandardInput = true
        }
      };
    }

    public void IfNoExistsDirectoyCreate(string path)
    {
      if (Directory.Exists(path))
        return;
      Directory.CreateDirectory(path);
    }

    public void ExecuteCommands(string pathDllProject, string pathTestPlatform, string dllProject, string pathReportGenerator)
    {
      try
      {
        string[] result = ReturnStringCommand(pathDllProject, pathTestPlatform, dllProject, pathReportGenerator).Split("#", StringSplitOptions.None);
        Process process = this.ExecuteCmd();
        process.Start();
        this.IfNoExistsDirectoyCreate("C:\\ResultRunCoverageCode\\OpenCoverXMLs\\");
        using (StreamWriter standardInput = process.StandardInput)
        {
          StreamReader standardOutput = process.StandardOutput;
          foreach (string str in this.ReturnStringCommand(pathDllProject, pathTestPlatform, dllProject, pathReportGenerator).Split("#", StringSplitOptions.None))
            ((TextWriter)standardInput).WriteLine(str);
          ((TextWriter)process.StandardInput).Close();
          while (!standardOutput.EndOfStream)
            Console.WriteLine(((TextReader)standardOutput).ReadLine());
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("ERRO: " + ex.Message.ToUpper());
      }
    }

    public static int SomaTeste(int num1, int num2)
    {
      return num1 + num2;
    }
  }
}