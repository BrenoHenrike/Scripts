//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class CoreSummer
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void DoAll()
    {
        DreadSpace();
        Deadmoor();
        Beleen();
        Collector();
        LivingDungeon();
        LunaCove();
        CyserosSecret();
    }

    public void DreadSpace(bool ReplicatorMerge = false)
    {
        if (Core.isCompletedBefore(4286))
            return;

        Story.PreLoad(this);

        // Ebil Dread Space (4263)
        Core.Join("dreadspace");
        Story.ChainQuest(4263);

        //1st Contact (4264)
        Story.KillQuest(4264, "dreadspace", "Undead Space Marine|Undead Space Warrior");

        //SO SLAY WE ALL! (4265)
        Story.KillQuest(4265, "dreadspace", "Undead Space Marine|Undead Space Warrior");

        //Starship Troopers (4266)
        Story.KillQuest(4266, "dreadspace", "Undead Space Marine|Undead Space Warrior");

        //Takai Me On (4267)
        Story.KillQuest(4267, "dreadspace", new[] { "Defense Turret", "Vaderix" });

        //We are the HORG! (4268)
        Story.KillQuest(4268, "dreadspace", "Cyber Horg");

        //Fix the Holodeck (4269)
        Story.KillQuest(4269, "dreadspace", new[] { "Cyber Horg", "Undead Space Warrior", "Undead Star Ranger", "Undead Space Marine" });

        //Gorn in 60 Seconds (4270)
        Story.KillQuest(4270, "dreadspace", new[] { "Dra'gorn", "Dra'gorn", "Dra'gorn" });

        //Captain Kirkland (4271)
        if (!Story.QuestProgression(4271))
        {
            Core.EnsureAccept(4271);
            Core.HuntMonster("dreadspace", "Red Trobble", "Red Space Fabric", 6, false);
            Adv.BuyItem("dreadspace", 1010, "Red Space Crew Shirt");
            Core.EnsureComplete(4271);
        }

        //Cowboyz vs Alienz (4273)
        Story.KillQuest(4273, "dreadspace", new[] { "Holo Gunslinger|Holo Outlaw", "Vaderix" });

        //Recover J6's Head! (4272)
        Story.KillQuest(4272, "dreadspace", "Holo Gunslinger|Holo Outlaw");
        if (ReplicatorMerge)
            return;

        //Titanic II (4274)
        Story.KillQuest(4274, "dreadspace", "Jack|Rosie");

        //Holo-Yulgar's (4275)
        Story.KillQuest(4275, "dreadspace", "Old Twilly");

        //Holodeck Survival Rate (4276)
        Story.KillQuest(4276, "dreadspace", "Hologram Turret");

        //Space Paladin Survivors!? (4277)
        Story.MapItemQuest(4277, "dreadspace", 3423, 6);

        //Trouble with Trobbles (4278)
        Story.KillQuest(4278, "dreadspace", "Trobble");

        //THEY LIVE! (4279)
        Story.KillQuest(4279, "dreadspace", "Defense Turret");

        //AlienZ (4280)
        Story.KillQuest(4280, "dreadspace", "Vaderix");

        //DO NOT OPEN - Space Door Codes (4281)
        Story.KillQuest(4281, "dreadspace", "Undead Space Marine|Undead Space Warrior");

        //The Search for Spork (4282)
        Story.KillQuest(4282, "dreadspace", "Undead Space Marine|Undead Space Warrior");

        //Crew Cut (4283)
        Story.KillQuest(4283, "dreadspace", new[] { "Vaderix", "Undead Space Marine", "Undead Space Warrior", "Cyber Horg" });

        //Restore Life Support (4284)
        Story.KillQuest(4284, "dreadspace", "Trobble|Cyber Horg");

        //DREAD SPACE BOSS (4285)
        Story.KillQuest(4285, "dreadspace", "Dread Space");

        //DREADER SPACE!!! (4286)
        Story.KillQuest(4286, "dreadspace", "Cyborg Beast Core");
    }

    public void Deadmoor()
    {
        if (Core.isCompletedBefore(4307))
            return;

        Story.PreLoad(this);

        // 4296 A Walking Nightmare
        Story.MapItemQuest(4296, "deadmoor", 3457);

        // 4297 A Ritual is Required
        Story.KillQuest(4297, "deadmoor", "Deadmoor Wraith");

        // 4298 A Spider's Finger
        Story.KillQuest(4298, "deadmoor", "Toxic Souleater");

        // 4299 Well Done
        Story.MapItemQuest(4299, "deadmoor", 3448);

        // 4300 Fighting A Horse
        Story.KillQuest(4300, "deadmoor", "Nightmare");

        // 4301 Wake the Dead
        Story.MapItemQuest(4301, "deadmoor", 3459);

        // 4302 Bone Breaking Bonds
        Story.MapItemQuest(4302, "deadmoor", new[] { 3449, 3450, 3451, 3452, 3453, 3454 });

        // 4303 Last Shreds of Humanity
        Story.KillQuest(4303, "deadmoor", "Toxic Souleater");

        // 4304 Consecrated Ground
        Story.MapItemQuest(4304, "deadmoor", 3455, 7);

        // 4305 Geist
        Story.KillQuest(4305, "deadmoor", "Geist");

        // 4306 The Last Caretaker
        Story.MapItemQuest(4306, "deadmoor", new[] { 3458, 3460, 3461, 3462, 3463, 3464, 3465 });

        // 4307 The Confrontation
        Story.KillQuest(4307, "deadmoor", "Banshee Mallora");
    }

    public void Beleen(bool ChaosFuzzyUnlockOnly = false)
    {
        //Quest Progress Check
        if (Core.isCompletedBefore(4330))
            return;

        //Needed AddDrop
        Core.AddDrop("Chaos Fuzzies");

        //Drearia on Demand - 4312
        Story.MapItemQuest(4312, "Drearia", 3485);
        Story.KillQuest(4312, "Drearia", new[] { "Dark Makai", "Evil Elemental", "Green Rat" });

        //Plant a Little Seed and Nature Grows - 4313
        Story.KillQuest(4313, "Drearia", "Dark Makai");

        //A Key Discovery - 4314
        Story.MapItemQuest(4314, "Drearia", 3466);
        Story.KillQuest(4314, "Drearia", "Dark Makai");

        //Creepy House... Yay! - 4315
        Story.MapItemQuest(4315, "Drearia", 3467);
        Story.KillQuest(4315, "Drearia", "Green Rat");

        //Sparkling Books - 4316
        Story.MapItemQuest(4316, "Drearia", 3468);

        //A Paladin in Peril - 4317
        Story.MapItemQuest(4317, "SwordHavenPink", 3469);

        //Pink Stinks! - 4318
        Story.MapItemQuest(4318, "SwordHavenPink", 3486, 5);
        Story.KillQuest(4318, "SwordHavenPink", "Pink Slime");

        //Rats, RATS! - 4319
        Story.KillQuest(4319, "SewerPink", "Pink Rat");

        //AdventureQuest Worm - 4320
        Story.KillQuest(4320, "SewerPink", "Cutie Grumbley");

        //UnBEARable Sight - 4321
        Story.MapItemQuest(4321, "PineWoodPink", 3470);
        Story.KillQuest(4321, "PineWoodPink", "Pink Grizzly");

        //Too Much Pink in Pinewood! - 4322
        Story.MapItemQuest(4322, "PineWoodPink", 3471, 5);
        Story.KillQuest(4322, "PineWoodPink", "Pink Shell Turtle");

        //Kill Sparkletooth - 4323
        Story.KillQuest(4323, "PineWoodPink", "Sparkletooth");

        //The Citadorable Plot - 4324
        Story.MapItemQuest(4324, "Citadel", 3472);
        if (ChaosFuzzyUnlockOnly)
            return;

        //Fuzzy Run Minigame - 4325
        if (!Core.isCompletedBefore(4325))
        {
            Core.EnsureAccept(4325);
            while (!Bot.ShouldExit && !Core.CheckInventory("Chaos Fuzzies", 30))
                Core.GetMapItem(3481, map: "Citadel");
            Core.EnsureComplete(4325);
        }

        //Fluffy Clouds - 4326
        Story.KillQuest(4326, "Pastelia", "Happy Cloud");

        //Super Model Makai Hair - 4327
        Story.KillQuest(4327, "Pastelia", "Cutie Makai");

        //Squeaky Business - 4328
        Story.KillQuest(4328, "Pastelia", "Pink Rat");

        //The Queen's Offering - 4329
        Story.MapItemQuest(4329, "Pastelia", 3473);

        //Boss Fight Beleen! - 4330
        Story.KillQuest(4330, "Pastelia", "Chaos Queen Beleen");
    }

    public void Collector()
    {
        if (Core.isCompletedBefore(1348))
            return;

        Story.PreLoad(this);

        // This Town in a Desktop Globe
        Story.MapItemQuest(1293, "Terrarium", 586);

        // Dust Bunnies Can't Hide
        Story.KillQuest(1294, "Terrarium", "Dustbunny of Doom");

        // Au Contr-air holes
        Story.MapItemQuest(1295, "Terrarium", 587, 10);

        // Hero Eats Everything. Even Pellets
        Story.MapItemQuest(1296, "Terrarium", 588, 12);

        // The Moth Defeats the Man?
        Story.KillQuest(1297, "Terrarium", "Death on Wings");

        // Can you find them?
        Story.MapItemQuest(1298, "Terrarium", new[] { 589, 590, 591, 592, 593 });

        // You've Been Looking Everywhere!
        Story.MapItemQuest(1299, "Terrarium", new[] { 593, 594, 595, 596, 604 });

        // Go for Grease!
        Story.KillQuest(1300, "Terrarium", "Death on Wings");

        // Doppelganger Wants to Hit You
        Story.KillQuest(1308, "Terrarium", new[] { "Doppleganger of Will", "Doppleganger of Fred" });

        // Catapult Climb
        Story.MapItemQuest(1309, "Terrarium", 604);

        // The Treasure that You Seek - 976
        Story.KillQuest(1339, "prehistoric", "Gigantosaurus");

        // These Trees - 1340
        Story.MapItemQuest(1340, "prehistoric", 630, 11);

        // Ewwww... Totally Gross - 1341
        Story.KillQuest(1341, "prehistoric", "Gigantosaurus");

        // The Eggs, Exciting and New - 1342
        Story.MapItemQuest(1342, "prehistoric", 631, 4);

        // Someday (Today) Their Mother Will Die - 1343
        Story.KillQuest(1343, "prehistoric", "Mother TerrorDOOMKill");

        // More Aliens Than You Can Handle?
        Story.KillQuest(1344, "Future", "Alien Dino");

        // Spy Eyes
        Story.MapItemQuest(1345, "Future", 632, 2);

        // Smashed to Pieces
        Story.MapItemQuest(1346, "Future", 633, 7);

        // Tired of My Vigilette Dreams
        if (!Story.QuestProgression(1347))
        {
            Core.EnsureAccept(1347);
            Core.HuntMonster("Future", "Red-Eyed Alien", "Small Fragment Acquired", 5);
            Core.HuntMonster("Future", "Red-Eyed Alien", "Medium Piece Acquired", 5);
            Core.EnsureComplete(1347);
        }

        // You're Not the Boss of Me Now
        if (!Story.QuestProgression(1348))
        {
            Core.EnsureAccept(1348);
            Core.HuntMonster("Future", "The Collector", "Collector Vanquished");
            Core.EnsureComplete(1348);
        }
    }

    public void LivingDungeon()
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
    public void LunaCove()
    {
        if (Core.CheckInventory("Your Moonstone"))
            return;

        Core.AddDrop("Ritual Items", "Stale Chips", "Air Pump", "Shiny Stone", "Were Hair", "For the Horde!", "Werewolf Discovery", "Stolen Lycanstone", "Gravefang's Elixir", "Dis-Lycan this", "Your Moonstone");

        //4399|Time to Rock this Finale!
        if (!Core.CheckInventory("Your Moonstone"))
        {
            //4398|Dis-lycan What Happened
            if (!Core.CheckInventory("Dis-Lycan this"))
            {
                //4397|Fur the Right Thing
                if (!Core.CheckInventory("Gravefang's Elixir"))
                {
                    //4396|Find the Lycanstone
                    if (!Core.CheckInventory("Stolen Lycanstone"))
                    {
                        //4394|"Were" Did We Go Wrong?
                        if (!Core.CheckInventory("Werewolf Discovery"))
                        {
                            //4395|Lycan this Next Task
                            if (!Core.CheckInventory("For the Horde!"))
                            {
                                //4393|Total Eclipse of the Heart
                                if (!Core.CheckInventory("Were Hair"))
                                {
                                    //4392|Hex on the Beach(balls)
                                    if (!Core.CheckInventory("Shiny Stone"))
                                    {
                                        //4391|Fish’n’Chips on my Shoulder
                                        if (!Core.CheckInventory("Air Pump"))
                                        {
                                            //4390|A Ritual by any Other Name
                                            if (!Core.CheckInventory("Stale Chips"))
                                            {
                                                //4389|Let's Get Started
                                                if (!Core.CheckInventory("Ritual Items"))
                                                {
                                                    Core.EnsureAccept(4389);
                                                    Core.HuntMonster("lunacove", "Cove Warrior", "Ritual Materials", 15);
                                                    Core.HuntMonster("lunacove", "Plessie", "Plessie Fang Tooth");
                                                    Core.HuntMonster("lunacove", "Island Girl", "Candle", 5);
                                                    Core.EnsureComplete(4389);
                                                    Bot.Wait.ForPickup("Ritual Items");
                                                }
                                                Core.EnsureAccept(4390);
                                                Core.GetMapItem(3533, 10, "lunacove");
                                                Core.HuntMonster("lunacove", "Cove Fisher", "Bag of Chips");
                                                Core.EnsureComplete(4390);
                                                Bot.Wait.ForPickup("Stale Chips");
                                            }
                                            Core.EnsureAccept(4391);
                                            Core.HuntMonster("lunacove", "Lunar Villager", "Chips", 3);
                                            Core.EnsureComplete(4391);
                                            Bot.Wait.ForPickup("Air Pump");
                                        }
                                        Core.EnsureAccept(4392);
                                        Core.HuntMonster("lunacove", "Lunar Villager", "Villager Chastised", 10);
                                        Core.HuntMonster("lunacove", "Beach Ball", "Deflated Beach Balls", 5);
                                        Core.EnsureComplete(4392);
                                        Bot.Wait.ForPickup("Shiny Stone");
                                    }
                                    Core.EnsureAccept(4393);
                                    Core.HuntMonster("lunacove", "Coral Merdraconian", "Coral Branch", 3);
                                    Core.HuntMonster("lunacove", "Plessie", "Plessie Scale", 2);
                                    Core.EnsureComplete(4393);
                                    Bot.Wait.ForPickup("Were Hair");
                                }
                                Core.EnsureAccept(4395);
                                Core.GetMapItem(3534, 7, "lunacove");
                                Core.EnsureComplete(4395);
                                Bot.Wait.ForPickup("For the Horde!");
                            }
                            Core.EnsureAccept(4394);
                            Core.HuntMonster("lunacove", "Horde Knight", "Horde Knight Defeated", 7);
                            Core.HuntMonster("lunacove", "Horde Lycan", "Horde Lycan Defeated", 8);
                            Core.EnsureComplete(4394);
                            Bot.Wait.ForPickup("Werewolf Discovery");
                        }
                        Core.EnsureAccept(4396);
                        Core.HuntMonster("lunacove", "Horde Knight", "Lycanstone");
                        Core.EnsureComplete(4396);
                        Bot.Wait.ForPickup("Stolen Lycanstone");
                    }
                    Core.EnsureAccept(4397);
                    Core.HuntMonster("lunacove", "Gravefang", "Gravefang Defeated");
                    Core.EnsureComplete(4397);
                    Bot.Wait.ForPickup("Gravefang's Elixir");
                }
                Core.EnsureAccept(4398);
                Core.HuntMonster("lunacove", "Beach Werewolf", "Beach Werewolf Defeated", 6);
                Core.HuntMonster("lunacove", "Lunar Lycan", "Lunar Lycan Subdued", 6);
                Core.EnsureComplete(4398);
                Bot.Wait.ForPickup("Dis-Lycan this");
            }
            Core.EnsureAccept(4399);
            Core.HuntMonster("lunacove", "Moonrock", "Moon Rock Smashed");
            Core.EnsureComplete(4399);
            Bot.Wait.ForPickup("Your Moonstone");
        }
    }


    public void CyserosSecret()
    {
        if (Core.isCompletedBefore(4375))
            return;

        Story.PreLoad(this);

        //Find The Leak (4365)
        Story.KillQuest(4365, "goose", "Queen's Sage");

        //Locking The Door (4366)
        Story.KillQuest(4366, "goose", "Chris P. Bacon");

        //Thin Their Ranks (4367)
        Story.KillQuest(4367, "goose", "Queen's Sage");

        //1st Goose Bone (4368)
        Story.KillQuest(4368, "goose", "Sock Gorilla");

        //2nd Goose Bone (4369)
        Story.KillQuest(4369, "goose", "Can of Paint");

        //Last Goose Bone (4370)
        Story.KillQuest(4370, "goose", "Queen's ArchSage");

        //Ancient Goose? (4371)
        Story.MapItemQuest(4371, "goose", 3562);

        //Raccoon Schematics (4372)
        Story.MapItemQuest(4372, "goose", 3561, 8);

        //No Fur For You (4373)
        Story.KillQuest(4373, "goose", "Queen's Sage");

        //Raccoon Heart (4374)
        Story.KillQuest(4374, "goose", "Can of Paint");

        //Shut It Down (4375)
        Story.KillQuest(4375, "goose", "Ancient Goose");
    }
}