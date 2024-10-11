namespace UnifyCov
{
  internal class Program
  {
    private static int Main(string[] args)
    {
      if (args.Length < 8 || args.Length > 8)
        throw new ArgumentException("É obrigatório passar os 4 parâmetros a seguir (-oc <value> -tp <value> -dll <value> -rg <value>), nenhum a menos e nenhum mais.");

      string parametros = string.Empty;
      for (int i = 0; i < args.Length; i++)
      {
        parametros += $"{args[i]} ";
      }

      string openCoverPath = args[1];
      string testPlataformPath = args[3];
      string testProjectDllPath = args[5];
      string reportGeneratorPath = args[7];

      new Integration().ExecuteCommands(openCoverPath, testPlataformPath, testProjectDllPath, reportGeneratorPath);
      return 1;
    }
  }
}

