using Microsoft.Zune.Util;

namespace ZuneUI;

public class NowPlayingLand : PlaybackPage
{
	private static string LandBackgroundUI => "res://ZuneShellResources!NowPlayingLand.uix#NowPlayingBackground";

	public NowPlayingLand()
	{
		base.BackgroundUI = LandBackgroundUI;
		base.UIPath = "NowPlaying";
	}

	protected override void OnNavigatedAwayWorker(IPage destination)
	{
		base.ShouldHandleEscape = false;
		base.OnNavigatedAwayWorker(destination);
	}

	public static void NavigateToLand()
	{
		PlaybackTrack currentTrack = SingletonModelItem<TransportControls>.Instance.CurrentTrack;
		bool exitOnPlaybackStopped = false;
		if (currentTrack != null && currentTrack.IsVideo)
		{
			exitOnPlaybackStopped = true;
		}
		NavigateToLand(showMix: false, exitOnPlaybackStopped);
	}

	public static void NavigateToLand(bool showMix, bool exitOnPlaybackStopped)
	{
		NowPlayingLand nowPlayingLand = ZuneShell.DefaultInstance.CurrentPage as NowPlayingLand;
		if (nowPlayingLand == null)
		{
			NowPlayingLand nowPlayingLand2 = new NowPlayingLand();
			nowPlayingLand2.ShowMixOnEntry = showMix;
			nowPlayingLand2.ExitOnPlaybackStopped = exitOnPlaybackStopped;
			ZuneShell.DefaultInstance.NavigateToPage(nowPlayingLand2);
			SQMLog.Log((SQMDataId)139, 1);
		}
	}
}
