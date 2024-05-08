/*
name: Age Of Ruin Drops
description: farms all acs drops from Age Of Ruin saga maps.
tags: age, ruin, drops
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/AgeOfRuin/CoreAOR.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class AgeOfRuinDrops
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAOR AOR = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetDrops();

        Core.SetOptions(false);
    }

    public void GetDrops()
    {
        AOR.DoAll();

        #region Ashray
        string[] KitefinSharkBait = {
            "Midnight Eroder Blade"
        };

        Core.EquipClass(ClassType.Farm);
        runEngine(KitefinSharkBait, "ashray", "Enter", "Left");

        string[] SeafoamElemental = {
            "Crossed Midnight Cutlasses",
            "Dual Nightmare Hunter Pistol",
            "Holstered Midnight Eroder Pistols",
            "Midnight Back Cutlasses"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(SeafoamElemental, "ashray", "r9", "Left");

        string[] StagnantWater = {
            "Nightmare Hunter Pistol"
        };

        Core.EquipClass(ClassType.Farm);
        runEngine(StagnantWater, "ashray", "r7", "Left");
        #endregion

        #region Midnight Zone
        string[] Sparagmos = {
            "ERAD Arrows",
            "Princess Brittany Portrait",
            "Royal Dress Display",
            "Sleeping Monitor",
            "Sparagmos A.I. Tank",
            "Sparagmos Wires",
            "Undine Coffee Table",
            "Undine Visitor Badge",
            "Water Temple Pedestal"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(Sparagmos, "midnightzone", "r9", "Left");

        string[] UndeadPrisoner = {
            "Experimentation Chair",
            "Scattered Bones",
            "Undine Observation Lights"
        };

        Core.EquipClass(ClassType.Farm);
        runEngine(UndeadPrisoner, "midnightzone", "r4", "Left");
        #endregion

        #region Abyssal Zone
        string[] BlightedWater = {
            "Avatar of Water Mural",
            "Drowning Machine Mural",
            "Waves of Tumult"
        };

        Core.EquipClass(ClassType.Farm);
        runEngine(BlightedWater, "abyssalzone", "r4", "Left");

        string[] FoamScavenger = {
            "Ancient Fissure",
            "Devouring Sea Mural"
        };

        Core.EquipClass(ClassType.Farm);
        runEngine(FoamScavenger, "abyssalzone", "r7", "Left");

        string[] ShadowViscera = {
            "Champion Undine Mural",
            "Colossus Mural"
        };

        Core.EquipClass(ClassType.Farm);
        runEngine(ShadowViscera, "abyssalzone", "r5", "Left");

        string[] TheAshray = {
            "Ascending Kathool Tentacle",
            "Ashray Plaque",
            "Descending Kathool Tentacle"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(TheAshray, "abyssalzone", "r13", "Right");
        #endregion

        #region Trench Observatory
        string[] LadyNoelle = {
            "Ashray Trench Pedestal",
            "Kathoolian Arms",
            "Kathoolian Pinnacle",
            "Kathoolian Tentacles",
            "Monstrous Fa",
            "Monstrous Fa Morph"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(LadyNoelle, "trenchobserve", "r9", "Left");

        string[] NecroAdipocere = {
            "Brackish Coral"
        };

        Core.EquipClass(ClassType.Farm);
        runEngine(NecroAdipocere, "trenchobserve", "r8", "Left");

        string[] SeaSpirit = {
            "Ashray Leyline",
            "Corruptive Tentacle"
        };

        Core.EquipClass(ClassType.Farm);
        runEngine(SeaSpirit, "trenchobserve", "r6", "Left");

        string[] SeabaseTurret = {
            "Portable Sparagmos Table"
        };

        Core.EquipClass(ClassType.Farm);
        runEngine(SeabaseTurret, "trenchobserve", "r4", "Left");

        string[] VeneratedWraith = {
            "Broken Undine Window",
            "Colossal Ocean View"
        };

        Core.EquipClass(ClassType.Farm);
        runEngine(VeneratedWraith, "trenchobserve", "r2", "Left");
        #endregion

        #region Sea Voice
        string[] VoiceintheSea = {
            "Calamity Atlanticus Trident",
            "Glaucus Companion",
            "Glaucus Hair",
            "Glaucus Locks",
            "Glaucus Mystic"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(VoiceintheSea, "seavoice", "r2", "Left");
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
                Core.RemoveDrop(drop);
                continue;
            }

            Core.Logger($"{drop}: ({i + 1}/{rewards.Length})");

            while (!Bot.ShouldExit && !Core.CheckInventory(drop))
                Core.KillMonster(map, cell, pad, "*", log: false);
        }

        foreach (string drop in rewards)
        {
            Core.Jump();
            Core.ToBank(drop);
        }
    }
}
