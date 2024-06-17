using System.Text;
using CommunityToolkit.Maui.Storage;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
	int count = 0;
	readonly IFileSaver fileSaver;
	readonly IFilePicker filePicker;
	readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
	string textInput = string.Empty;


	public MainPage(IFileSaver fileSaver, IFilePicker filePicker)
	{
		InitializeComponent();
		this.fileSaver = fileSaver;
		this.filePicker = filePicker;
		// CounterBtn.IsEnabled = false;
	}

	// private void OnCounterClicked(object sender, EventArgs e)
	// {
	// 	count++;

	// 	if (count == 1)
	// 		CounterBtn.Text = $"Clicked {count} time";
	// 	else
	// 		CounterBtn.Text = $"Clicked {count} times";

	// 	SemanticScreenReader.Announce(CounterBtn.Text);
	// }


	private void OnSaveFileClicked(object sender, EventArgs e)
	{
		// Creating the stream
		using var stream = new MemoryStream(Encoding.Default.GetBytes(this.textInput));
		// Calling  the SaveAsync method
		fileSaver.SaveAsync("test.txt", stream, cancellationTokenSource.Token);
	}

	private async void OnOpenFileClicked(object sender, EventArgs e)
	{
		PickOptions options = new()
		{
			PickerTitle = "Please select a text file"
		};

		// Calling the PickAsync method
		var filePicked = await filePicker.PickAsync(options);
		await DisplayAlert("File Picked", $"The file picked is: {filePicked.FileName}", "OK");

	}

	private void OnTextInputChanged(object sender, TextChangedEventArgs e)
	{
		this.textInput = e.NewTextValue;
	}

	private void OnTextOutputChanged(object sender, TextChangedEventArgs e)
	{
		this.textInput = e.NewTextValue;
	}

	private void OnEncryptTextClicked(object sender, EventArgs e)
	{
		return;
	}

	
	private void OnDecryptTextClicked(object sender, EventArgs e)
	{
		return;
	}
}

