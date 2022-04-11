//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class CoreAwe
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();

    public void GetAweRelic(string Item, int LegendQuest, int FragmentAmount, int ShardAmount, string Map, string Monster)
    {
        if (Core.CheckInventory($"{Item} Relic"))
            return;
        Core.AddDrop($"{Item} Fragment");
        int QuestID;

        if ((Core.IsMember || Core.CheckInventory("Legendary Awe Pass")) && LegendQuest != 4160)
        {
            Core.BuyItem("museum", 1130, "Legendary Awe Pass");
            QuestID = LegendQuest;
        }
        else if (_GuardianCheck())
        {
            Farm.BladeofAweREP(5, false);
            Farm.Experience(35);
            QuestID = LegendQuest + 1;
        }
        else
        {
            Farm.BladeofAweREP(10, false);
            Farm.Experience(55);
            Core.BuyItem("museum", 1130, "Armor of Awe Pass");
            QuestID = LegendQuest + 2;
        }

        if (Map.ToLower() == "doomvault" || Map.ToLower() == "doomvaultb")
        {
            Bot.Quests.UpdateQuest(3008);
            Core.SendPackets("%xt%zm%setAchievement%108927%ia0%18%1%");
            Bot.Quests.UpdateQuest(3004);
        }

        Core.EquipClass(ClassType.Solo);
        while (!Core.CheckInventory($"{Item} Fragment", FragmentAmount))
        {
            Core.EnsureAccept(QuestID);
            if (Map.ToLower() == "doomvault" || Map.ToLower() == "doomvaultb")
                Adv.KillUltra(Map, Map.ToLower().EndsWith('b') ? "r26" : "r5", "Left", Monster, $"{Item} Shard", ShardAmount, false);
            else Adv.BoostHuntMonster(Map, Monster, $"{Item} Shard", ShardAmount, false);
            Core.EnsureComplete(QuestID);
            Bot.Wait.ForPickup($"{Item} Fragment");
        }

        Core.BuyItem("museum", 1129, $"{Item} Relic");
    }

    private bool _GuardianCheck()
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
}
