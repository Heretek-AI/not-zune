using System.Collections.Generic;

namespace ZuneUI;

public abstract class WizardPropertyEditorPage : WizardPage
{
	private Dictionary<PropertyDescriptor, object> _committedValues;

	private WizardPropertyEditor _wizardPropertyEditor;

	public WizardPropertyEditor WizardPropertyEditor => _wizardPropertyEditor;

	public override bool IsValid
	{
		get
		{
			if (_wizardPropertyEditor != null)
			{
				return _wizardPropertyEditor.IsValid();
			}
			return true;
		}
	}

	protected WizardPropertyEditorPage(Wizard owner)
		: base(owner)
	{
	}

	protected void Initialize(WizardPropertyEditor wizardPropertyEditor)
	{
		_wizardPropertyEditor = wizardPropertyEditor;
		_committedValues = new Dictionary<PropertyDescriptor, object>();
		if (_wizardPropertyEditor != null)
		{
			PropertyDescriptor[] propertyDescriptors = _wizardPropertyEditor.PropertyDescriptors;
			foreach (PropertyDescriptor property in propertyDescriptors)
			{
				AddProperty(property);
			}
			_wizardPropertyEditor.Initialize(this);
		}
	}

	private void AddProperty(PropertyDescriptor property)
	{
		if (!_committedValues.ContainsKey(property))
		{
			_committedValues.Add(property, property.DefaultValue);
		}
	}

	public object GetCommittedValue(PropertyDescriptor property)
	{
		if (_committedValues.ContainsKey(property))
		{
			return _committedValues[property];
		}
		return null;
	}

	public void SetCommittedValue(PropertyDescriptor property, object value)
	{
		if (_committedValues.ContainsKey(property))
		{
			_committedValues[property] = value;
		}
		else
		{
			_committedValues.Add(property, value);
		}
		SetUncommittedValue(property, value);
	}

	internal void SetPropertyState(PropertyDescriptor property, object state)
	{
		if (_wizardPropertyEditor != null)
		{
			_wizardPropertyEditor.SetPropertyState(property, state);
		}
	}

	public override void RefreshValidationState()
	{
		if (_wizardPropertyEditor != null)
		{
			_wizardPropertyEditor.ResetExternalErrors();
		}
		base.RefreshValidationState();
	}

	protected object GetUncommittedValue(PropertyDescriptor property)
	{
		object result = null;
		if (_wizardPropertyEditor != null)
		{
			result = _wizardPropertyEditor.GetPropertyData(property);
		}
		return result;
	}

	protected void SetUncommittedValue(PropertyDescriptor property, object value)
	{
		if (_wizardPropertyEditor != null)
		{
			_wizardPropertyEditor.SetPropertyData(property, value);
		}
	}

	protected bool SetExternalError(PropertyDescriptor descriptor, HRESULT hr)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		bool result = false;
		MetadataEditProperty metadataEditProperty = null;
		if (_wizardPropertyEditor != null)
		{
			metadataEditProperty = _wizardPropertyEditor.GetProperty(descriptor);
		}
		if (metadataEditProperty != null)
		{
			result = true;
			metadataEditProperty.ExternalError = hr;
		}
		return result;
	}

	protected bool SetExternalError(string propertyName, HRESULT hr)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		bool result = false;
		MetadataEditProperty metadataEditProperty = null;
		if (_wizardPropertyEditor != null)
		{
			metadataEditProperty = _wizardPropertyEditor.GetProperty(propertyName);
		}
		if (metadataEditProperty != null)
		{
			result = true;
			metadataEditProperty.ExternalError = hr;
		}
		return result;
	}

	internal override bool OnMovingNext()
	{
		if (_wizardPropertyEditor != null && _wizardPropertyEditor.IsValid())
		{
			_wizardPropertyEditor.Commit();
		}
		return base.OnMovingNext();
	}
}
