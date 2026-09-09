using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class RatingSystemList : NotifyPropertyChangedImpl
{
	private static RatingSystemList s_instance;

	private IDictionary<string, RatingSystem> _ratings;

	public static RatingSystemList Instance
	{
		get
		{
			if (s_instance == null)
			{
				s_instance = new RatingSystemList();
			}
			return s_instance;
		}
	}

	public IList RatingSystems
	{
		get
		{
			string[] array = null;
			if (_ratings != null)
			{
				array = new string[_ratings.Keys.Count];
				_ratings.Keys.CopyTo(array, 0);
			}
			return array;
		}
	}

	public bool Loaded => _ratings != null;

	private RatingSystemList()
	{
	}

	public RatingSystem GetRatingSystem(string system)
	{
		return _ratings[system];
	}

	public void LoadData()
	{
		ThreadPool.QueueUserWorkItem(LoadDataOnWorkerThread);
	}

	private void LoadDataOnWorkerThread(object state)
	{
		LoadDataOnWorkerThread();
	}

	internal bool LoadDataOnWorkerThread()
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		bool flag = true;
		if (_ratings == null)
		{
			RatingSystemBase[] ratingSystems = Service.Instance.GetRatingSystems();
			Dictionary<string, RatingSystem> dictionary = null;
			if (ratingSystems != null && ratingSystems.Length > 0)
			{
				dictionary = new Dictionary<string, RatingSystem>(ratingSystems.Length);
				RatingSystemBase[] array = ratingSystems;
				foreach (RatingSystemBase val in array)
				{
					if (!string.IsNullOrEmpty(val.Description))
					{
						dictionary.Add(val.Name, new RatingSystem(val));
					}
				}
				if (dictionary == null || dictionary.Count == 0)
				{
					flag = false;
				}
				else
				{
					_ratings = dictionary;
				}
			}
		}
		if (flag)
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(NotifyLoaded), (object)null);
		}
		return flag;
	}

	private void NotifyLoaded(object args)
	{
		FirePropertyChanged("RatingSystems");
		FirePropertyChanged("Loaded");
	}
}
