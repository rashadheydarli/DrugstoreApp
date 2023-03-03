using System;
namespace Core.Helpers
{
	public class ConsoleHelper
	{
		public static void WriteWithColor(string text, ConsoleColor color = ConsoleColor.White)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(text);
			Console.ResetColor();
		}
	}
}

