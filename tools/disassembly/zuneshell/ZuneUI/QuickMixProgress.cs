using System.ComponentModel;
using Microsoft.Iris;
using Microsoft.Zune.QuickMix;

namespace ZuneUI;

public class QuickMixProgress : INotifyPropertyChanged
{
	private float _progress;

	private int _secondsLeft;

	public float Progress
	{
		get
		{
			return _progress;
		}
		private set
		{
			if (value != _progress)
			{
				_progress = value;
				FirePropertyChanged("Progress");
			}
		}
	}

	public int SecondsLeft
	{
		get
		{
			return _secondsLeft;
		}
		private set
		{
			if (value != _secondsLeft)
			{
				_secondsLeft = value;
				FirePropertyChanged("SecondsLeft");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public QuickMixProgress()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		QuickMix.Instance.OnProgress += new QuickMixProgressHandler(UpdateProgress);
	}

	protected void FirePropertyChanged(string propertyName)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	private void UpdateProgress(float progress, int secondsLeft)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			Progress = progress;
			SecondsLeft = secondsLeft;
		}, (object)null);
	}
}
