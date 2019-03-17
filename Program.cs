using System;
using Commons.Music.Midi;
using System.Linq;
using System.Threading.Tasks;
using Xwt;

namespace MidiBoard
{
	class Program
	{
		static void Main(string[] args)
		{
			Run(ToolkitType.Gtk);
		}

		public static void Run(ToolkitType type)
		{
			MainWindow w = null;
			try {
				Application.Initialize(type);
				w = new MainWindow();
				w.Show();
				Application.Run();
			}
			finally {
				w?.Dispose();
				Application.Dispose();
			}
		}

		static void Main2(string[] args)
		{
			// AlsaSharp.AlsaPortCapabilities


			Task t = TestMidi();
			t.Wait();
			return;

			while(true)
			{
				var keyInfo = Console.ReadKey(true);
				if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control) && keyInfo.Key == ConsoleKey.C) {
					break;
				}
				var key = MapKey(keyInfo);
				Console.WriteLine("key = "+key);

			}
		}

		static int MapKey(ConsoleKeyInfo keyInfo)
		{
			return (int)keyInfo.KeyChar;
		}

		static async Task TestMidi()
		{
			var access = MidiAccessManager.Default;
			foreach(var o in access.Outputs) {
				Console.WriteLine(
					"Id = "+o.Id
					+"\nManufacturer = "+o.Manufacturer
					+"\nName = "+o.Name
					+"\nVersion = "+o.Version
				);
				await Play(o);
				Console.WriteLine("");
			}

			// await Play(access.Outputs.Last());
		}

		static async Task Play(IMidiPortDetails port)
		{
			var access = MidiAccessManager.Default;
			using (var output = await access.OpenOutputAsync(port.Id))
			{
				Console.WriteLine("Note One");
				output.Send(new byte [] {MidiEvent.Program, GeneralMidi.Instruments.AcousticGrandPiano}, 0, 2, 0); // There are constant fields for each GM instrument
				output.Send(new byte [] {MidiEvent.NoteOn, 0x40, 0x7F}, 0, 3, 0); // There are constant fields for each MIDI event
				System.Threading.Thread.Sleep(1000);
				output.Send(new byte [] {MidiEvent.NoteOff, 0x40, 0x7F}, 0, 3, 0);
				
				Console.WriteLine("Note Two");
				output.Send(new byte [] {MidiEvent.Program, GeneralMidi.Instruments.StringEnsemble1}, 0, 2, 0); // Strings Ensemble
				output.Send(new byte [] {MidiEvent.NoteOn, 0x40, 0x7F}, 0, 3, 0);
				System.Threading.Thread.Sleep(1000);
				output.Send(new byte [] {MidiEvent.NoteOff, 0x40, 0x7F}, 0, 3, 0);
			}
		}
	}
}
