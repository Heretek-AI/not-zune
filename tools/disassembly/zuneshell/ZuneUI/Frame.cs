using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public abstract class Frame : ModelItem
{
	private bool _isCurrent;

	private Choice _experiences;

	public Choice Experiences
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			if (_experiences == null)
			{
				_experiences = new Choice((IModelItemOwner)(object)this);
				_experiences.Options = ExperiencesList;
			}
			return _experiences;
		}
		set
		{
			if (_experiences != value)
			{
				_experiences = value;
				((ModelItem)this).FirePropertyChanged("Experiences");
			}
		}
	}

	public abstract IList ExperiencesList { get; }

	public bool IsCurrent
	{
		get
		{
			return _isCurrent;
		}
		set
		{
			if (_isCurrent != value)
			{
				_isCurrent = value;
				OnIsCurrentChanged();
				((ModelItem)this).FirePropertyChanged("IsCurrent");
			}
		}
	}

	public Frame(IModelItemOwner owner)
		: base(owner)
	{
	}

	protected virtual void OnIsCurrentChanged()
	{
	}
}
