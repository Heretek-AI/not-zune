using System.Collections.Generic;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Zune.ErrorMapperApi;
using Microsoft.Zune.Service;

namespace ZuneUI;

public abstract class AccountManagementStep : WizardPropertyEditorPage
{
	private AccountManagementWizardState _state;

	private bool _parentAccount;

	private bool _serviceActivationRequestsDone;

	private bool _serviceDeactivationRequestsDone;

	private bool _serviceRequestsWorking;

	private bool _requireSignIn;

	private string _nextTextOverride;

	private string _finishTextOverride;

	public override bool IsEnabled => (!RequireSignIn || SignIn.Instance.SignedIn) && (_owner.CurrentPage != this || !ServiceRequestWorking);

	protected bool RequireSignIn
	{
		get
		{
			return _requireSignIn;
		}
		set
		{
			if (_requireSignIn != value)
			{
				_requireSignIn = value;
				((ModelItem)this).FirePropertyChanged("RequireSignIn");
				((ModelItem)this).FirePropertyChanged("IsEnabled");
			}
		}
	}

	public AccountManagementWizardState State => _state;

	public string NextTextOverride
	{
		get
		{
			return _nextTextOverride;
		}
		set
		{
			if (_nextTextOverride != value)
			{
				_nextTextOverride = value;
				((ModelItem)this).FirePropertyChanged("NextTextOverride");
			}
		}
	}

	public string FinishTextOverride
	{
		get
		{
			return _finishTextOverride;
		}
		set
		{
			if (_finishTextOverride != value)
			{
				_finishTextOverride = value;
				((ModelItem)this).FirePropertyChanged("FinishTextOverride");
			}
		}
	}

	public bool ServiceActivationRequestsDone
	{
		get
		{
			return _serviceActivationRequestsDone;
		}
		protected set
		{
			if (_serviceActivationRequestsDone != value)
			{
				_serviceActivationRequestsDone = value;
				((ModelItem)this).FirePropertyChanged("ServiceDeactivationRequestsDone");
			}
		}
	}

	public bool ServiceDeactivationRequestsDone
	{
		get
		{
			return _serviceDeactivationRequestsDone;
		}
		protected set
		{
			if (_serviceDeactivationRequestsDone != value)
			{
				_serviceDeactivationRequestsDone = value;
				((ModelItem)this).FirePropertyChanged("ServiceDeactivationRequestsDone");
			}
		}
	}

	public bool ServiceRequestWorking
	{
		get
		{
			return _serviceRequestsWorking;
		}
		private set
		{
			if (_serviceRequestsWorking != value)
			{
				_serviceRequestsWorking = value;
				((ModelItem)this).FirePropertyChanged("ServiceRequestWorking");
				if (_owner.CurrentPage == this)
				{
					((ModelItem)this).FirePropertyChanged("IsEnabled");
				}
			}
		}
	}

	public bool ParentAccount => _parentAccount;

	internal virtual Dictionary<int, PropertyDescriptor> ErrorPropertyMappings => null;

	public AccountManagementStep(Wizard owner, AccountManagementWizardState state, bool parentAccount)
		: base(owner)
	{
		_parentAccount = parentAccount;
		_state = state;
		_serviceActivationRequestsDone = true;
		_serviceDeactivationRequestsDone = true;
		_serviceRequestsWorking = false;
		base.EnableVerticalScrolling = true;
	}

	internal virtual ErrorMapperResult GetMappedErrorDescriptionAndUrl(HRESULT hr)
	{
		return ErrorMapperApi.GetMappedErrorDescriptionAndUrl(((HRESULT)(ref hr)).Int, (eErrorCondition)0);
	}

	internal void SetError(HRESULT hr, ServiceError serviceError)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		_owner.SetError(hr, new AccountManagementErrorState(ParentAccount, serviceError));
	}

	internal bool HandleError()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		HRESULT error = _owner.Error;
		AccountManagementErrorState accountManagementErrorState = null;
		if (_owner is AccountManagementWizard)
		{
			accountManagementErrorState = ((AccountManagementWizard)_owner).LastErrorState;
		}
		bool flag = ((HRESULT)(ref error)).IsSuccess && accountManagementErrorState == null;
		if (!flag)
		{
			flag = HandleError(error, accountManagementErrorState);
			if (flag)
			{
				_owner.ResetError();
			}
		}
		return flag;
	}

	internal virtual bool HandleError(HRESULT hr, AccountManagementErrorState errorState)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		bool flag = ((HRESULT)(ref hr)).IsSuccess && errorState == null;
		bool flag2 = (errorState == null || errorState.ParentAccount == ParentAccount) && IsEnabled;
		if (!flag && flag2)
		{
			if (SetExternalPropertyError(hr, null))
			{
				flag = true;
			}
			if (errorState != null && errorState.ServiceError != null)
			{
				if (SetExternalPropertyError(errorState.ServiceError.RootError, null))
				{
					flag = true;
				}
				if (errorState.ServiceError.PropertyErrors != null)
				{
					foreach (PropertyError propertyError in errorState.ServiceError.PropertyErrors)
					{
						if (SetExternalPropertyError(propertyError.Hr, propertyError.Name))
						{
							flag = true;
						}
					}
				}
			}
			if (flag)
			{
				ShowGenericErrorStatus();
				ShowValidation();
			}
		}
		return flag;
	}

	internal bool NavigateToErrorHandler()
	{
		bool result = false;
		if (_owner is AccountManagementWizard)
		{
			((AccountManagementWizard)_owner).NavigateToErrorHandler();
			result = true;
		}
		return result;
	}

	internal sealed override void Activate()
	{
		base.Activate();
		if (HandleError() || !NavigateToErrorHandler())
		{
			OnActivate();
		}
	}

	internal override bool OnMovingNext()
	{
		if (ServiceDeactivationRequestsDone)
		{
			base.StatusMessage = null;
			return base.OnMovingNext();
		}
		StartDeactivationRequests(null);
		return false;
	}

	protected bool SetExternalPropertyError(HRESULT hr, string propertyName)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		string statusMessage = null;
		bool flag = false;
		if (((HRESULT)(ref hr)).IsError && ErrorPropertyMappings != null)
		{
			ErrorMapperResult mappedErrorDescriptionAndUrl = GetMappedErrorDescriptionAndUrl(hr);
			if (mappedErrorDescriptionAndUrl != null && ErrorPropertyMappings.ContainsKey(mappedErrorDescriptionAndUrl.Hr))
			{
				flag = true;
				PropertyDescriptor propertyDescriptor = ErrorPropertyMappings[mappedErrorDescriptionAndUrl.Hr];
				if (propertyDescriptor == null || !SetExternalError(propertyDescriptor, new HRESULT(mappedErrorDescriptionAndUrl.Hr)))
				{
					statusMessage = mappedErrorDescriptionAndUrl.Description;
				}
			}
			else if (!string.IsNullOrEmpty(propertyName) && mappedErrorDescriptionAndUrl != null)
			{
				flag = SetExternalError(propertyName, new HRESULT(mappedErrorDescriptionAndUrl.Hr));
			}
		}
		if (flag)
		{
			base.StatusMessage = statusMessage;
		}
		return flag;
	}

	protected virtual void OnActivate()
	{
		if (!ServiceActivationRequestsDone)
		{
			StartActivationRequests(null);
		}
	}

	protected void StartActivationRequests(object state)
	{
		if (!ServiceRequestWorking)
		{
			ServiceRequestWorking = true;
			ThreadPool.QueueUserWorkItem(AsyncStartActivationRequests, state);
		}
	}

	private void AsyncStartActivationRequests(object state)
	{
		OnStartActivationRequests(state);
	}

	protected virtual void OnStartActivationRequests(object state)
	{
		EndActivationRequests(null);
	}

	protected void EndActivationRequests(object args)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredEndActivationRequests), args);
	}

	private void DeferredEndActivationRequests(object args)
	{
		OnEndActivationRequests(args);
		ServiceActivationRequestsDone = true;
		ServiceRequestWorking = false;
	}

	protected virtual void OnEndActivationRequests(object args)
	{
	}

	protected void StartDeactivationRequests(object state)
	{
		if (!ServiceRequestWorking)
		{
			base.StatusMessage = null;
			ServiceRequestWorking = true;
			ThreadPool.QueueUserWorkItem(AsyncStartDeactivationRequests, state);
		}
	}

	private void AsyncStartDeactivationRequests(object state)
	{
		OnStartDeactivationRequests(state);
	}

	protected virtual void OnStartDeactivationRequests(object state)
	{
		EndDeactivationRequests(null);
	}

	protected void EndDeactivationRequests(object args)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DerferredEndDeactivationRequests), args);
	}

	private void DerferredEndDeactivationRequests(object args)
	{
		ServiceDeactivationRequestsDone = true;
		ServiceRequestWorking = false;
		OnEndDeactivationRequests(args);
		_owner.MoveNext();
	}

	protected virtual void OnEndDeactivationRequests(object args)
	{
	}
}
