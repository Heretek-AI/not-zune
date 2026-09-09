using System;
using Microsoft.Iris;
using Microsoft.Zune.Messaging;

namespace ZuneUI;

public class PropertyEditProfileImage : NotifyPropertyChangedImpl
{
	private ProfileImage m_lastImage;

	public ProfileImage LastImage
	{
		get
		{
			return m_lastImage;
		}
		private set
		{
			if (m_lastImage != value)
			{
				m_lastImage = value;
				FirePropertyChanged("LastImage");
			}
		}
	}

	public event EventHandler CommitComplete;

	public void Commit(ProfileImage image)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		if (!ComposerHelper.ManageProfileImage(image, new MessagingCallback(OnCommitComplete), image))
		{
			OnCommitComplete(HRESULT._E_FAIL, image);
		}
	}

	private void OnCommitCompleteDeferred(object args)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		HRESULT val = (HRESULT)((object[])args)[0];
		ProfileImage lastImage = ((object[])args)[1] as ProfileImage;
		if (this.CommitComplete != null)
		{
			this.CommitComplete(this, null);
		}
		FirePropertyChanged("CommitComplete");
		if (((HRESULT)(ref val)).IsError)
		{
			LastImage = null;
			Shell.ShowErrorDialog(((HRESULT)(ref val)).Int, Shell.LoadString(StringId.IDS_PROFILE_EDIT_IMAGE_FAIL_TITLE));
		}
		else
		{
			LastImage = lastImage;
		}
	}

	private void OnCommitComplete(HRESULT hr, object state)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(OnCommitCompleteDeferred), (object)new object[2] { hr, state });
	}
}
