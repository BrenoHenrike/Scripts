/*
name: Blood Isles Story
description: This will finish the Blood Isles story.
tags: BLood, isles, Blood Isles, story, tlapd
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class BloodIsles
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoStory();

        Core.SetOptions(false);
    }

    public void DoStory()
    {
        #region Useable Monsters
        string[] UseableMonsters = new[]
        {
            "Blood Moon Pirate", // UseableMonsters[0],
            "Drowned Werewolf", // UseableMonsters[1],
            "Vampiric Lamprey", // UseableMonsters[2],
            "Drowned Vampire", // UseableMonsters[3],
            "Blood Veil Pirate", // UseableMonsters[4],
            "Drowned Horde", // UseableMonsters[5],
            "Bloodthirsty Bonnie", // UseableMonsters[6],
            "Sea King Kurok", // UseableMonsters[7],
            "Merpyre", // UseableMonsters[8]
            "Feral Flintfang", // UseableMonsters[9]
        };
        #endregion Useable Monsters

        if (Core.isCompletedBefore(9885) || !Core.isSeasonalMapActive("piratebloodhub"))
            return;

        Story.PreLoad(this);

        #region piratevampire
        Core.EquipClass(ClassType.Farm);
        if (!Bot.Quests.IsUnlocked(9870))
        {
            Core.EnsureAccept(9868);
            Core.KillMonster("piratevampire", "r2", "left", "*", "Vampire Pirate Medal", 5);
            Core.EnsureComplete(9868);
        }

        // 9870 | Siren's Pack
        Story.KillQuest(9870, "piratevampire", UseableMonsters[1]);

        // 9871 | Bitterfang
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(9871, "piratevampire", UseableMonsters[9], AutoCompleteQuest: false);
        #endregion

        #region BloodIsles
        Core.EquipClass(ClassType.Farm);
        // 9876 | Faceless Moon 
        Story.KillQuest(9876, "bloodisles", UseableMonsters[0]);

        // 9877 | Apex Prey
        Story.KillQuest(9877, "bloodisles", UseableMonsters[1]);

        // 9878 | False Triton
        Story.KillQuest(9878, "bloodisles", UseableMonsters[2]);
        Story.MapItemQuest(9878, "bloodisles", 13655);

        // 9879 | Blood Moonglade
        Story.KillQuest(9879, "bloodisles", UseableMonsters[3]);

        // 9880 | Aye Eye
        Story.KillQuest(9880, "bloodisles", UseableMonsters[4]);
        Story.MapItemQuest(9880, "bloodisles", 13656);

        // 9881 | Fang Profile
        Story.KillQuest(9881, "bloodisles", UseableMonsters[5]);
        Story.MapItemQuest(9881, "bloodisles", 13657);

        // 9882 | Necro-Putrefaction
        Story.KillQuest(9882, "bloodisles", new[] { UseableMonsters[2], UseableMonsters[5] });

        Core.EquipClass(ClassType.Solo);
        // 9883 | Counterpirate
        Story.KillQuest(9883, "bloodisles", UseableMonsters[6], AutoCompleteQuest: false);

        // 9884 | Abyssal Alpha
        Story.KillQuest(9884, "bloodisles", UseableMonsters[7], AutoCompleteQuest: false);

        // 9885 | Nyx Oceana
        Story.KillQuest(9885, "bloodisles", UseableMonsters[8], AutoCompleteQuest: false);

        #endregion
    }






}
