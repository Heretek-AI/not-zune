using System;
using System.ComponentModel;
using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class Node : Command
{
	private bool _isCurrent;

	private bool _pendingNavigation;

	private string _command;

	private SQMDataId _sqmCountID;

	public Experience Experience => (Experience)(object)((ModelItem)this).Owner;

	public bool IsCurrent
	{
		get
		{
			return _isCurrent;
		}
		set
		{
			if (_isCurrent != value)
			{
				_isCurrent = value;
				OnIsCurrentChanged();
				((ModelItem)this).FirePropertyChanged("IsCurrent");
			}
		}
	}

	public Node(Experience owner, string command, SQMDataId sqmCountID)
		: base((IModelItemOwner)(object)owner, (string)null, (EventHandler)null)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		_command = command;
		_sqmCountID = sqmCountID;
	}

	public Node(Experience owner, StringId id, string command, SQMDataId sqmCountID)
		: base((IModelItemOwner)(object)owner, Shell.LoadString(id), (EventHandler)null)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		_command = command;
		_sqmCountID = sqmCountID;
	}

	public Node(Experience owner, string name, string command)
		: base((IModelItemOwner)(object)owner, name, (EventHandler)null)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		_command = command;
		_sqmCountID = (SQMDataId)0;
	}

	protected virtual void OnIsCurrentChanged()
	{
	}

	protected override void OnInvoked()
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		Shell shell = (Shell)ZuneShell.DefaultInstance;
		if (shell.NavigationLocked)
		{
			shell.BlockedByNavigationLock = true;
			shell.DeferredNavigateNode = this;
			return;
		}
		bool flag = shell.CurrentNode == this;
		bool isRootPage = shell.CurrentPage.IsRootPage;
		if (!_pendingNavigation)
		{
			if (!flag || !isRootPage)
			{
				((ModelItem)shell).PropertyChanged += ShellPropertyChanged;
				Execute(shell);
				_pendingNavigation = true;
				if ((int)_sqmCountID != 0)
				{
					SQMLog.Log(_sqmCountID, 1);
				}
			}
			else
			{
				shell.CurrentPage.RefreshPage();
			}
		}
		((Command)this).OnInvoked();
	}

	protected virtual void Execute(Shell shell)
	{
		shell.Execute(_command, null);
	}

	private void ShellPropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		if (args.PropertyName == "CurrentPage")
		{
			_pendingNavigation = false;
			((ModelItem)(Shell)ZuneShell.DefaultInstance).PropertyChanged -= ShellPropertyChanged;
		}
	}
}
