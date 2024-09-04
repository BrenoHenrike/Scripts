/*
name: ShadowBlast Arena
description: This will finish the ShadowBlast Arena quest.
tags: story, quest, nation, shadowblast arena
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

    public void EmblemofGravelyn(int quant = 100)
    {
        if (Core.CheckInventory("Emblem of Gravelyn", quant))
            return;

        if (!Core.CheckInventory("Shadowscythe Round 4 Medal"))
            StoryLineP1();

        Core.RegisterQuests(4750);
        Core.FarmingLogger("Diamond Token of Dage", quant);
        Core.AddDrop("Emblem of Gravelyn");
        while (!Bot.ShouldExit & !Core.CheckInventory("Emblem of Gravelyn", quant))
        {
            Core.HuntMonster("shadowblast", "Carnage", "Shadow Seal", 1, false);
            Core.HuntMonster("shadowblast", "Legion Fenrir", "Gem of Superiority", 1, false);
            Bot.Wait.ForPickup("Emblem of Gravelyn");
        }
        Core.CancelRegisteredQuests();
    }

    public void DiamondTokenofGravelyn(int quant = 100)
    {
        if (Core.CheckInventory("Diamond Token of Dage", quant))
            return;

        if (!Core.CheckInventory("Shadowscythe Round 4 Medal"))
            StoryLineP1();

        Core.AddDrop("Legion Token", "Diamond Token of Dage");
        Core.FarmingLogger("Diamond Token of Dage", quant);
        Core.RegisterQuests(4750);
        while (!Bot.ShouldExit & !Core.CheckInventory("Diamond Token of Dage", quant))
        {
            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("tercessuinotlim", "m2", "Left", "*", "Defeated Makai", 25, false);

            Core.EquipClass(ClassType.Solo);
            Core.KillMonster("aqlesson", "Frame9", "Right", "Carnax", "Carnax Eye");
            Core.HuntMonster("deepchaos", "Kathool", "Kathool Tentacle");

            //More then one item of the same name as drop btoh temp and non-temp.
            while (!Bot.ShouldExit && !Core.CheckInventory(33257))
                Core.KillMonster("dflesson", "r12", "Right", "Fluffy the Dracolich", log: false);

            Core.HuntMonster("lair", "Red Dragon", "Red Dragon's Fang");
            Core.HuntMonster("bloodtitan", "Blood Titan", "Blood Titan's Blade");
            Bot.Wait.ForPickup("Diamond Token of Dage");
        }
        Core.CancelRegisteredQuests();
    }
}
