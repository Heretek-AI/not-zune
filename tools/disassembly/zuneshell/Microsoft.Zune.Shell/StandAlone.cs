using System;
using System.Collections;
using Microsoft.Iris;
using Microsoft.Win32;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Util;
using ZuneUI;

namespace Microsoft.Zune.Shell;

internal class StandAlone
{
	public static Hashtable Startup(string[] args, string defaultCommandLineSwitch)
	{
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Expected O, but got Unknown
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		WindowSize initialClientSize = default(WindowSize);
		((WindowSize)(ref initialClientSize))._002Ector(1012, 693);
		string text = null;
		bool showWindowFrame = false;
		bool flag = false;
		Hashtable hashtable = new Hashtable();
		bool flag2 = false;
		if (args != null)
		{
			CommandLineArgument[] array = CommandLineArgument.ParseArgs(args, defaultCommandLineSwitch);
			CommandLineArgument[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				CommandLineArgument commandLineArgument = array2[i];
				switch (commandLineArgument.Name)
				{
				case "gdi":
					Application.RenderingType = (RenderingType)0;
					flag2 = true;
					break;
				case "switchtogdi":
					ClientConfiguration.GeneralSettings.RenderingType = 0;
					break;
				case "dx9":
					Application.RenderingType = (RenderingType)1;
					flag2 = true;
					break;
				case "size":
					try
					{
						initialClientSize = ParseSize(commandLineArgument.Value);
					}
					catch (FormatException)
					{
					}
					catch (ArgumentException)
					{
					}
					break;
				case "minimized":
					flag = true;
					break;
				case "nativeframe":
					showWindowFrame = true;
					break;
				case "animations":
					try
					{
						Application.AnimationsEnabled = bool.Parse(commandLineArgument.Value);
					}
					catch (FormatException)
					{
					}
					break;
				default:
					hashtable[commandLineArgument.Name] = commandLineArgument.Value;
					break;
				}
			}
		}
		if (ClientConfiguration.GeneralSettings.UseGDI)
		{
			ClientConfiguration.GeneralSettings.RenderingType = 0;
			ClientConfiguration.GeneralSettings.UseGDI = false;
		}
		if (!flag2)
		{
			Application.RenderingType = (RenderingType)ClientConfiguration.GeneralSettings.RenderingType;
			Application.RenderingQuality = (RenderingQuality)ClientConfiguration.GeneralSettings.RenderingQuality;
		}
		switch (text)
		{
		case "ltr":
			Application.IsRTL = false;
			break;
		case "rtl":
			Application.IsRTL = true;
			break;
		}
		Application.AnimationsEnabled = ClientConfiguration.GeneralSettings.AnimationsEnabled;
		Application.Initialize();
		Application.Window.InitialClientSize = initialClientSize;
		object value = Registry.GetValue(ZuneUI.Shell.SettingsRegistryPath, "WindowPosition", null);
		if (value != null && value is string)
		{
			try
			{
				if (!flag)
				{
					Application.Window.SetSavedInitialPosition((string)value);
				}
				else
				{
					Application.Window.SetSavedInitialPosition((string)value, (WindowState)1);
				}
			}
			catch (ArgumentException)
			{
			}
		}
		Application.Window.RespectsStartupSettings = true;
		Application.Window.InitialPositionPolicy = (WindowPositionPolicy)3;
		Application.Window.ShowWindowFrame = showWindowFrame;
		Application.Window.SetBackgroundColor(ZuneUI.Shell.WindowColorFromRGB(ClientConfiguration.Shell.BackgroundColor));
		if (!flag)
		{
			Application.DeferredInvoke((DeferredInvokeHandler)delegate
			{
				Windowing.ForceSetForegroundWindow(Application.Window.Handle);
			}, (DeferredInvokePriority)1);
		}
		return hashtable;
	}

	private static WindowSize ParseSize(string argument)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		int num2 = 0;
		string[] array = argument.Split(new char[1] { ',' });
		if (array.Length == 2)
		{
			num = int.Parse(array[0]);
			num2 = int.Parse(array[1]);
		}
		return new WindowSize(num, num2);
	}

	public static void Run(DeferredInvokeHandler initialLoadComplete)
	{
		Application.Run(initialLoadComplete);
		if (!TaskbarPlayer.Instance.ToolbarVisible)
		{
			Registry.SetValue(ZuneUI.Shell.SettingsRegistryPath, "WindowPosition", Application.Window.GetSavedPosition(true));
		}
	}

	public static void Shutdown()
	{
		Application.Shutdown();
	}
}
