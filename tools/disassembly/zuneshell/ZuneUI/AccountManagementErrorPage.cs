using Microsoft.Iris;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class AccountManagementErrorPage : AccountManagementStep
{
	private HRESULT hr = HRESULT._S_OK;

	private ServiceError serviceError;

	public override bool IsEnabled
	{
		get
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			int result;
			if (!((HRESULT)(ref hr)).IsError)
			{
				if (serviceError != null)
				{
					HRESULT rootError = serviceError.RootError;
					result = (((HRESULT)(ref rootError)).IsError ? 1 : 0);
				}
				else
				{
					result = 0;
				}
			}
			else
			{
				result = 1;
			}
			return (byte)result != 0;
		}
	}

	public HRESULT Hr
	{
		get
		{
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (serviceError != null)
			{
				HRESULT rootError = serviceError.RootError;
				if (((HRESULT)(ref rootError)).IsError)
				{
					return serviceError.RootError;
				}
			}
			return hr;
		}
	}

	public override string UI => "res://ZuneShellResources!AccountCreation.uix#AccountManagementErrorPage";

	public AccountManagementErrorPage(Wizard owner, string description, string detailedDescription)
		: base(owner, null, parentAccount: false)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		((ModelItem)this).Description = description;
		base.DetailDescription = detailedDescription;
	}

	internal override bool HandleError(HRESULT hr, AccountManagementErrorState errorState)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		bool result = false;
		if (((HRESULT)(ref hr)).IsError)
		{
			goto IL_002b;
		}
		if (errorState != null && errorState.ServiceError != null)
		{
			HRESULT rootError = errorState.ServiceError.RootError;
			if (((HRESULT)(ref rootError)).IsError)
			{
				goto IL_002b;
			}
		}
		goto IL_0043;
		IL_0043:
		return result;
		IL_002b:
		result = true;
		this.hr = hr;
		if (errorState != null)
		{
			serviceError = errorState.ServiceError;
		}
		goto IL_0043;
	}
}
