using System;
using Microsoft.Iris;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class ContactInfoParentStep : RegionInfoStep
{
	private bool _isAdult;

	public override string UI => "res://ZuneShellResources!AccountInfo.uix#ParentContactInfoStep";

	public override bool IsEnabled
	{
		get
		{
			bool flag = base.IsEnabled;
			if (flag)
			{
				flag = base.State.BasicAccountInfoStep.IsParentAccountNeeded;
			}
			return flag;
		}
	}

	public bool IsAdult
	{
		get
		{
			return _isAdult;
		}
		protected set
		{
			if (_isAdult != value)
			{
				_isAdult = value;
				((ModelItem)this).FirePropertyChanged("IsAdult");
			}
		}
	}

	protected override PropertyDescriptor CountryDescriptor => ContactInfoParentPropertyEditor.Country;

	public ContactInfoParentStep(Wizard owner, AccountManagementWizardState state)
		: base(owner, state, parentAccount: true)
	{
		base.NextTextOverride = Shell.LoadString(StringId.IDS_I_ACCEPT_BUTTON);
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_PARENT_CONTACT_HEAD);
		WizardPropertyEditor wizardPropertyEditor = new ContactInfoParentPropertyEditor();
		Initialize(wizardPropertyEditor);
	}

	protected override void OnCountryChanged()
	{
		if (base.WizardPropertyEditor != null)
		{
			base.WizardPropertyEditor.SetPropertyState(BaseContactInfoPropertyEditor.FirstName, base.SelectedCountry);
			base.WizardPropertyEditor.SetPropertyState(BaseContactInfoPropertyEditor.LastName, base.SelectedCountry);
			base.WizardPropertyEditor.SetPropertyState(BaseContactInfoPropertyEditor.PhoneNumber, base.SelectedCountry);
			base.WizardPropertyEditor.SetPropertyState(BaseContactInfoPropertyEditor.PhoneExtension, base.SelectedCountry);
		}
	}

	protected override void OnActivate()
	{
		base.SelectedCountry = base.State.BasicAccountInfoStep.SelectedCountry;
		base.ServiceDeactivationRequestsDone = false;
		SetPropertyState(ContactInfoParentPropertyEditor.Birthday, base.State.BasicAccountInfoStep.SelectedLocale);
		SetUncommittedValue(ContactInfoParentPropertyEditor.Birthday, GetCommittedValue(ContactInfoParentPropertyEditor.Birthday));
		MetadataEditProperty property = base.WizardPropertyEditor.GetProperty(BaseContactInfoPropertyEditor.Email);
		if (string.IsNullOrEmpty(property.Value))
		{
			if (base.State.EmailSelectionParentStep.IsEmailPassportId)
			{
				property.Value = base.State.EmailSelectionParentStep.GetCommittedValue(EmailSelectionPropertyEditor.Email) as string;
			}
			else
			{
				string text = base.State.CreatePassportParentStep.GetCommittedValue(CreatePassportPropertyEditor.PassportId) as string;
				string text2 = base.State.CreatePassportParentStep.GetCommittedValue(CreatePassportPropertyEditor.PassportDomain) as string;
				if (text != null && text2 != null)
				{
					property.Value = text + "@" + text2;
				}
			}
		}
		base.OnActivate();
	}

	internal override bool OnMovingNext()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		if (base.ServiceDeactivationRequestsDone)
		{
			base.WizardPropertyEditor.GetProperty(ContactInfoParentPropertyEditor.Birthday).ExternalError = ((!IsAdult) ? HRESULT._ZUNE_E_SIGNUP_INVALID_PARENT_AGE : HRESULT._S_OK);
			if (IsAdult)
			{
				return base.OnMovingNext();
			}
			ShowValidation();
			base.ServiceDeactivationRequestsDone = false;
			return false;
		}
		StartDeactivationRequests(((DateTime?)GetUncommittedValue(ContactInfoParentPropertyEditor.Birthday)).Value);
		return false;
	}

	protected override void OnStartDeactivationRequests(object state)
	{
		DateTime birthday = (DateTime)state;
		bool flag = ObtainIsAdult(birthday);
		EndDeactivationRequests(flag);
	}

	protected override void OnEndDeactivationRequests(object args)
	{
		IsAdult = (bool)args;
	}

	private bool ObtainIsAdult(DateTime birthday)
	{
		AccountCountry country = AccountCountryList.Instance.GetCountry(base.State.BasicAccountInfoStep.SelectedCountry);
		bool result = false;
		if (country != null)
		{
			int num = ObtainAge(birthday);
			result = num >= ((CountryBaseDetails)country).AdultAge;
		}
		return result;
	}

	private int ObtainAge(DateTime birthday)
	{
		DateTime today = DateTime.Today;
		int num = today.Year - birthday.Year;
		if (today.Month < birthday.Month || (today.Month == birthday.Month && today.Day < birthday.Day))
		{
			num--;
		}
		return num;
	}
}
