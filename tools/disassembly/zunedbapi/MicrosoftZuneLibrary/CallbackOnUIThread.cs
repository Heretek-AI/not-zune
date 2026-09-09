using System;
using Microsoft.Iris;

namespace MicrosoftZuneLibrary;

public class CallbackOnUIThread : IRequestCallbackOnUIThread
{
	private CallbackOnUIThreadBimodalManaged_DONOTUSE bimodalHelper;

	public CallbackOnUIThread()
	{
		bimodalHelper = new CallbackOnUIThreadBimodalManaged_DONOTUSE(this);
	}

	public virtual void CallbackOnUIThreadRequest(CallbackPriorityManaged priority, int id, IntPtr pData, IntPtr pInterface)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		CallbackOnUIThreadPack callbackOnUIThreadPack = new CallbackOnUIThreadPack(id, pData, pInterface);
		DeferredInvokePriority val = (DeferredInvokePriority)((priority != CallbackPriorityManaged.eCallbackPriorityNormal) ? 1 : 0);
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredCallback), (object)callbackOnUIThreadPack, (DeferredInvokePriority)(int)val);
	}

	private void DeferredCallback(object args)
	{
		CallbackOnUIThreadPack callbackOnUIThreadPack = (CallbackOnUIThreadPack)args;
		bimodalHelper.Callback(callbackOnUIThreadPack._id, callbackOnUIThreadPack._pData, callbackOnUIThreadPack._pInterface);
	}
}
