namespace ZuneUI;

public abstract class PropertySource
{
	public virtual bool NeedsCommit => false;

	public abstract object Get(object media, PropertyDescriptor property);

	public abstract void Set(object media, PropertyDescriptor property, object value);

	public virtual void Commit(object media)
	{
	}
}
