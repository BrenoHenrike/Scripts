//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class CoreAwe
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreFarms Farm = new CoreFarms();

    public bool GuardianCheck()
    {
        Core.Logger("Checking AQ Guardian");
        if (Core.CheckInventory("Guardian Awe Pass", 1, true))
        {
            Core.Logger("You're AQ Guardian!");
            return true;
        }
        Core.BuyItem("museum", 53, "Guardian Awe Pass");
        if (Core.CheckInventory("Guardian Awe Pass"))
        {
            Core.Logger("Guardian Awe Pass bought successfully! You're AQ Guardian!");
            return true;
        }
        else
        {
            Core.Logger("You're not AQ Guardian.");
            return false;
        }
    }

    public void AweKill(int questID, string gear)
    {
        Core.EquipClass(ClassType.Solo);
        if (gear.Equals("pauldron"))
            Story.KillQuest(questID, "gravestrike", "Ultra Akriloth");
        else if (gear.Equals("breastplate"))
            Story.KillQuest(questID, "aqlesson", "Carnax");
        else if (gear.Equals("vambrace"))
            Story.KillQuest(questID, "bloodtitan", "Ultra Blood Titan");
        else if (gear.Equals("gauntlet"))
            Story.KillQuest(questID, "alteonbattle", "Ultra Alteon");
        else if (gear.Equals("greaves"))
            Story.KillQuest(questID, "bosschallenge", "Mutated Void Dragon");
        else if (gear.Equals("helm"))
        {
            Story.UpdateQuest(3008);
            Core.SendPackets("%xt%zm%setAchievement%108927%ia0%18%1%");
            Story.UpdateQuest(3004);
            Adv.KillUltra("doomvaultb", "r26", "Left", "Undead Raxgore", "Helm Shard", 5, false);
        }
        else
        {
            Story.UpdateQuest(3008);
            Adv.KillUltra("doomvault", "r5", "Left", "Binky", "Cape Shard", 1, false);
        }
    }

}
