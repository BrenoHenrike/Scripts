//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/BrightOak.cs
using RBot;

public class StoneCrusher
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public Core13LoC LOC = new();
    public BrightOak Oak = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetSC();

        Core.SetOptions(false);
    }

    public void GetSC(bool rankUpClass = true)
    {
        if (Core.CheckInventory("StoneCrusher"))
            return;

        Story.PreLoad();

        LOC.Kimberly();
        Farm.MythsongREP();
        Farm.ArcangroveREP();
        Oak.doall();
        Farm.BrightoakREP();

        Core.BuyItem("Gaiazor", 1210, "Earth's Song Token");
        Core.BuyItem("Gaiazor", 1210, "Shaman Armor");
        Core.BuyItem("Gaiazor", 1210, "StoneCrusher");

        if (rankUpClass)
            Adv.rankUpClass("StoneCrusher");
    }
}