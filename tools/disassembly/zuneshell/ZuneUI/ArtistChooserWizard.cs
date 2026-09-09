using Microsoft.Iris;

namespace ZuneUI;

public class ArtistChooserWizard : Wizard
{
	private bool _errorOccurred;

	public bool ErrorOccurred
	{
		get
		{
			return _errorOccurred;
		}
		set
		{
			if (_errorOccurred != value)
			{
				_errorOccurred = value;
				((ModelItem)this).FirePropertyChanged("ErrorOccurred");
			}
		}
	}

	public ArtistChooserWizard()
	{
		AddPage(new ArtistChooserWizardPage(this));
	}
}
