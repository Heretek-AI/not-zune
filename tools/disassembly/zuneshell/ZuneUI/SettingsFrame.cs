using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class SettingsFrame : Frame
{
	private Experience[] _experiences;

	private SettingsExperience _settings;

	private WizardExperience _wizard;

	public override IList ExperiencesList
	{
		get
		{
			if (_experiences == null)
			{
				_experiences = new Experience[1] { Settings };
			}
			return _experiences;
		}
	}

	public SettingsExperience Settings
	{
		get
		{
			if (_settings == null)
			{
				_settings = new SettingsExperience(this);
			}
			return _settings;
		}
	}

	public WizardExperience Wizard
	{
		get
		{
			if (_wizard == null)
			{
				_wizard = new WizardExperience(this);
			}
			return _wizard;
		}
	}

	public SettingsFrame(IModelItemOwner owner)
		: base(owner)
	{
	}

	protected override void OnIsCurrentChanged()
	{
		SingletonModelItem<UIDeviceList>.Instance.AllowUnreadyDevices = !base.IsCurrent || ZuneShell.DefaultInstance.CurrentPage is FirstLaunchLandPage || ZuneShell.DefaultInstance.CurrentPage is GDILandPage;
	}
}
