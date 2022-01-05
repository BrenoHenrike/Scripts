//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
using RBot;

public class UnlockMineCrafting_ArtixQuests
{
	public ScriptInterface Bot => ScriptInterface.Instance;
	public CoreBots Core => CoreBots.Instance;
	public CoreBLOD BLOD = new CoreBLOD();
	public void ScriptMain(ScriptInterface bot)
	{
		Core.SetOptions();
		Core.AddDrop("BLinding Light of Destiny Handle", "Bonegrinder Medal", 
					 "Bone Dust", "Undead Essence", "Undead Energy",
					 "Spirit Orb", "Loyal Spirit Orb");

		BLOD.UnlockMineCrafting();

		Core.SetOptions(false);
	}
}
