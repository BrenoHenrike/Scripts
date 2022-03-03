//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
using RBot;

public class CapeOfAwe
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAwe Awe = new CoreAwe();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetCoA();

        Core.SetOptions(false);
    }

    public void GetCoA()
    {
        if (Core.CheckInventory("Cape of Awe"))
            return;

        Core.AddDrop("Cape Fragment");
        Awe.AwePass(4178, 4179, 4180);
        Core.EquipClass(ClassType.Solo);
        Story.UpdateQuest(3008);
        while (!Core.CheckInventory("Cape Fragment", 10))
        {
            Core.EnsureAccept(Awe.QuestID);
            Adv.KillUltra("doomvault", "r5", "Left", "Binky", "Cape Shard", 1, false);
            Core.EnsureComplete(Awe.QuestID);
            Bot.Wait.ForPickup("Cape Fragment");
        }
        Core.BuyItem("museum", 1129, "Cape Relic");
        Core.BuyItem("museum", 1129, "Cape of Awe");
        Core.ToBank("Legendary Awe Pass", "Guardian Awe Pass", "Armor of Awe Pass");
    }
}