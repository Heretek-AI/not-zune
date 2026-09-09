using Microsoft.Zune.Playlist;

namespace ZuneUI;

public class StandardPlaylistFactory : PlaylistFactory
{
	private readonly string _baseUniqueTitle;

	private StandardPlaylistFactory(string baseUniqueTitle)
		: base(navigateOnCreate: false)
	{
		base.Ready = true;
		_baseUniqueTitle = baseUniqueTitle;
	}

	public static StandardPlaylistFactory CreateInstance(string baseUniqueTitle)
	{
		return new StandardPlaylistFactory(baseUniqueTitle);
	}

	public override string GetUniqueTitle()
	{
		return PlaylistManager.Instance.GetUniquePlaylistTitle(_baseUniqueTitle);
	}

	public override PlaylistResult CreatePlaylist(string title, CreatePlaylistOption option)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return PlaylistManager.Instance.CreatePlaylist(title, option);
	}
}
