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
	}

	private async void OnSaveFileClicked(object sender, EventArgs e)
	{
		// Creating the stream
		using var stream = new MemoryStream(Encoding.Default.GetBytes(EncryptedTextDisplayField.Text));
		// Calling  the SaveAsync method
		var fileSaved = await fileSaver.SaveAsync("test.txt", stream, cancellationTokenSource.Token);
	}

	private async void OnOpenFileClicked(object sender, EventArgs e)
	{
		PickOptions options = new()
		{
			PickerTitle = "Please select a text file"
		};

		// Calling the PickAsync method
		var fileOpened = await filePicker.PickAsync(options);
		EncryptedTextDisplayField.Text = await File.ReadAllTextAsync(fileOpened.FullPath);
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
		if(string.IsNullOrEmpty(DecryptedTextDisplayField.Text)){
			EncryptBtn.IsEnabled = false;
		}
		else{
			EncryptBtn.IsEnabled = true;
		}
	}

	private void OnEncryptedTextChanged(object sender, TextChangedEventArgs e)
	{
		if(string.IsNullOrEmpty(EncryptedTextDisplayField.Text)){
			DecryptBtn.IsEnabled = false;
			SaveFileBtn.IsEnabled = false;
		}
		else{
			DecryptBtn.IsEnabled = true;
			SaveFileBtn.IsEnabled = true;
		}
	}
}