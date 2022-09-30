//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class CoreAwe
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    private int QuestID;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void GetAweRelic(string Item, int LegendQuest, int FragmentAmount, int ShardAmount, string Map, string Monster)
    {
        if (Core.CheckInventory($"{Item} Relic"))
            return;
        Core.AddDrop($"{Item} Fragment");
        int QuestID;

        if (Core.IsMember)
        {
            Core.BuyItem("museum", 1130, "Legendary Awe Pass");
            QuestID = LegendQuest;
        }
        else if (Bot.Flash.GetGameObject<int>("world.myAvatar.objData.intAQ") > 0)
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
        while (!Bot.ShouldExit && !Core.CheckInventory($"{Item} Fragment", FragmentAmount))
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
}
