//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class VoidCrystals
{
	// This bot only gets the items, do the merge yourself for precaution
	public CoreBots Core => CoreBots.Instance;
	public CoreFarms Farm = new CoreFarms();
	public CoreDailys Dailys = new CoreDailys();
	public CoreNulgath Nulgath = new CoreNulgath();

	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();

		Core.AddDrop(Nulgath.bagDrops);

		Nulgath.FarmUni13(1);
		Dailys.EldersBlood();

		//If you have /TowerofDoom10 unlocked and can solo Slugbutter uncomment
		//Nulgath.DirtyDeedsDoneDirtCheap(200);
		Nulgath.FarmUni10(200);

		Nulgath.FarmGemofNulgath(150);

		Nulgath.FarmDarkCrystalShard(200);

		Nulgath.FarmDiamondofNulgath(200);

		Nulgath.FarmBloodGem(30);

		Nulgath.FarmTotemofNulgath(15);

		Nulgath.SwindleBulk(200);

		Core.SetOptions(false);
	}
}