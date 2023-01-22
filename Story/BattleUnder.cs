/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class BattleUnder
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
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

        Story.PreLoad(this);

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

        Story.PreLoad(this);

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

        Story.PreLoad(this);

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

        Story.PreLoad(this);

        Core.AddDrop("Battle Pickaxe");
        BattleUnderC();

        //Note; Mobs on the map are randomly spawned / room 
        //with 3 or 4 spawns sets that can be made/room.. thus breaking hunt(and killquest)

        if (!Story.QuestProgression(2211))
        {
            Core.EnsureAccept(2211);
            Core.KillMonster("battleunderd", "Enter", "Spawn", "Shivering Bones", "Shivering Bone", 10);
            Core.EnsureComplete(2211);
        }

        if (!Story.QuestProgression(2212))
        {
            Core.EnsureAccept(2212);
            Core.KillMonster("battleunderd", "r2", "Left", "Icy Banshee", "Banshee Slain", 7);
            Story.MapItemQuest(2212, "battleunderd", 1286, 8);
        }

        if (!Story.QuestProgression(2213))
        {
            Core.EnsureAccept(2213);
            Core.KillMonster("battleunderd", "r5", "Left", "Skeletal Warrior", "Icy Pickaxe Found");
            Core.EnsureComplete(2213);
        }

        if (!Story.QuestProgression(2214))
        {
            Core.EnsureAccept(2214);
            Core.KillMonster("battleunderd", "r5", "Left", "Glacial Horror", "Glacial Horror Slain");
            Core.GetMapItem(1287, 4, "battleunderd");
            Core.EnsureComplete(2214);
        }

        Story.MapItemQuest(2215, "battleunderd", 1288);
    }

    public void BattleUnderE()
    {
        if (Core.isCompletedBefore(5928))
            return;

        Story.PreLoad(this);

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
