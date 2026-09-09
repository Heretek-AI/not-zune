using System;
using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class NotifyChoice : Choice
{
	private InvokePolicy _invokePolicy;

	public NotifyChoice()
		: this(null)
	{
	}

	public NotifyChoice(IModelItemOwner owner)
		: this(owner, (InvokePolicy)0)
	{
	}

	public NotifyChoice(IModelItemOwner owner, InvokePolicy invokePolicy)
		: base(owner)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		_invokePolicy = invokePolicy;
	}

	protected override void OnChosenChanged()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		if (((Choice)this).ChosenValue != null)
		{
			Command val = (Command)((Choice)this).ChosenValue;
			val.Invoke(_invokePolicy);
		}
	}

	protected override void ValidateOptionsListWorker(IList potentialOptions)
	{
		((Choice)this).ValidateOptionsListWorker(potentialOptions);
		if (potentialOptions == null)
		{
			return;
		}
		foreach (object potentialOption in potentialOptions)
		{
			if (!(potentialOption is Command))
			{
				throw new ArgumentException("Contents must be of type Command");
			}
		}
	}
}
