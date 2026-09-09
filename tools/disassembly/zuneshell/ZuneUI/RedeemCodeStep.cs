using System;
using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class RedeemCodeStep : AccountManagementStep
{
	private class ServiceData
	{
		public TokenDetails TokenDetails;

		public EClientTypeFlags TokenClientTypes;

		public bool TokenInvalid;

		public bool TokenExpired;

		public AlbumOfferCollection AlbumOffers;

		public TrackOfferCollection TrackOffers;

		public VideoOfferCollection VideoOffers;

		public AppOfferCollection AppOffers;
	}

	private TokenDetails _tokenDetails;

	private TokenDetails _confirmedTokenDetails;

	private EClientTypeFlags _tokenClientTypes;

	private bool _tokenExpired;

	private bool _tokenInvalid;

	private Command _redeemMediaCommand;

	private AlbumOfferCollection _albumOffers;

	private TrackOfferCollection _trackOffers;

	private VideoOfferCollection _videoOffers;

	private AppOfferCollection _appOffers;

	public override string UI => "res://ZuneShellResources!AccountInfo.uix#RedeemCodeStep";

	public TokenDetails TokenDetails
	{
		get
		{
			return _tokenDetails;
		}
		private set
		{
			if (_tokenDetails != value)
			{
				_tokenDetails = value;
				((ModelItem)this).FirePropertyChanged("TokenDetails");
				((ModelItem)this).FirePropertyChanged("TokenSupported");
				((ModelItem)this).FirePropertyChanged("MatchingBillingOffer");
			}
		}
	}

	public TokenDetails ConfirmedTokenDetails
	{
		get
		{
			return _confirmedTokenDetails;
		}
		private set
		{
			if (_confirmedTokenDetails != value)
			{
				_confirmedTokenDetails = value;
				((ModelItem)this).FirePropertyChanged("ConfirmedTokenDetails");
			}
		}
	}

	public bool TokenSupported
	{
		get
		{
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Expected I4, but got Unknown
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Invalid comparison between Unknown and I4
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Invalid comparison between Unknown and I4
			if (_tokenDetails != null)
			{
				ETokenType tokenType = _tokenDetails.TokenType;
				switch ((int)tokenType)
				{
				case 1:
				case 2:
					return true;
				case 0:
					if (TokenClientTypesSupported)
					{
						if ((int)_tokenDetails.PurchaseOfferType != 3)
						{
							return (int)_tokenDetails.PurchaseOfferType == 5;
						}
						return true;
					}
					return false;
				}
			}
			return false;
		}
	}

	public EClientTypeFlags TokenClientTypes
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _tokenClientTypes;
		}
		private set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			if (_tokenClientTypes != value)
			{
				_tokenClientTypes = value;
				((ModelItem)this).FirePropertyChanged("TokenClientTypes");
			}
		}
	}

	private bool TokenClientTypesSupported
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Invalid comparison between Unknown and I4
			if ((int)_tokenClientTypes != 0)
			{
				return (_tokenClientTypes & 1) == 1;
			}
			return true;
		}
	}

	public bool TokenInvalid
	{
		get
		{
			return _tokenInvalid;
		}
		private set
		{
			if (_tokenInvalid != value)
			{
				_tokenInvalid = value;
				((ModelItem)this).FirePropertyChanged("TokenInvalid");
			}
		}
	}

	public bool TokenExpired
	{
		get
		{
			return _tokenExpired;
		}
		private set
		{
			if (_tokenExpired != value)
			{
				_tokenExpired = value;
				((ModelItem)this).FirePropertyChanged("TokenExpired");
			}
		}
	}

	public BillingOffer MatchingBillingOffer
	{
		get
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Invalid comparison between Unknown and I4
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Invalid comparison between Unknown and I4
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Expected O, but got Unknown
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Expected O, but got Unknown
			BillingOffer result = null;
			if (TokenDetails != null)
			{
				if ((int)TokenDetails.TokenType == 1)
				{
					result = new BillingOffer(TokenDetails.BillingOfferId, (EBillingOfferType)8, TokenDetails.OfferName);
				}
				else if ((int)TokenDetails.TokenType == 2)
				{
					result = new BillingOffer(TokenDetails.BillingOfferId, (EBillingOfferType)1, TokenDetails.OfferName);
				}
			}
			return result;
		}
	}

	public AlbumOfferCollection AlbumOffers
	{
		get
		{
			return _albumOffers;
		}
		private set
		{
			if (_albumOffers != value)
			{
				_albumOffers = value;
				((ModelItem)this).FirePropertyChanged("AlbumOffers");
			}
		}
	}

	public TrackOfferCollection TrackOffers
	{
		get
		{
			return _trackOffers;
		}
		private set
		{
			if (_trackOffers != value)
			{
				_trackOffers = value;
				((ModelItem)this).FirePropertyChanged("TrackOffers");
			}
		}
	}

	public VideoOfferCollection VideoOffers
	{
		get
		{
			return _videoOffers;
		}
		private set
		{
			if (_videoOffers != value)
			{
				_videoOffers = value;
				((ModelItem)this).FirePropertyChanged("VideoOffers");
			}
		}
	}

	public AppOfferCollection AppOffers
	{
		get
		{
			return _appOffers;
		}
		private set
		{
			if (_appOffers != value)
			{
				_appOffers = value;
				((ModelItem)this).FirePropertyChanged("AppOffers");
			}
		}
	}

	public Command RedeemMediaCommand => _redeemMediaCommand;

	public string MediaTitle
	{
		get
		{
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			if (_videoOffers != null && _videoOffers.Items.Count > 0)
			{
				return ((Offer)(VideoOffer)_videoOffers.Items[0]).Title;
			}
			return null;
		}
	}

	public bool IsMediaRental
	{
		get
		{
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			if (_videoOffers != null && VideoOffers.Items.Count > 0)
			{
				return ((VideoOffer)VideoOffers.Items[0]).IsRental;
			}
			return false;
		}
	}

	public bool IsMediaSD
	{
		get
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Expected O, but got Unknown
			if (_videoOffers != null)
			{
				foreach (VideoOffer item in _videoOffers.Items)
				{
					VideoOffer val = item;
					if (!val.IsHD)
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	public bool IsMediaHD
	{
		get
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Expected O, but got Unknown
			if (_videoOffers != null)
			{
				foreach (VideoOffer item in _videoOffers.Items)
				{
					VideoOffer val = item;
					if (val.IsHD)
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	public RedeemCodeStep(Wizard owner, AccountManagementWizardState state)
		: base(owner, state, parentAccount: false)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_BILLING_PREPAID_CODE_TITLE);
		base.DetailDescription = Shell.LoadString(StringId.IDS_BILLING_PREPAID_CODE_HEADER);
		WizardPropertyEditor wizardPropertyEditor = new RedeemCodePropertyEditor();
		_redeemMediaCommand = new Command();
		Initialize(wizardPropertyEditor);
	}

	public string CreateRedeemCodeUrl(bool returnArguments)
	{
		string endPointUri = Service.GetEndPointUri((EServiceEndpointId)1);
		string urlPath = endPointUri + "/client/RedeemCode.ashx";
		if (returnArguments)
		{
			string text = Service.GetEndPointUri((EServiceEndpointId)12) + "/social/articles/backtosoftware.htm";
			return UrlHelper.MakeUrl(endPointUri + "/client/RedeemCode.ashx", "ru", text, "aru", text);
		}
		return UrlHelper.MakeUrl(urlPath);
	}

	protected override void OnActivate()
	{
		base.OnActivate();
		base.ServiceDeactivationRequestsDone = TokenDetails != null;
	}

	internal override bool OnMovingNext()
	{
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Invalid comparison between Unknown and I4
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Invalid comparison between Unknown and I4
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Invalid comparison between Unknown and I4
		string text = GetUncommittedValue(RedeemCodePropertyEditor.Code) as string;
		string value = GetCommittedValue(RedeemCodePropertyEditor.Code) as string;
		if (!text.Equals(value, StringComparison.InvariantCultureIgnoreCase))
		{
			TokenDetails = null;
			TokenExpired = false;
			TokenClientTypes = (EClientTypeFlags)0;
			ConfirmedTokenDetails = null;
			base.ServiceDeactivationRequestsDone = false;
			AlbumOffers = null;
			TrackOffers = null;
			VideoOffers = null;
			AppOffers = null;
		}
		if (base.ServiceDeactivationRequestsDone)
		{
			if (ConfirmedTokenDetails != null)
			{
				if (!TokenSupported)
				{
					ZuneShell.DefaultInstance.Execute("Web\\" + CreateRedeemCodeUrl(returnArguments: true), null);
					_owner.Cancel();
					return false;
				}
				if (AlbumOffers == null && TrackOffers == null && VideoOffers == null && AppOffers == null)
				{
					return base.OnMovingNext();
				}
				_redeemMediaCommand.Invoke((InvokePolicy)0);
				return false;
			}
			bool flag = TokenDetails != null && ((int)TokenDetails.TokenType == 1 || (int)TokenDetails.TokenType == 2);
			bool flag2 = !TokenExpired && (int)TokenClientTypes != 0;
			if (flag || flag2)
			{
				ConfirmedTokenDetails = TokenDetails;
			}
			return false;
		}
		StartDeactivationRequests(text);
		base.WizardPropertyEditor.Commit();
		return false;
	}

	protected override void OnStartDeactivationRequests(object state)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Expected O, but got Unknown
		//IL_0050: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		ServiceData serviceData = VerifyToken(state as string);
		if (!serviceData.TokenInvalid && (int)serviceData.TokenDetails.TokenType == 0)
		{
			Service.Instance.GetOfferDetails(serviceData.TokenDetails.MediaOfferId, new GetOfferDetailsCompleteCallback(OnGetOfferDetailsSuccess), new GetOfferDetailsErrorCallback(OnGetOfferDetailsError), (object)serviceData);
		}
		else
		{
			EndDeactivationRequests(serviceData);
		}
	}

	protected override void OnEndDeactivationRequests(object args)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		ServiceData serviceData = (ServiceData)args;
		TokenClientTypes = serviceData.TokenClientTypes;
		TokenDetails = serviceData.TokenDetails;
		TokenInvalid = serviceData.TokenInvalid;
		TokenExpired = serviceData.TokenExpired;
		AlbumOffers = serviceData.AlbumOffers;
		TrackOffers = serviceData.TrackOffers;
		VideoOffers = serviceData.VideoOffers;
		AppOffers = serviceData.AppOffers;
	}

	private ServiceData VerifyToken(string token)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		ServiceData serviceData = new ServiceData();
		HRESULT val = HRESULT.op_Implicit(Service.Instance.VerifyToken(token, ref serviceData.TokenDetails));
		if (((HRESULT)(ref val)).IsError)
		{
			serviceData.TokenInvalid = true;
		}
		return serviceData;
	}

	private void OnGetOfferDetailsSuccess(AlbumOfferCollection albums, TrackOfferCollection tracks, VideoOfferCollection videos, EClientTypeFlags clientTypes, object state)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		if (albums == null || tracks == null || videos == null)
		{
			OnGetOfferDetailsError(HRESULT._E_UNEXPECTED, state);
			return;
		}
		ServiceData serviceData = (ServiceData)state;
		ZeroOfferPoints(albums.Items);
		ZeroOfferPoints(tracks.Items);
		ZeroOfferPoints(videos.Items);
		serviceData.AlbumOffers = albums;
		serviceData.TrackOffers = tracks;
		serviceData.VideoOffers = videos;
		serviceData.TokenClientTypes = clientTypes;
		serviceData.TokenExpired = IsTokenExpired(albums.Items, tracks.Items, videos.Items);
		serviceData.AppOffers = Service.Instance.CreateEmptyAppCollection();
		EndDeactivationRequests(serviceData);
	}

	private bool IsTokenExpired(IList albums, IList tracks, IList videos)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		if ((albums != null && albums.Count > 0) || (tracks != null && tracks.Count > 0))
		{
			return false;
		}
		if (videos != null && videos.Count > 0)
		{
			foreach (VideoOffer video in videos)
			{
				VideoOffer val = video;
				if (!val.ExpirationDate.HasValue || val.ExpirationDate.Value > DateTime.UtcNow)
				{
					return false;
				}
			}
			return true;
		}
		return false;
	}

	private void OnGetOfferDetailsError(HRESULT hr, object state)
	{
		ServiceData serviceData = (ServiceData)state;
		if (((HRESULT)(ref hr)).IsError)
		{
			serviceData.TokenInvalid = true;
		}
		EndDeactivationRequests(serviceData);
	}

	private void ZeroOfferPoints(IEnumerable offers)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		if (offers == null)
		{
			return;
		}
		foreach (Offer offer in offers)
		{
			Offer val = offer;
			val.PriceInfo.MakeFree();
		}
	}
}
