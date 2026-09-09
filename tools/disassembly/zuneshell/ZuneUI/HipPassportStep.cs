using System.Collections.Generic;
using Microsoft.Iris;
using Microsoft.Zune.ErrorMapperApi;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class HipPassportStep : AccountManagementStep
{
	private bool _firstView;

	private WinLiveInformation _winLiveHip;

	private Dictionary<int, PropertyDescriptor> _errorMappings;

	public WinLiveInformation WinLiveHip
	{
		get
		{
			return _winLiveHip;
		}
		private set
		{
			if (_winLiveHip != value)
			{
				_winLiveHip = value;
				((ModelItem)this).FirePropertyChanged("WinLiveHip");
				((ModelItem)this).FirePropertyChanged("IsEnabled");
			}
		}
	}

	public override string UI => "res://ZuneShellResources!CreatePassport.uix#HipPassportStep";

	public override bool IsEnabled
	{
		get
		{
			bool flag = base.IsEnabled && !CreatePassportStep.CreatedPassport;
			if (flag)
			{
				flag = CreatePassportStep.IsEnabled && (_owner.CurrentPage != this || WinLiveHip != null);
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
				_errorMappings = new Dictionary<int, PropertyDescriptor>(1);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_HIP_SOLUTION_INVALID)).Int, null);
			}
			return _errorMappings;
		}
	}

	private CreatePassportStep CreatePassportStep
	{
		get
		{
			CreatePassportStep createPassportStep = null;
			if (base.ParentAccount)
			{
				return base.State.CreatePassportParentStep;
			}
			return base.State.CreatePassportStep;
		}
	}

	public HipPassportStep(Wizard owner, AccountManagementWizardState state, bool parentAccount)
		: base(owner, state, parentAccount)
	{
		_firstView = true;
		if (parentAccount)
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_PARENTAL_INPUT_HEAD);
		}
		else
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_PASSPORT_STEP);
		}
		HipPropertyEditor wizardPropertyEditor = new HipPropertyEditor();
		Initialize(wizardPropertyEditor);
	}

	public void Refresh()
	{
		if (IsEnabled)
		{
			OnActivate();
		}
	}

	protected override void OnActivate()
	{
		SetCommittedValue(HipPropertyEditor.HipCharacters, "");
		if (_firstView)
		{
			_firstView = false;
			WinLiveHip = CreatePassportStep.WinLiveInformation;
		}
		else
		{
			WinLiveHip = null;
		}
		base.ServiceActivationRequestsDone = WinLiveHip != null;
		if (!base.ServiceActivationRequestsDone)
		{
			StartActivationRequests(base.State.BasicAccountInfoStep.SelectedLocale);
		}
	}

	internal override ErrorMapperResult GetMappedErrorDescriptionAndUrl(HRESULT hr)
	{
		return ErrorMapperApi.GetMappedErrorDescriptionAndUrl(((HRESULT)(ref hr)).Int, (eErrorCondition)4);
	}

	protected override void OnStartActivationRequests(object state)
	{
		WinLiveInformation args = ObtainWinLiveHip(state as string);
		EndActivationRequests(args);
	}

	protected override void OnEndActivationRequests(object args)
	{
		if (args == null)
		{
			NavigateToErrorHandler();
		}
		else
		{
			WinLiveHip = (WinLiveInformation)((args is WinLiveInformation) ? args : null);
		}
	}

	private WinLiveInformation ObtainWinLiveHip(string local)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		WinLiveInformation result = null;
		ServiceError serviceError = null;
		HRESULT information = base.State.WinLiveSignup.GetInformation(local, (EHipType)0, ref result, ref serviceError);
		if (((HRESULT)(ref information)).IsError)
		{
			SetError(information, serviceError);
		}
		return result;
	}
}
