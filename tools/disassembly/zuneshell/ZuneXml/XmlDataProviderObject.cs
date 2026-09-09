using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.Iris;
using Microsoft.Zune.Shell;
using MicrosoftZuneLibrary;

namespace ZuneXml;

public class XmlDataProviderObject : DataProviderObject, IXmlDataProviderObject, IDatabaseMedia
{
	private static string _strUuidPrefix1 = "urn:uuid:";

	private static string _strUuidPrefix2 = "uid:uuid:";

	private Dictionary<string, object> _propertyValues;

	internal IPageInfo NextPage
	{
		get
		{
			IPageInfo result = null;
			foreach (DataProviderMapping value in ((DataProviderObject)this).Mappings.Values)
			{
				string propertyTypeName = value.PropertyTypeName;
				if (propertyTypeName == "List")
				{
					XmlDataVirtualList xmlDataVirtualList = (XmlDataVirtualList)((DataProviderObject)this).GetProperty(value.PropertyName);
					if (xmlDataVirtualList != null)
					{
						result = xmlDataVirtualList.NextPage;
						break;
					}
				}
			}
			return result;
		}
		set
		{
			foreach (DataProviderMapping value2 in ((DataProviderObject)this).Mappings.Values)
			{
				string propertyTypeName = value2.PropertyTypeName;
				if (propertyTypeName == "List")
				{
					XmlDataVirtualList xmlDataVirtualList = (XmlDataVirtualList)((DataProviderObject)this).GetProperty(value2.PropertyName);
					if (xmlDataVirtualList != null)
					{
						xmlDataVirtualList.NextPage = value;
					}
				}
			}
		}
	}

	public bool PageToEnd
	{
		get
		{
			bool result = false;
			foreach (DataProviderMapping value in ((DataProviderObject)this).Mappings.Values)
			{
				string propertyTypeName = value.PropertyTypeName;
				if (propertyTypeName == "List")
				{
					XmlDataVirtualList xmlDataVirtualList = (XmlDataVirtualList)((DataProviderObject)this).GetProperty(value.PropertyName);
					if (xmlDataVirtualList != null)
					{
						result = xmlDataVirtualList.PageToEnd;
						break;
					}
				}
			}
			return result;
		}
		set
		{
			foreach (DataProviderMapping value2 in ((DataProviderObject)this).Mappings.Values)
			{
				string propertyTypeName = value2.PropertyTypeName;
				if (propertyTypeName == "List")
				{
					XmlDataVirtualList xmlDataVirtualList = (XmlDataVirtualList)((DataProviderObject)this).GetProperty(value2.PropertyName);
					if (xmlDataVirtualList != null)
					{
						xmlDataVirtualList.PageToEnd = value;
					}
				}
			}
		}
	}

	public XmlDataProviderObject(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
		foreach (DataProviderMapping value in ((DataProviderObject)this).Mappings.Values)
		{
			string propertyTypeName = value.PropertyTypeName;
			if (!IsXmlValueType(propertyTypeName))
			{
				if (propertyTypeName == "List")
				{
					((DataProviderObject)this).SetProperty(value.PropertyName, (object)new XmlDataVirtualList(((DataProviderObject)this).Owner, value.UnderlyingCollectionTypeCookie));
				}
				else
				{
					((DataProviderObject)this).SetProperty(value.PropertyName, (object)XmlDataProviderObjectFactory.CreateObject(((DataProviderObject)this).Owner, value.PropertyTypeCookie));
				}
			}
		}
	}

	public override object GetProperty(string propertyName)
	{
		object value = null;
		if (_propertyValues != null && _propertyValues.TryGetValue(propertyName, out value))
		{
			return value;
		}
		if (((DataProviderObject)this).Mappings.TryGetValue(propertyName, out var value2))
		{
			object defaultValue = value2.DefaultValue;
			if (defaultValue != null)
			{
				value = defaultValue;
			}
			else
			{
				switch (value2.PropertyTypeName)
				{
				case "String":
					value = string.Empty;
					break;
				case "Int32":
					value = 0;
					break;
				case "Int64":
					value = 0;
					break;
				case "DateTime":
					value = new DateTime(0L);
					break;
				case "TimeSpan":
					value = new TimeSpan(0L);
					break;
				case "Guid":
					value = Guid.Empty;
					break;
				case "Boolean":
					value = false;
					break;
				case "Double":
					value = 0.0;
					break;
				case "Single":
					value = 0f;
					break;
				}
			}
		}
		if (value != null)
		{
			EnsureValuesDictionary();
			_propertyValues[propertyName] = value;
		}
		return value;
	}

	public override void SetProperty(string propertyName, object value)
	{
		if (EnsureValuesDictionary() || !_propertyValues.ContainsKey(propertyName) || !object.Equals(_propertyValues[propertyName], value))
		{
			_propertyValues[propertyName] = value;
			((DataProviderObject)this).FirePropertyChanged(propertyName);
		}
	}

	public virtual void GetMediaIdAndType(out int mediaId, out EMediaTypes mediaType)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected I4, but got Unknown
		object property = ((DataProviderObject)this).GetProperty("LibraryId");
		if (property != null)
		{
			mediaId = (int)property;
		}
		else
		{
			mediaId = -1;
		}
		mediaType = (EMediaTypes)(int)NameToMediaType(((DataProviderObject)this).TypeName);
	}

	public static EMediaTypes NameToMediaType(string typeName)
	{
		switch (typeName)
		{
		case "ProfileData":
			return (EMediaTypes)96;
		case "Track":
		case "PlaylistTrack":
		case "ProfileTrack":
		case "TrackDownloadHistory":
		case "TrackPurchaseHistory":
		case "ChannelTrack":
			return (EMediaTypes)3;
		case "Album":
			return (EMediaTypes)11;
		case "PodcastSeries":
			return (EMediaTypes)18;
		case "MusicVideo":
			return (EMediaTypes)4;
		default:
			return (EMediaTypes)(-1);
		}
	}

	private bool EnsureValuesDictionary()
	{
		if (_propertyValues != null)
		{
			return false;
		}
		_propertyValues = new Dictionary<string, object>();
		return true;
	}

	internal void SetPropertyFromStringValue(DataProviderMapping propertyMapping, string stringValue)
	{
		object obj = stringValue;
		string propertyName = propertyMapping.PropertyName;
		string propertyTypeName = propertyMapping.PropertyTypeName;
		try
		{
			switch (propertyTypeName)
			{
			case "Int32":
				obj = int.Parse(stringValue);
				break;
			case "Int64":
				obj = long.Parse(stringValue);
				break;
			case "TimeSpan":
				obj = XmlConvert.ToTimeSpan(stringValue);
				break;
			case "DateTime":
				obj = XmlConvert.ToDateTime(stringValue, (XmlDateTimeSerializationMode)1);
				break;
			case "Guid":
				obj = ((!stringValue.StartsWith(_strUuidPrefix1)) ? ((!stringValue.StartsWith(_strUuidPrefix2)) ? ((object)new Guid(stringValue)) : ((object)new Guid(stringValue.Substring(_strUuidPrefix2.Length)))) : ((object)new Guid(stringValue.Substring(_strUuidPrefix1.Length))));
				break;
			case "Boolean":
				obj = bool.Parse(stringValue);
				break;
			case "Double":
				obj = double.Parse(stringValue, CultureInfo.InvariantCulture);
				break;
			case "Single":
				obj = float.Parse(stringValue, CultureInfo.InvariantCulture);
				break;
			default:
				if (propertyTypeName != "String")
				{
					obj = null;
				}
				break;
			}
			((DataProviderObject)this).SetProperty(propertyName, obj);
		}
		catch (FormatException)
		{
			_ = TraceSwitches.DataProviderSwitch.TraceError;
		}
	}

	private bool IsXmlValueType(string propertyType)
	{
		switch (propertyType)
		{
		default:
			return propertyType == "Single";
		case "String":
		case "Int32":
		case "Int64":
		case "TimeSpan":
		case "DateTime":
		case "Guid":
		case "Boolean":
		case "Double":
			return true;
		}
	}

	public bool ProcessXPath(string currentXPath, Hashtable attributes, List<XmlDataProviderQuery.XPathMatch> matches)
	{
		bool flag = false;
		foreach (DataProviderMapping value in ((DataProviderObject)this).Mappings.Values)
		{
			if (value.Source == null || !MatchesXPath(currentXPath, attributes, value.Source, out var tailXPath, out var matchingAttributeName))
			{
				continue;
			}
			string propertyTypeName = value.PropertyTypeName;
			bool flag2 = string.IsNullOrEmpty(tailXPath);
			bool flag3 = !flag2 && (tailXPath.StartsWith("/") || tailXPath.StartsWith("@"));
			bool flag4 = flag2 || flag3;
			bool flag5 = flag2 && value.Target == "Xml";
			if (flag2 && (IsXmlValueType(propertyTypeName) || flag5))
			{
				_ = TraceSwitches.DataProviderSwitch.TraceVerbose;
				matches.Add(new XmlDataProviderQuery.XPathMatch(this, value, matchingAttributeName, flag5));
				flag = true;
			}
			else if (flag4 && propertyTypeName == "List")
			{
				IXmlDataProviderObject xmlDataProviderObject = (IXmlDataProviderObject)((DataProviderObject)this).GetProperty(value.PropertyName);
				_ = TraceSwitches.DataProviderSwitch.TraceVerbose;
				flag |= xmlDataProviderObject.ProcessXPath(tailXPath, attributes, matches);
				if (!TraceSwitches.DataProviderSwitch.TraceVerbose)
				{
				}
			}
			else if (flag3)
			{
				_ = TraceSwitches.DataProviderSwitch.TraceVerbose;
				if (((DataProviderObject)this).GetProperty(value.PropertyName) is XmlDataProviderObject xmlDataProviderObject2)
				{
					flag |= xmlDataProviderObject2.ProcessXPath(tailXPath, attributes, matches);
				}
				_ = TraceSwitches.DataProviderSwitch.TraceVerbose;
			}
		}
		return flag;
	}

	internal void TransferToAppThread()
	{
		if (_propertyValues == null)
		{
			return;
		}
		foreach (object value in _propertyValues.Values)
		{
			if (value is XmlDataProviderObject xmlDataProviderObject)
			{
				xmlDataProviderObject.TransferToAppThread();
			}
			else if (value is XmlDataVirtualList xmlDataVirtualList)
			{
				xmlDataVirtualList.TransferToAppThread();
			}
		}
	}

	internal void OnQueryComplete()
	{
		if (_propertyValues == null)
		{
			return;
		}
		foreach (object value in _propertyValues.Values)
		{
			if (value is XmlDataVirtualList xmlDataVirtualList)
			{
				xmlDataVirtualList.OnQueryComplete();
			}
		}
	}

	internal static bool MatchesXPath(string xpathCheck, Hashtable attributes, string scriptSource, out string tailXPath, out string matchingAttributeName)
	{
		tailXPath = string.Empty;
		matchingAttributeName = null;
		if (!SplitScriptSource(scriptSource, out var sourceXPath, out var condAttrName, out var condAttrValue))
		{
			return false;
		}
		if (condAttrName != null && (!(attributes[condAttrName] is string text) || condAttrValue != text))
		{
			return false;
		}
		if (!xpathCheck.StartsWith(sourceXPath))
		{
			return false;
		}
		if (xpathCheck.Length > sourceXPath.Length && xpathCheck[sourceXPath.Length] != '/' && xpathCheck[sourceXPath.Length] != '@')
		{
			return false;
		}
		int num = sourceXPath.IndexOf('@');
		if (num >= 0)
		{
			tailXPath = xpathCheck.Substring(sourceXPath.Length);
			matchingAttributeName = sourceXPath.Substring(num + 1);
		}
		else if (xpathCheck.Length > sourceXPath.Length)
		{
			tailXPath = xpathCheck.Substring(sourceXPath.Length);
		}
		return true;
	}

	private static bool SplitScriptSource(string scriptSource, out string sourceXPath, out string condAttrName, out string condAttrValue)
	{
		sourceXPath = scriptSource;
		condAttrName = null;
		condAttrValue = null;
		int num = scriptSource.IndexOf('[');
		if (num < 0)
		{
			return true;
		}
		int num2 = scriptSource.IndexOf(']', num + 1);
		if (num == 0 || num2 < 0 || num2 != scriptSource.Length - 1)
		{
			return false;
		}
		sourceXPath = scriptSource.Substring(0, num).Trim();
		string text = scriptSource.Substring(num + 1, num2 - (num + 1)).Trim();
		if (text.Length == 0 || text[0] != '@')
		{
			return false;
		}
		text = text.Substring(1);
		int num3 = text.IndexOf('=');
		if (num3 == 0)
		{
			return false;
		}
		if (num3 < 0)
		{
			condAttrName = text;
		}
		else
		{
			condAttrName = text.Substring(0, num3).Trim();
			if (text.Length < num3 + 2)
			{
				return false;
			}
			condAttrValue = text.Substring(num3 + 2, text.Length - (num3 + 2) - 1).Trim();
		}
		return true;
	}

	internal void ChangeListSort(string newSortBy)
	{
		if (_propertyValues == null)
		{
			return;
		}
		foreach (object value in _propertyValues.Values)
		{
			if (value is XmlDataVirtualList xmlDataVirtualList)
			{
				xmlDataVirtualList.SortBy = newSortBy;
			}
		}
	}
}
