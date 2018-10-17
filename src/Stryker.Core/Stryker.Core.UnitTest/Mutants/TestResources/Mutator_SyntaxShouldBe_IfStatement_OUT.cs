namespace StrykerNet.UnitTest.Mutants.TestResources
{
    public class TestClass
    {
        void TestMethod()
        {
            int i = 0;
            i = (System.Environment.GetEnvironmentVariable("ActiveMutation") == "0" ? (i - 1) : (i + 1));
        }
    }
}
