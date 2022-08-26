using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace IMLokesh.SerilogTextBox;

public class LogTextBox : TextBox
{
    public TextBoxSink TextBoxSink
    {
        get { return (TextBoxSink)GetValue(TextBoxSinkProperty); }
        set { SetValue(TextBoxSinkProperty, value); }
    }

    /// <summary>
    /// If false, new log messages won't be written to the textbox. Log messages are still saved in the background and will be written when this is set as true. Default is true. 
    /// </summary>
    public bool IsActive
    {
        get { return (bool)GetValue(IsActiveProperty); }
        set { SetValue(IsActiveProperty, value); }
    }

    public static readonly DependencyProperty TextBoxSinkProperty = DependencyProperty.Register(nameof(TextBoxSink), typeof(TextBoxSink), typeof(LogTextBox), new PropertyMetadata(default(TextBoxSink), OnLogWriterPropertyChanged));

    public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(nameof(IsActive), typeof(bool), typeof(LogTextBox));

    public LogTextBox() : base()
    {
        TextWrapping = TextWrapping.WrapWithOverflow;
        VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        AcceptsReturn = true;
        IsActive = true;
    }

    protected override void OnGotFocus(RoutedEventArgs e)
    {
        base.OnGotFocus(e);
        CaretIndex = Text.Length;
    }

    private static void OnLogWriterPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
    {
        var logWriter = e.NewValue as TextBoxSink;
        if (logWriter == null)
        {
            return;
        }

        var logTextBox = source as LogTextBox;

        if (logTextBox == null)
        {
            return;
        }

        logWriter.LogMessages.CollectionChanged += (s, e) =>
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
            {
                return;
            }

            if (!logTextBox.IsActive)
            {
                return;
            }

            lock (logWriter.SyncRoot)
            {
                var text = string.Join("", logWriter.LogMessages.ToArray());

                logTextBox.Dispatcher.Invoke(() =>
                {
                    logTextBox.AppendText(text);
                    if (logTextBox.CaretIndex == logTextBox.Text.Length)
                    {
                        logTextBox.ScrollToEnd();
                    }
                });

                logWriter.LogMessages.Clear();
            }
        };
    }
}
