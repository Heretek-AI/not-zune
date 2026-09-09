using System.Collections;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Util;
using MicrosoftZunePlayback;

namespace ZuneUI;

public class BandwidthTest : ModelItem
{
	private const int c_minHDRequirement = 2800000;

	private const int c_minSDRequirement = 700000;

	private const int WIN32ERRCODE_TIMEOUT = -2147023436;

	private BandwidthTestInterop m_bandwidthTestInterop;

	private int m_bandwidthTestProgressPercent;

	private BandwidthCapacity m_bandwidthTestResult;

	private IList m_videoOffers;

	private string m_uri;

	private bool m_fBandwidthTestRunning;

	private bool m_fBandwidthTestCanceled;

	private bool m_fBandwidthTestCompleted;

	private bool m_fBandwidthTestErrFound;

	private BandwidthCapacity m_lastTestResult;

	private bool m_fIsRetried;

	private bool m_fIsContentSDOnly;

	private bool m_fIsContentHDOnly;

	public int BandwidthTestProgressPercent
	{
		get
		{
			return m_bandwidthTestProgressPercent;
		}
		private set
		{
			m_bandwidthTestProgressPercent = value;
			((ModelItem)this).FirePropertyChanged("BandwidthTestProgressPercent");
		}
	}

	public bool IsBandwidthTestErrFound
	{
		get
		{
			return m_fBandwidthTestErrFound;
		}
		private set
		{
			m_fBandwidthTestErrFound = value;
			((ModelItem)this).FirePropertyChanged("IsBandwidthTestErrFound");
		}
	}

	public bool IsBandwidthTestRunning
	{
		get
		{
			return m_fBandwidthTestRunning;
		}
		private set
		{
			m_fBandwidthTestRunning = value;
			((ModelItem)this).FirePropertyChanged("IsBandwidthTestRunning");
		}
	}

	public bool IsBandwidthTestCanceled
	{
		get
		{
			return m_fBandwidthTestCanceled;
		}
		private set
		{
			m_fBandwidthTestCanceled = value;
			((ModelItem)this).FirePropertyChanged("IsBandwidthTestCanceled");
		}
	}

	public bool IsBandwidthTestCompleted
	{
		get
		{
			return m_fBandwidthTestCompleted;
		}
		private set
		{
			m_fBandwidthTestCompleted = value;
			((ModelItem)this).FirePropertyChanged("IsBandwidthTestCompleted");
		}
	}

	public BandwidthCapacity BandwidthTestResult
	{
		get
		{
			return m_bandwidthTestResult;
		}
		private set
		{
			m_bandwidthTestResult = value;
			((ModelItem)this).FirePropertyChanged("BandwidthTestResult");
		}
	}

	public bool IsContentSDOnly
	{
		get
		{
			return m_fIsContentSDOnly;
		}
		set
		{
			m_fIsContentSDOnly = value;
			((ModelItem)this).FirePropertyChanged("IsContentSDOnly");
		}
	}

	public bool IsContentHDOnly
	{
		get
		{
			return m_fIsContentHDOnly;
		}
		set
		{
			m_fIsContentHDOnly = value;
			((ModelItem)this).FirePropertyChanged("IsContentHDOnly");
		}
	}

	protected override void OnDispose(bool disposing)
	{
		((ModelItem)this).OnDispose(disposing);
		if (disposing && m_bandwidthTestInterop != null)
		{
			m_bandwidthTestInterop = null;
		}
	}

	public void StartBandwidthTest(IList videoOffers)
	{
		if (!IsBandwidthTestRunning)
		{
			m_videoOffers = videoOffers;
			IsBandwidthTestRunning = true;
			IsBandwidthTestCanceled = false;
			IsBandwidthTestCompleted = false;
			IsBandwidthTestErrFound = false;
			StartGettingContentUri();
		}
	}

	public void CancelBandwidthTest()
	{
		if (m_bandwidthTestInterop != null && IsBandwidthTestRunning)
		{
			m_bandwidthTestInterop.Cancel();
			m_bandwidthTestInterop = null;
			IsBandwidthTestCanceled = true;
			IsBandwidthTestRunning = false;
			if (DownloadManager.Instance.IsQueuePaused)
			{
				DownloadManager.Instance.ResumeQueue();
			}
		}
	}

	public void RetryBandwidthTest()
	{
		if (m_videoOffers != null)
		{
			m_fIsRetried = true;
			StartBandwidthTest(m_videoOffers);
		}
	}

	public void LogHDSDChoice(int chosenHDSDChoice)
	{
		if (BandwidthTestResult != BandwidthCapacity.None)
		{
			SQMLog.LogToStream((SQMDataId)10, (uint)BandwidthTestResult, (uint)chosenHDSDChoice);
		}
	}

	private void StartGettingContentUri()
	{
		ThreadPool.QueueUserWorkItem(delegate
		{
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			string contentUri = GetContentUri();
			Application.DeferredInvoke(new DeferredInvokeHandler(OnGotContentUri), (object)contentUri);
		}, null);
	}

	private string GetContentUri()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		string result = null;
		object? obj = m_videoOffers[0];
		VideoOffer val = (VideoOffer)((obj is VideoOffer) ? obj : null);
		if (val != null)
		{
			if (!ZuneApplication.Service.InCompleteCollection(((Offer)val).Id, (EContentType)3) && !string.IsNullOrEmpty(m_uri))
			{
				result = m_uri;
			}
			else
			{
				ZuneApplication.Service.GetContentUri(((Offer)val).Id, (EContentType)3, (EContentUriFlags)9, val.IsHD, val.IsRental, ref result);
			}
		}
		return result;
	}

	private void OnGotContentUri(object obj)
	{
		string text = (string)obj;
		if (string.IsNullOrEmpty(text))
		{
			IsBandwidthTestErrFound = true;
			return;
		}
		m_uri = text;
		if (!DownloadManager.Instance.IsQueuePaused)
		{
			DownloadManager.Instance.PauseQueue();
		}
		int bandwidthTestTimeoutSec = ClientConfiguration.GeneralSettings.BandwidthTestTimeoutSec;
		StartBandwidthTestCore(m_uri, bandwidthTestTimeoutSec);
	}

	private void StartBandwidthTestCore(string uri, int testTimeout)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		if (!string.IsNullOrEmpty(uri))
		{
			IsBandwidthTestErrFound = false;
			m_bandwidthTestInterop = new BandwidthTestInterop();
			m_bandwidthTestInterop.BandwidthTestUpdate += new BandwidthTestUpdateEventHandler(OnBandwidthTestUpdate);
			m_bandwidthTestInterop.BandwidthTestError += new BandwidthTestErrorEventHandler(OnBandwidthTestError);
			m_bandwidthTestInterop.Start(uri, testTimeout);
		}
	}

	private void OnBandwidthTestUpdate(object sender, BandwidthUpdateArgs args)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(OnBandwidthTestUpdateOnApp), (object)args);
	}

	private void OnBandwidthTestError(object sender, BandwidthTestErrorArgs args)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(OnBandwidthTestErrorOnApp), (object)args);
	}

	private void OnBandwidthTestUpdateOnApp(object obj)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected I4, but got Unknown
		if (obj == null)
		{
			return;
		}
		BandwidthUpdateArgs val = (BandwidthUpdateArgs)obj;
		if (val == null)
		{
			return;
		}
		MBRHeuristicState currentState = val.currentState;
		MBRHeuristicState val2 = currentState;
		switch ((int)val2)
		{
		case 0:
			BandwidthTestProgressPercent = 5;
			break;
		case 1:
			if (val.PercentComplete > 5)
			{
				BandwidthTestProgressPercent = val.PercentComplete;
			}
			break;
		case 2:
			if (DownloadManager.Instance.IsQueuePaused)
			{
				DownloadManager.Instance.ResumeQueue();
			}
			if (val.TotalAverageBandwidth >= 2800000)
			{
				BandwidthTestResult = BandwidthCapacity.HDCapable;
			}
			else if (val.TotalAverageBandwidth >= 700000)
			{
				BandwidthTestResult = BandwidthCapacity.SDCapable;
			}
			else
			{
				BandwidthTestResult = BandwidthCapacity.None;
			}
			BandwidthTestProgressPercent = 100;
			IsBandwidthTestCompleted = true;
			IsBandwidthTestRunning = false;
			m_bandwidthTestInterop = null;
			if (BandwidthTestResult == BandwidthCapacity.None)
			{
				SQMLog.Log((SQMDataId)11, 1);
			}
			if (m_fIsRetried && BandwidthTestResult != m_lastTestResult)
			{
				SQMLog.Log((SQMDataId)13, 1);
			}
			m_lastTestResult = BandwidthTestResult;
			break;
		}
	}

	private void OnBandwidthTestErrorOnApp(object obj)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		IsBandwidthTestErrFound = true;
		IsBandwidthTestRunning = false;
		if (DownloadManager.Instance.IsQueuePaused)
		{
			DownloadManager.Instance.ResumeQueue();
		}
		m_bandwidthTestInterop = null;
		BandwidthTestErrorArgs val = (BandwidthTestErrorArgs)obj;
		if (val.ErrorCode == -2147023436)
		{
			ShipAssert.Assert(false);
			SQMLog.Log((SQMDataId)12, 1);
		}
	}
}
