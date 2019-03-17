using System;
using Eto;
using Eto.Forms;

namespace MidiBoard
{
	public class App : Application
	{
		public App(Platform platform) : base(platform)
		{
			
		}

		protected override void OnInitialized(EventArgs e)
		{
			MainForm = new MainForm();
			MainForm.Show();

			base.OnInitialized(e);
		}
	}
}
