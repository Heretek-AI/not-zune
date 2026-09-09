using System;
using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public abstract class Experience : Command
{
	private bool _isCurrent;

	private Choice _nodes;

	private SQMDataId _sqmClickId;

	public Choice Nodes
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			if (_nodes == null)
			{
				_nodes = new Choice((IModelItemOwner)(object)this);
				_nodes.Options = NodesList;
			}
			return _nodes;
		}
		set
		{
			if (_nodes != value)
			{
				_nodes = value;
				((ModelItem)this).FirePropertyChanged("Nodes");
			}
		}
	}

	public abstract IList NodesList { get; }

	public Frame Frame => (Frame)(object)((ModelItem)this).Owner;

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

	public virtual string DefaultUIPath => "";

	public Experience(Frame frameOwner)
		: base((IModelItemOwner)(object)frameOwner, "", (EventHandler)null)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		_sqmClickId = (SQMDataId)0;
	}

	public Experience(Frame frameOwner, StringId nameId, SQMDataId SQMClickId)
		: base((IModelItemOwner)(object)frameOwner, Shell.LoadString(nameId), (EventHandler)null)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		_sqmClickId = SQMClickId;
	}

	protected int GetNodeIndex(Node node)
	{
		for (int i = 0; i < NodesList.Count; i++)
		{
			if (NodesList[i] == node)
			{
				return i;
			}
		}
		return -1;
	}

	protected virtual void OnIsCurrentChanged()
	{
	}

	protected override void OnInvoked()
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		if (!IsCurrent)
		{
			((Frame)(object)((ModelItem)this).Owner).Experiences.ChosenValue = this;
			Node node = (Node)Nodes.ChosenValue;
			if (node == null && NodesList != null && NodesList.Count > 0)
			{
				node = (Node)NodesList[0];
			}
			if (node != null)
			{
				((Command)node).Invoke((InvokePolicy)0);
			}
			if ((int)_sqmClickId != 0)
			{
				SQMLog.Log(_sqmClickId, 1);
			}
			((Command)this).OnInvoked();
		}
	}
}
