//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class BattleUnder
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

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
        if (Core.isCompletedBefore(377))
            return;

        Story.PreLoad();

        Core.EquipClass(ClassType.Farm);
        Story.KillQuest(374, "battleundera", "Skeletal Warrior");
        Story.KillQuest(375, "battleundera", "Skeletal Warrior");
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(376, "battleundera", "Bone Terror");
        Core.EquipClass(ClassType.Farm);
        Story.KillQuest(377, "battleundera", "Skeletal Warrior");
    }

    public void BattleUnderB()
    {
        if (Core.isCompletedBefore(935))
            return;

        Story.PreLoad();

        BattleUnderA();

        Core.EquipClass(ClassType.Farm);
        Story.KillQuest(689, "battleunderb", "Skeleton Warrior");
        Story.KillQuest(690, "battleunderb", "Skeleton Warrior");
        Story.KillQuest(691, "battleunderb", "Skeleton Warrior");
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(692, "battleunderb", "Undead Champion", GetReward: false);
        Story.MapItemQuest(935, "battleunderb", 253);
    }

    public void BattleUnderC()
    {
        if (Core.isCompletedBefore(939))
            return;

        Story.PreLoad();

        BattleUnderB();

        Story.KillQuest(936, "battleunderc", "Blue Crystalized Undead");
        Story.KillQuest(937, "battleunderc", "Blue Crystalized Undead");
        Story.KillQuest(938, "battleunderc", "Crystalized Jellyfish");
        if (!Story.QuestProgression(939))
        {
            Core.EnsureAccept(939);
            Core.HuntMonster("battleundera", "Bone Terror", "Bone Terror Soul");
            Core.HuntMonster("battleunderb", "Undead Champion", "Undead Champion Soul");
            Core.HuntMonster("battleunderc", "Crystalized Jellyfish", "Jellyfish Soul");
            Core.EnsureComplete(939);
        }
    }

    public void BattleUnderD() //use Core.KillMonster(map: "MapName", cell: "Cell", pad: "pad", monster: "Mob", item = "item", quant: Amount) - map is a broke otherwise spawns random enemies.
    {
        if (!Core.IsMember || Core.isCompletedBefore(2215))
            return;

        Story.PreLoad();

        BattleUnderC();

        Story.KillQuest(2211, "battleunderd", "Shivering Bones");
        Story.MapItemQuest(2212, "battleunderd", 1286, 8);
        Story.KillQuest(2212, "battleunderd", "Icy Banshee");
        Story.KillQuest(2213, "battleunderd", "Skeletal Warrior");
        Story.MapItemQuest(2214, "battleunderd", 1287, 4);
        Story.KillQuest(2214, "battleunderd", "Glacial Horror");
        Story.MapItemQuest(2215, "battleunderd", 1288);
    }

    public void BattleUnderE()
    {
        if (Core.isCompletedBefore(5928))
            return;

        Story.PreLoad();

        Story.KillQuest(5927, "battleundere", "Lava Guard");
        Story.MapItemQuest(5927, "battleundere", 5362);
        Story.KillQuest(5928, "battleundere", "Hot Mama");
    }

    public void Understone()
    {
        if (Core.CheckInventory("Understone"))
            return;

        Core.AddDrop("Understone");

        Core.EnsureAccept(7289);
        Bot.Quests.UpdateQuest(935);
        Core.KillMonster("battleunderc", "Enter", "Spawn", "*", "Fluorite Shard", 10);
        Core.EnsureComplete(7289);
        Bot.Wait.ForPickup("Understone");
    }
}