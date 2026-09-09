using Microsoft.Iris;
using Microsoft.Zune.Messaging;

namespace ZuneUI;

public class PropertyEditProfile : MetadataEditMedia
{
	private DataProviderObject _profile;

	private static PropertyDescriptor[] s_dataProviderProperties;

	public static ProfileMultiLinePropertyDescriptor s_Biography = new ProfileMultiLinePropertyDescriptor("Biography", "bio", StringId.IDS_PROFILE_EDIT_BIO, 300);

	public static ProfilePropertyDescriptor s_DisplayName = new ProfilePropertyDescriptor("DisplayName", "displayname", StringId.IDS_PROFILE_EDIT_DISPLAYNAME, 15);

	public static ProfilePropertyDescriptor s_Location = new ProfilePropertyDescriptor("Location", "location", StringId.IDS_PROFILE_EDIT_LOCATION, 30);

	public static ProfilePropertyDescriptor s_Status = new ProfilePropertyDescriptor("Status", "status", StringId.IDS_PROFILE_EDIT_STATUS, 60);

	public DataProviderObject Profile => _profile;

	public static ProfileMultiLinePropertyDescriptor Biography => s_Biography;

	public static ProfilePropertyDescriptor DisplayName => s_DisplayName;

	public static ProfilePropertyDescriptor Location => s_Location;

	public static ProfilePropertyDescriptor Status => s_Status;

	public PropertyEditProfile(DataProviderObject profile)
	{
		_source = DataProviderObjectPropertySource.Instance;
		Initialize(profile);
	}

	protected void Initialize(DataProviderObject profile)
	{
		_profile = profile;
		if (s_dataProviderProperties == null)
		{
			s_dataProviderProperties = new PropertyDescriptor[4] { s_Biography, s_DisplayName, s_Location, s_Status };
		}
		Initialize(new object[1] { _profile }, s_dataProviderProperties);
	}

	public override void Commit()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		base.Commit();
		PropertyDescriptor[] array = s_dataProviderProperties;
		for (int i = 0; i < array.Length; i++)
		{
			ProfilePropertyDescriptor profilePropertyDescriptor = (ProfilePropertyDescriptor)array[i];
			MetadataEditProperty property = GetProperty(profilePropertyDescriptor);
			if (property.Modified)
			{
				ComposerHelper.ManageProfile(profilePropertyDescriptor.ServiceName, profilePropertyDescriptor.GetServiceValue(property.Value), new MessagingCallback(OnCommitComplete), property);
			}
		}
	}

	public void Undo()
	{
		PropertyDescriptor[] array = s_dataProviderProperties;
		foreach (PropertyDescriptor descriptor in array)
		{
			MetadataEditProperty property = GetProperty(descriptor);
			property.Value = property.OriginalValue;
			property.Modified = false;
		}
	}

	private void OnCommitCompleteDeferred(object args)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		HRESULT val = (HRESULT)((object[])args)[0];
		if (!(((object[])args)[1] is MetadataEditProperty metadataEditProperty))
		{
			return;
		}
		if (((HRESULT)(ref val)).IsSuccess)
		{
			metadataEditProperty.OriginalValue = metadataEditProperty.Value;
		}
		else
		{
			if (metadataEditProperty.Descriptor is ProfilePropertyDescriptor)
			{
				StringId displayNameId = ((ProfilePropertyDescriptor)metadataEditProperty.Descriptor).DisplayNameId;
				string title = Shell.LoadString(displayNameId);
				ErrorDialogInfo.Show(((HRESULT)(ref val)).Int, title);
			}
			metadataEditProperty.Value = metadataEditProperty.OriginalValue;
		}
		metadataEditProperty.Modified = false;
	}

	private void OnCommitComplete(HRESULT hr, object state)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(OnCommitCompleteDeferred), (object)new object[2] { hr, state });
	}
}
