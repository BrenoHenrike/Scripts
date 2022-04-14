//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class BrightOak
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailies Daily = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        doall();

        Core.SetOptions(false);
    }

    public void doall()
    {
        if (Core.isCompletedBefore(4810))
            return;

        Story.PreLoad();

        Ælfred();
        AvenGreywhorl();
        FlixSpiderwhisp();
        LapisPart1();
        RavinosBrightgladePart1();
        RavinosBrightgladePart2();
        LapisPart2();
        RavinosBrightgladePart3();
        ExtraREP(true);
    }

    public void Ælfred()
    {
        if (Core.isCompletedBefore(4644))
            return;

        // Map: Rivensylth
        // Equip spirit animal packets
        Core.SendPackets("%xt%zm%getMapItem%104347%3935%");
        Bot.Sleep(Core.ActionDelay);
        Core.SendPackets("%xt%zm%equipItem%104347%32057%");

        // Take to the Skies 
        Story.MapItemQuest(4637, "Rivensylth", 3944);

        // Ready to Pounce
        Story.KillQuest(4638, "Rivensylth", "Cave Creeper");

        // What a Hoot
        Story.MapItemQuest(4639, "Rivensylth", 3945, 4);

        // Shroom Spreading
        Story.KillQuest(4640, "Rivensylth", "Mushroom");

        // Protect the Nest
        Story.KillQuest(4641, "Pines", "Pine Grizzly");
        Story.MapItemQuest(4641, "Rivensylth", 3948, 4);

        // Night in Shining Armor
        Story.KillQuest(4642, "Rivensylth", "Cave Creeper|Draklet|Mushroom|Rivensylth Spider");

        // Finding Rivensylth
        Story.MapItemQuest(4643, "Rivensylth", 3946);

        // Zero Tolerance for Nature
        Story.KillQuest(4644, "Rivensylth", "Avada");
    }

    public void AvenGreywhorl()
    {
        if (Core.isCompletedBefore(4668))
            return;

        Core.AddDrop("Restoration of Nature Potion", "Paddylump's Elixir");

        // Rose By Any Other Name
        Story.KillQuest(4466, "brightoak", "Bright Treeant");

        // Map: Elfhame
        // Corruption of Elfhame
        Story.KillQuest(4659, "elfhame", "Ruin Stalker");
        Story.MapItemQuest(4659, "elfhame", 3983);

        // Restoring Nature
        if (!Story.QuestProgression(4660))
            NaturePotion(1);

        // Unlock the Guardian's First Rune
        if (!Story.QuestProgression(4661))
        {
            NaturePotion(1);
            Story.KillQuest(4661, "elfhame", "Blighted Deer ");
        }

        // Gnome Sweet Gnome
        if (!Story.QuestProgression(4662))
        {
            Core.EnsureAccept(4662);
            if (!Core.CheckInventory("Paddylump's Elixir"))
            {
                //Prereq. Quest: Omnomnoms:
                //Prereq. Item: Paddylump's Elixir:
                //--- Mapname: mudluk
                Core.AddDrop("Paddylump's Elixir");
                Core.EnsureAccept(820);
                Core.HuntMonster("cloister", "Acornent", "Wiggly Worm", 15);
                Core.HuntMonster("cloister", "Karasu", "MegaMite", 10);
                Core.EnsureComplete(820);
                Bot.Wait.ForPickup("Paddylump's Elixir");
            }
            Core.HuntMonster("elfhame", "Ruin Dweller", "Ruin Dweller Defeated", 12);
            Core.EnsureComplete(4662);
        }

        // Unlock the Guardian's Second Rune
        if (!Story.QuestProgression(4663))
        {
            Core.EnsureAccept(4663);
            NaturePotion(2);
            Core.HuntMonster("elfhame", "Wolfrider", "Wolfrider Maimed", 4);
            Core.EnsureComplete(4663);
        }

        // I Know kaRATe
        Story.KillQuest(4664, "elfhame", "Ratawampus");

        // Unlock the Guardian's Third Rune
        if (!Story.QuestProgression(4665))
        {
            Core.EnsureAccept(4665);
            NaturePotion(3);
            Core.HuntMonster("elfhame", "Ratawampus", "Ratawampus Cleared", 2);
            Core.HuntMonster("elfhame", "Ruin Dweller", "Ruin Dweller Cleared", 3);
            Core.EnsureComplete(4665);
        }

        // Unlock the Guardian's Fourth Rune
        if (!Story.QuestProgression(4666))
        {
            Core.EnsureAccept(4666);
            NaturePotion(4);
            Core.HuntMonster("elfhame", "Ruin Stalker", "Ruin Stalker Contained", 6);
            Core.EnsureComplete(4666);
        }

        // Unlocking the Guardian's Mouth
        Story.MapItemQuest(4667, "elfhame", 3984);

        // Defeat the Guardian Spirit
        Story.KillQuest(4668, "elfhame", "Guardian Spirit");

        void NaturePotion(int quant)
        {
            if (Core.CheckInventory("Restoration of Nature Potion", quant))
                return;

            Core.AddDrop("Restoration of Nature Potion");

            while (!Core.CheckInventory("Restoration of Nature Potion", quant))
            {
                Core.EnsureAccept(4660);
                Core.BuyItem("sandsea", 245, "Water of Life");
                Core.HuntMonster("brightoak", "Bright Treeant", "Bright Ore", 3);
                Core.KillMonster("brightoak", "r8", "Left", "*", "Herbal Remedy", 4);
                Core.EnsureComplete(4660);
                Bot.Wait.ForPickup("Restoration of Nature Potion");
            }
        }
    }

    public void FlixSpiderwhisp()
    {
        if (Core.isCompletedBefore(4470))
            return;

        // World Tree Wrangling
        Story.KillQuest(4469, "Brightoak", "Hootbear");

        // Guardians Gone Bad
        Story.KillQuest(4470, "Brightoak", "Brightpool Guardian");
        Story.MapItemQuest(4470, "Brightoak", 3667, 5);
    }

    public void LapisPart1()
    {
        if (Core.isCompletedBefore(4468))
            return;

        // That's So Lapis
        Story.KillQuest(4467, "Brightoak", "Grove Spore");
        Story.MapItemQuest(4467, "Brightoak", 3666, 10);

        // Guts for a Greater Mind     
        Story.KillQuest(4468, "Brightoak", "Twisted Goblin");
    }

    public void RavinosBrightgladePart1()
    {
        if (Core.isCompletedBefore(4700))
            return;

        // Cleanse the Grove
        Story.KillQuest(4463, "Darkheart", new[] { "Wolfwood", "Hootbear", "Tainted Earth" });

        // Survey the Landscape
        Story.MapItemQuest(4692, "Darkheart", 4052);

        // Corruption in the Grove 
        Story.KillQuest(4693, "Darkheart", "Mutated Leech");

        // Perilous Supply Run
        Story.KillQuest(4694, "Darkheart", "Mutated Leech|Tainted Earth|Toxic Grove Spider|Wisterrora");

        // Purify Wisterrora
        Story.KillQuest(4695, "Darkheart", "Wisterrora");
        Story.MapItemQuest(4695, "Darkheart", 4053, 6);

        // Arachnophobia
        Story.KillQuest(4696, "Darkheart", "Toxic Grove Spider");
        Story.MapItemQuest(4696, "Darkheart", 4054, 6);

        // Brighten up your Day
        Story.KillQuest(4697, "Brightoak", "Brightpool Guardian");
        Story.MapItemQuest(4697, "Darkheart", 4055, 7);

        // Cleanse the Grove
        if (!Story.QuestProgression(4698))
        {
            Core.EnsureAccept(4698);
            Core.HuntMonster("Darkheart", "Tainted Earth", "Tainted Earth Removed", 8);
            Core.HuntMonster("Darkheart", "Toxic Grove Spider", "Toxic Grove Spider Dispatched", 5);
            Core.HuntMonster("Darkheart", "Mutated Leech", "Mutated Leech Slain", 6);
            Core.EnsureComplete(4698);
        }

        // X Marks the Spot
        Story.MapItemQuest(4699, "Darkheart", 4056);

        // Defeat the Gaiazor
        Story.KillQuest(4700, "Darkheart", "Gaiazor");
    }

    public void RavinosBrightgladePart2()
    {
        if (Core.isCompletedBefore(4804))
            return;

        Core.AddDrop("Ravinos Token I", "Ravinos Token II", "Ravinos Token III", "Ravinos Token IV", "Ravinos Token V");

        // It's Too Bad I'm Poisonous Now
        // Ravinos Token V
        if (!Core.CheckInventory("Ravinos Token V"))
        {
            // Like One Of Those Toddler Gates
            // Ravinos Token VI
            if (!Core.CheckInventory("Ravinos Token IV"))
            {
                // But...Our Stuff!
                // Ravinos Token III
                if (!Core.CheckInventory("Ravinos Token III"))
                {
                    // Too Many Nasties
                    // Ravinos Token II
                    if (!Core.CheckInventory("Ravinos Token II"))
                    {
                        // We Have Some Stragglers
                        // Ravinos Token I
                        if (!Core.CheckInventory("Ravinos Token I"))
                        {
                            Core.EnsureAccept(4799);
                            Core.Join("Gaiazor");
                            Core.GetMapItem(4204, 6);
                            Core.EnsureComplete(4799);
                            Bot.Wait.ForPickup("Ravinos Token I");
                        }
                        Core.EnsureAccept(4800);
                        Core.HuntMonster("Gaiazor", "Wolfwood", "Wolfwood Slain", 4);
                        Core.HuntMonster("Gaiazor", "Wisterrora", "Wisterrora Slain", 4);
                        Core.HuntMonster("Gaiazor", "Tree Golem", "Tree Golem Slain", 4);
                        Core.EnsureComplete(4800);
                        Bot.Wait.ForPickup("Ravinos Token II");
                    }
                    Core.EnsureAccept(4801);
                    Core.HuntMonster("Gaiazor", "Tree Golem", "Lapis' Runestones");
                    Core.HuntMonster("Gaiazor", "Wolfwood", "Flix's Fertilizer");
                    Core.HuntMonster("Gaiazor", "Wisterrora", "Zephyr's Toolkit");
                    Core.EnsureComplete(4801);
                    Bot.Wait.ForPickup("Ravinos Token III");
                }
                Core.EnsureAccept(4802);
                Core.HuntMonster("Gaiazor", "Tree Golem", "Tree Golem Roots", 5);
                Core.HuntMonster("Gaiazor", "Wisterrora", "Wisterrora Thorns", 5);
                Core.GetMapItem(4205, 5);
                Core.EnsureComplete(4802);
                Bot.Wait.ForPickup("Ravinos Token IV");
            }
            Core.EnsureAccept(4803);
            Core.HuntMonster("Darkheart", "Toxic Grove Spider", "Grove Spider Silk", 6);
            Core.HuntMonster("Bloodtusk", "Trollola Plant", "Trollola Nectar", 5);
            Core.HuntMonster("Firestorm", "Sulfur Imp", "Searbush", 2);
            Core.EnsureComplete(4803);
            Bot.Wait.ForPickup("Ravinos Token V");
        }

        // Talk to Lapis
        Story.MapItemQuest(4804, "Gaiazor", 4206);

    }

    public void LapisPart2()
    {
        if (Core.isCompletedBefore(4808))
            return;

        Core.AddDrop("Lapis Token I", "Lapis Token II", "Lapis Token III");

        // Even a Noob Can Do It
        // Lapis Token III
        if (!Core.CheckInventory("Lapis Token III"))
        {
            // Outside Assistance
            // Lapis Token II
            if (!Core.CheckInventory("Lapis Token II"))
            {
                // Better Than A Magic 8-Ball
                // Lapis Token I
                if (!Core.CheckInventory("Lapis Token I"))
                {
                    Core.EnsureAccept(4805);
                    Core.HuntMonster("Gaiazor", "Wolfwood", "Wolfwood Twigs", 7);
                    Core.HuntMonster("Gaiazor", "Wisterrora", "Drop of Wisterrora Ichor");
                    Core.EnsureComplete(4805);
                    Bot.Wait.ForPickup("Lapis Token I");
                }
                Core.EnsureAccept(4806);
                if (!Core.CheckInventory("Sparrow's Blood"))
                    Daily.SparrowsBlood();
                Core.EnsureComplete(4806);
                Bot.Wait.ForPickup("Lapis Token II");
            }
            Core.EnsureAccept(4807);
            Core.GetMapItem(4207, 1, "Gaiazor");
            Core.GetMapItem(4208, 1, "Gaiazor");
            Core.GetMapItem(4209, 1, "Gaiazor");
            Core.EnsureComplete(4807);
            Bot.Wait.ForPickup("Lapis Token III");
        }

        // Return to Ravinos
        if (Core.CheckInventory("Lapis Token III") && !Story.QuestProgression(4808))
        {
            Core.EnsureAccept(4808);
            Core.GetMapItem(4210, 1, "Gaiazor");
            Core.EnsureComplete(4808);
        }
    }

    public void RavinosBrightgladePart3()
    {
        if (Core.isCompletedBefore(4810))
            return;

        // Defeat the Traitor
        Story.KillQuest(4809, "Gaiazor", "Nevanna");

        // Defeat the Beast
        Story.KillQuest(4810, "Gaiazor", "Gaiazor");
    }

    public void ExtraREP(bool Extra = true)
    {
        if (!Extra)
            return;

        // Catering to Craftsmanship
        if (Daily.CheckDaily(4471))
        {
            Core.EnsureAccept(4471);
            Core.HuntMonster("Brightoak", "Bright Treeant", "Treeant Trunk", 5);
            Core.KillMonster("Brightoak", "r8", "Spawn", "Wolfwood", "Wolfwood Fur", 7);
            Core.EnsureComplete(4471);
        }

        // Some Disassembly Required   
        if (Daily.CheckDaily(4472))
        {
            Core.EnsureAccept(4472);
            Core.HuntMonster("Brightoak", "Brightpool Guardian", "Disciplined Guardian", 7);
            Core.EnsureComplete(4472);
        }
    }
}