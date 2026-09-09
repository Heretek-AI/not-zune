using System.Collections;
using System.Collections.Generic;
using Microsoft.Iris;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class ZuneTagStep : AccountManagementStep
{
	private struct ServiceData
	{
		public string ZuneTag;

		public string CountryCode;
	}

	private IList _tagSuggestions;

	private Dictionary<int, PropertyDescriptor> _errorMappings;

	public override string UI => "res://ZuneShellResources!AccountInfo.uix#ZuneTagStep";

	public IList TagSuggestions
	{
		get
		{
			return _tagSuggestions;
		}
		private set
		{
			if (_tagSuggestions != value)
			{
				_tagSuggestions = value;
				((ModelItem)this).FirePropertyChanged("TagSuggestions");
			}
		}
	}

	public string EmailAddress
	{
		get
		{
			if (base.State != null)
			{
				return base.State.GetEmailAddress();
			}
			return string.Empty;
		}
	}

	public override bool IsEnabled
	{
		get
		{
			bool flag = base.IsEnabled;
			if (flag)
			{
				flag = base.State.PassportPasswordStep.CanCreateAccount;
			}
			return flag;
		}
	}

	internal override Dictionary<int, PropertyDescriptor> ErrorPropertyMappings
	{
		get
		{
			if (_errorMappings == null)
			{
				_errorMappings = new Dictionary<int, PropertyDescriptor>(3);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZEST_E_ACCOUNT_ZUNETAG_OCCUPIED)).Int, null);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZEST_E_INVALID_ARG_ZUNETAG)).Int, null);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZEST_E_ZUNETAG_OFFENSIVE)).Int, null);
			}
			return _errorMappings;
		}
	}

	public ZuneTagStep(Wizard owner, AccountManagementWizardState state, bool parentAccount)
		: base(owner, state, parentAccount)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_ZUNE_TAG_HEADER);
		ZuneTagPropertyEditor wizardPropertyEditor = new ZuneTagPropertyEditor();
		Initialize(wizardPropertyEditor);
	}

	protected override void OnActivate()
	{
		base.ServiceDeactivationRequestsDone = false;
		base.OnActivate();
	}

	internal override bool OnMovingNext()
	{
		string text = GetUncommittedValue(ZuneTagPropertyEditor.ZuneTag) as string;
		string text2 = GetCommittedValue(ZuneTagPropertyEditor.ZuneTag) as string;
		if (text == text2 || (TagSuggestions != null && TagSuggestions.Contains(text)))
		{
			TagSuggestions = null;
			base.ServiceDeactivationRequestsDone = true;
		}
		if (base.ServiceDeactivationRequestsDone)
		{
			if (TagSuggestions == null || TagSuggestions.Count == 0)
			{
				return base.OnMovingNext();
			}
			base.ServiceDeactivationRequestsDone = false;
			return false;
		}
		ServiceData serviceData = default(ServiceData);
		serviceData.ZuneTag = text;
		serviceData.CountryCode = base.State.BasicAccountInfoStep.SelectedCountry;
		StartDeactivationRequests(serviceData);
		return false;
	}

	protected override void OnStartDeactivationRequests(object state)
	{
		IList args = ValidateUniqueZuneTag((ServiceData)state);
		EndDeactivationRequests(args);
	}

	protected override void OnEndDeactivationRequests(object args)
	{
		TagSuggestions = args as IList;
	}

	private IList ValidateUniqueZuneTag(ServiceData serviceData)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		ServiceError serviceError = null;
		IList result = null;
		HRESULT val = base.State.AccountManagement.ReserveZuneTag(serviceData.ZuneTag, serviceData.CountryCode, ref result, ref serviceError);
		if (((HRESULT)(ref val)).IsError && val != HRESULT._ZEST_E_ACCOUNT_ZUNETAG_OCCUPIED)
		{
			SetError(val, serviceError);
		}
		return result;
	}
}
