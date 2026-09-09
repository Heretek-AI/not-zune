using System;

namespace ZuneUI;

public class MathConstants
{
	public static float PI => (float)Math.PI;

	public static float HalfPI => (float)Math.PI / 2f;

	public static float E => (float)Math.E;

	public static float DegreeToRadian(float value)
	{
		return (float)((double)value / 180.0 * Math.PI);
	}
}
