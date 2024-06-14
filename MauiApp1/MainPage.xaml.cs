using System.Text;
using CommunityToolkit.Maui.Storage;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
	int count = 0;
	readonly IFileSaver fileSaver;
	readonly IFilePicker filePicker;
	readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();


	public MainPage(IFileSaver fileSaver, IFilePicker filePicker)
	{
		InitializeComponent();
		this.fileSaver = fileSaver;
		this.filePicker = filePicker;
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}


	private void OnSaveFileClicked(object sender, EventArgs e)
	{
		// Creating the stream
		using var stream = new MemoryStream(Encoding.Default.GetBytes("Howdy! I'm a new file!"));
		// Calling  the SaveAsync method
		fileSaver.SaveAsync("SampleFile.txt", stream, cancellationTokenSource.Token);
	}

	private async void OnPickFileClicked(object sender, EventArgs e)
	{
		PickOptions options = new()
		{
			PickerTitle = "Please select a text file"
		};

		// Calling the PickAsync method
		var filePicked = await filePicker.PickAsync(options);
		await DisplayAlert("File Picked", $"The file picked is: {filePicked.FileName}", "OK");

	}
	
}

