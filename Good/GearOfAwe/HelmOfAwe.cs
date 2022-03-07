//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
using RBot;

public class HelmOfAwe
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreAwe Awe = new CoreAwe();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetHoA();

        Core.SetOptions(false);
    }

    public void GetHoA()
    {
        if (Core.CheckInventory("Helm of Awe"))
            return;

        Core.AddDrop("Helm Fragment");
        Awe.AwePass(4175, 4176, 4177);
        Core.EquipClass(ClassType.Solo);
        Story.UpdateQuest(3008);
        Core.SendPackets("%xt%zm%setAchievement%108927%ia0%18%1%");
        Story.UpdateQuest(3004);
        if (!Core.CheckInventory("Helm Relic"))
            while (!Core.CheckInventory("Helm Fragment", 10))
            {
                Core.EnsureAccept(Awe.QuestID);
                Adv.KillUltra("doomvaultb", "r26", "Left", "Undead Raxgore", "Helm Shard", 5, false);
                Core.EnsureComplete(Awe.QuestID);
                Bot.Wait.ForPickup("Helm Fragment");
            }
        Core.BuyItem("museum", 1129, "Helm Relic");
        Core.BuyItem("museum", 1129, "Helm of Awe");
        Core.ToBank("Legendary Awe Pass", "Guardian Awe Pass", "Armor of Awe Pass");
    }
}