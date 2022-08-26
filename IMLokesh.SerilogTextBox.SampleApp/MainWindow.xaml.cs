using Serilog;
using System;
using System.Windows;

namespace IMLokesh.SerilogTextBox.SampleApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel mainWindowViewModel)
    {
        InitializeComponent();
        DataContext = mainWindowViewModel;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Log.Debug(Guid.NewGuid().ToString());
    }
}
