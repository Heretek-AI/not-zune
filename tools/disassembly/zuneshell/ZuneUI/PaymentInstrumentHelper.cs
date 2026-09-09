using System;
using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;

namespace ZuneUI;

public class PaymentInstrumentHelper : ModelItem
{
	private CreditCardCollection m_creditCards;

	private CreditCard m_newCreditCard;

	private PaymentInstrument m_defaultPaymentInstrument;

	private HRESULT m_errorCode;

	public IList CreditCards
	{
		get
		{
			if (m_creditCards == null)
			{
				return null;
			}
			return m_creditCards.Items;
		}
	}

	public PaymentInstrument Default
	{
		get
		{
			return m_defaultPaymentInstrument;
		}
		private set
		{
			if (m_defaultPaymentInstrument != value)
			{
				m_defaultPaymentInstrument = value;
				((ModelItem)this).FirePropertyChanged("Default");
			}
		}
	}

	public CreditCard NewCreditCard
	{
		get
		{
			return m_newCreditCard;
		}
		private set
		{
			if (m_newCreditCard != value)
			{
				m_newCreditCard = value;
				((ModelItem)this).FirePropertyChanged("NewCreditCard");
			}
		}
	}

	public HRESULT ErrorCode
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return m_errorCode;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if (m_errorCode != value)
			{
				m_errorCode = value;
				((ModelItem)this).FirePropertyChanged("ErrorCode");
			}
		}
	}

	public event EventHandler GetPaymentInstrumentsCompleted;

	public void GetPaymentInstruments()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		//IL_003b: Expected O, but got Unknown
		m_defaultPaymentInstrument = null;
		m_creditCards = null;
		ErrorCode = HRESULT._S_OK;
		ZuneApplication.Service.GetPaymentInstruments(new GetPaymentInstrumentsCompleteCallback(OnGetPaymentInstrumentsSuccess), new GetPaymentInstrumentsErrorCallback(OnGetPaymentInstrumnetsError));
	}

	public void AddPaymentInstrument(PaymentInstrument paymentInstrument)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_002e: Expected O, but got Unknown
		ErrorCode = HRESULT._S_OK;
		ZuneApplication.Service.AddPaymentInstrument(paymentInstrument, new AddPaymentInstrumentCompleteCallback(OnAddPaymentInstrumentSuccess), new AddPaymentInstrumentErrorCallback(OnAddPaymentInstrumnetError));
	}

	private void SetError(HRESULT hrError)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		ErrorCode = hrError;
	}

	private void SetCreditCards(CreditCardCollection creditCards)
	{
		if (((ModelItem)this).IsDisposed)
		{
			if (creditCards != null)
			{
				creditCards.Dispose();
			}
			return;
		}
		if (m_creditCards != null)
		{
			m_creditCards.Dispose();
		}
		m_creditCards = creditCards;
		((ModelItem)this).FirePropertyChanged("CreditCards");
		CalculateDefault();
	}

	private void CalculateDefault()
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Expected O, but got Unknown
		CreditCard val = null;
		if (NewCreditCard != null)
		{
			val = NewCreditCard;
		}
		else if (CreditCards != null && CreditCards.Count > 0)
		{
			DateTime now = DateTime.Now;
			foreach (CreditCard creditCard in CreditCards)
			{
				CreditCard val2 = creditCard;
				if (val2.ExpirationDate.Year > now.Year || (val2.ExpirationDate.Year == now.Year && val2.ExpirationDate.Month >= now.Month))
				{
					val = val2;
				}
			}
		}
		Default = (PaymentInstrument)(object)val;
	}

	private void OnGetPaymentInstrumentsSuccess(CreditCardCollection creditCards)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredGetPaymentInstrumentsSuccess), (object)creditCards);
	}

	private void OnGetPaymentInstrumnetsError(HRESULT hrError)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredGetPaymentInstrumentsError), (object)hrError);
	}

	private void OnAddPaymentInstrumentSuccess(PaymentInstrument paymentInstrument)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredAddPaymentInstrumentSuccess), (object)paymentInstrument);
	}

	private void OnAddPaymentInstrumnetError(HRESULT hrError)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredAddPaymentInstrumentError), (object)hrError);
	}

	private void DeferredGetPaymentInstrumentsSuccess(object args)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		SetCreditCards((CreditCardCollection)args);
		OnGetPaymentInstrumentsCompleted();
	}

	private void DeferredGetPaymentInstrumentsError(object args)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		SetError((HRESULT)args);
		OnGetPaymentInstrumentsCompleted();
	}

	private void DeferredAddPaymentInstrumentSuccess(object args)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		NewCreditCard = (CreditCard)args;
		CalculateDefault();
	}

	private void DeferredAddPaymentInstrumentError(object args)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		SetError((HRESULT)args);
	}

	protected override void OnDispose(bool disposing)
	{
		((ModelItem)this).OnDispose(disposing);
		if (disposing && m_creditCards != null)
		{
			m_creditCards.Dispose();
			m_creditCards = null;
		}
	}

	private void OnGetPaymentInstrumentsCompleted()
	{
		if (this.GetPaymentInstrumentsCompleted != null)
		{
			this.GetPaymentInstrumentsCompleted(this, null);
		}
		((ModelItem)this).FirePropertyChanged("GetPaymentInstrumentsCompleted");
	}
}
