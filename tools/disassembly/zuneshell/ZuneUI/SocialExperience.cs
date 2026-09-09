using System;
using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class SocialExperience : Experience
{
	private int _messageCount;

	private Node[] _nodes;

	private Node _friends;

	private Node _me;

	private Node _inbox;

	public override IList NodesList
	{
		get
		{
			if (_nodes == null)
			{
				_nodes = new Node[3] { Friends, Me, Inbox };
			}
			return _nodes;
		}
	}

	public Node Friends
	{
		get
		{
			if (_friends == null)
			{
				_friends = new Node(this, StringId.IDS_FRIENDS_PIVOT, "Social\\Friends", (SQMDataId)125);
			}
			return _friends;
		}
	}

	public Node Me
	{
		get
		{
			if (_me == null)
			{
				_me = new Node(this, StringId.IDS_PROFILE_PIVOT, "Social\\Profile", (SQMDataId)126);
			}
			return _me;
		}
	}

	public Node Inbox
	{
		get
		{
			if (_inbox == null)
			{
				_inbox = new Node(this, StringId.IDS_INBOX_PIVOT, "Social\\Inbox", (SQMDataId)127);
			}
			return _inbox;
		}
	}

	public int MessageCount
	{
		get
		{
			return _messageCount;
		}
		set
		{
			if (_messageCount == value)
			{
				return;
			}
			if (value > _messageCount)
			{
				((ModelItem)this).FirePropertyChanged("MessagesArrived");
				if (this.MessagesArrived != null)
				{
					this.MessagesArrived(this, null);
				}
			}
			_messageCount = value;
			((ModelItem)this).FirePropertyChanged("MessageCount");
		}
	}

	public int PlayCount
	{
		get
		{
			if (SignIn.Instance.SignedIn)
			{
				return ProfileDataHelper.ProfilePlayCount;
			}
			return -1;
		}
		set
		{
			int profilePlayCount = ProfileDataHelper.ProfilePlayCount;
			if (profilePlayCount != value && SignIn.Instance.SignedIn)
			{
				ProfileDataHelper.ProfilePlayCount = value;
				((ModelItem)this).FirePropertyChanged("PlayCount");
			}
		}
	}

	public DateTime CommentsLastRead
	{
		get
		{
			if (SignIn.Instance.SignedIn)
			{
				return ProfileDataHelper.CommentsLastRead;
			}
			return DateTime.MinValue;
		}
		set
		{
			DateTime commentsLastRead = ProfileDataHelper.CommentsLastRead;
			if (commentsLastRead != value && SignIn.Instance.SignedIn)
			{
				ProfileDataHelper.CommentsLastRead = value;
				((ModelItem)this).FirePropertyChanged("CommentsLastRead");
			}
		}
	}

	public override string DefaultUIPath => "Social\\Default";

	public event EventHandler MessagesArrived;

	public SocialExperience(Frame frameOwner)
		: base(frameOwner, StringId.IDS_SOCIAL_PIVOT, (SQMDataId)102)
	{
	}
}
