/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CoreDageTheEvilIsland
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Core.RunCore();

        Core.SetOptions(false);
    }

    public void CompleteDageTheEvilIslandStory()
    {
        if (Core.isCompletedBefore(5658))
        {
            Core.Logger("You have already completed DageTheEvil island storyline");
            return;
        }
        DarkFortress();
        Seraph();
        LegionCrypt();
        EnvyMap();
        Laken();
    }

    public void DarkFortress()
    {
        if (Core.isCompletedBefore(4092))
        {
            Core.Logger("You have already completed DarkFortress storyline");
            return;
        }
        Story.PreLoad(this);

        Core.AddDrop("Ultra Dark Mystery Stone Of Evil Animosity");

        //Test 1: Proving Ground 4084
        Story.KillQuest(4084, "DarkFortress", new[] { "Cloaked Fiend", "Dark Makai" });

        //Test 2: Nulgath's Disdain 4085
        Story.KillQuest(4085, "DarkFortress", new[] { "Ninja Spy", "Dark Elemental", "Cloaked Fiend" });

        //Test 3: Bonesaw Break 4086
        if (!Story.QuestProgression(4086) || !Core.CheckInventory("Ultra Dark Mystery Stone Of Evil Animosity"))
        {
            Core.AddDrop("Ultra Dark Mystery Stone Of Evil Animosity");
            Core.EnsureAccept(4086);
            Core.HuntMonster("DarkFortress", "Infernalfiend", "Ultra Dark Mystery Stone");
            Core.EnsureComplete(4086);
            Bot.Wait.ForPickup("Ultra Dark Mystery Stone Of Evil Animosity");
        }

        //Test 4: Dark Battleground 4088
        Story.KillQuest(4088, "DarkFortress", new[] { "Cloaked Fiend|Dark Elemental|Dark Makai|Infernalfiend" });

        //Test 5: Shadows Within Shadows 4089
        Story.KillQuest(4089, "DarkFortress", "Dark Elemental");

        //Test 6: Runed Fiends 4090
        Story.KillQuest(4090, "DarkFortress", new[] { "Dark Elemental", "Cloaked Fiend" });

        //Surprise Test: Truth Revealed 4091
        Story.KillQuest(4091, "DarkFortress", "Wilhelm");

        //Final Exam: Your Own Worst Enemy 4092
        Story.KillQuest(4092, "DarkFortress", "Dage The Evil");
    }

    public void Seraph()
    {
        if (Bot.Quests.IsUnlocked(4186))
        {
            Core.Logger("You have already completed Seraph storyline");
            return;
        }
        Story.PreLoad(this);

        //Explore the Order's Headquarters 4181
        Story.MapItemQuest(4181, "Seraph", 3280);

        //Good Secrets are Hard to Find 4182
        Story.KillQuest(4182, "Seraph", "Seraphic Recruit");

        //Whispers Rumors and Seraphic Scrolls 4183
        Story.KillQuest(4183, "Seraph", new[] { "Legion Infiltrator", "Seraphic Recruit", "Legion Augur" });
        //Me? A Spy? 4184
        Story.KillQuest(4184, "Seraph", "Legion Augur");

        //It's War! 4185
        if (!Story.QuestProgression(4185))
        {
            Core.EnsureAccept(4185);
            Core.KillMonster("Seraph", "r6", "Left", "Legion Infiltrator", "Legion Infiltrators Defeated", 10, log: false);
            Core.EnsureComplete(4185);
        }

        Core.Logger("if the quest \"Finders Keepers\" [4186] is not Unlocked, the Bot will then do the previous Quest \"It's War!\" 10x");

        Core.RegisterQuests(4185);
        while (!Bot.ShouldExit && !Bot.Quests.IsUnlocked(4186))
            Core.KillMonster("Seraph", "r6", "Left", "Legion Infiltrator", "Legion Infiltrators Defeated", 10, log: false);
        Core.CancelRegisteredQuests();
    }

    public void LegionCrypt()
    {
        if (Core.isCompletedBefore(4195))
        {
            Core.Logger("You have already completed LegionCrypt storyline");
            return;
        }
        Story.PreLoad(this);

        //Finders Keepers 4186
        Story.KillQuest(4186, "LegionCrypt", "Gravedigger");

        //Infiltrate the Infantry 4187
        Story.KillQuest(4187, "LegionCrypt", "Undead Infantry");

        //You’re Doomed 4188
        Story.KillQuest(4188, "LegionCrypt", "Legion Doomknight");

        //Eyes on the Prize 4189
        Story.MapItemQuest(4189, "LegionCrypt", 3295, 5);
        Story.KillQuest(4189, "LegionCrypt", "Gravedigger");

        //Filler Quest 4190
        Story.MapItemQuest(4190, "LegionCrypt", 3296, 5);
        Story.KillQuest(4190, "LegionCrypt", "Legion Doomknight");

        //Insider Information 4191
        Story.MapItemQuest(4191, "LegionCrypt", 3297);

        //Spiky Situation 4192
        Story.KillQuest(4192, "LegionCrypt", new[] { "Gravedigger", "Undead Infantry|Legion Doomknight" });
        //Run it Back Again 4193
        Story.KillQuest(4193, "LegionCrypt", "Legion Doomknight");

        //Defeat Vincenzo! 4194
        Story.KillQuest(4194, "LegionCrypt", "Vincenzo");

        //Defeat Brutus! 4195
        Story.KillQuest(4195, "LegionCrypt", "Brutus");
    }

    public string[] RequiredItems = { "Envy Token V", "Envy Token IV", "Envy Token III", "Envy Token II", "Envy Token I" };

    public void EnvyMap()
    {
        if (Core.isCompletedBefore(4893))
        {
            Core.Logger("You have already completed Envy storyline");
            return;
        }
        Story.PreLoad(this);

        Core.AddDrop(RequiredItems);

        if (!Bot.Quests.IsUnlocked(4891))
        {
            if (!Core.CheckInventory("Envy Token V"))
            {
                if (!Core.CheckInventory("Envy Token IV"))
                {
                    if (!Core.CheckInventory("Envy Token III"))
                    {
                        if (!Core.CheckInventory("Envy Token II"))
                        {
                            if (!Core.CheckInventory("Envy Token I"))
                            {
                                //An Enviable Army 4884
                                Core.EnsureAccept(4884);
                                Core.HuntMonster("Envy", "Legion Defector", "Legion Defectors Beaten", 3);
                                Core.HuntMonster("Envy", "Fawning Sycophant", "Fawning Sycophants Beaten", 4);
                                Core.EnsureComplete(4884);
                                Bot.Wait.ForPickup("Envy Token I");
                            }
                            //Where’s Darkon? 4885
                            Core.EnsureAccept(4885);
                            Core.GetMapItem(4278, 4, "Envy");
                            Core.EnsureComplete(4885);
                            Bot.Wait.ForPickup("Envy Token II");
                        }
                        //Her Biggest Fans 4886
                        Core.EnsureAccept(4886);
                        Core.HuntMonster("Envy", "Fawning Sycophant", "Sycophant Hood");
                        Core.HuntMonster("Envy", "Fawning Sycophant", "Sycophant Tunic");
                        Core.HuntMonster("Envy", "Fawning Sycophant", "Sycophant Medallion");
                        Core.HuntMonster("Envy", "Fawning Sycophant", "Sycophant Boots");
                        Core.EnsureComplete(4886);
                        Bot.Wait.ForPickup("Envy Token III");
                    }
                    //Squee for Envy 4887
                    Core.EnsureAccept(4887);
                    Core.HuntMonster("Envy", "Fawning Sycophant", "Clue");
                    Core.HuntMonster("Envy", "Fawning Sycophant", "Secret");
                    Core.HuntMonster("Envy", "Fawning Sycophant", "Gossip");
                    Core.HuntMonster("Envy", "Fawning Sycophant", "Sincere but Unhelpful Praise");
                    Core.EnsureComplete(4887);

                    //Higher Up The Totem Pole 4888
                    Core.EnsureAccept(4888);
                    Core.HuntMonster("Envy", "Disciple of Envy", "Disciple Helm");
                    Core.HuntMonster("Envy", "Disciple of Envy", "Disciple Chestplate");
                    Core.HuntMonster("Envy", "Disciple of Envy", "Disciple Leggings");
                    Core.HuntMonster("Envy", "Disciple of Envy", "Disciple Blade");
                    Core.EnsureComplete(4888);
                    Bot.Wait.ForPickup("Envy Token IV");
                }

                //Fight for Envy 4889
                Core.EnsureAccept(4889);
                Core.HuntMonster("Envy", "Legion Spy", "Legion Spies Defeated", 8);
                Core.EnsureComplete(4889);
                Bot.Wait.ForPickup("Envy Token V");

                //Where’s That Employee Lounge Again? 4890
                Core.EnsureAccept(4890);
                Core.HuntMonster("Envy", "Disciple of Envy", "Semi-Useful Information");
                Core.HuntMonster("Envy", "Disciple of Envy", "Cult Propaganda");
                Core.HuntMonster("Envy", "Disciple of Envy", "Another Clue");
                Core.HuntMonster("Envy", "Disciple of Envy", "Bad Puns");
                Core.EnsureComplete(4890);
            }
        }
        //Group Effort 4891
        Story.KillQuest(4891, "Envy", new[] { "Legion Defector", "Fawning Sycophant", "Disciple of Envy" });

        //Defeat Envy 4892
        Story.KillQuest(4892, "Envy", "Envy");

        //The Queen of Envy 4893
        Story.KillQuest(4893, "Envy", "Queen of Envy");
    }

    public void Laken()
    {
        if (Core.isCompletedBefore(5658))
        {
            Core.Logger("You have already completed Laken storyline");
            return;
        }
        Story.PreLoad(this);

        //Get Rid of the Guards 5648
        Story.KillQuest(5648, "Laken", new[] { "Cyborg Dog", "Augmented Guard" });

        //Get the Data 5649
        Story.KillQuest(5649, "Laken", "Mad Scientist");

        //Find the Door Code 5650
        Story.KillQuest(5650, "Laken", "Augmented Guard|Mad Scientist");

        //Find the Lab 5651
        Story.MapItemQuest(5651, "Laken", 5123);

        //Take out Dr. Eisenbacke 5652
        Story.KillQuest(5652, "Laken", "Dr Eisenbacke");

        //Find Ada 5653
        Story.MapItemQuest(5653, "Laken", 5124);
        Story.KillQuest(5653, "Laken", "Dustbunny");

        //Keep Looking 5654
        Story.MapItemQuest(5654, "Laken", 5125);
        Story.KillQuest(5654, "Laken", "Closet Moth");

        //Gotcha! 5655
        Story.MapItemQuest(5655, "Laken", 5126);

        //Combat Practice 5656
        Core.EnsureAccept(5656);
        Core.KillMonster("Laken", "r10a", "Left", "Ada", "Spar With Ada");
        Core.EnsureComplete(5656);

        //Round 2 5657
        Core.EnsureAccept(5657);
        Core.KillMonster("Laken", "r11", "Left", "Ada", "Spar Again");
        Core.EnsureComplete(5657);

        //Third Time's A Charm 5658
        Core.EnsureAccept(5658);
        Core.KillMonster("Laken", "r12", "Left", "Ada", "Spar One More Time");
        Core.EnsureComplete(5658);
    }
}
