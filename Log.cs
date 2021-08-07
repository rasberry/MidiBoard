using System;
using System.Diagnostics;

namespace MidiBoard
{
	public static class Log
	{
		public static void Message(string m)
		{
			Console.WriteLine(m);
		}

		public static void Error(string m)
		{
			Console.Error.WriteLine(m);
		}

		public static void Debug(string m)
		{
			#if DEBUG
			Trace.WriteLine(m);
			#endif
		}
	}
}