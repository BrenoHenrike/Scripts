/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class LivingDungeon
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        LivingDungeonStory();

        Core.SetOptions(false);
    }

    public void LivingDungeonStory()
    {
        if (Core.isCompletedBefore(4384))
            return;

        Story.PreLoad(this);

        // Titan Hollow
        Story.ChainQuest(4348);

        // Roots of all Evil
        Story.KillQuest(4349, "livingdungeon", "Root of Evil");

        // Venus Hero Trap
        Story.KillQuest(4350, "livingdungeon", "Seed Spitter");

        // Bark is worse than its bite
        Story.KillQuest(4351, "livingdungeon", new[] { "Evil Plant Horror", "Titan Decay" });

        // Knot what you expected
        Story.KillQuest(4352, "livingdungeon", "Weeping Widowmaker");

        // Cha Cha Cha Chia!
        Story.KillQuest(4353, "livingdungeon", "Chia Warrior");

        // Leaf me alone!
        Story.KillQuest(4354, "livingdungeon", new[] { "Seed Spitter", "Evil Plant Horror", "Titan Decay" });

        // Evil Faerie Ambush!
        Story.KillQuest(4355, "livingdungeon", "Evil Tree Faerie");

        // Check the Trunk
        Story.KillQuest(4356, "livingdungeon", "Vulchurion");

        // Committing Tree-son
        Story.KillQuest(4357, "livingdungeon", new[] { "Evil Plant Horror", "Evil Tree Faerie", "Vulchurion" });

        // Heartwood
        Story.KillQuest(4358, "livingdungeon", new[] { "Evil Plant Horror", "Evil Tree Faerie", "Vulchurion" });

        // Drayko BOSS FIGHT!
        Story.KillQuest(4359, "livingdungeon", "Drayko");

        // Foilaged again!
        Story.KillQuest(4360, "livingdungeon", new[] { "Evil Plant Horror", "Evil Tree Faerie" });

        // Mind Games
        Story.KillQuest(4361, "livingdungeon", new[] { "Evil Plant Horror", "Evil Tree Faerie" });

        // DRAGON vs TITAN
        Story.KillQuest(4362, "treetitanbattle", "Dakka the Dire Dragon");

        // Smells like trouble!
        Story.KillQuest(4363, "livingdungeon", "Lil' Poot");

        // EPIC DROP!
        Story.KillQuest(4364, "livingdungeon", "Epic Drop");


        Core.AddDrop("Wooden Ring", "Salad!", "Weeping Widowmaker Bone", "Chia in a pot!", "Fairy Phone", "Vulchurion Quill", "Drayko's Medallion", "Giant Dakka Fang");
        // ------------------------------------------        
        // Drayko Battle!
        if (!Core.CheckInventory("Drayko's Medallion"))
        {
            Core.Logger("Drayko's Medallion not found, finding it for you");
            Core.EnsureAccept(4383);

            // Vulchurions
            if (!Core.CheckInventory("Vulchurion Quill"))
            {
                Core.Logger("Vulchurion Quill not found, finding it for you");
                Core.EnsureAccept(4382);

                // Evil Tree Faeries
                if (!Core.CheckInventory("Fairy Phone"))
                {
                    Core.Logger("Fairy Phone not found, finding it for you");
                    Core.EnsureAccept(4381);

                    // Chia Warriors
                    if (!Core.CheckInventory("Chia in a pot!"))
                    {
                        Core.Logger("Chia in a pot! not found, finding it for you");
                        Core.EnsureAccept(4380);

                        if (!Core.CheckInventory("Weeping Widowmaker Bone"))
                        {
                            Core.Logger("Weeping Widowmaker Bone not found, finding it for you");
                            Core.EnsureAccept(4379);

                            if (!Core.CheckInventory("Salad!"))
                            {
                                Core.Logger("Salad! not found, finding it for you");
                                Core.EnsureAccept(4378);

                                if (!Core.CheckInventory("Wooden Ring"))
                                {
                                    Core.Logger("Wooden Ring not found, finding it for you");
                                    Core.EnsureAccept(4377);
                                    Core.HuntMonster("livingdungeon", "Root of Evil", "Wooden Ring Piece", 5);
                                    Core.EnsureComplete(4377);
                                    Bot.Wait.ForPickup("Wooden Ring");
                                    Bot.Sleep(1000);
                                }
                                Core.HuntMonster("livingdungeon", "Evil Plant Horror", "Evil Plant Horror Leaf", 6);
                                Core.EnsureComplete(4378);
                                Bot.Wait.ForPickup("Salad!");
                                Bot.Sleep(1000);
                            }
                            Core.HuntMonster("livingdungeon", "Weeping Widowmaker", "Widowmaker deboned", 5);
                            Core.EnsureComplete(4379);
                            Bot.Wait.ForPickup("Weeping Widowmaker Bone");
                            Bot.Sleep(1000);
                        }
                        Core.HuntMonster("livingdungeon", "Chia Warrior", "Chia Warrior defeated", 3);
                        Core.EnsureComplete(4380);
                        Bot.Wait.ForPickup("Chia in a pot!");
                        Bot.Sleep(1000);
                    }
                    Core.HuntMonster("livingdungeon", "Evil Tree Faerie", "Fairy Purse", 5);
                    Core.EnsureComplete(4381);
                    Bot.Wait.ForPickup("Fairy Phone");
                    Bot.Sleep(1000);
                }
                Core.HuntMonster("livingdungeon", "Vulchurion", "Vulchurion Feather", 3);
                Core.EnsureComplete(4382);
                Bot.Wait.ForPickup("Vulchurion Quill");
                Bot.Sleep(1000);
            }
            Core.HuntMonster("livingdungeon", "Drayko", "Drayko Defeated... again");
            Core.EnsureComplete(4383);
            Bot.Wait.ForPickup("Drarko's Medalion");
            Bot.Sleep(1000);
        }
        // DRAGON vs TITAN Rematch! - 4384
        if (!Story.QuestProgression(4384))
        {
            Core.Logger("Giant Dakka Fang not found, finding it for you");
            Core.EnsureAccept(4384);
            Core.HuntMonster("treetitanbattle", "Dakka the Dire Dragon", "Dakka Defeated... again");
            Core.EnsureComplete(4384);
            // Bot.Wait.ForPickup("Giant Dakka Fang");
        }
    }
}
