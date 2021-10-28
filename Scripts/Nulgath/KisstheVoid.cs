//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;
public class KisstheVoid
{
	public CoreBots Core = new CoreBots();
	public CoreNulgath Nulgath = new CoreNulgath();
	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		Nulgath.KisstheVoid();

		Core.SetOptions(false);
	}
}