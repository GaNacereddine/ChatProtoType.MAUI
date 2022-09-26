using ChatProtoType.MAUI.Views;

namespace ChatProtoType.MAUI;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        MainPage = new NavigationPage(new MainView());
    }
}
