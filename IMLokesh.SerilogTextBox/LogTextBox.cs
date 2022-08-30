using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace IMLokesh.SerilogTextBox;

public class LogTextBox : TextBox
{
    public static readonly DependencyProperty TextBoxSinkProperty = DependencyProperty.Register(nameof(TextBoxSink), typeof(TextBoxSink), typeof(LogTextBox), new PropertyMetadata(default(TextBoxSink), OnLogWriterPropertyChanged));

    public TextBoxSink TextBoxSink
    {
        get { return (TextBoxSink)GetValue(TextBoxSinkProperty); }
        set { SetValue(TextBoxSinkProperty, value); }
    }

    public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(nameof(IsActive), typeof(bool), typeof(LogTextBox));

    /// <summary>
    /// If false, new log messages won't be written to the textbox. Log messages are still saved in the background and will be written when this is set as true. Default is true. 
    /// </summary>
    public bool IsActive
    {
        get { return (bool)GetValue(IsActiveProperty); }
        set { SetValue(IsActiveProperty, value); }
    }

    public static readonly DependencyProperty AlwaysAutoscrollProperty = DependencyProperty.Register(nameof(AlwaysAutoscroll), typeof(bool), typeof(LogTextBox));

    /// <summary>
    /// If true, the text box will autoscroll irrespective of the caret or scrollbar position. 
    /// </summary>
    public bool AlwaysAutoscroll
    {
        get { return (bool)GetValue(AlwaysAutoscrollProperty); }
        set { SetValue(AlwaysAutoscrollProperty, value); }
    }

    public LogTextBox() : base()
    {
        TextWrapping = TextWrapping.NoWrap;
        VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
        AcceptsReturn = true;
        IsActive = true;
    }

    private bool IsScrolledToEnd()
    {
        return VerticalOffset + ViewportHeight == ExtentHeight;
    }

    private static void OnLogWriterPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
    {
        var textBoxSink = e.NewValue as TextBoxSink;
        if (textBoxSink == null)
        {
            return;
        }

        var logTextBox = source as LogTextBox;

        if (logTextBox == null)
        {
            return;
        }

        textBoxSink.LogMessages.CollectionChanged += (s, e) =>
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
            {
                return;
            }

            if (!logTextBox.Dispatcher.Invoke(() => logTextBox.IsActive))
            {
                return;
            }

            lock (textBoxSink.SyncRoot)
            {
                var text = string.Join("", textBoxSink.LogMessages.ToArray());

                logTextBox.Dispatcher.Invoke(() =>
                {
                    logTextBox.AppendText(text);
                    if (logTextBox.CaretIndex == logTextBox.Text.Length || !logTextBox.IsFocused || logTextBox.IsScrolledToEnd() || logTextBox.AlwaysAutoscroll)
                    {
                        logTextBox.ScrollToEnd();
                    }
                });

                textBoxSink.LogMessages.Clear();
            }
        };
    }
}
