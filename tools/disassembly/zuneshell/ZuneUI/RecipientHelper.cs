using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Iris;

namespace ZuneUI;

public class RecipientHelper : AutoCompleteHelper
{
	private List<string> _validRecipients;

	private string _errorMessage;

	public string ErrorMessage
	{
		get
		{
			return _errorMessage;
		}
		private set
		{
			if (_errorMessage != value)
			{
				_errorMessage = value;
				((ModelItem)this).FirePropertyChanged("ErrorMessage");
			}
		}
	}

	public IList ValidRecipients => _validRecipients;

	public RecipientHelper()
	{
		_validRecipients = new List<string>();
	}

	public void ClearState()
	{
		_validRecipients = new List<string>();
		base.Entry = string.Empty;
		ErrorMessage = null;
	}

	public static bool ValidZuneTag(string tag)
	{
		return ZuneTagHelper.IsValid(tag);
	}

	public static bool ValidEmail(string email)
	{
		return EmailHelper.IsValid(email);
	}

	public bool ValidateAll(bool allowEmailRecipients)
	{
		return Validate(base.Entry, allowEmailRecipients);
	}

	private bool Validate(string entry, bool allowEmailRecipients)
	{
		bool flag = true;
		StringBuilder stringBuilder = new StringBuilder();
		_validRecipients.Clear();
		if (entry != null)
		{
			string[] array = entry.Split(AutoCompleteHelper.s_entrySeparators);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (text.Length == 0)
				{
					continue;
				}
				if (!ValidZuneTag(text) && (!allowEmailRecipients || !ValidEmail(text)))
				{
					flag = false;
					if (i != 0)
					{
						stringBuilder.Append(' ');
					}
					stringBuilder.Append(text);
				}
				else
				{
					_validRecipients.Add(text);
				}
			}
		}
		if (!flag)
		{
			StringId stringId = (allowEmailRecipients ? StringId.IDS_COMPOSE_MESSAGE_ERROR_FRIENDS : StringId.IDS_COMPOSE_MESSAGE_ERROR_FRIENDS_NO_EMAIL);
			ErrorMessage = string.Format(Shell.LoadString(stringId), stringBuilder.ToString());
		}
		else
		{
			ErrorMessage = null;
		}
		return flag;
	}

	public static IList MakeStringList()
	{
		return new List<string>();
	}
}
