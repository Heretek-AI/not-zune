namespace ZuneUI;

public class RadioStation
{
	private string m_title;

	private string m_sourceUrl;

	private string m_image;

	public string Title
	{
		get
		{
			return m_title;
		}
		private set
		{
			if (value != m_title)
			{
				m_title = value;
			}
		}
	}

	public string SourceURL
	{
		get
		{
			return m_sourceUrl;
		}
		private set
		{
			if (value != m_sourceUrl)
			{
				m_sourceUrl = value;
			}
		}
	}

	public string ImagePath
	{
		get
		{
			return m_image;
		}
		private set
		{
			if (value != m_image)
			{
				m_image = value;
			}
		}
	}

	public RadioStation(string Title, string SourceURL, string ImagePath)
	{
		m_title = Title;
		m_sourceUrl = SourceURL;
		m_image = ImagePath;
	}
}
