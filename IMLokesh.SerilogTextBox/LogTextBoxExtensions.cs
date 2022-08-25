using Serilog;
using Serilog.Configuration;

namespace IMLokesh.SerilogTextBox;

public static class LogTextBoxExtensions
{
    public static LoggerConfiguration TextBox(this LoggerSinkConfiguration loggerConfiguration, TextBoxSink textBoxLogWriter)
    {
        return loggerConfiguration.Sink(textBoxLogWriter);
    }
}