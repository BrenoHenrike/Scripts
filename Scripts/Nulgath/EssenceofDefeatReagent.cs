//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class EssenceofDefeatReagent
{
	public CoreBots Core = new CoreBots();
	public CoreNulgath Nulgath = new CoreNulgath();

	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		Core.AddDrop(Nulgath.bagDrops);

		Nulgath.EssenceofDefeatReagent();

		Core.SetOptions(false);
	}
}