using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class ZuneWebHost : ModelItem
{
	private static int s_partnerServiceUnknownError;

	private WebHostNavigationResult m_result;

	private string m_currentUrl;

	private string m_initialUrl;

	private string m_successUrl;

	private string m_failureUrl;

	private long m_childHwnd;

	private ZuneWebHost m_host;

	private NavErrorData m_navErrorData;

	public long ChildHwnd
	{
		get
		{
			return m_childHwnd;
		}
		set
		{
			if (m_childHwnd != value)
			{
				m_childHwnd = value;
				((ModelItem)this).FirePropertyChanged("ChildHwnd");
			}
		}
	}

	public string CurrentUrl
	{
		get
		{
			return m_currentUrl;
		}
		set
		{
			if (string.Compare(value, m_currentUrl, ignoreCase: false) != 0)
			{
				m_currentUrl = value;
				((ModelItem)this).FirePropertyChanged("CurrentUrl");
			}
		}
	}

	public string ResponseToken { get; private set; }

	public NavErrorData NavigationError
	{
		get
		{
			return m_navErrorData;
		}
		set
		{
			if (m_navErrorData == null || !m_navErrorData.Equals(value))
			{
				m_navErrorData = value;
				((ModelItem)this).FirePropertyChanged("NavigationError");
			}
		}
	}

	public WebHostNavigationResult NavResult
	{
		get
		{
			return m_result;
		}
		set
		{
			if (m_result != value)
			{
				m_result = value;
				((ModelItem)this).FirePropertyChanged("NavResult");
			}
		}
	}

	public string InitialUrl
	{
		get
		{
			return m_initialUrl;
		}
		set
		{
			if (string.Compare(value, m_initialUrl, ignoreCase: false) != 0)
			{
				m_initialUrl = value;
				((ModelItem)this).FirePropertyChanged("InitialUrl");
			}
		}
	}

	public string SuccessUrl
	{
		get
		{
			return m_successUrl;
		}
		set
		{
			if (string.Compare(value, m_successUrl, ignoreCase: false) != 0)
			{
				m_successUrl = value;
				((ModelItem)this).FirePropertyChanged("SuccessUrl");
			}
		}
	}

	public string FailureUrl
	{
		get
		{
			return m_failureUrl;
		}
		set
		{
			if (string.Compare(value, m_failureUrl, ignoreCase: false) != 0)
			{
				m_failureUrl = value;
				((ModelItem)this).FirePropertyChanged("FailureUrl");
			}
		}
	}

	public int PartnerServiceUnknownError => s_partnerServiceUnknownError;

	public ZuneWebHost()
	{
		m_host = ZuneWebHost.Instance;
	}

	static ZuneWebHost()
	{
		s_partnerServiceUnknownError = ((HRESULT)(ref HRESULT._ZEST_E_PARTNER_SERVICE_UNKNOWN_ERROR)).Int;
	}

	public bool Initialize(long hWndHost, int width, int height)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		m_host.SetNavigationCompleteHandler((NavigationCompleteHandler)delegate(string data)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Expected O, but got Unknown
			Application.DeferredInvoke((DeferredInvokeHandler)delegate
			{
				CurrentUrl = data;
				if (!string.IsNullOrEmpty(SuccessUrl) && SuccessUrl.Length <= m_currentUrl.Length && string.Compare(SuccessUrl, m_currentUrl.Substring(0, SuccessUrl.Length), ignoreCase: false) == 0)
				{
					ResponseToken = string.Empty;
					NavResult = WebHostNavigationResult.SuccessUrl;
				}
				else if (!string.IsNullOrEmpty(FailureUrl) && FailureUrl.Length <= m_currentUrl.Length && string.Compare(FailureUrl, m_currentUrl.Substring(0, FailureUrl.Length), ignoreCase: false) == 0)
				{
					NavResult = WebHostNavigationResult.FailureUrl;
				}
			}, (object)data);
		});
		m_host.SetNavigationErrorHandler((NavigationErrorHandler)delegate(string errorUrl, int errorCode)
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			NavErrorData data = new NavErrorData(errorUrl, errorCode);
			Application.DeferredInvoke((DeferredInvokeHandler)delegate
			{
				NavigationError = data;
			}, (object)data);
		});
		long num = m_host.Initialize(InitialUrl, hWndHost, width, height);
		if (num == 0)
		{
			NavigationError = new NavErrorData(InitialUrl, ((HRESULT)(ref HRESULT._ZEST_E_PARTNER_SERVICE_UNKNOWN_ERROR)).Int);
			NavResult = WebHostNavigationResult.FailureUrl;
		}
		else
		{
			ChildHwnd = num;
		}
		return num != 0;
	}

	public bool SetSize(long hWndHost, int width, int height)
	{
		return m_host.SetSize(hWndHost, width, height);
	}
}
