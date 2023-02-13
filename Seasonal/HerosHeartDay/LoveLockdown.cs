/*
name: Love Lockdown
description: This will complete the Love Lockdown story quest.
tags: love-lockdown, seasonal, hero, heart, story
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class LoveLockdown
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Heart Token I", "Heart Token II", "Heart Token III",
                                               "Heart Token IV", "Heart Token V", "Heart Token VI",
                                               "Heart Token VII", "Heart Token VIII", "Heart Token IX",
                                               "Heart Token X", "Heart Token XI", "Heart Token XII",
                                               "Final Heart Token" });
        Core.SetOptions();

        StoryLine();
        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.CheckInventory("Final Heart Token"))
            return;
            
        if (!Core.isSeasonalMapActive("lovelockdown"))
            return;

        Story.LegacyQuestManager(QuestLogic, 4814, 4815, 4816, 4817, 4818, 4819, 4820, 4821, 4822, 4823, 4824, 4825, 4826); // Or use Core.FromTo(0001, 0009)

        void QuestLogic()
        {
            switch (Story.LegacyQuestID)
            {
                case 4814: // A Fishy Frenzy 4814
                    Core.HuntMonster("lovelockdown", "Sweeter Fish", "Sweeter Fish Swished", 6);
                    break;

                case 4815: // Love You To DEATH 4815
                    Core.HuntMonster("lovelockdown", "O-Dokurose", "O-DokuROSE Defeated");
                    break;

                case 4816: // Potion Commotion 4816
                    Core.GetMapItem(4217, 7, "lovelockdown");
                    break;

                case 4817: // The Melancholy of Alina 4817
                    Core.HuntMonster("lovelockdown", "Alchemist’s Sorrow", "Alchemist's Sorrow Slain");
                    break;

                case 4818: // The Escapist 4818
                    Core.GetMapItem(4218, map: "lovelockdown");
                    Core.GetMapItem(4219, map: "lovelockdown");
                    break;

                case 4819: // A Wary Library 4819
                    Core.HuntMonster("lovelockdown", "Book Swarm", "Book Swarm Defeated", 7);
                    break;

                case 4820: // The More You Glow 4820
                    Core.GetMapItem(4220, 6, "lovelockdown");
                    break;

                case 4821: // Socktacular Slayhem 4821
                    Core.HuntMonster("lovelockdown", "Sock Wraith", "Sock Wraith Slain", 6);
                    break;

                case 4822: // Socktimus Prime 4822
                    Core.HuntMonster("lovelockdown", "Left Socktopus", "Left Socktopus Socked");
                    break;

                case 4823: // Primordial Fear 4823
                    Core.HuntMonster("lovelockdown", "Thanotops", "Thanatops Obliterated");
                    break;

                case 4824: // Bunnihilation 4824
                    Core.GetMapItem(4221, 6, "lovelockdown");
                    break;

                case 4825: // Bunnihilation, Vol. II 4825
                    Core.HuntMonster("lovelockdown", "Ultra Cuddles", "Ultra Cuddles Defeated");
                    break;

                case 4826: // Unrequited Love 4826
                    Core.HuntMonster("lovelockdown", "The Unrequited", "Unrequited Love Defeated");
                    break;

            }
        }
    }
}
