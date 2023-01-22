/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class StarFestival
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(8756))
            return;

        if (!Story.QuestProgression(8748))
        {
            Core.EnsureAccept(8748);
            // A Healer’s Wish 8748
            if (!Core.CheckInventory("Twilly Twig"))
            {
                Core.AddDrop("Twilly Twig");
                Core.EnsureAccept(11);
                Core.HuntMonster("farm", "Treeant", "Treeant Branch");
                Core.EnsureComplete(11);
                Bot.Wait.ForPickup("Twilly Twig");
            }
            Core.HuntMonster("brightoak", "Bright Treeant", "Brightest Branch", 6);
            Core.HuntMonster("farm", "Treeant", "Treant Leaf");
            Core.HuntMonster("guardiantree", "Blossoming Treeant", "Beautiful Blossom", 6);
            Core.HuntMonster("NibbleOn", "Mean Old Treeant", "Bitter Bark", 8);
            Core.EnsureComplete(8748);
        }

        // A Fishy Wish 8749
        if (!Story.QuestProgression(8749))
        {
            Core.EnsureAccept(8749);
            Core.HuntMonster("pirates", "Fishman Soldier", "Fish", isTemp: false);
            Core.HuntMonster("pirates", "Shark Bait", "Shark Bait Slice");
            Core.HuntMonster("pirates", "Fishwing", "Fish Wings", 8);
            Core.HuntMonster("Natatorium", "Anglerfish", "Anglerfish Filets", 7);
            Core.HuntMonster("river", "Kuro", "Kuro Kut");
            Core.EnsureComplete(8749);
        }

        // A Villain’s Wish 8750
        if (!Story.QuestProgression(8750))
        {
            Core.EnsureAccept(8750);
            Core.HuntMonster("dragonplane", "Earth Elemental", "Raw Elemental Earth", 4);
            Core.HuntMonster("dragonplane", "Fire Elemental", "Raw Elemental Fire", 2);
            Core.HuntMonster("dragonplane", "Water Elemental", "Raw Elemental Water", 8);
            Core.HuntMonster("gilead", "Mana Elemental", "Raw Elemental Mana", 3);
            Core.HuntMonster("poisonforest", "Bandit", "Carving Supplies", 10);
            Core.EnsureComplete(8750);
        }

        // A Child’s Wish 8751
        if (!Story.QuestProgression(8751))
        {
            Core.EquipClass(ClassType.Farm);
            Core.EnsureAccept(8751);
            Core.KillMonster($"battleunderb", "Enter", "Spawn", "*", "Bundle O’ Bones", 30);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonsterMapID($"Odokuro", 1, "Odokuro’s Occipital");
            Core.HuntMonster($"bonecastle", "Vaden", "Vaden’s Other Arm");
            Core.HuntMonster($"vordredboss", "Vordred", "Vordred’s Skull(s)", 3);
            Core.EnsureComplete(8751);
        }

        // A Leader’s Wish 8752
        if (!Story.QuestProgression(8752))
        {
            Core.EnsureAccept(8752);
            Core.HuntMonster("feverfew", "Major Thermas", "Major Thermas Pledged");
            Core.HuntMonster("fireforge", "Firestorm Scout", "Firestorm Pledges", 30);
            Core.HuntMonster("fireforge", "Firestorm General", "Firestorm General Pledged", 3);
            Core.HuntMonster("fireforge", "Firestorm Corporal", "Firestorm Corporal Pledged", 4);
            Core.HuntMonster("fireforge", "Firestorm Major", "Firestorm Major Pledged", 4);
            Core.EnsureComplete(8752);
        }

        // A Conqueror’s Wish 8753
        if (!Story.QuestProgression(8753))
        {
            Core.EnsureAccept(8753);
            Core.HuntMonster("Tercessuinotlim", "Tainted Elemental", "Tainted Essence Collected", 10);
            Core.HuntMonster("Tercessuinotlim", "Dark Makai", "Makai Essence Collected", 20);
            Core.HuntMonster("necrodungeon", "5 Headed Dracolich", "Dracolich Soul Collected", 15);
            Core.HuntMonster("necrodungeon", "SlimeSkull", "Necropolis Soul Collected", 15);
            Core.HuntMonster("necrodungeon", "Doom Overlord", "Doom Power Catalyst", 2);
            Core.EnsureComplete(8753);
        }

        // A Survivor’s Wish 8754
        if (!Story.QuestProgression(8754))
        {
            Core.EnsureAccept(8754);
            Core.HuntMonster("astraviajudge", "Hand", "Echo of Astravia", 22);
            Core.HuntMonster("astraviajudge", "La", "Righteous Requiem");
            Core.HuntMonster("theworld", "Ti", "Gentle Glissando");
            Core.HuntMonster("theworld", "Re", "Reckless Rhapsody");
            Adv.BoostHuntMonster($"theworld", "Encore Darkon", "Conductor’s Canata");
            Core.EnsureComplete(8754);
        }

        // A Chaotic Wish 8755
        if (!Story.QuestProgression(8755))
        {
            Core.EnsureAccept(8755);
            Bot.Quests.UpdateQuest(8094);
            Adv.BoostHuntMonster($"transformation", "Queen of Monsters", "Queen’s Remnant");
            Core.HuntMonster("transformation", "Chaos Spitter", "Terrestrial Chaos", 30);
            Core.HuntMonster($"dreadforest", "Lord Reignolds", "Traitor’s Remnant");
            Core.HuntMonster($"lagunabeach", "Heart of Chaos", "Heart’s Remnant");
            Core.HuntMonster($"lagunabeach", "Chaos Kelp", "Aquatic Chaos", 25);
            Core.EnsureComplete(8755);
        }

        // A Hero’s Wish 8756
        if (!Story.QuestProgression(8756))
        {
            Core.Join("starfest");
            Core.EnsureAccept(8756);
            Core.GetMapItem(1803, 1, "starfest");
            Core.EnsureComplete(8756);
        }
    }
}
