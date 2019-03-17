using System;
using Eto.Drawing;
using Eto.Forms;

namespace MidiBoard
{
	public class MainForm : Form
	{
		public MainForm()
		{
			this.Size = new Size(600,400);
			//TODO add dropdown for the output list
			Messages = new Label();
			this.Content = Messages;
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			Messages.Text += "u";
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			Messages.Text += "d";
		}

		Label Messages = null;
	}
}