using System.Collections.Generic;

namespace ZuneUI;

internal class WirelessTypeGrid
{
	private int _iColumn;

	private int _cColumns;

	private List<WirelessNetworkTypeCommand> _items = new List<WirelessNetworkTypeCommand>();

	public List<WirelessNetworkTypeCommand> NetworkList => _items;

	public WirelessTypeGrid(int columns)
	{
		_cColumns = columns;
	}

	public void NewRow()
	{
		if (_iColumn > 0)
		{
			while (_iColumn < _cColumns)
			{
				AddItem(new WirelessNetworkTypeCommand(null, null, null, null));
			}
		}
		_iColumn = 0;
	}

	public bool AddItem(WirelessNetworkTypeCommand item)
	{
		if (_iColumn < _cColumns)
		{
			_items.Add(item);
			_iColumn++;
			return true;
		}
		return false;
	}
}
