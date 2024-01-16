/*
name: ShadowBlast Arena
description: This will finish the ShadowBlast Arena quest.
tags: story, quest, nation, ShadowBlast Arena
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class ShadowBlastArena
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] {  "Shadowscythe Round 1 Medal", "Shadowscythe Round 2 Medal",
                                               "Shadowscythe Round 3 Medal", "Shadowscythe Round 4 Medal",
                                                "Emblem of Gravelyn", "Diamond Token of Gravelyn" });
        Core.SetOptions();
        Doall();


        Core.SetOptions(false);
    }

    public void Doall()
    {
        StoryLineP1();
        StoryLineP2();

    }

    public void StoryLineP1()
    {
        if (Core.CheckInventory("Shadowscythe Round 4 Medal"))
            return;

        Story.LegacyQuestManager(QuestLogic, Core.FromTo(4733, 4736));
        void QuestLogic()
        {

            switch (Story.LegacyQuestID)
            {
                case 4733: // The Shadowscythe Needs YOU! 4733
                    Core.HuntMonster("shadowblast", "Legion Airstrike", "Legion Rookie Defeated", 5);
                    Core.HuntMonster("shadowblast", "CaesarIsTheDark", "Nation Rookie Defeated", 5);
                    break;

                case 4734: // Show Me More, Shadow-Noob 4734
                    Core.HuntMonster("shadowblast", "Legion Fenrir", "Legion Veteran Defeated", 7);
                    Core.HuntMonster("shadowblast", "Carnage", "Nation Veteran Defeated", 7);
                    break;

                case 4735: // For the Shadowscythe! 4735
                    Core.HuntMonster("shadowblast", "Legion Cannon", "Legion Elite Defeated", 10);
                    Core.HuntMonster("shadowblast", "Minotaurofwar", "Nation Elite Defeated", 10);
                    break;

                case 4736: // Gravelyn Likes Your Style 4736
                    Core.HuntMonster("shadowblast", "Shadow Destroyer", "Shadowscythe Destroyer Vanquished");
                    break;
            }
        }
    }

    public void StoryLineP2()
    {
        if (Core.isCompletedBefore(4737) && Core.CheckInventory("Shadowscythe Round 4 Medal"))
            return;

        if (!Core.isCompletedBefore(4750) || !Core.CheckInventory("Shadowscythe Round 4 Medal"))
            StoryLineP1();

        Story.PreLoad(this);

        // Shadowscythe Recruits: Embrace the Shadow 4750
        if (!Story.QuestProgression(4750))
        {
            Core.EnsureAccept(4750);
            Core.HuntMonster("shadowblast", "Carnage", "Shadow Seal", 1, false);
            Core.HuntMonster("shadowblast", "Legion Fenrir", "Gem of Superiority", 1, false);
            Core.EnsureComplete(4750);

        }

        // Shadowscythe Loyalty Rewarded 4737
        if (!Story.QuestProgression(4737))
        {
            if (!Core.CheckInventory("Defeated Makai", 25))
            {
                Core.EquipClass(ClassType.Farm);
                Core.KillMonster("tercessuinotlim", "m2", "Left", "*", "Defeated Makai", 25, false);
                Core.JumpWait();
                Core.Join("aqlesson");
            }
            Core.EquipClass(ClassType.Solo);
            Core.KillMonster("aqlesson", "Frame9", "Right", "Carnax", "Carnax Eye", publicRoom: true);
            Core.HuntMonster("deepchaos", "Kathool", "Kathool Tentacle", publicRoom: true);

            //More then one item of the same name as drop btoh temp and non-temp.
            while (!Bot.ShouldExit && !Core.CheckInventory(33257))
                Core.KillMonster("dflesson", "r12", "Right", "Fluffy the Dracolich", publicRoom: true);

            Core.HuntMonster("lair", "Red Dragon", "Red Dragon's Fang");
            Core.HuntMonster("bloodtitan", "Blood Titan", "Blood Titan's Blade", publicRoom: true);
            Bot.Drops.Pickup("Legion Token", "Diamond Token of Dage");
        }

    }
}
