using System;
using Microsoft.Iris;
using UIXControls;

namespace ZuneUI;

public class ConfirmCloseDialog : DialogHelper
{
	private EventHandler _handler;

	internal static void Show(string ui, EventHandler handler)
	{
		ConfirmCloseDialog confirmCloseDialog = new ConfirmCloseDialog(ui, handler);
		((DialogHelper)confirmCloseDialog).Show();
	}

	public static void ShowDefault()
	{
		ShowDefault("res://ZuneShellResources!ConfirmClose.uix#ConfirmCloseContentUI");
	}

	public static void ShowDefault(string ui)
	{
		Show(ui, ForceCloseHandler);
	}

	private static void ForceCloseHandler(object sender, EventArgs args)
	{
		Application.Window.ForceClose();
	}

	private ConfirmCloseDialog(string ui, EventHandler handler)
		: base(ui)
	{
		_handler = handler;
	}

	public void Close()
	{
		_handler(this, null);
	}
}
