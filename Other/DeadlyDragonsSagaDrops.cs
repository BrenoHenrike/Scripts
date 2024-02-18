/*
name: Seven Deadly Dragons Saga Drops
description: get drops from all monsters from this saga.
tags: deadly, seven, dragons, drops
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class DeadlyDragonsDrops
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public Core7DD DD = new();
    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetDrops();

        Core.SetOptions(false);
    }

    public void GetDrops()
    {
        DD.Complete7DD();

        #region Ashfallcamp
        string[] Smoldur = {
            "Smoldur's Wings"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(Smoldur, "ashfallcamp", "r8", "Left");

        string[] Infernus = {
            "Infernus' Wings"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(Infernus, "ashfallcamp", "r14", "Left");

        string[] Blackrawk = {
            "Blackrawk's Wings"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(Blackrawk, "ashfallcamp", "r11", "Left");
        #endregion

        #region Gluttony
        string[] DeflatedGlutus = {
            "Burning Brand Banners Cape",
            "Burning Ember and Mace",
            "Dragonfang Piercer",
            "FlameScourge Helmet",
            "FlameScourge Hood",
            "FlameScourge Horns",
            "FlameScourge Mage",
            "FlameScourge Warrior"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(DeflatedGlutus, "gluttony", "Enter2", "Left");

        string[] Colonicus = {
            "Colonicus Club",
            "Colonicus' Wing Dagger",
            "Feceus Morph"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(Colonicus, "gluttony", "r12", "Left");
        #endregion

        #region Pride
        string[] CellarGuard = {
            "Cellar Guard's Storm Mace",
            "Drakel Guard's Prod",
            "Drakel Guard Morph",
            "Storm Drakel Tail"
        };

        Core.EquipClass(ClassType.Farm);
        runEngine(CellarGuard, "pride", "r5", "Right");

        string[] EliteGuard = {
            "Elite Guard's Scythe"
        };

        Core.EquipClass(ClassType.Farm);
        runEngine(EliteGuard, "pride", "r9", "Left");

        string[] Valsarian = {
            "BoltStriker Armor",
            "BoltStriker Blade",
            "BoltStriker Cape",
            "BoltStriker Claymore",
            "BoltStriker Hood",
            "BoltStriker Horn",
            "BoltStriker Wings",
            "LightningStorm Dragonoid"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(Valsarian, "pride", "r13", "Left");
        #endregion

        #region Greed
        string[] Goregold = {
            "Baby Ice Dragon",
            "Dragon's Treasure Pile",
            "Miniature Goregold Pet",
            "Sneezy the Ice Dragonoid",
            "WardKeeper's Armor",
            "Ward of the Ancients",
            "WardKeeper's War Axe"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(Goregold, "greed", "r16", "Left");

        string[] TreasurePile = {
            "Bright Keeper's Hood",
            "Bright Keeper's Locks",
            "Golden Ore Elemental",
            "Golden Reaper Helm",
            "Icy Dragonoid",
            "Keeper's Hood",
            "Keeper's Mask",
            "Magnifying Glass",
            "Maiden's Aegis Helm",
            "Treasure Chest (Misc)",
            "Warden's Aegis Helm",
            "WardKeeper's Blade",
            "WardKeeper's Double-edged Blade"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(TreasurePile, "greed", "r16", "Left");
        #endregion

        #region Sloth
        string[] ActualSlothDragon = {
            "Actual Slothagon Pet",
            "Baby Slothagon Pet"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(ActualSlothDragon, "sloth", "r13c", "Left");

        string[] CuredPhlegnn = {
            "Bloodborne Death's Shadow Cape"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(CuredPhlegnn, "sloth", "r13b", "Left");

        string[] MutatedPlague = {
            "Mutated Plague Cape",
            "Plague's Death Shadow Cape"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(MutatedPlague, "sloth", "r11c", "Left");

        string[] Phlegnn = {
            "Dragon's Plague Scythe",
            "Re-Shroom Mace",
            "Slimed Dragonoid"
        };

        Core.EquipClass(ClassType.Farm);
        runEngine(Phlegnn, "sloth", "r13a", "Left");
        #endregion

        #region Lust
        string[] KillekDeadChewer = {
            "Cursed Dungeon Spirit",
            "Dungeon Defender",
            "Dungeon Guard's Blade",
            "Dungeon Guard Hood",
            "Guard's Shag"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(KillekDeadChewer, "lust", "r16", "Left");

        string[] Lascivia = {
            "Blood Paladin",
            "Blood Paladin Hair",
            "Blood Paladin Wings",
            "Cecily's Crown",
            "Evolved Agony Chain",
            "Lascivian Dragonoid",
            "Lascivia in your House",
            "Royal Attendant",
            "Royal Attendant Beard",
            "Royal Attendant Cape",
            "Royal Attendant Locks"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(Lascivia, "lust", "r14", "Bottom");

        string[] Viscyra = {
            "Royal Dancer",
            "Royal Dancer Morph",
            "Viscyra's Axe"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(Viscyra, "lust", "r13", "Left");
        #endregion

        #region Envy
        string[] Argo = {
            "Dark Draco Tail",
            "Draco Tenebris",
            "Draco Tenebris Helm",
            "Draco Tenebris Hood",
            "Draco Tenebris Horns",
            "Draco Tenebris Flail",
            "Draco Tenebris Scythe",
            "Dual Tenebris Flails",
            "Tenebris Hood And Horns",
            "Wings of Darkness"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(Argo, "dragoncrown", "r2a", "Left");
        #endregion

        #region Wrath
        string[] Gorgorath = {
            "Boneaxe of Gorgorath",
            "Boneblade of Gorgorath",
            "Bone Wings of Gorgorath",
            "DracoMori",
            "Dragonslayer Slayer",
            "Dragonslayer Slayer Backbeast",
            "Dragonslayer Slayer Hair",
            "Dragonslayer Slayer Head",
            "Dragonslayer Slayer Helm",
            "Dragonslayer Slayer Locks",
            "Dragonslayer Slayer Mask",
            "Dragonslayer Slayer Tail",
            "Dragonslayer Slayer Wings",
            "Dragonslayer Slayer Wings + Tail",
            "Dragonslayers Dread",
            "Gorgorath's Head",
            "Hooded Horns of Gorgorath",
            "Horns of Gorgorath",
            "Mane of Gorgorath",
            "Reversed Dragonslayer's Demise",
            "Shadow Eater of Wrath",
            "Skull Scarf of Gorgorath",
            "Tail of Gorgorath"
        };

        Core.EquipClass(ClassType.Solo);
        runEngine(Gorgorath, "wrath", "r12", "Left");
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
