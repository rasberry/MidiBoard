using System;
using Eto.Forms;

namespace MidiBoard
{
	public static class Events
	{
		public static event EventHandler<KeyEventArgs> OnWindowKeyDown;
		public static event EventHandler<KeyEventArgs> OnWindowKeyUp;
		public static event EventHandler<OutputChangeEventArgs> OnMidiOutputChange;
	}

	public class OutputChangeEventArgs : EventArgs
	{
		public OutputChangeEventArgs(string id) {
			Id = id;
		}
		public string Id { get; private set; }
	}
}