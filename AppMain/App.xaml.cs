namespace AppMain;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

#if WINDOWS
        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);

            if (window != null)
            {
                window.Title = "Family Expenses List";
            }

            return window;
        }
#endif
}
