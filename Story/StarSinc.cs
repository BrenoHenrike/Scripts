/*
name: StarSinc Story
description: This will finish the StarSinc Story.
tags: story, quest, starsinc
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class StarSinc
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Nova Badge 1.0", "Nova Badge 2.0", "Nova Badge 3.0",
                                               "Nova Badge 3.0", "Nova Badge 4.0", "Nova Badge 5.0",
                                               "Nova Badge 6.0", "Nova Badge 7.0", "Nova Badge 8.0",
                                               "Nova Badge 9.0", "Nova Badge 10.0", "Nova Badge 11.0",
                                               "SuperNova Badge" });
        Core.SetOptions();

        StarSincQuests();

        Core.SetOptions(false);
    }

    public void StarSincQuests()
    {
        if (Core.CheckInventory("SuperNova Badge"))
            return;

        foreach (int Qid in new[] { 4400, 4401, 4402, 4403, 4404, 4405, 4406, 4407, 4408, 4409, 4410, 4412 })
            Bot.Drops.Add(Core.QuestRewards(Qid));

        Core.EquipClass(ClassType.Farm);

        Story.LegacyQuestManager(QuestLogic, 4400, 4401, 4402, 4403, 4404, 4405, 4406, 4407, 4408, 4409, 4410, 4412);

        void QuestLogic()
        {
            switch (Story.LegacyQuestID)
            {
                case 4400: // Weaken his Powers
                    Core.Logger("Nova Badge 1");
                    Core.HuntMonster("starsinc", "Star Sprites", "Stardust", 15);
                    break;

                case 4401: // Light and Dark
                    Core.Logger("Nova Badge 2");
                    Core.HuntMonster("starsinc", "Star Sprites", "Sprite Magic Essence", 7);
                    break;

                case 4402: // Paintings Give Him Strength
                    Core.Logger("Nova Badge 3");
                    Core.GetMapItem(3607, 4, "starsinc");
                    break;

                case 4403: // Learning the Land
                    Core.Logger("Nova Badge 4");
                    Core.GetMapItem(3608, 1, "starsinc");
                    break;

                case 4404: // Slay the Light and Dark
                    Core.Logger("Nova Badge 5");
                    Core.HuntMonster("starsinc", "Infernal Imp", "Darkness Fragment", 5);
                    Core.HuntMonster("starsinc", "Living Star", "Light Fragment", 5);
                    break;

                case 4405: // Chaos Fragments
                    Core.Logger("Nova Badge 6");
                    Core.KillMonster("watchtower", "Frame2", "Left", "Chaos Spider", "Chaos Fragment", 10);
                    break;

                case 4406: // Kill Them All
                    Core.Logger("Nova Badge 7");
                    Core.HuntMonster("starsinc", "Star Sprites", "Monster Killed", 15);
                    break;

                case 4407: // Get Rid of Those Guards
                    Core.Logger("Nova Badge 8");
                    Core.HuntMonster("starsinc", "Fortress Guard", "Guard Slain", 5);
                    break;

                case 4408: // Breach the Gate
                    Core.Logger("Nova Badge 9");
                    Core.HuntMonster("starsinc", "Fortress Guard", "Guard's Key");
                    break;

                case 4409: // Defeat the Prime Dominus
                    Core.Logger("Nova Badge 10");
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("starsinc", "Prime Dominus", "Prime Defeated", isTemp: false);
                    Core.Join("whitemap");
                    Core.Logger("Resetting map");
                    break;

                case 4410: // Place the Beacons
                    Core.EquipClass(ClassType.Solo);
                    Core.Logger("Nova Badge 11");
                    Core.GetMapItem(3609, 6, "starsinc");
                    Core.Logger("Resetting map");
                    Core.Join("whitemap");
                    break;

                case 4412: // Retrieve the Core
                    Core.Logger("SuperNova Badge");
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("starsinc", "Final", "Final Defeated");
                    break;
            }
        }
    }
}
