namespace IMLokesh.SerilogTextBox.SampleApp;

public class MainWindowViewModel
{
	public TextBoxSink TextBoxSink { get; set; }

	public MainWindowViewModel(TextBoxSink textBoxSink)
	{
		TextBoxSink = textBoxSink;
	}
}
