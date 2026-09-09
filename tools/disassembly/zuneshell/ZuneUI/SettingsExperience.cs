using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class SettingsExperience : Experience
{
	private ArrayListDataSet _nodes;

	private CategoryPageNode _software;

	private CategoryPageNode _device;

	private CategoryPageNode _account;

	private Category _nameYourDevice;

	public override IList NodesList
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			if (_nodes == null)
			{
				_nodes = new ArrayListDataSet((IModelItemOwner)(object)this);
				((ListDataSet)_nodes).Add((object)Software);
				if (FeatureEnablement.IsFeatureEnabled((Features)1))
				{
					((ListDataSet)_nodes).Add((object)Device);
				}
				if (FeatureEnablement.IsFeatureEnabled((Features)20))
				{
					((ListDataSet)_nodes).Add((object)Account);
				}
			}
			return (IList)_nodes;
		}
	}

	public CategoryPageNode Software
	{
		get
		{
			if (_software == null)
			{
				_software = new CategoryPageNode(this, StringId.IDS_SOFTWARE_PIVOT, new Category[11]
				{
					SettingCategories.Collection,
					SettingCategories.Filetype,
					SettingCategories.Privacy,
					SettingCategories.Podcast,
					SettingCategories.Sharing,
					SettingCategories.Photo,
					SettingCategories.Display,
					SettingCategories.Rip,
					SettingCategories.Burn,
					SettingCategories.Metadata,
					SettingCategories.General
				}, (SQMDataId)0, allowBackNavigation: true, hideDeviceOnCancel: false);
			}
			return _software;
		}
	}

	public CategoryPageNode Device
	{
		get
		{
			if (_device == null)
			{
				List<Category> list = new List<Category>();
				list.Add(SettingCategories.SyncOptions);
				list.Add(SettingCategories.SyncGroups);
				_nameYourDevice = SettingCategories.NameDevice;
				list.Add(_nameYourDevice);
				list.Add(SettingCategories.MoreOnWeb);
				if (ShouldShowDeviceMarketplaceCategory)
				{
					list.Add(SettingCategories.DeviceMarketplace);
				}
				list.Add(SettingCategories.FirmwareUpdate);
				list.Add(SettingCategories.WirelessSetup);
				list.Add(SettingCategories.PictureVideo);
				list.Add(SettingCategories.Transcoding);
				list.Add(SettingCategories.SpaceReservation);
				list.Add(SettingCategories.DevicePrivacy);
				_device = new CategoryPageNode(this, StringId.IDS_DEVICE_PIVOT, list, (SQMDataId)0, allowBackNavigation: true, hideDeviceOnCancel: false);
			}
			return _device;
		}
	}

	public CategoryPageNode Account
	{
		get
		{
			if (_account == null)
			{
				List<Category> list = new List<Category>();
				list.Add(SettingCategories.AccountLinks);
				if (FeatureEnablement.IsFeatureEnabled((Features)2))
				{
					if (FeatureEnablement.IsFeatureEnabled((Features)14) || FeatureEnablement.IsFeatureEnabled((Features)11))
					{
						list.Add(SettingCategories.Devices);
					}
					list.Add(SettingCategories.PurchaseHistory);
					list.Add(SettingCategories.RentalHistory);
					if (FeatureEnablement.IsFeatureEnabled((Features)14))
					{
						list.Add(SettingCategories.SubscriptionHistory);
					}
				}
				_account = new CategoryPageNode(this, StringId.IDS_ACCOUNT_PIVOT, list, (SQMDataId)0, allowBackNavigation: true, hideDeviceOnCancel: false);
			}
			return _account;
		}
	}

	public static bool ShouldShowDeviceMarketplaceCategory
	{
		get
		{
			if (!FeatureEnablement.IsFeatureEnabled((Features)2))
			{
				return FeatureEnablement.IsFeatureEnabled((Features)5);
			}
			return true;
		}
	}

	public SettingsExperience(Frame frameOwner)
		: base(frameOwner, StringId.IDS_SETTINGS_PIVOT, (SQMDataId)0)
	{
		((ModelItem)SyncControls.Instance).PropertyChanged += OnSyncPropertyChanged;
	}

	protected override void OnInvoked()
	{
		if (!Shell.SettingsFrame.Wizard.IsCurrent)
		{
			base.OnInvoked();
		}
	}

	protected override void OnDispose(bool disposing)
	{
		((ModelItem)SyncControls.Instance).PropertyChanged -= OnSyncPropertyChanged;
		((ModelItem)this).OnDispose(disposing);
	}

	private void OnSyncPropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		if (args.PropertyName == "CurrentDevice")
		{
			((ModelItem)Device).Description = SyncControls.Instance.CurrentDevice.PivotDescription;
			if (_nameYourDevice != null)
			{
				((ModelItem)_nameYourDevice).Description = Shell.LoadString(StringId.IDS_NAME_ZUNE_HEADER);
			}
		}
	}

	internal void ShowDevice(bool show)
	{
		IList nodesList = NodesList;
		bool flag = false;
		foreach (object item in nodesList)
		{
			if (item == Device)
			{
				flag = true;
				break;
			}
		}
		if (show == flag)
		{
			return;
		}
		((Command)Device).Available = show;
		if (show)
		{
			if (nodesList.Count > 1 && nodesList[0] == Software)
			{
				nodesList.Insert(1, Device);
			}
			else
			{
				nodesList.Add(Device);
			}
		}
		else
		{
			nodesList.Remove(Device);
		}
	}
}
