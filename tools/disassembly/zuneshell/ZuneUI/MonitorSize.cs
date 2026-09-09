namespace ZuneUI;

internal struct MonitorSize(RECT rcTotal, RECT rcWork)
{
	public readonly RECT TotalArea = rcTotal;

	public readonly RECT WorkArea = rcWork;
}
