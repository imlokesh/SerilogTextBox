# SerilogTextBox

This library provides a LogTextBox and a TextBoxSink to be used with [Serilog](https://serilog.net/) in wpf. By default, LogTextBox will autoscroll if the caret is at the end of the textbox or vertical scrollbar is scrolled all the way to bottom.

<picture>

<img  alt="SerilogTextBox Sample App"  src="https://user-images.githubusercontent.com/1937466/187139093-1ece954d-68a8-4c60-b200-d884f869558f.gif">

</picture>

&nbsp;

## Getting Started

Here's a super quick start guide. For a more in depth example using MVVM pattern, check out the [sample app](https://github.com/imlokesh/SerilogTextBox/tree/master/IMLokesh.SerilogTextBox.SampleApp). 

1. Create a new WPF application and install [SerilogTextBox from nuget](https://www.nuget.org/packages/IMLokesh.SerilogTextBox).

2. Add the following code to your MainWindow.xaml.cs. 

```c#
public partial class MainWindow : Window
{
    public TextBoxSink LogSink { get; set; }

    private Timer LogTimer { get; set; }

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;

        LogSink = new TextBoxSink();

        // Setup Serilog with TextBoxSink
        Log.Logger = new LoggerConfiguration()
                        .WriteTo
                        .TextBox(LogSink)
                        .CreateLogger();

        // Log every 500ms
        LogTimer = new Timer(c => Log.Information(Guid.NewGuid().ToString()), null, 1000, 500);
    }
}
```

3. Add the LogTextBox control to your MainWindow.xaml.

```xml
<Window ...
		xmlns:stb="http://imlokesh.com/serilog/textbox"
        ...>
    <Grid>
        <stb:LogTextBox TextBoxSink="{Binding LogSink}" />
    </Grid>
</Window>
```

4. Press F5 and you'll have a wpf application with a log message every 500ms in an autoscrolling log text box. 