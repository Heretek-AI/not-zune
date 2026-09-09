using Microsoft.Zune.Service;

namespace ZuneUI;

public class AccountManagementErrorState
{
	private bool _parentAccount;

	private ServiceError _serviceError;

	public bool ParentAccount => _parentAccount;

	public ServiceError ServiceError => _serviceError;

	internal AccountManagementErrorState(bool parentAccount, ServiceError serviceError)
	{
		_parentAccount = parentAccount;
		_serviceError = serviceError;
	}
}
