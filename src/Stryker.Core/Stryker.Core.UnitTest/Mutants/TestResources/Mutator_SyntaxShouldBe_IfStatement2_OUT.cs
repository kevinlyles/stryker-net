namespace StrykerNet.UnitTest.Mutants.TestResources
{
    public class TestClass
    {
        void TestMethod()
        {
            int a = 0;
            int b = (System.Environment.GetEnvironmentVariable("ActiveMutation") == "0" ? (a - 1) : (a + 1));
            if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "1")
            {
                b--;
            }
            else
            {
                b++;
            }
            System.Console.WriteLine(b);
        }
    }
}