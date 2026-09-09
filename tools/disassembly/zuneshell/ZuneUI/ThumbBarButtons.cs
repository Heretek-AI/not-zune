using System;
using System.ComponentModel;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class ThumbBarButtons : SingletonModelItem<ThumbBarButtons>
{
	private enum ThumbButtonID : uint
	{
		PlayPause,
		Previous,
		Next,
		Rating,
		Spacer
	}

	private class UpdateThumbBarJob
	{
		public int Cookie;

		public bool IsPlaying;

		public bool CanPlay;

		public bool CanPause;

		public bool CanGoForward;

		public bool CanGoBack;

		public bool IsCurrentTrackRatable;

		public int CurrentTrackRating;
	}

	private ThumbBar _thumbBar;

	private ThumbBarButton _playPause;

	private ThumbBarButton _previous;

	private ThumbBarButton _next;

	private ThumbBarButton _rating;

	private int _lastUpdateJobCookie;

	private object _updateJobLock = new object();

	private bool _thumbBarInitializationComplete;

	private static string s_playTooltip = Shell.LoadString(StringId.IDS_PLAY);

	private static string s_pauseTooltip = Shell.LoadString(StringId.IDS_PAUSE);

	private static string s_unratedTooltip = Shell.LoadString(StringId.IDS_NOWPLAYING_UNRATED_TOOLTIP);

	private static string s_loveItTooltip = Shell.LoadString(StringId.IDS_NOWPLAYING_LIKEIT_TOOLTIP);

	private static string s_hateItTooltip = Shell.LoadString(StringId.IDS_NOWPLAYING_DONTLIKEIT_TOOLTIP);

	public void Phase3Init()
	{
		TransportControls instance = SingletonModelItem<TransportControls>.Instance;
		((ModelItem)instance).PropertyChanged += OnTransportControlsPropertyChanged;
		((ModelItem)instance.Play).PropertyChanged += OnTransportControlsCommandPropertyChanged;
		((ModelItem)instance.Pause).PropertyChanged += OnTransportControlsCommandPropertyChanged;
		((ModelItem)instance.Back).PropertyChanged += OnTransportControlsCommandPropertyChanged;
		((ModelItem)instance.Forward).PropertyChanged += OnTransportControlsCommandPropertyChanged;
		string previousText = Shell.LoadString(StringId.IDS_PREVIOUS);
		string nextText = Shell.LoadString(StringId.IDS_NEXT);
		IntPtr windowHandle = Application.Window.Handle;
		ThreadPool.QueueUserWorkItem(delegate
		{
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			//IL_013f: Unknown result type (might be due to invalid IL or missing references)
			//IL_014a: Expected O, but got Unknown
			Win7ShellManager.Instance.BeginThumbBarSession(windowHandle, ref _thumbBar);
			Win7ShellManager.Instance.OnThumbBarButtonPress += new ThumbBarButtonPressHandler(OnThumbButtonPressed);
			ThumbBarButton val = default(ThumbBarButton);
			_thumbBar.CreateButton(ref val);
			val.UniqueID = 4u;
			val.IsHidden = true;
			_thumbBar.CreateButton(ref _previous);
			_previous.UniqueID = 1u;
			_previous.Tooltip = previousText;
			_thumbBar.CreateButton(ref _playPause);
			_playPause.UniqueID = 0u;
			_thumbBar.CreateButton(ref _next);
			_next.UniqueID = 2u;
			_next.Tooltip = nextText;
			_thumbBar.CreateButton(ref _rating);
			_rating.UniqueID = 3u;
			Application.DeferredInvoke((DeferredInvokeHandler)delegate
			{
				_thumbBarInitializationComplete = true;
				UpdateButtonStatus();
			}, (object)null);
		}, null);
	}

	private bool IsCurrentTrackRatable()
	{
		return SingletonModelItem<TransportControls>.Instance.CurrentTrack?.CanRate ?? false;
	}

	private void OnTransportControlsPropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		if (args.PropertyName == "CurrentTrackRating" || args.PropertyName == "Playing")
		{
			UpdateButtonStatus();
		}
	}

	private void OnTransportControlsCommandPropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		if (args.PropertyName == "Available")
		{
			UpdateButtonStatus();
		}
	}

	private void OnThumbButtonPressed(uint uniqueID)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			TransportControls instance = SingletonModelItem<TransportControls>.Instance;
			switch ((ThumbButtonID)uniqueID)
			{
			case ThumbButtonID.PlayPause:
				if (instance.Playing)
				{
					instance.Pause.Invoke();
				}
				else
				{
					instance.Play.Invoke();
				}
				break;
			case ThumbButtonID.Previous:
				instance.Back.Invoke();
				break;
			case ThumbButtonID.Next:
				instance.Forward.Invoke();
				break;
			case ThumbButtonID.Rating:
				if (IsCurrentTrackRatable())
				{
					PlaybackTrack currentTrack = instance.CurrentTrack;
					if (currentTrack.UserRating == 0)
					{
						currentTrack.UserRating = 8;
					}
					else if (currentTrack.UserRating <= 5)
					{
						currentTrack.UserRating = 0;
					}
					else
					{
						currentTrack.UserRating = 2;
					}
				}
				UpdateButtonStatus();
				break;
			}
		}, (object)null);
	}

	private void UpdateButtonStatus()
	{
		if (_thumbBarInitializationComplete)
		{
			TransportControls instance = SingletonModelItem<TransportControls>.Instance;
			UpdateThumbBarJob updateThumbBarJob = new UpdateThumbBarJob();
			updateThumbBarJob.Cookie = Interlocked.Increment(ref _lastUpdateJobCookie);
			updateThumbBarJob.IsPlaying = instance.Playing;
			updateThumbBarJob.CanPlay = instance.Play.Available;
			updateThumbBarJob.CanPause = instance.Pause.Available;
			updateThumbBarJob.CanGoBack = instance.Back.Available;
			updateThumbBarJob.CanGoForward = instance.Forward.Available;
			updateThumbBarJob.IsCurrentTrackRatable = IsCurrentTrackRatable();
			if (instance.CurrentTrack != null)
			{
				updateThumbBarJob.CurrentTrackRating = instance.CurrentTrack.UserRating;
			}
			ThreadPool.QueueUserWorkItem(UpdateButtonStatusWorker, updateThumbBarJob);
		}
	}

	private void UpdateButtonStatusWorker(object data)
	{
		if (!(data is UpdateThumbBarJob updateThumbBarJob))
		{
			return;
		}
		lock (_updateJobLock)
		{
			if (_lastUpdateJobCookie != updateThumbBarJob.Cookie)
			{
				return;
			}
			if (updateThumbBarJob.IsPlaying)
			{
				_playPause.IsEnabled = updateThumbBarJob.CanPause;
				_playPause.Tooltip = s_pauseTooltip;
				_playPause.Icon = (ThumbBarButtonIcons)2;
			}
			else
			{
				_playPause.IsEnabled = updateThumbBarJob.CanPlay;
				_playPause.Tooltip = s_playTooltip;
				_playPause.Icon = (ThumbBarButtonIcons)(!updateThumbBarJob.CanPlay);
			}
			_previous.IsEnabled = updateThumbBarJob.CanGoBack;
			_previous.Icon = (ThumbBarButtonIcons)(updateThumbBarJob.CanGoBack ? 3 : 4);
			_next.IsEnabled = updateThumbBarJob.CanGoForward;
			_next.Icon = (ThumbBarButtonIcons)(updateThumbBarJob.CanGoForward ? 5 : 6);
			if (!updateThumbBarJob.IsCurrentTrackRatable)
			{
				_rating.IsHidden = true;
			}
			else
			{
				_rating.IsHidden = false;
				if (updateThumbBarJob.CurrentTrackRating == 0)
				{
					_rating.Tooltip = s_unratedTooltip;
					_rating.Icon = (ThumbBarButtonIcons)9;
				}
				else if (updateThumbBarJob.CurrentTrackRating <= 5)
				{
					_rating.Tooltip = s_hateItTooltip;
					_rating.Icon = (ThumbBarButtonIcons)8;
				}
				else
				{
					_rating.Tooltip = s_loveItTooltip;
					_rating.Icon = (ThumbBarButtonIcons)7;
				}
			}
			_thumbBar.UpdateThumbBar();
		}
	}
}
