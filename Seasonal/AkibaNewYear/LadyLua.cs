/*
name: Lady Lua's Quests
description: Completes Lady Lua's quests in akibacny
tags: yokai, seasonal, akiba new year, akibacny, story
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class LadyLua
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Storyline();
        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(8505) || !Core.isSeasonalMapActive("akibacny"))
            return;

        Story.PreLoad(this);

        //Earning Your Stripes (8498)
        Story.KillQuest(8498, "ravenloss", new[] { "Evolved Dreadspider", "ChaosWeaver Magi", "Underbeast" });

        //A New Year in Bloom (8499)
        Story.KillQuest(8499, "akibacny", new[] { "Bamboo Treeant", "Lingzhi", "Xingzhi" });

        //The Eye(s) of the Tiger (8500)
        if (!Story.QuestProgression(8500))
        {
            Core.EnsureAccept(8500);
            Core.HuntMonster("mobius", "Chaos Sp-Eye", "Chaos Eyeball", 5, log: false);
            Core.HuntMonster("ebonslate", "Sp-Eye", "Evil Eyeball", 5, log: false);
            Core.HuntMonster("deathgazer", "Deathgazer", "Deadly Eyeball", 2, log: false);
            Core.HuntMonster("battlewedding", "Jimmy the Eye Heart", "Heartbreaking Eyeball", log: false);
            Core.EnsureComplete(8500);
        }

        //A Royal Feast (8501)
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(8501, "akibacny", new[] { "Lu Niu", "Honorable Boar", "Hinezumi" });

        //Winning Streak (8502)
        Core.EquipClass(ClassType.Farm);
        if (!Story.QuestProgression(8502))
        {
            Core.EnsureAccept(8502);
            Core.HuntMonster("creatures", "White Tiger", "Bright as a White Tiger", 4, log: false);
            Core.HuntMonster("phoenixrise", "Cinderclaw", "Superior to Cinderclaw", 2, log: false);
            Core.HuntMonster("fireplanewar", "ShadowClaw", "Overshadowed Shadowclaw", log: false);
            Core.HuntMonster("phoenixrise", "Firestorm Tiger", "Blazed Through Underlings", 12, log: false);
            Core.EnsureComplete(8502);
        }

        //Yin and Yang (8503)
        Story.KillQuest(8503, "shadowfortress", new[] { "1st Head of Orochi", "2nd Head of Orochi", "3rd Head of Orochi", "4th Head of Orochi", "5th Head of Orochi", "6th Head of Orochi" });

        //Tyger, Tyger, Burning Bright (8504)
        Story.KillQuest(8504, "akibacny", new[] { "Fiery Lantern", "Lin Kuei" });

        //Tame That Tiger! (8505)
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(8505, "akibacny", "Umitora");
    }
}
