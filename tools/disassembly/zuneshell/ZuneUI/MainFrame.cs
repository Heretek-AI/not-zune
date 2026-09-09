using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class MainFrame : Frame
{
	private ArrayListDataSet _experiences;

	private QuickplayExperience _quickplay;

	private CollectionExperience _collection;

	private DeviceExperience _device;

	private MarketplaceExperience _marketplace;

	private SocialExperience _social;

	private TestExperience _test;

	private DiscExperience _disc;

	public override IList ExperiencesList
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			if (_experiences == null)
			{
				_experiences = new ArrayListDataSet((IModelItemOwner)(object)this);
				if (FeatureEnablement.IsFeatureEnabled((Features)0))
				{
					((ListDataSet)_experiences).Add((object)Quickplay);
				}
				((ListDataSet)_experiences).Add((object)Collection);
				if (FeatureEnablement.IsFeatureEnabled((Features)2))
				{
					((ListDataSet)_experiences).Add((object)Marketplace);
				}
				if (FeatureEnablement.IsFeatureEnabled((Features)5))
				{
					((ListDataSet)_experiences).Add((object)Social);
				}
			}
			return (IList)_experiences;
		}
	}

	public QuickplayExperience Quickplay
	{
		get
		{
			if (_quickplay == null)
			{
				_quickplay = new QuickplayExperience(this);
			}
			return _quickplay;
		}
	}

	public CollectionExperience Collection
	{
		get
		{
			if (_collection == null)
			{
				_collection = new CollectionExperience(this);
			}
			return _collection;
		}
	}

	public MarketplaceExperience Marketplace
	{
		get
		{
			if (_marketplace == null)
			{
				_marketplace = new MarketplaceExperience(this);
			}
			return _marketplace;
		}
	}

	public SocialExperience Social
	{
		get
		{
			if (_social == null)
			{
				_social = new SocialExperience(this);
			}
			return _social;
		}
	}

	public DeviceExperience Device
	{
		get
		{
			if (_device == null)
			{
				_device = new DeviceExperience(this);
				_device.UpdateShowDevice();
			}
			return _device;
		}
	}

	public TestExperience Test
	{
		get
		{
			if (_test == null)
			{
				_test = new TestExperience(this);
			}
			return _test;
		}
	}

	public DiscExperience Disc
	{
		get
		{
			if (_disc == null)
			{
				_disc = new DiscExperience(this);
				IList experiencesList = ExperiencesList;
				if (experiencesList == null || experiencesList[experiencesList.Count - 1] != _disc)
				{
					((Command)_disc).Available = false;
				}
			}
			return _disc;
		}
	}

	public MainFrame(IModelItemOwner owner)
		: base(owner)
	{
	}

	internal void ShowDevice(bool show)
	{
		IList experiencesList = ExperiencesList;
		int num = experiencesList.Count - 1;
		bool flag = experiencesList[num] == Device || (num > 0 && experiencesList[num - 1] == Device);
		if (show == flag)
		{
			return;
		}
		((Command)Device).Available = show;
		if (show)
		{
			if (experiencesList[num] == Disc)
			{
				experiencesList.Insert(num, Device);
			}
			else
			{
				experiencesList.Add(Device);
			}
		}
		else
		{
			experiencesList.Remove(Device);
		}
	}

	internal void ShowDisc(bool show)
	{
		IList experiencesList = ExperiencesList;
		int index = experiencesList.Count - 1;
		bool flag = experiencesList[index] == Disc;
		if (show != flag)
		{
			((Command)Disc).Available = show;
			if (show)
			{
				experiencesList.Add(Disc);
			}
			else
			{
				experiencesList.RemoveAt(index);
			}
		}
	}

	internal void ShowTest(bool show)
	{
		IList experiencesList = ExperiencesList;
		bool flag = experiencesList.Contains(Test);
		int num = experiencesList.Count - 1;
		while (num >= 0 && (experiencesList[num] == Disc || experiencesList[num] == Device))
		{
			num--;
		}
		if (show != flag)
		{
			((Command)Test).Available = show;
			if (show)
			{
				experiencesList.Insert(num + 1, Test);
			}
			else
			{
				experiencesList.Remove(Test);
			}
		}
	}
}
