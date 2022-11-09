//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class NecroDungeon
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        NecrodungeonStoryLine();

        Core.SetOptions(false);
    }

    public void NecrodungeonStoryLine()
    {
        if (Core.isCompletedBefore(2077))
        {
            Core.Logger($"Story: Necro Dungeon - Complete");
            return;
        }

        Story.PreLoad(this);


        //descent into darkness - 2044
        if (!Story.QuestProgression(2044))
        {
            Core.EnsureAccept(2044);
            Core.JumpWait();
            Core.KillMonster("necrodungeon", "r6", "Down", "*", "1 Floor Descended", 10);
            Core.EnsureComplete(2044);
        }

        //retrieve the past, room 1 - 2045
        Story.MapItemQuest(2045, "necrodungeon", 1001);
        Story.KillQuest(2045, "necrodungeon", "Necropolis Soldier");

        //retrieve the past, room 2 - 2046
        Story.MapItemQuest(2046, "necrodungeon", 1015, 5);
        Story.MapItemQuest(2046, "necrodungeon", 1002);

        //retrieve the past, room 3 - 2047
        Story.MapItemQuest(2047, "necrodungeon", 1003);
        Story.KillQuest(2047, "necrodungeon", "SlimeSkull");

        //retrieve the past, room 4 - 2048
        Story.MapItemQuest(2048, "necrodungeon", 1004);
        Story.KillQuest(2048, "necrodungeon", new[] { "Necropolis Soldier", "Ghoul" });

        //deeper into darkness - 2049
        if (!Story.QuestProgression(2049))
        {
            Core.EnsureAccept(2049);
            Core.KillMonster("necrodungeon", "r11", "Down", "*", "1 Floor Descended", 10);
            Core.JumpWait();
            Core.EnsureComplete(2049);
        }

        //retrieve the past, room 5 - 2050
        Story.MapItemQuest(2050, "necrodungeon", 1005);
        Story.MapItemQuest(2050, "necrodungeon", 1016, 3);
        Story.KillQuest(2050, "necrodungeon", new[] { "SlimeSkull", "Necropolis Soldier" });

        //retrieve the past, room 6 - 2051
        Story.MapItemQuest(2051, "necrodungeon", 1017, 5);
        Story.MapItemQuest(2051, "necrodungeon", 1006);

        //retrieve the past, room 7 - 2052
        Story.MapItemQuest(2052, "necrodungeon", 1007);
        Story.KillQuest(2052, "necrodungeon", "SlimeSkull");

        //monter subway ahead - 2053
        Story.MapItemQuest(2053, "necrodungeon", 1008);
        Story.KillQuest(2053, "necrodungeon", "Doom Crawler");

        //underground railroad to... DOOM! - 2054
        Story.KillQuest(2054, "necrodungeon", "Ghoul");
        Story.MapItemQuest(2054, "necrodungeon", 1009);
        Story.MapItemQuest(2054, "necrodungeon", 1018, 8);

        //retrieve the past, room 10 - 2055
        Story.MapItemQuest(2055, "necrodungeon", 1010);
        Story.KillQuest(2055, "necrodungeon", "Necropolis Soldier");

        //the deepest descent - 2056
        if (!Story.QuestProgression(2056))
        {
            Core.EnsureAccept(2056);
            Core.JumpWait();
            Core.KillMonster("necrodungeon", "r18", "Down", "*", "1 Floor Descended", 10);
            Core.EnsureComplete(2056);
        }

        //retrieve the past, room 11 - 2057
        Story.MapItemQuest(2057, "necrodungeon", 1016, 5);
        Story.MapItemQuest(2057, "necrodungeon", 1011);

        //retrieve the past, room 12 - 2058
        Story.MapItemQuest(2058, "necrodungeon", 1012);
        Story.KillQuest(2058, "necrodungeon", "Necropolis Soldier");

        //retrieve the past, room 13 - 2059
        Story.MapItemQuest(2059, "necrodungeon", 1019, 5);
        Story.MapItemQuest(2059, "necrodungeon", 1013);

        //five times the fury - 2060
        if (!Bot.Quests.IsUnlocked(2061))
        {
            Core.EnsureAccept(2060);
            Core.KillMonster("necrodungeon", "r22", "Down", 889);
            Core.KillMonster("necrodungeon", "r22", "Down", 890);
            Core.KillMonster("necrodungeon", "r22", "Down", 891);
            Core.KillMonster("necrodungeon", "r22", "Down", 892);
            Core.KillMonster("necrodungeon", "r22", "Down", 893);
            Core.EnsureComplete(2060);
        }

        //the past will haunt you - 2061
        Story.MapItemQuest(2061, "necrodungeon", 1020);

        // [[[ Necrocavern Map]]]

        //Thou Shalt Not Pass 2070
        Story.KillQuest(2070, "necrocavern", "Shadowstone Elemental");

        //Blinded by the Darkness 2071
        Story.MapItemQuest(2071, "necrocavern", 1042, 6);
        Story.KillQuest(2071, "necrocavern", "Shadow Imp");

        //The Tale Never Dies 2072
        Story.MapItemQuest(2072, "necrocavern", 1044);
        Story.MapItemQuest(2072, "necrocavern", 1045, 3);
        Story.KillQuest(2072, "necrocavern", new[] { "Shadowstone Elemental", "Shadow Imp" });

        //Doom Outside the Dome 2073
        Story.KillQuest(2073, "necrocavern", "Shadowstone Elemental");

        //Last Bastion of Light 2074
        Story.KillQuest(2074, "necrocavern", new[] { "Shadowstone Elemental", "Shadow Imp" });

        //Shadowy Corruption 2075
        Story.MapItemQuest(2075, "necrocavern", 1043, 5);

        //Strength of the Darkness 2076
        Story.KillQuest(2076, "necrocavern", "Shadow Dragon");

        //Bring Down the Necropolis 2077
        Story.KillQuest(2077, "necrocavern", "Shadowstone Support");
    }
}