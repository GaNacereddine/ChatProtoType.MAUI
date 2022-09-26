using ChatProtoType.MAUI.Controls;
using ChatProtoType.MAUI.Handlers;

namespace ChatProtoType.MAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler(typeof(PointerInputView), typeof(PointerInputViewHandler));
            });

		return builder.Build();
	}
}
