﻿using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;

namespace MauiApp1;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);
		builder.Services.AddSingleton<IFilePicker>(FilePicker.Default);
		builder.Services.AddTransient<MainPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
