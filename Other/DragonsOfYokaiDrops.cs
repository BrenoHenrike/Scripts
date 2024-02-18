/*
name: Dragons of Yokai Drops
description: farms all drops from Dragons of Yokai saga maps.
tags: yokai, dragons, drops
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/DragonsOfYokai/CoreDOY.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class DragonsOfYokaiDrops
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreDOY DOY = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetDrops();

        Core.SetOptions(false);
    }

    public void GetDrops()
    {
        DOY.DoAll();

        #region Yokai Pirate
        string[] DisguisedPirate = {
            "Backup Battle Blunderbuss",
            "Battlescarred Backup Cutlass",
            "Coastal Raider's Backup Rifle",
            "Coastal Raider's Beard",
            "Disguised Pirate's BattleGear",
            "Disguised Pirate's Crossed Cutlasses",
            "Disguised Pirate's EyePatch",
            "Disguised Pirate's Hair",
            "Disguised Pirate's Tricorn",
            "Hippodrome Star Cutlass",
            "Hippodrome Star Cutlasses",
            "Lovely Roger Flag",
            "Marauder's Monkey on yer Back",
            "Maurader's Mane",
            "Maurader's Mane + Beard",
            "Maurader's Monkey Morph + Hat",
            "Pi-Rat Pet",
            "Star Pirate's Cutlass + Rapier"
        };

        Core.EquipClass(ClassType.Farm);
        runEngine(DisguisedPirate, "yokaipirate", "r2", "Left");

        string[] NevergladesKnight = {
            "Swashbuckler's Rapier",
            "Yokai Gunpowder"
        };

        Core.EquipClass(ClassType.Farm);
        runEngine(NevergladesKnight, "yokaipirate", "Enter", "Spawn");
        #endregion

        #region Yokai Treasure
        string[] AdmiralZheng = {
            "Daitengu Cloak",
            "Dual Loyal Alloy Tourne",
            "Dual Pearl Dust Shuriken",
            "Golden Long Head",
            "Golden Long Statue",
            "Loyal Alloy Dagger",
            "Loyal Alloy Daggers",
            "Loyal Alloy Tourne",
            "Moonlit Steel Back Rapier",
            "Moonlit Steel Rapier",
            "Necro Crewmember's Keg", //Going rare
            "Necro Crewmember's Knife", //Going rare
            "Necro Crewmember's Knives", //Going rare
            "Pearl Dust Shuriken",
            "Tengu Feather Cap",
            "Tengu Feather Locks",
            "Tengu Typhoon Cutlass",
            "Wuji Zodiac Wheel"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(AdmiralZheng, "yokaitreasure", "r10", "Bottom");

        string[] ImperialWarrior = {
            "Iron Flight Cutlass",
            "Loyal Alloy Dirk",
            "Loyal Alloy Dirks",
            "Loyal Alloy Knife",
            "Loyal Alloy Knives",
            "Mercury Phial",
            "Resting Iron Flight Cutlass",
            "Stealthy Sea Hair",
            "Stealthy Sea Locks",
            "Stealthy Sea Patch Hair",
            "Stealthy Sea Patch Locks",
            "Stygian Navigator's Hair",
            "Stygian Navigator's Locks",
            "Stygian Navigator's Morph",
            "Stygian Navigator's Visage"
        };

        Core.EquipClass(ClassType.Farm);
        runEngine(ImperialWarrior, "yokaitreasure", "r9", "Right");

        string[] NeedleMouth = {
            "Grim Sailor's Battle Gear",
            "GrimSailor's Pistol",
            "GrimSailor's Pistols",
            "Necro Crewmember's Battle Gear",
            "Necro Crewmember's Blade + Sheath",
            "Necro Crewmember's Blades",
            "Necro Crewmember's Blunderbuss",
            "Necro Crewmember's Flintlock",
            "Necro Crewmember's Flintlocks"
        };

        Core.EquipClass(ClassType.Farm);
        runEngine(NeedleMouth, "yokaitreasure", "r2", "Right");

        string[] Quicksilver = {
            "J6's Backup Shotguns",
            "Raging Storm Blunderbuss",
            "Westion Commander's Backup Shotguns"
        };

        Core.EquipClass(ClassType.Farm);
        runEngine(Quicksilver, "yokaitreasure", "r3", "Left");
        #endregion

        #region Haku Village
        string[] DaiTengu = {
            "Elegant Changshan",
            "Hooded Loose Noble Topknot",
            "Loose Noble Topknot",
            "Noble Incognito Cloak"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(DaiTengu, "hakuvillage", "r5", "Left");

        string[] Nagami = {
            "Doom Katana",
            "Magma Katana",
            "Nagami Armor"
        };

        Core.EquipClass(ClassType.Farm);
        runEngine(Nagami, "hakuvillage", "r4", "Right");
        #endregion
    }

    private void runEngine(string[] rewards, string map, string cell, string pad)
    {
        foreach (string drop in rewards)
            Core.AddDrop(drop);

        for (int i = 0; i < rewards.Length; i++)
        {
            string drop = rewards[i];
            if (Core.CheckInventory(drop, toInv: false))
            {
                Core.Jump();
                Core.RemoveDrop(drop);
                Core.ToBank(drop);
                continue;
            }

            Core.Logger($"{drop}: ({i + 1}/{rewards.Length})");

            while (!Bot.ShouldExit && !Core.CheckInventory(drop))
                Core.KillMonster(map, cell, pad, "*", log: false);
        }
    }
}
