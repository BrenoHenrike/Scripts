//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Battledoom
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Unlucky Gem I", "Unlucky Gem II", "Unlucky Gem III",
                                               "Unlucky Gem IV", "Unlucky Gem V", "Unlucky Gem VI",
                                               "Unlucky Gem VII", "Cursed Mirror of Enutrof", "Shadowglass Shard" });
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.CheckInventory("Cursed Mirror of Enutrof"))
            return;

        Story.LegacyQuestManager(QuestLogic, 4648, 4649, 4650, 4651, 4652, 4653, 4654, 4655);

        void QuestLogic()
        {
            switch (Story.LegacyQuestID)
            {
                case 4648: // Shadowy Reconnaissance
                    Core.HuntMonster("battledoom", "Shadow Slime", "Shadow Slime Defeated", 5);
                    Core.HuntMonster("battledoom", "Shadow Flying Eye", "Shadow Eyeball Defeated", 3);
                    break;

                case 4649: // Slippery Shadows
                    Core.HuntMonster("battledoom", "Shadow Skelly", "Necronomicon Page", 6);
                    break;
                
                case 4650: // Through the Looking-Glass
                    Core.HuntMonster("battledoom", "Shadow Skelly", "Mirror Fragment Retrieved");
                    break;
                
                case 4651: // Necro-Polished
                    Core.GetMapItem(3976, 1, "necropolis");
                    Core.HuntMonster("battledoom", "Shadow Skelly", "Shadow Skeletons Defeated", 13);
                    break;

                case 4652: // Cavernous Chaos
                    Core.HuntMonster("necrocavern", "Shadow Imp", "Mirror Fragment Found");
                    Core.HuntMonster("necrocavern", "ShadowStone Elemental", "Mirror Fragment Located");
                    break;
                
                case 4653: // Mirror, Mirror, Off the Wall
                    Core.GetMapItem(3975, 1, "battleoff");
                    Core.HuntMonster("battleoff", "Evil Moglin", "Evil Moglin Defeated", 3);
                    break;
                
                case 4654: // To the Underworld!
                    Core.HuntMonster("underworld", "Undead Legend", "Mirror Fragment Obtained");
                    Core.HuntMonster("underworld", "Klunk", "Mirror Fragment Acquired");
                    break;

                case 4655: // Shadow of Corruption
                    Core.HuntMonster("celestialrealm", "Shadow Beast", "Final Mirror Fragment Found");
                    break;

                case 4656: // Hunt for Shadowglass Shards
                    Core.HuntMonster("battledoom", "Shadow Skelly", "Shadow Skeleton Defeated", 5);
                    Core.HuntMonster("battledoom", "Shadow Slime", "Shadow Slime Defeated", 5);
                    Core.HuntMonster("battledoom", "Shadow Flying Eye", "Shadow Eyeball Defeated", 5);
                    Core.HuntMonster("battledoom", "Shadow Beast", "Shadow Beast Defeated", 5);
                    break;
            }
        }
    }
}