using System;
using Microsoft.Iris;
using Microsoft.Zune.QuickMix;

namespace ZuneUI;

public class QuickMixSessionManager : ModelItem
{
	private QuickMixSession _quickMixSession;

	private bool _isRefreshing;

	private int _playlistId;

	public int PlaylistId => _playlistId;

	internal QuickMixSession QuickMixSession => _quickMixSession;

	public bool IsRefreshing
	{
		get
		{
			return _isRefreshing;
		}
		set
		{
			if (_isRefreshing != value)
			{
				_isRefreshing = value;
				((ModelItem)this).FirePropertyChanged("IsRefreshing");
			}
		}
	}

	public QuickMixSessionManager(int playlistId)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		_playlistId = playlistId;
		int[] array = new int[1] { playlistId };
		QuickMix.Instance.CreateSession((EQuickMixMode)0, array, (EMediaTypes)9, ref _quickMixSession);
	}

	protected override void OnDispose(bool disposing)
	{
		if (_quickMixSession != null)
		{
			_quickMixSession.Dispose();
			_quickMixSession = null;
		}
		((ModelItem)this).OnDispose(disposing);
	}

	public void Refresh(bool deepRefresh)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (_quickMixSession != null && !IsRefreshing)
		{
			HRESULT val = _quickMixSession.Refresh(TimeSpan.FromMilliseconds(5000.0), deepRefresh, (SimilarMediaBatchHandler)null, new BatchEndHandler(RefreshedHandler));
			if (((HRESULT)(ref val)).IsSuccess)
			{
				IsRefreshing = true;
			}
		}
	}

	private void RefreshedHandler(HRESULT hrAsync)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			IsRefreshing = false;
		}, (object)null);
	}
}
