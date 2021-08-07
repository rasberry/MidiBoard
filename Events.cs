using System;
using Eto.Forms;
using System.Collections.Generic;

namespace MidiBoard
{
	public static class Events
	{
		public static event EventHandler<KeyEventArgs> WindowKeyDown;
		public static event EventHandler<KeyEventArgs> WindowKeyUp;
		public static event EventHandler<OutputSelectedEventArgs> MidiOutputSelected;
		public static event EventHandler<OutputPopulatedEventArgs> MidiOutputPopulated;

		public static void OnWindowKeyDown(KeyEventArgs e)
		{
			Log.Debug("OnWindowKeyDown");
			FireEvent(WindowKeyDown,e);
		}
		public static void OnWindowKeyUp(KeyEventArgs e)
		{
			Log.Debug("OnWindowKeyUp");
			FireEvent(WindowKeyUp,e);
		}
		public static void OnMidiOutputChange(OutputSelectedEventArgs e)
		{
			Log.Debug("OnMidiOutputChange");
			FireEvent(MidiOutputSelected,e);
		}
		public static void OnMidiOutputPopulated(OutputPopulatedEventArgs e)
		{
			Log.Debug("OnMidiOutputPopulated");
			FireEvent(MidiOutputPopulated,e);
		}

		static void FireEvent<A>(EventHandler<A> handler,A args)
		{
			Log.Debug("FireEvent 1");
			if (handler != null) {
				Log.Debug("FireEvent "+handler.Method.Name+" - "+args.GetType().Name);
				handler.Invoke(null,args);
			}
			Log.Debug("FireEvent 2");
		}
	}

	public class OutputSelectedEventArgs : EventArgs
	{
		public OutputSelectedEventArgs(string id) {
			Id = id;
		}
		public string Id { get; private set; }
	}

	public class OutputPopulatedEventArgs : EventArgs
	{
		public OutputPopulatedEventArgs(IEnumerable<KeyValuePair<string,string>> items) {
			Items = items;
		}
		public IEnumerable<KeyValuePair<string,string>> Items { get; private set; }
	}
}