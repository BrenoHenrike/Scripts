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

    public int QuestID;

    public void AwePass(int LegendQuest, int GuardianQuest, int FreeQuest)
    {
        if (Core.IsMember || Core.CheckInventory("Legendary Awe Pass"))
        {
            Core.BuyItem("museum", 1130, "Legendary Awe Pass");
            QuestID = 4175;
        }
        else if (Awe.GuardianCheck())
        {
            Farm.BladeofAweREP(5, false);
            Farm.Experience(35);
            QuestID = 4176;
        }
        else
        {
            Farm.BladeofAweREP(10, false);
            Farm.Experience(55);
            Core.BuyItem("museum", 1130, "Armor of Awe Pass");
            QuestID = 4177;
        }
    }

    public bool GuardianCheck()
    {
        if (Core.CheckInventory("Guardian of Awe Pass"))
            return true;

        Core.Logger("Checking AQ Guardian");
        Core.BuyItem("museum", 53, "Guardian Awe Pass");
        if (Core.CheckInventory("Guardian Awe Pass"))
        {
            Core.Logger("You own the Guardian Awe Pass! You're AQ Guardian!");
            return true;
        }
        Core.Logger("You're not AQ Guardian.");
        return false;
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
        else
        {
            Story.UpdateQuest(3008);
            Adv.KillUltra("doomvault", "r5", "Left", "Binky", "Cape Shard", 1, false);
        }
    }
}
