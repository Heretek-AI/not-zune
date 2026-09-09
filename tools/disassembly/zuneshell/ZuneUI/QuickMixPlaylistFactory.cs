using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.PerfTrace;
using Microsoft.Zune.Playlist;
using Microsoft.Zune.QuickMix;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class QuickMixPlaylistFactory : PlaylistFactory
{
	private const int _minimumSimilarArtistsRequired = 10;

	private QuickMixSession _quickMixSession;

	private bool _shouldDisposeSession;

	private HRESULT _hrCreation = HRESULT._E_PENDING;

	private EQuickMixMode _mode;

	private List<string> _similarArtistStrings;

	public IList SimilarArtists => _similarArtistStrings;

	private QuickMixPlaylistFactory(int mediaId, MediaType mediaType, EQuickMixMode mode, out HRESULT hr)
		: base(navigateOnCreate: true)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Invalid comparison between Unknown and I4
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00a9: Expected O, but got Unknown
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		TimeSpan timeSpan = TimeSpan.FromMilliseconds(5000.0);
		int[] array = new int[1] { mediaId };
		_mode = mode;
		QuickMix instance = QuickMix.Instance;
		hr = instance.CreateSession(_mode, array, (EMediaTypes)mediaType, ref _quickMixSession);
		if (((HRESULT)(ref hr)).IsSuccess)
		{
			_shouldDisposeSession = true;
			if ((int)_mode != 2)
			{
				PerfTrace.TraceUICollectionEvent(UICollectionEvent.QuickMixBegin, "");
			}
			hr = _quickMixSession.GetSimilarMedia((uint)ClientConfiguration.QuickMix.DefaultPlaylistLength, timeSpan, new SimilarMediaBatchHandler(SimilarBatchHandler), new BatchEndHandler(BatchEndHandler));
		}
	}

	private QuickMixPlaylistFactory(Guid serviceMediaId, MediaType mediaType, EQuickMixMode mode, out HRESULT hr)
		: base(navigateOnCreate: true)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		//IL_008a: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		TimeSpan timeSpan = TimeSpan.FromMilliseconds(10000.0);
		_mode = mode;
		QuickMix instance = QuickMix.Instance;
		hr = instance.CreateSession(_mode, serviceMediaId, (EMediaTypes)mediaType, (string)null, ref _quickMixSession);
		if (((HRESULT)(ref hr)).IsSuccess)
		{
			_shouldDisposeSession = true;
			hr = _quickMixSession.GetSimilarMedia((uint)ClientConfiguration.QuickMix.DefaultPlaylistLength, timeSpan, new SimilarMediaBatchHandler(SimilarBatchHandler), new BatchEndHandler(BatchEndHandler));
		}
	}

	private QuickMixPlaylistFactory(QuickMixSession quickMixSession)
		: base(navigateOnCreate: true)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		_quickMixSession = quickMixSession;
		_shouldDisposeSession = false;
		_hrCreation = HRESULT._S_OK;
		base.Ready = true;
	}

	public override void Dispose()
	{
		if (_shouldDisposeSession && _quickMixSession != null)
		{
			_quickMixSession.Dispose();
			_quickMixSession = null;
		}
	}

	public static QuickMixPlaylistFactory CreateInstance(int mediaId, MediaType mediaType)
	{
		return CreateInstance(mediaId, (EQuickMixMode)0, mediaType);
	}

	public static QuickMixPlaylistFactory CreateInstance(int mediaId, EQuickMixMode mode, MediaType mediaType)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		HRESULT hr;
		QuickMixPlaylistFactory result = new QuickMixPlaylistFactory(mediaId, mediaType, mode, out hr);
		if (((HRESULT)(ref hr)).IsSuccess)
		{
			return result;
		}
		return null;
	}

	public static QuickMixPlaylistFactory CreateInstance(Guid serviceMediaId, EQuickMixMode mode, MediaType mediaType)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		HRESULT hr;
		QuickMixPlaylistFactory result = new QuickMixPlaylistFactory(serviceMediaId, mediaType, mode, out hr);
		if (((HRESULT)(ref hr)).IsSuccess)
		{
			return result;
		}
		return null;
	}

	public static QuickMixPlaylistFactory CreateInstance(QuickMixSession quickMixSession)
	{
		return new QuickMixPlaylistFactory(quickMixSession);
	}

	public override string GetUniqueTitle()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		string result = "";
		_quickMixSession.GetPlaylistTitle(ref result);
		return result;
	}

	public override PlaylistResult CreatePlaylist(string title, CreatePlaylistOption option)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		SQMLog.Log((SQMDataId)196, 1);
		int playlistId = -1;
		HRESULT hr = HRESULT._E_PENDING;
		if (base.Ready)
		{
			hr = _hrCreation;
			if (((HRESULT)(ref hr)).IsSuccess)
			{
				hr = _quickMixSession.SaveAsPlaylist(title, option, ref playlistId);
			}
		}
		return new PlaylistResult(playlistId, hr);
	}

	private void BatchEndHandler(HRESULT hrAsync)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Invalid comparison between Unknown and I4
			_hrCreation = hrAsync;
			base.Ready = true;
			if ((int)_mode != 2)
			{
				PerfTrace.TraceUICollectionEvent(UICollectionEvent.QuickMixComplete, "");
			}
		}, (object)null);
	}

	private void SimilarBatchHandler(IList itemList)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Invalid comparison between Unknown and I4
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Expected O, but got Unknown
			if ((int)_mode == 2)
			{
				if (_similarArtistStrings == null)
				{
					_similarArtistStrings = new List<string>();
				}
				foreach (QuickMixItem item in itemList)
				{
					QuickMixItem val = item;
					_similarArtistStrings.Add(val.ArtistName);
				}
				if (_similarArtistStrings.Count > 10)
				{
					base.Ready = true;
				}
			}
		}, (object)null);
	}
}
