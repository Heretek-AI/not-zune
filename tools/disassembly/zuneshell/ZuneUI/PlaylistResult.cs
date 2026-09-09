namespace ZuneUI;

public class PlaylistResult
{
	private PlaylistError _error;

	private int _playlistId;

	private HRESULT _hr;

	public PlaylistError Error => _error;

	public int PlaylistId => _playlistId;

	public HRESULT HR => _hr;

	internal PlaylistResult(int playlistId, HRESULT hr)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		_hr = hr;
		if (((HRESULT)(ref hr)).IsSuccess && playlistId > 0)
		{
			_playlistId = playlistId;
			return;
		}
		_playlistId = PlaylistManager.InvalidPlaylistId;
		if (HRESULT.op_Implicit(((HRESULT)(ref hr)).Int) == HRESULT._DB_E_RESOURCEEXISTS)
		{
			_error = PlaylistError.NameExists;
		}
		else if (HRESULT.op_Implicit(((HRESULT)(ref hr)).Int) == HRESULT._DB_E_BADPARAMETERNAME)
		{
			_error = PlaylistError.InvalidName;
		}
		else
		{
			_error = PlaylistError.Other;
		}
	}

	public PlaylistResult(int playlistId, PlaylistError error)
	{
		_playlistId = playlistId;
		_error = error;
	}
}
