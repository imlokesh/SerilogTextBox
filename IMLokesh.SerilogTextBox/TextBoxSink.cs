using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
using System.Collections.ObjectModel;
using System.IO;

namespace IMLokesh.SerilogTextBox;

public class TextBoxSink : ILogEventSink
{
    public ObservableCollection<string> LogMessages { get; set; }

    private ITextFormatter _textFormatter;

    public readonly object SyncRoot = new();

    public TextBoxSink(string outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
    {
        _textFormatter = new MessageTemplateTextFormatter(outputTemplate, null);
        LogMessages = new ObservableCollection<string>();
    }

    public void Emit(LogEvent logEvent)
    {
        lock (SyncRoot)
        {
            var stringWriter = new StringWriter();
            _textFormatter.Format(logEvent, stringWriter);
            LogMessages.Add(stringWriter.ToString());
        }
    }
}
