namespace StrykerNet.UnitTest.Mutants.TestResources
{
	public class TestClass
	{
		void TestMethod()
		{
			bool a = true;
			int b = 1;
            bool c = System.Environment.GetEnvironmentVariable("ActiveMutation")=="0"?a || b > 0:a && b > 0;
		}
	}
}