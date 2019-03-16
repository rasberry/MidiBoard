using System;
using System.Net;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Remote;
using Avalonia.Markup.Xaml;
using Avalonia.Remote.Protocol;
using Avalonia.Remote.Protocol.Designer;
using Avalonia.Remote.Protocol.Viewport;
using Avalonia.Threading;

namespace MidiBoard
{
	public class MainWindow : Window
	{
		TextBlock MainTextInput;

		public MainWindow()
		{
			this.InitializeComponent();

			//MainTextInput = this.FindControl<TextBlock>("MainTextInput");
			//MainTextInput.TextWrapping = Avalonia.Media.TextWrapping.Wrap;
		}

		protected override void OnKeyDown(Avalonia.Input.KeyEventArgs e)
		{
			this.Background = Avalonia.Media.Brushes.Blue;
			//MainTextInput.Text += "↓";
			base.OnKeyDown(e);
		}
		protected override void OnKeyUp(Avalonia.Input.KeyEventArgs e)
		{
			this.Background = Avalonia.Media.Brushes.Red;
			//MainTextInput.Text += "↑";
			base.OnKeyUp(e);
		}

		void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
