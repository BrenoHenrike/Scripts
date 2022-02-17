//cs_include Scripts/CoreBots.cs
using RBot;
public class DeadlyDungeon
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        DeadlyDungeonQuest_Line();

        Core.SetOptions(false);
    }

    public void DeadlyDungeonQuest_Line()
    {
        if (Core.isCompletedBefore(3699))
            return;

        Core.EquipClass(ClassType.Solo);

        if (!Core.IsMember)
            Core.Logger("You need to be a member for complete this questline.", messageBox: true, stopBot: true);
        else
            // Floor 1
            Core.KillQuest(3680, "deadlydungeon", "Dire Muncher ");
        // Floor 2
        Core.KillQuest(3681, "deadlydungeon", "Hulking Dire Wolf");
        // Floor 3
        Core.KillQuest(3682, "deadlydungeon", "Undead Dungeon Crawler");
        // Floor 4
        Core.KillQuest(3683, "deadlydungeon", "Hulking Dire Wolf");
        // Floor 5
        Core.KillQuest(3684, "deadlydungeon", "Dire Draugr");
        Core.MapItemQuest(3684, "deadlydungeon", 2764, 1);
        // Floor 6
        Core.MapItemQuest(3685, "deadlydungeon", 2756, 8);
        Core.KillQuest(3685, "deadlydungeon", "Hulking Dire Wolf");
        // Floor 7
        Core.KillQuest(3686, "deadlydungeon", "Weeping Spyball");
        // Floor 8
        Core.KillQuest(3687, "deadlydungeon", "Dire Muncher ");
        // Floor 9
        Core.KillQuest(3688, "deadlydungeon", "Weeping Spyball");
        // Floor 10
        Core.KillQuest(3689, "deadlydungeon", "DoomKitten");
        Core.MapItemQuest(3689, "deadlydungeon", 2766, 1);
        // Floor 11
        Core.KillQuest(3690, "deadlydungeon", "Dire Draugr");
        Core.MapItemQuest(3690, "deadlydungeon", 2765, 1);
        // Floor 12
        Core.KillQuest(3691, "deadlydungeon", "Giant Dungeon Spider");
        // Floor 13
        Core.KillQuest(3692, "deadlydungeon", "Undead Dungeon Crawler");
        // Floor 14
        Core.KillQuest(3693, "deadlydungeon", "Giant Dungeon Spider");
        // Floor 15
        Core.KillQuest(3694, "deadlydungeon", "Dire Draugr|DoomKitten|Giant Dungeon Spider|Undead Dungeon Crawler");
        // Floor 16
        Core.KillQuest(3695, "deadlydungeon", "Dire Draugr|DoomKitten|Giant Dungeon Spider|Undead Dungeon Crawler");
        // Floor 17
        Core.KillQuest(3696, "deadlydungeon", "DoomKitten");
        Core.MapItemQuest(3696, "deadlydungeon", 2767, 1);
        // Floor 18
        Core.KillQuest(3697, "deadlydungeon", "Giant Dungeon Spider|Hulking Dire Wolf|Undead Dungeon Crawler|Weeping Spyball");
        // Floor 19
        Core.KillQuest(3698, "deadlydungeon", "Dire Draugr|Dire Muncher ");
        // Floor 20
        Core.KillQuest(3699, "deadlydungeon", "Chest Guardian");
    }
}
