//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class RavenlossSaga
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        if (Core.isCompletedBefore(3460))
            return;

        TwilightEdge();
        WeaverWar();
        Ravenloss();
    }

    public void TwilightEdge()
    {
        if (Core.isCompletedBefore(3428))
            return;

        //Clear Twilight’s Edge 3428
        Story.MapItemQuest(3428, "TwilightEdge", 2577);
    }

    public void WeaverWar()
    {
        if (Core.isCompletedBefore(3439))
            return;

        Story.PreLoad(this);

        //Cleanse Swordhaven 3429
        if (!Story.QuestProgression(3429))
        {
            Core.EnsureAccept(3429);
            Core.GetMapItem(2578, 1, "WeaverWar");
            Core.KillMonster("WeaverWar", "s1", "Spawn", "Weaver Queen's Hound", "Weaver Hounds Slain", 6);
            Core.EnsureComplete(3429);
        }

        //Cleanse Faerie Forest 3430
        if (!Story.QuestProgression(3430))
        {
            Core.EnsureAccept(3430);
            Core.GetMapItem(2579, 1, "WeaverWar");
            Core.KillMonster("WeaverWar", "f1", "Spawn", "ChaosWeaver Mage", "Spider Slain", 6);
            Core.EnsureComplete(3430);
        }

        //Cleanse DarkoviaGrave 3431
        if (!Story.QuestProgression(3431))
        {
            Core.EnsureAccept(3431);
            Core.GetMapItem(2580, 1, "WeaverWar");
            Core.KillMonster("WeaverWar", "d1", "Spawn", "Dread Arysa", "Arysa Slain", 6);
            Core.EnsureComplete(3431);
        }

        //Cleanse Doomwood Forest 3432
        if (!Story.QuestProgression(3432))
        {
            Core.EnsureAccept(3432);
            Core.GetMapItem(2581, 1, "WeaverWar");
            Core.KillMonster("WeaverWar", "do1", "Spawn", "Weaver Queen's Hound", "Weaver Hound Slain", 6);
            Core.EnsureComplete(3432);
        }

        //Cleanse Firestorm 3433
        if (!Story.QuestProgression(3433))
        {
            Core.EnsureAccept(3433);
            Core.GetMapItem(2582, 1, "WeaverWar");
            Core.KillMonster("WeaverWar", "fr1", "Spawn", "ChaosWeaver Mage", "Weaver Slain", 6);
            Core.EnsureComplete(3433);
        }

        //Cleanse Northlandlights 3434
        if (!Story.QuestProgression(3434))
        {
            Core.EnsureAccept(3434);
            Core.GetMapItem(2583, 1, "WeaverWar");
            Core.KillMonster("WeaverWar", "n1", "Spawn", "Evolved Dreadspider", "Spider Slain", 6);
            Core.EnsureComplete(3434);
        }

        //Cleanse Battleunder 3435
        if (!Story.QuestProgression(3435))
        {
            Core.EnsureAccept(3435);
            Core.GetMapItem(2584, 1, "WeaverWar");
            Core.KillMonster("WeaverWar", "b1", "Spawn", "Dread Arysa", "Arysa Slain", 6);
            Core.EnsureComplete(3435);
        }

        //Cleanse Guru Forest 3436
        if (!Story.QuestProgression(3436))
        {
            Core.EnsureAccept(3436);
            Core.GetMapItem(2585, 1, "WeaverWar");
            Core.KillMonster("WeaverWar", "g1", "Spawn", "ChaosWeaver Mage", "Weaver Slain", 6);
            Core.EnsureComplete(3436);
        }

        //Cleanse Sleuthhound Inn 3437
        if (!Story.QuestProgression(3437))
        {
            Core.EnsureAccept(3437);
            Core.GetMapItem(2586, 1, "WeaverWar");
            Core.KillMonster("WeaverWar", "sl1", "Spawn", "ChaosWeaver Mage", "Weaver Slain", 6);
            Core.EnsureComplete(3437);
        }

        //Cleanse Battleon 3438
        if (!Story.QuestProgression(3438))
        {
            Core.EnsureAccept(3438);
            Core.GetMapItem(2587, 1, "WeaverWar");
            Core.KillMonster("WeaverWar", "ba1", "Spawn", "Evolved Dreadspider", "Spider Slain", 6);
            Core.EnsureComplete(3438);
        }

        //Save the SoulWeaver 3439
        Story.KillQuest(3439, "TwilightEdge", "ChaosWeaver Warrior");
    }

    public void Ravenloss()
    {
        if (Core.isCompletedBefore(3460))
            return;

        Story.PreLoad(this);

        //Chaos Weavers' Magic  3450
        Story.KillQuest(3450, "RavenLoss", "ChaosWeaver Magi");

        //Legendary Heroes of the Chaos Weavers 3451
        Story.KillQuest(3451, "RavenLoss", "ChaosWeaver Knight");

        //Weaver Tales and Legends 3452
        Story.MapItemQuest(3452, "RavenLoss", 2594);

        //Weaving Rebellion 3453
        Story.KillQuest(3453, "RavenLoss", "Weaver Queen's Hound");

        //Weaver Tactics and Strategy 3454
        Story.KillQuest(3454, "RavenLoss", "ChaosWeaver Knight");

        //Rulers of the Chaos Weavers 3455
        Story.KillQuest(3455, "RavenLoss", "ChaosWeaver Knight");

        //Fate and Saviors 3456
        Story.KillQuest(3456, "RavenLoss", "ChaosWeaver Magi");

        //The Nature of Chaos 3457
        Story.KillQuest(3457, "RavenLoss", "ChaosWeaver Magi");

        //Chaos Weaver Tribes 3458
        Story.MapItemQuest(3458, "RavenLoss", 2595);

        //Chaos Weaver Castes 3459
        Story.KillQuest(3459, "RavenLoss", "Evolved Dreadspider");

        //Defeat the ChaosWeaver Cleric 3460
        Story.KillQuest(3460, "ChaosWeb", "ChaosWeaver Cleric");

    }

}
