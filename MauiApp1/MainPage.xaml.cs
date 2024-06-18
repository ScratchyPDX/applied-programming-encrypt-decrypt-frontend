using System.Text;
using CommunityToolkit.Maui.Storage;
using CryptoLib;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
	readonly IFileSaver fileSaver;
	readonly IFilePicker filePicker;
	readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
	private string filePath = string.Empty;
	
	public MainPage(IFileSaver fileSaver, IFilePicker filePicker)
	{
		InitializeComponent();
		this.fileSaver = fileSaver;
		this.filePicker = filePicker;
		SaveFileBtn.IsEnabled = false;
		DecryptBtn.IsEnabled = false;
		EncryptBtn.IsEnabled = false;
		this.filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.txt");
	}

	private async void OnSaveFileClicked(object sender, EventArgs e)
	{
		// Creating the stream
		using var stream = new MemoryStream(Encoding.Default.GetBytes(EncryptedTextDisplayField.Text));
		// Calling  the SaveAsync method
		var fileSaved = await fileSaver.SaveAsync("test.txt", stream, cancellationTokenSource.Token);
		if(fileSaved.IsSuccessful) {
			this.filePath = fileSaved.FilePath;
		}
	}

	private async void OnOpenFileClicked(object sender, EventArgs e)
	{
		PickOptions options = new()
		{
			PickerTitle = "Please select a text file"
		};

		// Calling the PickAsync method
		var fileOpened = await filePicker.PickAsync(options);
		this.filePath = fileOpened.FullPath;
		EncryptedTextDisplayField.Text = await File.ReadAllTextAsync(filePath);
	}

	private void OnEncryptTextClicked(object sender, EventArgs e)
	{
		EncryptedTextDisplayField.Text = Crypto.EncryptString(DecryptedTextDisplayField.Text);
		DecryptedTextDisplayField.Text = string.Empty;
	}

	
	private void OnDecryptTextClicked(object sender, EventArgs e)
	{
		DecryptedTextDisplayField.Text = Crypto.DecryptString(EncryptedTextDisplayField.Text);
		EncryptedTextDisplayField.Text = string.Empty;
	}

	private void OnResetFormClicked(object sender, EventArgs e)
	{
		DecryptedTextDisplayField.Text = string.Empty;
		EncryptedTextDisplayField.Text = string.Empty;
	}

	private void OnDecryptedTextChanged(object sender, TextChangedEventArgs e)
	{
		EncryptBtn.IsEnabled = !string.IsNullOrEmpty(DecryptedTextDisplayField.Text);
	}

	private void OnEncryptedTextChanged(object sender, TextChangedEventArgs e)
	{
		SaveFileBtn.IsEnabled = !string.IsNullOrEmpty(EncryptedTextDisplayField.Text);
		DecryptBtn.IsEnabled = !string.IsNullOrEmpty(EncryptedTextDisplayField.Text);
	}
}