//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/BrightOak.cs
using Skua.Core.Interfaces;

public class StoneCrusher
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public Core13LoC LOC = new();
    public BrightOak Oak = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetSC();

        Core.SetOptions(false);
    }

    public void GetSC(bool rankUpClass = true)
    {
        if (Core.CheckInventory("StoneCrusher"))
            return;

        LOC.Kimberly();
        Oak.doall();

        Adv.BuyItem("Gaiazor", 1210, "StoneCrusher");

        if (rankUpClass)
            Adv.rankUpClass("StoneCrusher");
    }
}