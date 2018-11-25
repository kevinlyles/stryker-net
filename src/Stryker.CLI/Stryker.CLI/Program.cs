using Stryker.Core;

namespace Stryker.CLI
{
    public class Program
    {
        private static int Main(string[] args)
        {
            StrykerRunner stryker = new StrykerRunner();
            StrykerCLI app = new StrykerCLI(stryker);
            return app.Run(args);
        }
    }
}
