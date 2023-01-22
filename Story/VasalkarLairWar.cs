/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class LairWar
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        doAll();

        Core.SetOptions(false);
    }

    public void doAll()
    {
        if (Core.isCompletedBefore(6689) && Core.isCompletedBefore(6694))
            return;

        Defend();
        Attack();
    }

    public void Defend()
    {
        if (Core.isCompletedBefore(6689))
            return;

        Story.PreLoad(this);

        //6685 | Flamefang Medals
        Core.RegisterQuests(6685, 6686);
        if (!Story.QuestProgression(6685, GetReward: false))
        {
            Core.KillMonster("lairdefend", "Bridge", "LeftUp", "*", "Flamefang Medal", 3);
            Bot.Wait.ForQuestComplete(6685);
        }
        Core.CancelRegisteredQuests();

        //6686 | Mega Flamefang Medals
        //Can be skipped, has the same quest value

        //6687 | SoulSucking Enemy
        Story.KillQuest(6687, "lairdefend", "Soul Wyvern", GetReward: false);

        // 6688 | The Dragon Summoner
        if (!Story.QuestProgression(6688, GetReward: false))
        {
            Core.EnsureAccept(6688);
            Core.HuntMonster("lairdefend", "Dragon Summoner", "Dragon Claw", 3, isTemp: true);
            Core.EnsureComplete(6688);
        }

        //6689 | The Flame Dragon
        Story.KillQuest(6689, "lairdefend", "Flame Dragon General", GetReward: false);

        Core.CancelRegisteredQuests();
    }

    public void Attack()
    {
        if (Core.isCompletedBefore(6694))
            return;

        Story.PreLoad(this);

        //6690 | Defender Medals
        Core.RegisterQuests(6690, 6691);
        if (!Story.QuestProgression(6690, GetReward: false))
        {
            Core.KillMonster("lairattack", "Bridge", "LeftUp", "*", "Defender Medal", 3);
            Bot.Wait.ForQuestComplete(6690);
        }
        Core.CancelRegisteredQuests();

        //6691 | Mega Defender Medals
        //Can be skipped, has the same quest value

        //6692 | Magic and Mayhem
        Story.KillQuest(6692, "lairattack", "Dracomancer", GetReward: false);

        //6693 | The Dragon Defender
        Story.KillQuest(6693, "lairattack", "Dragon Defender", GetReward: false);

        //6694 | The Flame Dragon
        Story.KillQuest(6694, "lairattack", "Flame Dragon General", GetReward: false);

        Core.CancelRegisteredQuests();
    }
}
