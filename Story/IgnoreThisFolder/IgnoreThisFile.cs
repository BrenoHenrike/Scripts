//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Borgars.cs
using Skua.Core.Interfaces;

public class BrightFortress
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public Borgars Bo = new ();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        Bo.BorgarQuests();
        DageTheEvil();
        DageTheGood();
        BrightFortressQuests();
    }

    public void DageTheEvil()
    {
        if (Core.CheckInventory("Dark Seal VIII"))
            return;

        Core.BankingBlackList.AddRange(new[] { "Dark Seal I", "Dark Seal II", "Dark Seal III", "Dark Seal IV", "Dark Seal V", "Dark Seal VI", "Dark Seal VII", "Dark Seal VIII" });

        Story.LegacyQuestManager(QuestLogic, 4440, 4441, 4442, 4443, 4444, 4445, 4446, 4447);

        void QuestLogic()
        {
            switch (Story.LegacyQuestID)
            {
                case 4440: // Invade the Overworld 4440
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("brightfortress", "Brightscythe Reaver", "Weak Spot Found", 4);
                    break;
                
                case 4441: // Reflections of Destruction 4441
                    Core.GetMapItem(3645, 4, "brightfortress");
                    Core.HuntMonster("brightfortress", "Brightscythe Reaver", "Shieldbearer Slain", 5);
                    break;

                case 4442: // Undercover in the Overworld 4442
                    Core.HuntMonster("brightfortress", "Bright Shinobi", "Fortress Map Page", 7);
                    break;

                case 4443: // Shatter the Mirror Defenders 4443
                    Core.HuntMonster("brightfortress", "Skeletal Ice Mage", "Defender Slain", 13);
                    break;
                
                case 4444: // Time to Strengthen the Rift 4444
                    Core.GetMapItem(3648, 1, "brightfortress");
                    Core.HuntMonster("brightfortress", "Brightscythe Reaver", "Brightscythe Reaver Spirits", 9);
                    break;

                case 4445: // Grind down the Mirror 4445
                    Core.HuntMonster("brightfortress", "Imbalanced Knight", "Defenders Slain", 12);
                    break;

                case 4446: // Shine a Light on Traitors 4446
                    Core.GetMapItem(3641, 1, "brightfortress");
                    Core.GetMapItem(3642, 1, "brightfortress");
                    Core.GetMapItem(3646, 1, "brightfortress");
                    break;

                case 4447: // Destroy Nulgath the ArchAngel 4447
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("brightfortress", "Archangel Nulgath", "Nulgath Defeated");
                    break;
            }
        }
    }

    public void DageTheGood()
    {
        if (Core.CheckInventory("Bright Seal VIII"))
            return;

        Core.BankingBlackList.AddRange(new[] { "Bright Seal I", "Bright Seal II", "Bright Seal III", "Bright Seal IV", "Bright Seal V", "Bright Seal VI", "Bright Seal VII", "Bright Seal VIII" });

        Story.LegacyQuestManager(QuestLogic, 4448, 4449, 4450, 4451, 4452, 4453, 4454, 4455);

        void QuestLogic()
        {
            switch (Story.LegacyQuestID)
            {
                case 4448: // Protect the Overworld 4448
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("brightfortress", "Undead Bruiser", "Weak Spot Found", 4);
                    break;
                
                case 4449: // Reflections of Salvation 4449
                    Core.GetMapItem(3644, 4, "brightfortress");
                    Core.HuntMonster("brightfortress", "Imbalanced Knight", "Knight Slain", 5);
                    break;

                case 4450: // Undercover in the Overworld 4450
                    Core.HuntMonster("brightfortress", "Dark Assassin", "Fortress Map Page", 7);
                    break;

                case 4451: // Shatter the Mirror Attackers 4451
                    Core.HuntMonster("brightfortress", "Undead Bruiser", "Undead Bruiser Slain", 13);
                    break;

                case 4452: // Time to Strengthen the Rift 4452
                    Core.GetMapItem(3647, 1, "brightfortress");
                    Core.HuntMonster("brightfortress", "Skeletal Ice Mage", "Evil Ice Mage Spirit", 9);
                    break;

                case 4453: // Grind down the Mirror 4453
                    Core.HuntMonster("brightfortress", "Imbalanced Knight", "Attackers Slain", 12);
                    break;

                case 4454: // Shine a Light on Traitors 4454
                    Core.GetMapItem(3638, 1, "brightfortress");
                    Core.GetMapItem(3639, 1, "brightfortress");
                    Core.GetMapItem(3640, 1, "brightfortress");
                    break;

                case 4455: // Defeat Commander Yulgar 4455
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("brightfortress", "Commander Yulgar", "Yulgar Defeated");
                    break;
            }
        }
    }

    public void BrightFortressQuests()
    {
        if (Core.CheckInventory("Duality Seal IV"))
            return;
            
        Core.BankingBlackList.AddRange(new[] { "Duality Seal I", "Duality Seal II", "Duality Seal III", "Duality Seal IV" });

        Story.LegacyQuestManager(QuestLogic, 4456, 4457, 4458, 4459);

        void QuestLogic()
        {
            switch (Story.LegacyQuestID)
            {
                case 4456: // Break into the Timelock 4456
                    Core.GetMapItem(3649, 1, "brightfortress");
                    break;
                
                case 4457: // Battle Dark ArchMage Brentan 4457
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("brightfortress", "Archmage Brentan", "Brentan Defeated");
                    break;

                case 4458: // Battle Cryomancer Drakonnan 4458
                    Core.HuntMonster("brightfortress", "Frigid Drakkonan", "Drakkonan Defeated");
                    break;

                case 4459: // Battle Alteon the Imbalanced 4459
                    Core.HuntMonster("brightfortress", "Imbalanced Alteon", "Alteon Defeated");
                    break;
            }
        }
    }
}