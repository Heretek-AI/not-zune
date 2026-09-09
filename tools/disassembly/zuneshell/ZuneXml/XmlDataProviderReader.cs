using System.Collections;
using System.IO;
using System.Xml;

namespace ZuneXml;

internal class XmlDataProviderReader
{
	private Stack _internalStack;

	private XmlTextReader _currentReader;

	private int _parentDepth;

	public int Depth
	{
		get
		{
			int num = _parentDepth;
			if (Count > 0)
			{
				num += ((XmlReader)_currentReader).Depth;
			}
			return num;
		}
	}

	public string LocalName
	{
		get
		{
			string result = string.Empty;
			if (_currentReader != null)
			{
				result = ((XmlReader)_currentReader).LocalName;
			}
			return result;
		}
	}

	public string Name
	{
		get
		{
			string result = string.Empty;
			if (_currentReader != null)
			{
				result = ((XmlReader)_currentReader).Name;
			}
			return result;
		}
	}

	public string Value
	{
		get
		{
			string result = string.Empty;
			if (_currentReader != null)
			{
				result = ((XmlReader)_currentReader).Value;
			}
			return result;
		}
	}

	public XmlNodeType NodeType
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			XmlNodeType result = (XmlNodeType)0;
			if (_currentReader != null)
			{
				result = ((XmlReader)_currentReader).NodeType;
			}
			return result;
		}
	}

	private int Count => ((_internalStack != null) ? _internalStack.Count : 0) + ((_currentReader != null) ? 1 : 0);

	public XmlDataProviderReader(Stream xmlStream)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		XmlTextReader val = new XmlTextReader(xmlStream);
		val.WhitespaceHandling = (WhitespaceHandling)2;
		PushReader(val);
	}

	public void Close()
	{
		while (_currentReader != null)
		{
			CloseCurrent();
		}
	}

	public bool Read()
	{
		bool flag = false;
		if (_currentReader != null)
		{
			flag = ((XmlReader)_currentReader).Read();
			if (!flag)
			{
				CloseCurrent();
			}
		}
		return flag;
	}

	public bool MoveToFirstAttribute()
	{
		bool result = false;
		if (_currentReader != null)
		{
			result = ((XmlReader)_currentReader).MoveToFirstAttribute();
		}
		return result;
	}

	public bool MoveToNextAttribute()
	{
		bool result = false;
		if (_currentReader != null)
		{
			result = ((XmlReader)_currentReader).MoveToNextAttribute();
		}
		return result;
	}

	public void PushElement(string xmlElement)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		if (Count > 0 && !string.IsNullOrEmpty(xmlElement))
		{
			XmlNamespaceManager val = new XmlNamespaceManager(((XmlReader)_currentReader).NameTable);
			XmlParserContext val2 = new XmlParserContext(((XmlReader)_currentReader).NameTable, val, ((XmlReader)_currentReader).XmlLang, (XmlSpace)0, _currentReader.Encoding);
			XmlTextReader val3 = new XmlTextReader(xmlElement, (XmlNodeType)1, val2);
			val3.WhitespaceHandling = (WhitespaceHandling)2;
			PushReader(val3);
		}
	}

	private void CloseCurrent()
	{
		if (_currentReader != null)
		{
			((XmlReader)_currentReader).Close();
		}
		PopReader();
	}

	private void PushReader(XmlTextReader reader)
	{
		if (_currentReader != null)
		{
			if (_internalStack == null)
			{
				_internalStack = new Stack(2);
			}
			_parentDepth += ((XmlReader)_currentReader).Depth;
			_internalStack.Push(_currentReader);
		}
		else
		{
			_parentDepth = 0;
		}
		_currentReader = reader;
	}

	private void PopReader()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		if (_internalStack != null && _internalStack.Count > 0)
		{
			_currentReader = (XmlTextReader)_internalStack.Pop();
			_parentDepth -= ((XmlReader)_currentReader).Depth;
			if (_parentDepth < 0)
			{
				_parentDepth = 0;
			}
		}
		else
		{
			_currentReader = null;
			_parentDepth = 0;
		}
	}
}
