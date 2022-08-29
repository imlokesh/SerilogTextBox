using Serilog;
using Serilog.Configuration;

namespace IMLokesh.SerilogTextBox;

public static class LogTextBoxExtensions
{
    /// <summary>
    /// Serilog sink with a custom textbox that supports AutoScroll. 
    /// </summary>
    /// <param name="loggerConfiguration"></param>
    /// <param name="textBoxLogWriter">Instance of TextBoxSink. This will need to be set on LogTextBox as well in order to complete integration. </param>
    /// <returns></returns>
    public static LoggerConfiguration TextBox(this LoggerSinkConfiguration loggerConfiguration, TextBoxSink textBoxLogWriter)
    {
        return loggerConfiguration.Sink(textBoxLogWriter);
    }
}