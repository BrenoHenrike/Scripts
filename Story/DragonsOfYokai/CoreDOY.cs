/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CoreDOY
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void DoAll()
    {
        YokaiPirate();
        YokaiTreasure();
        HakuVillage();
        HakuWar();
        YokaiPortal();
        YokaiRealm();
        NovaShrine();
    }

    public void YokaiPirate()
    {
        if (Core.isCompletedBefore(9387))
            return;

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Farm);

        // Wokou 9378
        Story.MapItemQuest(9378, "yokaipirate", new[] { 12133, 12134, 12135 });

        // Shanty Sha-N-Ti 9379
        Story.KillQuest(9379, "yokaipirate", "Disguised Pirate");

        // Yokai's Ark 9380
        if (!Story.QuestProgression(9380))
        {
            Core.EnsureAccept(9380);
            Core.HuntMonster("yokaipirate", "Serpent Warrior", "Serpent Badge", 7);
            Story.MapItemQuest(9380, "yokaipirate", new[] { 12136, 12137 });
        }

        // Fashion Fathoms 9381
        Story.KillQuest(9381, "yokaipirate", "Disguised Pirate");

        // Hoo Wants a Cracker 9382
        Story.KillQuest(9382, "yokaipirate", "Noble Owl");

        // Papers Please 9383
        Story.MapItemQuest(9383, "yokaipirate", 12138, 7);

        Core.EquipClass(ClassType.Farm);
        // King and Coral Snakes 9384
        if (!Story.QuestProgression(9384))
        {
            Core.EnsureAccept(9384);
            Core.HuntMonster("yokaipirate", "Disguised Pirate", "Pirate Interrogated", 6);
            Core.HuntMonster("yokaipirate", "Serpent Warrior", "Warrior Interrogated", 6);
            Story.MapItemQuest(9384, "yokaipirate", 12139);
        }

        // Horizon's Green Flash 9385
        Story.KillQuest(9385, "yokaipirate", "Noble Owl");

        // Highly Buoyant Metal Armor 9386
        if (!Story.QuestProgression(9386))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(9386);
            Core.HuntMonsterMapID("yokaipirate", 1, "Knight Captured", 8);
            Core.EnsureComplete(9386);
        }

        // Salty Roots 9387
        if (!Story.QuestProgression(9387))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(9387);
            Core.HuntMonsterMapID("yokaipirate", 11, "Neverglades Lord Dueled");
            Core.EnsureComplete(9387);
        }
    }

    public void YokaiTreasure()
    {
        if (Core.isCompletedBefore(9405))
            return;

        YokaiPirate();

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Farm);

        // All Hands Report (9396)
        Story.MapItemQuest(9396, "yokaitreasure", new[] { 12162, 12163, 12164 });

        // Starving Ghosts (9397)
        Story.KillQuest(9397, "yokaitreasure", "Needle Mouth");

        // Sudden Striker (9398)
        Story.KillQuest(9398, "yokaitreasure", "Quicksilver");

        // Onmyoji Maiden (9399)
        Story.MapItemQuest(9399, "yokaitreasure", new[] { 12165, 12166 });

        // Proper Parley (9400)
        Story.KillQuest(9400, "yokaitreasure", "Imperial Warrior");

        // Hunger Pains (9401)
        Story.KillQuest(9401, "yokaitreasure", "Needle Mouth");

        // Terror-cotta Warriors (9402)
        Story.KillQuest(9402, "yokaitreasure", "Imperial Warrior");

        // Indoor Fireworks (9403)
        Story.MapItemQuest(9403, "yokaitreasure", 12167, 4);

        // Sons of Biscuit Eaters (9404)
        Story.KillQuest(9404, "yokaitreasure", new[] { "Needle Mouth", "Imperial Warrior" });

        // Pearl of my Heart (9405)
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(9405, "yokaitreasure", "Admiral Zheng");
        Core.EquipClass(ClassType.Farm);
    }


    public void HakuVillage()
    {
        if (Core.isCompletedBefore(9599))
            return;

        YokaiTreasure();

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Farm);

        // 99th Birthday (9590)
        Story.KillQuest(9590, "hakuvillage", "Tsukumogami");

        // Red and Blue (9591)
        Story.KillQuest(9591, "hakuvillage", "Mountain Oni");
        Story.MapItemQuest(9591, "hakuvillage", 12706);

        // Cold Sunshower (9592)
        Story.KillQuest(9592, "hakuvillage", "Tsukumogami");
        Story.MapItemQuest(9592, "hakuvillage", 12707);

        // Inari Boom (9593)
        Story.MapItemQuest(9593, "hakuvillage", new[] { 12708, 12709 });
        Story.KillQuest(9593, "hakuvillage", "Kitsune Spy");

        // Cui Niao (9594)
        Story.KillQuest(9594, "hakuvillage", "Nagami");
        Story.MapItemQuest(9594, "hakuvillage", 12710);

        // Huli Jing (9595)
        Story.KillQuest(9595, "hakuvillage", "Kitsune Spy");

        // Aka Oni Ao Oni (9596)
        Story.KillQuest(9596, "hakuvillage", "Mountain Oni");

        // Stone Shangyuan Fest (9597)
        Story.KillQuest(9597, "hakuvillage", "Tsukumogami");

        // Nagami Sashimi (9598)
        Story.KillQuest(9598, "hakuvillage", "Nagami");

        // Divine Derision (9599)
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(9599, "hakuvillage", "Dai Tengu");
    }

    public void HakuWar()
    {
        if (Core.isCompletedBefore(9607))
            return;

        HakuVillage();

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Farm);

        // Haku Medal (9601)
        Story.KillQuest(9601, "hakuwar", "Dark Zmey");

        // Tall Tails (9603)
        Story.KillQuest(9603, "hakuwar", "Kitsune Spy");

        // Flightless (9604)
        Story.KillQuest(9604, "hakuwar", "Dark Zmey");

        // NorthEast Ushitora (9605)
        Story.KillQuest(9605, "hakuwar", "Mountain Oni");

        // Zmey Gorynich (9606)
        Story.KillQuest(9606, "hakuwar", "Zmey Warrior");

        // Head Kukol'nyy (9607)
        if (!Core.isCompletedBefore(9607))
        {
            Core.EquipClass(ClassType.Solo);
            Story.KillQuest(9607, "hakuwar", "Zakhvatchik");
        }
    }

    public void YokaiPortal()
    {
        if (Core.isCompletedBefore(9676))
            return;

        HakuWar();

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Farm);

        // Tense Reunion (9667)
        Story.MapItemQuest(9667, "yokaiportal", new[] { 12982, 12983, 12984 });

        // Kimon (9668)
        Story.KillQuest(9668, "yokaiportal", "Oni Spirits");

        // Shrine of Love (9669)
        Story.MapItemQuest(9669, "yokaiportal", new[] { 12985, 12986 });

        // Hoshi no Tama (9670)
        Story.KillQuest(9670, "yokaiportal", "Kitsune Spirits");

        // Childish Nostalgia (9671)
        Story.MapItemQuest(9671, "yokaiportal", new[] { 12987, 12988 });

        // Simple Wishes (9672)
        Story.KillQuest(9672, "yokaiportal", "Snake Shikigami");
        Story.MapItemQuest(9672, "yokaiportal", new[] { 12989, 12995, 12990 });

        // Left Behind (9673)
        Story.KillQuest(9673, "yokaiportal", "Puppeted Dragonling");

        // Our Love (9674)
        Story.MapItemQuest(9674, "yokaiportal", new[] { 12991, 12992, 12993 });

        // Amano Iwato (9675)
        Story.KillQuest(9675, "yokaiportal", new[] { "Kitsune Spirits", "Puppeted Dragonling" });
        Story.MapItemQuest(9675, "yokaiportal", 12994);

        // Corrupted Protector (9676)
        if (!Core.isCompletedBefore(9676))
        {
            Core.EquipClass(ClassType.Solo);
            Story.KillQuest(9676, "yokaiportal", "Kitsune Kukol'nyy");
        }
    }

    public void YokaiRealm()
    {
        if (Core.isCompletedBefore(9689))
            return;

        YokaiPortal();

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Farm);

        // Tail End (9680)
        Story.MapItemQuest(9680, "yokairealm", new[] { 13036, 13037 });
        Story.KillQuest(9680, "yokairealm", "Snake Shikigami");

        // A Thousand Hills (9681)
        Story.MapItemQuest(9681, "yokairealm", new[] { 13038, 13039 });
        Story.KillQuest(9681, "yokairealm", "Snake Shikigami");

        // Mount the Land and Skies (9682)
        Story.MapItemQuest(9682, "yokairealm", new[] { 13040, 13041 });
        Story.KillQuest(9682, "yokairealm", "Puppeted Dragonling");

        // Distant Shores (9683)
        Story.MapItemQuest(9683, "yokairealm", new[] { 13042, 13043 });
        Story.KillQuest(9683, "yokairealm", "Dark Zmey");

        // Taizi (9684)
        Story.MapItemQuest(9684, "yokairealm", 13044);
        Story.KillQuest(9684, "yokairealm", new[] { "Puppeted Dragonling", "Dark Zmey" });

        // Begrudged Oni (9685)
        Story.MapItemQuest(9685, "yokairealm", new[] { 13045, 13046 });
        Story.KillQuest(9685, "yokairealm", "Oni Spirits");

        // Lingering Discord (9686)
        Story.MapItemQuest(9686, "yokairealm", 13047);
        Story.KillQuest(9686, "yokairealm", "Kitsune Spirits");

        // Yokai's Cycle (9687)
        Story.KillQuest(9687, "yokairealm", new[] { "Oni Spirits", "Kitsune Spirits" });

        // Kojutsu (9688)
        if (!Story.QuestProgression(9688))
        {
            Core.EquipClass(ClassType.Solo);
            Story.KillQuest(9688, "yokairealm", "Inugami");
        }

        // Ame no Murakumo (9689)
        if (!Story.QuestProgression(9689))
        {
            Core.EquipClass(ClassType.Solo);
            Story.KillQuest(9689, "yokairealm", "Mikoto Kukol'nyy");
        }
    }

    public void NovaShrine()
    {
        if (Core.isCompletedBefore(9802))
            return;

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Farm);

        // Byakko 9798
        Story.KillQuest(9798, "lavarun", "Firestorm Tiger");

        // Suzaku 9799
        Story.KillQuest(9799, "dreampalace", "Flaming Harpy");

        // Genbu 9800
        if (!Story.QuestProgression(9800))
        {
            Core.EnsureAccept(9800);
            Core.KillMonster("yokaitreasure", "r3", "Left", "Quicksilver", "Silver Scales", 15);
            Core.KillMonster("hakuvillage", "r4", "Left", "Nagami", "Nagami Scales", 15);
            Core.EnsureComplete(9800);
        }

        // Seiryu 9801
        if (!Story.QuestProgression(9801))
        {
            Bot.Quests.UpdateQuest(9607);
            Story.KillQuest(9801, "hakuwar", new[] { "Zmey Warrior", "Zakhvatchik" });
        }
        // Hoshiyoru 9802
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(9802, "novashrine", "Nova Empyrean");
    }

}
