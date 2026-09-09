using System;
using Microsoft.Zune.Messaging;

namespace ZuneUI;

public abstract class PropertySetAttachment : Attachment
{
	public abstract IPropertySetMessageData PropertySet { get; }

	public PropertySetAttachment(Guid id, string title, string subtitle, string imageUri)
		: base(Guid.Empty, title, subtitle, imageUri)
	{
	}
}
