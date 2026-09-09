using Microsoft.Zune.ErrorMapperApi;
using UIXControls;

namespace ZuneUI;

public class ErrorDialogInfo : DialogHelper
{
	private int _hr;

	private int _hrOriginal;

	private string _title;

	private string _description;

	private string _webHelpUrl;

	private eErrorCondition _condition;

	public int HR => _hr;

	public int OriginalHR => _hrOriginal;

	public eErrorCondition ErrorCondition => _condition;

	public string Title => _title;

	public string Description => _description;

	public string WebHelpUrl => _webHelpUrl;

	internal static void Show(int hr, string title)
	{
		Show(hr, (eErrorCondition)0, title, null);
	}

	internal static void Show(int hr, eErrorCondition condition, string title)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		Show(hr, condition, title, null);
	}

	internal static void Show(int hr, string title, string description)
	{
		Show(hr, (eErrorCondition)0, title, description);
	}

	internal static void Show(int hr, eErrorCondition condition, string title, string description)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		ErrorDialogInfo errorDialogInfo = new ErrorDialogInfo(hr, condition, title, description);
		((DialogHelper)errorDialogInfo).Show();
	}

	private ErrorDialogInfo(int hr, eErrorCondition condition, string title, string description)
		: base("res://ZuneShellResources!ErrorDialog.uix#ErrorDialogContentUI")
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		_title = title;
		_hrOriginal = hr;
		_condition = condition;
		ErrorMapperResult mappedErrorDescriptionAndUrl = ErrorMapperApi.GetMappedErrorDescriptionAndUrl(_hrOriginal, _condition);
		_hr = mappedErrorDescriptionAndUrl.Hr;
		_description = description ?? mappedErrorDescriptionAndUrl.Description;
		_webHelpUrl = mappedErrorDescriptionAndUrl.WebHelpUrl;
	}
}
