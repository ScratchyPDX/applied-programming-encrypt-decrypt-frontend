namespace MauiApp1;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

	protected override Window CreateWindow(IActivationState activationState)
	{
			var window = base.CreateWindow(activationState);
			window.MinimumHeight = 1100; // Set minimal window height
			window.MinimumWidth = 800; // Set minimal window width
			return window;
	}
}
