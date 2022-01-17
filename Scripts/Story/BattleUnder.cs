//cs_include Scripts/CoreBots.cs
using RBot;

public class BattleUnder
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        BattleUnderAll();

        Core.SetOptions(false);
    }

    public void BattleUnderAll()
    {
        BattleUnderA();
        BattleUnderB();
        BattleUnderC();
        BattleUnderD();
        BattleUnderE();
    }

    public void BattleUnderA()
    {
        if (Bot.Quests.IsUnlocked(689))
            return;
        else
        {
            if (!Bot.Quests.IsUnlocked(375))
                Bot.Quests.EnsureAccept(374);
            if (!Bot.Quests.IsUnlocked(376))
                Bot.Quests.EnsureAccept(375);
            Bot.Quests.EnsureAccept(377);
        }
        Core.EquipClass(ClassType.Farm);
        Core.KillQuest(374, "battleundera", "Skeletal Warrior|Skeletal Fire Mage|Skeletal Ice Mage|Skeletal Soldier");
        Core.KillQuest(375, "battleundera", "Skeletal Warrior|Skeletal Fire Mage|Skeletal Ice Mage|Skeletal Soldier");
        Core.EquipClass(ClassType.Solo);
        Core.KillQuest(376, "battleundera", "Bone Terror");
        Core.EquipClass(ClassType.Farm);
        Core.KillQuest(377, "battleundera", "Skeletal Warrior|Skeletal Fire Mage|Skeletal Ice Mage|Skeletal Soldier", FollowupIDOverwrite: 689);
    }

    public void BattleUnderB()
    {
        BattleUnderA();

        if (!Bot.Quests.IsUnlocked(935))
        {
            if (!Bot.Quests.IsUnlocked(692))
            {
                if (!Bot.Quests.IsUnlocked(690))
                    Bot.Quests.EnsureAccept(689);
                if (!Bot.Quests.IsUnlocked(691))
                    Bot.Quests.EnsureAccept(690);
                Bot.Quests.EnsureAccept(691);
            }
            Core.EquipClass(ClassType.Farm);
            Core.KillQuest(689, "battleunderb", "Skeleton Warrior|Skeleton Fighter");
            Core.KillQuest(690, "battleunderb", "Skeleton Warrior|Skeleton Fighter");
            Core.KillQuest(691, "battleunderb", "Skeleton Warrior|Skeleton Fighter");
            Core.EquipClass(ClassType.Solo);
            Core.KillQuest(692, "battleunderb", "Undead Champion", GetReward: false, FollowupIDOverwrite: 935);
        }
        Core.MapItemQuest(935, "battleunderb", 253);
    }

    public void BattleUnderC()
    {
        if (Bot.Quests.IsUnlocked(2211))
            return;
        
        BattleUnderB();

        Core.KillQuest(936, "battleunderc", "Blue Crystalized Undead|Green Crystalized Undead|Purple Crystalized Undead");
        Core.KillQuest(937, "battleunderc", "Blue Crystalized Undead|Green Crystalized Undead|Purple Crystalized Undead|Purple Crystalized Jellyfish");
        Core.KillQuest(938, "battleunderc", "Crystalized Jellyfish");
        Core.KillQuest(939, "battleundera", "Bone Terror", FollowupIDOverwrite: 2211);
        Core.KillQuest(939, "battleunderb", "Undead Champion", FollowupIDOverwrite: 2211);
        Core.KillQuest(939, "battleunderc", "Crystalized Jellyfish", FollowupIDOverwrite: 2211);
    }

    public void BattleUnderD()
    {
        if (!Core.IsMember)
            return;

        if (!Bot.Quests.IsUnlocked(2215))
        {
            Core.KillQuest(2211, "battleunderd", "Shivering Bones");
            Core.KillQuest(2212, "battleunderd", "Icy Banshee");
            Core.MapItemQuest(2212, "battleunderd", 1286, 8);
            Core.KillQuest(2213, "battleunderd", "Skeletal Warrior");
            Core.MapItemQuest(2214, "battleunderd", 1287, 4);
            Core.KillQuest(2214, "battleunderd", "Glacial Horror");
        }
        Core.MapItemQuest(2215, "battleunderd", 1288, hasFollowup: false);
    }

    public void BattleUnderE()
    {
        Core.KillQuest(5927, "battleundere", "Lava Guard");
        Core.MapItemQuest(5927, "battleundere", 5362);
        Core.KillQuest(5928, "battleundere", "Hot Mama", hasFollowup: false);
    }

    public void Understone(int Quantity)
    {
        if (Core.CheckInventory("Understone", Quantity))
            return;

        Core.Logger($"Farming {Quantity} Understone");
        Core.AddDrop("Understone");
        int i = 1;
        while (!Core.CheckInventory("Understone", Quantity))
        {
            Core.EnsureAccept(7289);
            Core.KillMonster("battleunderc", "Enter", "Spawn", "*", "Fluorite Shard", 10);
            Core.EnsureComplete(7289);
            Bot.Wait.ForPickup("Understone");
            Core.Logger($"Completed {i++}x");
        }
        Core.Logger($"Completed farming {Quantity} Understone");
    }
}