/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CrashSite
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "DwaToken I", "DwaToken II", "DwaToken III",
                                               "DwaToken IV", "DwaToken V", "DwaToken VI",
                                               "DwaToken VII", "DwaToken VIII", "DwaToken IX",
                                               "DwaToken X" });
        Core.SetOptions();

        StoryLine();


        Core.SetOptions(false);

    }

    public void StoryLine()
    {
        if (Core.CheckInventory("LastQuestDrop"))
            return;

        //Add questIDs of the tokenQuests on their right ordre
        Story.LegacyQuestManager(QuestLogic, 4787, 4788, 4789, 4790, 4791, 4792, 4793, 4794, 4795, 4796);

        void QuestLogic()
        {
            switch (Story.LegacyQuestID)
            {

                case 4787: //Mug Me Some Dwakels 4787
                    Core.HuntMonster("crashsite", "Dwakel Warrior", "Pneumatic Relay");
                    Core.HuntMonster("crashsite", "Flamethrower Dwakel", "Sonic Transducer");
                    Core.HuntMonster("crashsite", "Dwakel Blaster", "Solenoid Helix");

                    break;

                case 4788: //Mug me One More 4788
                    Core.HuntMonster("crashsite", "Mithril Man", "Cosmic Dongle");
                    break;

                case 4789: //Hot Spot 4789
                    Core.GetMapItem(4192, 10, "crashsite");
                    break;

                case 4790: //Wibbly Wobbly 4790
                    Core.HuntMonster("crashruins", "Spacetime Anomaly", "Anomalies Destroyed", 12);
                    break;

                case 4791: //Indiana Bones 4791
                    Core.HuntMonster("crashruins", "Unlucky Explorer", "Rusty Key", 7);
                    break;

                case 4792: //Timey Wimey 4792
                    Core.HuntMonster("crashruins", "Spacetime Anomaly", "Space-time Energy", 10);
                    break;

                case 4793: //These Flares Are The Bomb 4793
                    Core.GetMapItem(4191, 5, "crashruins");
                    Core.HuntMonster("crashruins", "Unlucky Explorer", "Pyrotechnic Flares", 3);
                    Core.HuntMonster("crashruins", "Unlucky Explorer", "Matchbook");
                    Core.HuntMonster("crashruins", "Dwakel Soldier", "Metal Shrapnel", 10);
                    break;

                case 4794: //Find My Rod! 4794
                    Core.HuntMonster("crashruins", "Chest", "Useless Junk", 12);
                    break;

                case 4795: //My Precious 4795
                    Core.GetMapItem(4190, 6, "crashruins");
                    break;

                case 4796: //Fried Chicken 4796
                    Core.HuntMonster("crashruins", "Cluckmoo Idol", "Cluckmoo Idol Defeated");
                    break;

            }
        }
    }
}
