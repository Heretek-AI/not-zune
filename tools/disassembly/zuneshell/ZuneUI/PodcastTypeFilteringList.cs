using Microsoft.Iris;
using ZuneXml;

namespace ZuneUI;

public class PodcastTypeFilteringList : FilterList
{
	private const string PodcastTypeAudio = "audio";

	private const string PodcastTypeVideo = "video";

	private const string PodcastTypeAudioAndVideo = "both";

	private string _filterType;

	public string FilterType
	{
		get
		{
			return _filterType;
		}
		set
		{
			if (_filterType != value)
			{
				_filterType = value;
				((ModelItem)this).FirePropertyChanged("FilterType");
				ProduceFilteredList();
			}
		}
	}

	protected override bool ShouldIncludeItem(int sourceIndex, int targetIndex, object item)
	{
		if (item is PodcastSeries { Type: var type })
		{
			if (_filterType == null)
			{
				return true;
			}
			return type == _filterType;
		}
		return false;
	}
}
