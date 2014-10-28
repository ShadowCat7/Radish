using System;

namespace RadishTests
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Beginning tests...\n");

			bool success = false;

			try
			{
				RadishTests.RunAllTests();
				success = true;
			}
			catch (Exception e)
			{
				Console.WriteLine("Test failed:");
				Console.WriteLine(e.StackTrace);
			}

			if (success)
				Console.WriteLine("All tests successful. Press any key to exit.");

			Console.ReadKey();
		}
	}
}
