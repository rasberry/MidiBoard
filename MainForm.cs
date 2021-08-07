using System;
using System.Collections.Generic;
using Eto.Drawing;
using Eto.Forms;

namespace MidiBoard
{
	public class MainForm : Form
	{
		public MainForm()
		{
			this.Size = new Size(600,400);
			RootPanel = new StackLayout();
			ChooseOutput = new DropDown();
			ChooseOutput.SelectedIndexChanged += OnOutputSelectedIndexChanged;
			Messages = new Label();
			
			RootPanel.SuspendLayout();
			RootPanel.HorizontalContentAlignment = HorizontalAlignment.Stretch;
			RootPanel.Items.Add(ChooseOutput);
			RootPanel.Items.Add(Messages);
			RootPanel.ResumeLayout();

			this.Content = RootPanel;

			Events.MidiOutputPopulated += WhenMidiOutputPopulated;
		}

		bool DownAlreadyFired = false;

		protected override void OnKeyUp(KeyEventArgs e)
		{
			//Messages.Text += "u";
			Events.OnWindowKeyUp(e);
			DownAlreadyFired = false;
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (DownAlreadyFired) { return; }
			DownAlreadyFired = true;
			//Messages.Text += "d";
			Events.OnWindowKeyDown(e);
		}

		void WhenMidiOutputPopulated(object s,OutputPopulatedEventArgs e)
		{
			Log.Debug("WhenMidiOutputPopulated");
			RootPanel.SuspendLayout();
			ChooseOutput.Items.Clear();
			OutputKeyByIndex.Clear();
			foreach(var item in e.Items) {
				Log.Debug("WhenMidiOutputPopulated added ["+item.Key+","+item.Value+"]");
				ChooseOutput.Items.Add(item.Value);
				OutputKeyByIndex.Add(item.Key);
			}
			ChooseOutput.SelectedIndex = 0;
			RootPanel.ResumeLayout();
		}

		void OnOutputSelectedIndexChanged(object s, EventArgs e)
		{
			Log.Debug("OnOutputSelectedIndexChanged");
			string id = OutputKeyByIndex[ChooseOutput.SelectedIndex];
			Events.OnMidiOutputChange(new OutputSelectedEventArgs(id));
		}

		Label Messages = null;
		DropDown ChooseOutput = null;
		StackLayout RootPanel = null;
		List<string> OutputKeyByIndex = new List<string>();
	}
}