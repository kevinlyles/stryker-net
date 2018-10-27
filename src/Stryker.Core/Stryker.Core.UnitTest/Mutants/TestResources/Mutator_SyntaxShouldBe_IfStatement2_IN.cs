namespace StrykerNet.UnitTest.Mutants.TestResources
{
	public class TestClass
	{
		void TestMethod()
		{
			int a = 0;
			int b = a + 1;
			b++;
			System.Console.WriteLine(b);
		}
	}
}
