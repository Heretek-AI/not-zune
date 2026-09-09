using System;
using Microsoft.Zune.Util;
using UIXControls;

namespace ZuneUI;

public class FeaturesChanged : IDisposable
{
	private bool m_featuresHaveChanged;

	private static FeaturesChanged m_instance;

	public static FeaturesChanged Instance
	{
		get
		{
			if (m_instance == null)
			{
				m_instance = new FeaturesChanged();
			}
			return m_instance;
		}
	}

	public bool FeaturesHaveChanged
	{
		get
		{
			return m_featuresHaveChanged;
		}
		private set
		{
			if (!m_featuresHaveChanged && value)
			{
				MessageBox.Show(Shell.LoadString(StringId.IDS_FEATURESCHANGED_TITLE), Shell.LoadString(StringId.IDS_FEATURESCHANGED_CONTENT), (EventHandler)null);
				m_featuresHaveChanged = value;
			}
		}
	}

	public void StartUp()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		m_featuresHaveChanged = false;
		FeaturesChangedApi.Instance.OnFeaturesChangedEvent += new FeaturesChangedHandler(OnFeaturesChangedCallback);
	}

	private void OnFeaturesChangedCallback(bool featuresHaveChanged)
	{
		FeaturesHaveChanged = featuresHaveChanged;
		if (Shell.MainFrame.Marketplace.IsCurrent)
		{
			CultureHelper.CheckMarketplaceCulture();
		}
	}

	public void Dispose()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		FeaturesChangedApi.Instance.OnFeaturesChangedEvent -= new FeaturesChangedHandler(OnFeaturesChangedCallback);
	}
}
