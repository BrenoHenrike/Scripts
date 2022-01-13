//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
using RBot;

public class UltimateWeaponKit
{
	public ScriptInterface Bot => ScriptInterface.Instance;
	public CoreBots Core => CoreBots.Instance;
	public CoreBLOD BLOD = new CoreBLOD();
	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();
		Core.AddDrop("Ultimate Weapon Kit", "Blinding Light Fragments", "Bright Aura", "Spirit Orb", "Loyal Spirit Orb", "Great Ornate Warhammer");

		BLOD.UltimateWK("Bright Aura", 10000);

		Core.SetOptions(false);
	}
}
