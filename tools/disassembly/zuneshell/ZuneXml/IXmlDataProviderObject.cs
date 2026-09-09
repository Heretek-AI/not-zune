using System.Collections;
using System.Collections.Generic;

namespace ZuneXml;

internal interface IXmlDataProviderObject
{
	bool ProcessXPath(string currentXPath, Hashtable attributes, List<XmlDataProviderQuery.XPathMatch> matches);
}
