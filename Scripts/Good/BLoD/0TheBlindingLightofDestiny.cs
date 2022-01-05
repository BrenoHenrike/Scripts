//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/BLoD/CoreBLOD.cs
using RBot;

public class TheBlindingLightofDestiny
{
	public ScriptInterface Bot => ScriptInterface.Instance;
	public CoreBots Core => CoreBots.Instance;
	public CoreBLOD BLOD = new CoreBLOD();
	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();
		Core.AddDrop(BLOD.BLoDItems);

		BLOD.UnlockMineCrafting();

		BLOD.BlindingMace();

		BLOD.BlindingBow();

		BLOD.BlindingBlade();

		BLOD.TheBlindingLightofDestiny();

		Core.SetOptions(false);
	}
}
