using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class JumpListManager : SingletonModelItem<JumpListManager>
{
	private class UpdateJumpListJob
	{
		public int Cookie;

		public bool ShowResumeNowPlaying;

		public string ResumeNowPlayingName;

		public string ShuffleAllName;

		public string PinCategoryName;

		public string QuickMixCategoryName;

		public JumpListPin[] PinList;

		public JumpListPin[] QuickMixList;
	}

	private const int _maxPins = 6;

	private const int _maxQuickMixes = 5;

	private const int _normalIconIndex = -12;

	private const int _quickMixIconIndex = -13;

	private Command _updatePins;

	private IList _pinList;

	private IList _quickMixList;

	private bool _pinListsHaveBeenSetAtLeastOnce;

	private bool _finalCommitHasBeenCompleted;

	private bool _showResumeNowPlaying;

	private bool _currentPlaylistWillBeSaved;

	private bool _transportControlsHavePlaylist;

	private string _resumeNowPlayingName;

	private string _shuffleAllName;

	private string _pinCategoryName;

	private string _quickMixCategoryName;

	private static int _lastUpdateJobCookie = 0;

	private static object _updateJobLock = new object();

	public Command JumpListPinUpdateRequested => _updatePins;

	public JumpListManager()
		: base((IModelItemOwner)(object)SingletonModelItem<TransportControls>.Instance)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		_updatePins = new Command((IModelItemOwner)(object)this);
		_pinListsHaveBeenSetAtLeastOnce = false;
		_finalCommitHasBeenCompleted = false;
		_showResumeNowPlaying = false;
		_resumeNowPlayingName = Shell.LoadString(StringId.IDS_JUMP_LIST_RESUME_NOWPLAYING);
		_shuffleAllName = Shell.LoadString(StringId.IDS_JUMP_LIST_SHUFFLE_ALL);
		_pinCategoryName = Shell.LoadString(StringId.IDS_JUMP_LIST_QUICKPLAY_CATEGORY);
		_quickMixCategoryName = Shell.LoadString(StringId.IDS_JUMP_LIST_QUICKMIX_CATEGORY);
		((ModelItem)SingletonModelItem<TransportControls>.Instance).PropertyChanged += OnTransportControlsPropertyChanged;
		ZuneApplication.Closing += OnApplicationClosing;
	}

	protected override void OnDispose(bool disposing)
	{
		if (disposing)
		{
			ZuneApplication.Closing -= OnApplicationClosing;
			((ModelItem)SingletonModelItem<TransportControls>.Instance).PropertyChanged -= OnTransportControlsPropertyChanged;
		}
		((ModelItem)this).OnDispose(disposing);
	}

	public void SetPins(IList pinList, IList quickMixList)
	{
		_pinList = pinList;
		_quickMixList = quickMixList;
		_pinListsHaveBeenSetAtLeastOnce = true;
		UpdateJumpList();
	}

	public static void PlayPin(JumpListPin pin)
	{
		if (pin != null)
		{
			SingletonModelItem<TransportControls>.Instance.RequestedJumpListPin = pin;
		}
	}

	private void OnTransportControlsPropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		if (args.PropertyName == "HasPlaylist" || args.PropertyName == "Playing")
		{
			TransportControls instance = SingletonModelItem<TransportControls>.Instance;
			bool flag = instance.HasPlaylist && !instance.Playing;
			if (_showResumeNowPlaying != flag)
			{
				_showResumeNowPlaying = flag;
				UpdateJumpList();
			}
			_transportControlsHavePlaylist = instance.HasPlaylist;
		}
		else if (args.PropertyName == "CurrentPlaylist")
		{
			_currentPlaylistWillBeSaved = SingletonModelItem<TransportControls>.Instance.WillSaveCurrentPlaylistOnShutdown();
		}
	}

	private void OnApplicationClosing(object sender, EventArgs args)
	{
		if (!_currentPlaylistWillBeSaved)
		{
			_showResumeNowPlaying = false;
		}
		else if (_transportControlsHavePlaylist)
		{
			_showResumeNowPlaying = true;
		}
		UpdateJumpList();
		_finalCommitHasBeenCompleted = true;
	}

	private void UpdateJumpList()
	{
		if (OSVersion.IsWin7() && _pinListsHaveBeenSetAtLeastOnce && !_finalCommitHasBeenCompleted)
		{
			UpdateJumpListJob updateJumpListJob = new UpdateJumpListJob();
			updateJumpListJob.Cookie = Interlocked.Increment(ref _lastUpdateJobCookie);
			updateJumpListJob.ShowResumeNowPlaying = _showResumeNowPlaying;
			updateJumpListJob.ResumeNowPlayingName = _resumeNowPlayingName;
			updateJumpListJob.ShuffleAllName = _shuffleAllName;
			updateJumpListJob.PinCategoryName = _pinCategoryName;
			updateJumpListJob.QuickMixCategoryName = _quickMixCategoryName;
			updateJumpListJob.PinList = new JumpListPin[_pinList.Count];
			for (int i = 0; i < _pinList.Count; i++)
			{
				updateJumpListJob.PinList[i] = new JumpListPin((JumpListPin)_pinList[i]);
			}
			updateJumpListJob.QuickMixList = new JumpListPin[_quickMixList.Count];
			for (int j = 0; j < _quickMixList.Count; j++)
			{
				updateJumpListJob.QuickMixList[j] = new JumpListPin((JumpListPin)_quickMixList[j]);
			}
			ThreadPool.QueueUserWorkItem(UpdateJumpListWorker, updateJumpListJob);
		}
	}

	private static void UpdateJumpListWorker(object data)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		if (!(data is UpdateJumpListJob updateJumpListJob))
		{
			return;
		}
		lock (_updateJobLock)
		{
			if (_lastUpdateJobCookie != updateJumpListJob.Cookie)
			{
				return;
			}
			List<JumpListEntry> list = null;
			JumpListSession val2 = default(JumpListSession);
			HRESULT val = HRESULT.op_Implicit(Win7ShellManager.Instance.BeginJumpListSession(ref val2));
			if (((HRESULT)(ref val)).IsSuccess)
			{
				val = HRESULT.op_Implicit(val2.GetDisallowedDestinations(ref list));
			}
			if (((HRESULT)(ref val)).IsSuccess)
			{
				List<string> disallowedCommandLineArguments = list.ConvertAll((JumpListEntry rawEntry) => rawEntry.CommandLineArguments);
				JumpListEntry val3 = default(JumpListEntry);
				if (updateJumpListJob.ShowResumeNowPlaying)
				{
					val2.CreateTask(ref val3);
					val3.Name = updateJumpListJob.ResumeNowPlayingName;
					val3.CommandLineArguments = "/resumenowplaying";
					val3.IconIndex = -12;
				}
				val2.CreateTask(ref val3);
				val3.Name = updateJumpListJob.ShuffleAllName;
				val3.CommandLineArguments = "/shuffleall";
				val3.IconIndex = -12;
				JumpListCategory val4 = default(JumpListCategory);
				if (updateJumpListJob.PinList != null && updateJumpListJob.PinList.Length > 0)
				{
					val2.CreateCategory(ref val4);
					val4.Name = updateJumpListJob.PinCategoryName;
					for (int num = 0; num < 6 && num < updateJumpListJob.PinList.Length; num++)
					{
						PopulateJumpListIfPossible(val4, updateJumpListJob.PinList[num], -12, disallowedCommandLineArguments);
					}
				}
				if (updateJumpListJob.QuickMixList != null && updateJumpListJob.QuickMixList.Length > 0)
				{
					val2.CreateCategory(ref val4);
					val4.Name = updateJumpListJob.QuickMixCategoryName;
					for (int num2 = 0; num2 < 5 && num2 < updateJumpListJob.QuickMixList.Length; num2++)
					{
						PopulateJumpListIfPossible(val4, updateJumpListJob.QuickMixList[num2], -13, disallowedCommandLineArguments);
					}
				}
				val2.Commit();
			}
			else if (val2 != null)
			{
				val2.Cancel();
			}
		}
	}

	private static void PopulateJumpListIfPossible(JumpListCategory category, JumpListPin pin, int iconIndex, List<string> disallowedCommandLineArguments)
	{
		string text = $"/playpin:{pin.ToString()}";
		if (!disallowedCommandLineArguments.Contains(text))
		{
			JumpListEntry val = default(JumpListEntry);
			category.CreateDestination(ref val);
			val.Name = pin.Name;
			val.CommandLineArguments = text;
			val.IconIndex = iconIndex;
		}
	}
}
