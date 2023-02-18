/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class TokenStoryTemplate
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "QuestDropName", "QuestDropName", "QuestDropName",
                                               "QuestDropName", "QuestDropName", "QuestDropName",
                                               "QuestDropName", "QuestDropName", "QuestDropName",
                                               "QuestDropName", "QuestDropName" });
        Core.SetOptions();

        StoryLine();
        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.CheckInventory("LastQuestDrop"))
            return;

        //Add questIDs of the tokenQuests on their right ordre
        Story.LegacyQuestManager(QuestLogic, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009); // Or use Core.FromTo(0001, 0009)

        void QuestLogic()
        {
            switch (Story.LegacyQuestID)
            {
                case 0001: // QuestName 0001
                    Core.HuntMonster("MapName", "MonsterName", "DropName", 0000);
                    Core.GetMapItem(0000, 0000, "MapName");
                    break;

                case 0002: // QuestName 0002
                    Core.HuntMonster("MapName", "MonsterName", "DropName", 0000);
                    Core.GetMapItem(0000, 0000, "MapName");
                    break;

                case 0003: // QuestName 0003
                    Core.HuntMonster("MapName", "MonsterName", "DropName", 0000);
                    Core.GetMapItem(0000, 0000, "MapName");
                    break;

                case 0004: // QuestName 0004
                    Core.HuntMonster("MapName", "MonsterName", "DropName", 0000);
                    Core.GetMapItem(0000, 0000, "MapName");
                    break;

                case 0005: // QuestName 0005
                    Core.HuntMonster("MapName", "MonsterName", "DropName", 0000);
                    Core.GetMapItem(0000, 0000, "MapName");
                    break;

                case 0006: // QuestName 0006
                    Core.HuntMonster("MapName", "MonsterName", "DropName", 0000);
                    Core.GetMapItem(0000, 0000, "MapName");
                    break;

                case 0007: // QuestName 0007
                    Core.HuntMonster("MapName", "MonsterName", "DropName", 0000);
                    Core.GetMapItem(0000, 0000, "MapName");
                    break;

                case 0008: // QuestName 0008
                    Core.HuntMonster("MapName", "MonsterName", "DropName", 0000);
                    Core.GetMapItem(0000, 0000, "MapName");
                    break;

                case 0009: // QuestName 0009
                    Core.HuntMonster("MapName", "MonsterName", "DropName", 0000);
                    Core.GetMapItem(0000, 0000, "MapName");
                    break;

            }
        }
    }
}
