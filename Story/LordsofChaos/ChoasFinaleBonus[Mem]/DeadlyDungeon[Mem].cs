/*
name: (Chaos Finale Bonus) Deadly Dungeon
description: This will finish the Deadly Dungeon quest.
tags: story, quest, chaos-saga, 13-lords-of-chaos, chaos-finale, deadly-dungeon, member
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;

public class DeadlyDungeon
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DeadlyDungeonQuest_Line();

        Core.SetOptions(false);
    }

    public void DeadlyDungeonQuest_Line()
    {
        if (Core.isCompletedBefore(3699))
            return;

        if (!Core.IsMember)
        {
            Core.Logger("You need to be a member for complete this questline.");
            return;
        }

        Story.PreLoad(this);

        // Floor 1
        Story.KillQuest(3680, "deadlydungeon", "Dire Muncher ");
        // Floor 2
        Story.KillQuest(3681, "deadlydungeon", "Hulking Dire Wolf");
        // Floor 3
        Story.KillQuest(3682, "deadlydungeon", "Undead Dungeon Crawler");
        // Floor 4
        Story.KillQuest(3683, "deadlydungeon", "Hulking Dire Wolf");
        // Floor 5
        Story.KillQuest(3684, "deadlydungeon", "Dire Draugr");
        Story.MapItemQuest(3684, "deadlydungeon", 2764, 1);
        // Floor 6
        Story.MapItemQuest(3685, "deadlydungeon", 2756, 8);
        Story.KillQuest(3685, "deadlydungeon", "Hulking Dire Wolf");
        // Floor 7
        Story.KillQuest(3686, "deadlydungeon", "Weeping Spyball");
        // Floor 8
        Story.KillQuest(3687, "deadlydungeon", "Dire Muncher ");
        // Floor 9
        Story.KillQuest(3688, "deadlydungeon", "Weeping Spyball");
        // Floor 10
        Story.KillQuest(3689, "deadlydungeon", "DoomKitten");
        Story.MapItemQuest(3689, "deadlydungeon", 2766, 1);
        // Floor 11
        Story.KillQuest(3690, "deadlydungeon", "Dire Draugr");
        Story.MapItemQuest(3690, "deadlydungeon", 2765, 1);
        // Floor 12
        Story.KillQuest(3691, "deadlydungeon", "Giant Dungeon Spider");
        // Floor 13
        Story.KillQuest(3692, "deadlydungeon", "Undead Dungeon Crawler");
        // Floor 14
        Story.KillQuest(3693, "deadlydungeon", "Giant Dungeon Spider");
        // Floor 15
        Story.KillQuest(3694, "deadlydungeon", "Dire Draugr");
        // Floor 16
        Story.KillQuest(3695, "deadlydungeon", "Dire Draugr");
        // Floor 17
        Story.KillQuest(3696, "deadlydungeon", "DoomKitten");
        Story.MapItemQuest(3696, "deadlydungeon", 2767, 1);
        // Floor 18
        Story.KillQuest(3697, "deadlydungeon", "Giant Dungeon Spider");
        // Floor 19

        if (!Story.QuestProgression(3698))
        {
            Core.EnsureAccept(3698);
            Quest? quest = Bot.Quests.EnsureLoad(3698);

            if (quest != null)
            {
                ItemBase? Item = quest.Requirements.FirstOrDefault();

                if (Item != null)
                {
                    if (Core.CheckInventory(Item.ID, 100))
                        Core.SellItem(Item.Name, 1);

                    Core.HuntMonster("deadlydungeon", "Dire Draugr", Item.Name, 100, isTemp: false);
                    Core.EnsureComplete(3698);
                }
                else
                    Core.Logger($"Quest {quest.Name} returned no requirements.");
            }
            else
                Core.Logger("Quest could not be loaded.");
        }


        // Floor 20
        Story.KillQuest(3699, "deadlydungeon", "Chest Guardian");

    }
}
