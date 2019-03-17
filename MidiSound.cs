using System;
using Commons.Music.Midi;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MidiBoard
{
	public class MidiSound : IDisposable
	{
		public IEnumerable<KeyValuePair<string,string>> AvailableOutputs()
		{
			var access = MidiAccessManager.Default;
			foreach(var o in access.Outputs) {
				yield return new KeyValuePair<string, string>(
					o.Id,o.Name
				);
			}
		}

		public void NoteOn(MidiNote note, MidiOctave octave)
		{
			EnsureReady();
			Output.Send(new byte[] {
				MidiEvent.NoteOn, MapNote(note,octave), 0x7f
			},0,3,0);
		}

		public void NoteOff(MidiNote note, MidiOctave octave)
		{
			EnsureReady();
			Output.Send(new byte[] {
				MidiEvent.NoteOff, MapNote(note,octave), 0x7f
			},0,3,0);
		}

		public void Dispose()
		{
			if (Output != null) {
				Output.CloseAsync().Wait();
				Output = null;
			}
			Access = null;
		}

		void EnsureReady()
		{
			if (Access != null) { return; }
			Access = MidiAccessManager.Default;
			PortId = Access.Outputs.First().Id;
			InitOutput();
		}

		void InitOutput()
		{
			if (PortId == null) { return; }
			if (Output != null) {
				Output.CloseAsync().Wait();
			}
			Output = Access.OpenOutputAsync(PortId).Result;
			SetVoice(GeneralMidi.Instruments.AcousticGrandPiano);
		}

		void SetVoice(byte voice) //GeneralMidi.Instruments
		{
			EnsureReady();
			Output.Send(new byte [] {
				MidiEvent.Program,
				voice
			}, 0, 2, 0);
		}

		static byte MapNote(MidiNote note, MidiOctave octave)
		{
			int midinum = 12 * (int)octave + (int)note + 12;
			return (byte)midinum;
		}

		IMidiAccess Access;
		string PortId;
		IMidiOutput Output;
	}

	public enum MidiNote : int
	{
		C=0,CmD=1,D=2,DmE=3,E=4,F=5,FmG=6,G=7,GmA=8,A=9,AmB=10,B=11
	}
	public enum MidiOctave : int
	{
		Om=-1,O0=0,O1=1,O2=2,O3=3,O4=4,O5=5,O6=6,O7=7,O8=8
	}
}