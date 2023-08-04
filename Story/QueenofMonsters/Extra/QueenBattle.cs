/*
name: Queen Battle (Extra)
description: This will finish the Queen Battle quest.
tags: story, quest, queen-of-monsters, queen-battle, extra
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/OrbHunt.cs
using Skua.Core.Interfaces;

public class QueenBattle
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public OrbHunt OrbHunt = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        OrbHunt.OrbHuntSaga();

        if (Core.isCompletedBefore(8361))
            return;

        Story.PreLoad(this);

        //Forces of Chaorruption 8350
        Story.KillQuest(8350, "queenbattle", "Chaorruption");

        //Phantom Chaos 8351
        Story.KillQuest(8351, "queenbattle", "Chaos Ghost");

        //Spies of the Queen 8352
        Story.KillQuest(8352, "queenbattle", "Chaos Sp-eye");

        //Portal of Chaos 8353
        Story.MapItemQuest(8353, "queenbattle", 9204);
        Story.KillQuest(8353, "queenbattle", "Chaos Ghost");

        //Slain Children 8354
        if (!Story.QuestProgression(8354))
        {
            Core.EnsureAccept(8354);
            Core.HuntMonster("queenbattle", "Extriki Shade", "Extriki Shade Banished", log: false);
            Core.HuntMonster("queenbattle", "Kolyaban Shade", "Kolyaban Shade Banished", log: false);
            Core.HuntMonster("queenbattle", "Horothotep Shade", "Horothotep Shade Banished", log: false);
            Core.HuntMonster("queenbattle", "Sa-Laatan Shade", "Sa-Laatan Shade Banished", log: false);
            Core.HuntMonster($"queenbattle", "Grou'luu Shade", "Grou'luu Shade Banished", log: false);
            Core.EnsureComplete(8354);
        }

        //Dragons Felled By Chaos 8355
        Story.KillQuest(8355, "queenbattle", "Chaos Dracolich");

        //The Queen's Generals 8356
        Story.KillQuest(8356, "queenbattle", "Chaos General");

        //Portal of Chaos II 8357
        if (!Story.QuestProgression(8357))
        {
            Core.EnsureAccept(8357);
            Core.HuntMonster("queenbattle", "Chaos General", "Potent Chaotic Energy", 12, log: false);
            Story.MapItemQuest(8357, "queenbattle", 9205);
        }

        //Guilt of the Past 8358
        Story.KillQuest(8358, "queenbattle", "Chaotic Guilt");

        //A Giant of Chaos 8359
        Story.KillQuest(8359, "queenbattle", "Chaos Giant");

        //The First Champion of Chaos 8360
        if (!Story.QuestProgression(8360))
        {
            Core.EnsureAccept(8360);
            Core.HuntMonster("queenbattle", "Proto Chaos Champion", "Proto Chaos Champion Defeated", log: false);
            Core.EnsureComplete(8360);
        }

        //The Queen of Monsters 8361
        if (!Story.QuestProgression(8361))
        {
            Core.EnsureAccept(8361);
            Core.HuntMonster("queenbattle", "Queen of Monsters", "Queen of Monsters Sealed", log: false);
            Core.EnsureComplete(8361);
        }
    }
}
