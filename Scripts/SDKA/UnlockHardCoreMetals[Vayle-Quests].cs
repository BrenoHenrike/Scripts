//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/SDKA/CoreSDKA.cs
using RBot;

public class UnlockHardCoreMetals_Vayle_Quests
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreSDKA SDKA = new CoreSDKA();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        Core.AddDrop("Dark Energy", "Dark Spirit Orb", "DoomKnight Hood", 
                     "Experimental Dark Item", "Shadow Terror Axe", "Elders' Blood", 
                     "DoomCoin", "Shadow Creeper Enchant", "Shadow Serpent Scythe", 
                     "Dark Skull", "Corrupt Spirit Orb");

        SDKA.UnlockHardCoreMetals();

        Core.SetOptions(false);
    }
}