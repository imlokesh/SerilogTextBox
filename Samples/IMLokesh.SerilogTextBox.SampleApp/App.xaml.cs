using Serilog;
using System.Windows;

namespace IMLokesh.SerilogTextBox.SampleApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        var textboxSink = new TextBoxSink();

        Log.Logger = new LoggerConfiguration().MinimumLevel.Verbose().WriteTo.TextBox(textboxSink).CreateLogger();

        new MainWindow(new MainWindowViewModel(textboxSink)).Show();
    }
}
