/*
name: Fausto's and Lengjin's Quests
description: Storyline for the map: "Parades"
tags: seasonal, yokai, akibacny, akiba new year, story, fausto, lengjin
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Parades
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
        if (Core.CheckInventory("Parades Token XI") || !Core.isSeasonalMapActive("parades"))
            return;

        Core.EquipClass(ClassType.Farm);

        Story.LegacyQuestManager(QuestLogic, 4830, 4831, 4832, 4833, 4834, 4835, 4836, 4837, 4838, 4839, 4840, 4841, 4842);

        void QuestLogic()
        {
            switch (Story.LegacyQuestID)
            {
                #region  Fausto
                case 4830:
                    //Two Parades! 4830
                    if (!Core.CheckInventory("Cool Lemonade"))
                    {
                        Core.EnsureAccept(4830);
                        Core.GetMapItem(4226, 1, "parades");
                        Core.EnsureComplete(4830);
                        Bot.Wait.ForPickup("Cool Lemonade");
                    }
                    break;
                // Un-decorate Yokai 4831
                case 4831:
                    Core.HuntMonster("parades", "Paper Lantern", "Paper Lantern Destroyed", 6, log: false);
                    Core.GetMapItem(4232, 5, "parades");
                    break;
                // Naughty Fairies 4832
                case 4832:
                    Core.HuntMonster("parades", "Xingzhi", "Xingzhi Slain", 8, log: false);
                    break;
                // Dancers Love “Flours” 4833
                case 4833:
                    Core.GetMapItem(4227, 1, "parades");
                    Core.GetMapItem(4228, 6, "parades");
                    break;
                //Spooky Ghosties 4834
                case 4834:
                    Core.HuntMonster("parades", "Shushen", "Shushen Slain", 10, log: false);
                    break;
                // Plan B 4835
                case 4835:
                    Core.HuntMonster("parades", "Raven", "Raven Feathers", 5, log: false);
                    Core.HuntMonster("parades", "Lingzhi", "Lingzhi Spores", 5, log: false);
                    break;
                // A Gift For Lengjing 4836
                case 4836:
                    Core.GetMapItem(4233, 1, "parades");
                    Bot.Wait.ForPickup("Sleepytime Necklace");
                    break;
                #endregion Fausto

                #region  Lengjing
                // I'm Not That Stupid 4837
                case 4837:
                    Core.GetMapItem(4229, 8, "parades");
                    Core.HuntMonster("parades", "Carnaval Mask", "Carnaval Mask Destroyed", 6, log: false);
                    break;
                // Why Did It Have To Be Snakes 4838
                case 4838:
                    Core.HuntMonster("parades", "Boiuna", "Boiuna Slain", 10, log: false);
                    break;
                // Flash! Bang! 4839
                case 4839:
                    Core.GetMapItem(4230, 8, "parades");
                    break;
                // I Don't Retaliate, I Escalate 4840
                case 4840:
                    Core.HuntMonster("parades", "Shushen", "Spectral Essence", 8, log: false);
                    Core.HuntMonster("parades", "Raven", "Raven's Blood", 4, log: false);
                    break;
                // One Last Thing 4841
                case 4841:
                    Core.HuntMonster("hachiko", "Ninja Nopperabo", "Scroll of Summoning", log: false);
                    break;
                // Defeat the Oni 4842
                case 4842:
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("parades", "Oni", "Oni Defeated", log: false);
                    break;
                    #endregion  Lengjing
            }
        }
    }
}
