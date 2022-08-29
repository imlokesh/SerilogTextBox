using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IMLokesh.SerilogTextBox.SampleApp;

public partial class MainWindowViewModel : ObservableObject
{
    public TextBoxSink TextBoxSink { get; set; }

    public MainWindowViewModel(TextBoxSink textBoxSink)
    {
        TextBoxSink = textBoxSink;
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task StartLogging(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            Log.Information(Guid.NewGuid().ToString());
            await Task.Delay(200, token).ContinueWith(t => { });
        }
    }
}
