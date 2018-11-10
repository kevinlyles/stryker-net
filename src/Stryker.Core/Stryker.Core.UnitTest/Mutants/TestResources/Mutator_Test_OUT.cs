namespace StrykerNet.UnitTest.Mutants.TestResources
{
	public class TestClass
	{
		void TestMethod()
		{
			int a = 1;
			int b = System.Environment.GetEnvironmentVariable("ActiveMutation") == "0" ? a -= 1 + 2 : System.Environment.GetEnvironmentVariable("ActiveMutation") == "1" ? a += 1 - 2 : a += 1 + 2;
			if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "3")
			{
				b = a *= 2 + 3;
			}
			else
			{
				if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "2")
				{
					b = a /= 2 - 3;
				}
				else
				{
					b = a *= 2 - 3;
				}
			}
			System.Console.WriteLine(b);
		}
	}
}