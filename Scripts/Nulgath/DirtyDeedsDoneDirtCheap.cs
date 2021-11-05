//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;
using System.Linq;

public class DirtyDeedsDoneDirtCheap
{
	public CoreBots Core = new CoreBots();
	public CoreNulgath Nulgath = new CoreNulgath();
	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		Core.AddDrop("Unidentified 10", "Receipt of Swindle", "Blood Gem of the Archfiend");

		Nulgath.DirtyDeedsDoneDirtCheap();

		Core.SetOptions(false);
	}
}