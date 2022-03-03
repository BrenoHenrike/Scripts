//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class BrightOak
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailys Dailys = new CoreDailys();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        doall();

        Core.SetOptions(false);
    }

    public void doall()
    {
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
        Story.KillQuest(4641, "Rivensylth", "Pine Grizzly");
        Story.MapItemQuest(4641, "Rivensylth", 3946, 4);

        // Night in Shining Armor
        Story.KillQuest(4642, "Rivensylth", "Cave Creeper|Draklet|Mushroom|Rivensylth Spider");

        // Finding Rivensylth
        Story.MapItemQuest(4643, "Rivensylth", 3947);

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
        if (!Story.QuestProgression(4660) || !Core.CheckInventory("Restoration of Nature Potion", 10))
        {
            Core.EnsureAccept(4660);
            Core.BuyItem("sandsea", 245, "Water of Life");
            Core.HuntMonster("elfhame", "Bright Treeant", "Bright Ore ", 3);
            Core.HuntMonster("elfhame", "Wolfwood", "Herbal Remedy", 4);
            Core.EnsureComplete(4660);
        }

        // Unlock the Guardian's First Rune
        if (!Story.QuestProgression(4661))
        {
            // Gnome Sweet Gnome
            if (!Core.CheckInventory("Paddylump's Elixir"))
            {
                //Prereq. Quest: Omnomnoms:
                //Prereq. Item: Paddylump's Elixir:
                //--- Mapname: mudluk
                Core.EnsureAccept(820);
                Core.HuntMonster("cloister", "Acornent");
                Core.HuntMonster("cloister", "Karasu");
                Core.EnsureComplete(820);
                Bot.Wait.ForPickup("Paddylump's Elixir");
            }
            Story.KillQuest(4661, "elfhame", " Ruin Dweller");
        }

        // Unlock the Guardian's Second Rune
        if (!Core.CheckInventory("Restoration of Nature Potion", 4) || !Story.QuestProgression(4665))
        {
            Core.EnsureAccept(4663);
            Core.HuntMonster("elfhame", "Wolfrider", "Wolfrider Maimed", 4);
            Core.EnsureComplete(4663);
        }

        // I Know kaRATe
        Story.KillQuest(4664, "elfhame", "Ratawampus");

        // Unlock the Guardian's Third Rune
        if (!Core.CheckInventory("Restoration of Nature Potion", 4) || !Story.QuestProgression(4665))
        {
            Core.EnsureAccept(4665);
            Core.HuntMonster("elfhame", "Ratawampus", "Ratawampus Cleared", 2);
            Core.HuntMonster("elfhame", "Ruin Dweller", "Ruin Dweller Cleared", 3);
            Core.EnsureComplete(4665);
        }

        // Unlock the Guardian's Fourth Rune
        if (!Core.CheckInventory("Restoration of Nature Potion", 4) || !Story.QuestProgression(4667))
        {
            Core.EnsureAccept(4667);
            Core.HuntMonster("elfhame", "Ruin Stalker", "Ruin Stalker Contained", 6);
            Core.EnsureComplete(4667);
        }

        // Unlocking the Guardian's Mouth
        Story.MapItemQuest(4667, "elfhame", 3984);

        // Defeat the Guardian Spirit
        Story.KillQuest(4668, "elfhame", "Guardian Spirit");
    }

    public void FlixSpiderwhisp()
    {

        if (Core.isCompletedBefore(4470))
            return;

        // World Tree Wrangling
        Story.KillQuest(4469, "Brightoak", "Hootbear");

        // Guardians Gone Bad
        Story.KillQuest(4469, "Brightoak", "Brightpool Guardian");
        Story.MapItemQuest(4470, "Brightoak", 3667, 5);
    }

    public void LapisPart1()
    {

        if (Core.isCompletedBefore(4808))
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

        // Cleanse the Grove 4463
        Story.KillQuest(4698, "Darkheart", new[] { "Wolfwood", "Hootbear", "Tainted Earth" });

        // Survey the Landscape
        Story.MapItemQuest(4692, "Brightoak", 4052);

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
        Story.KillQuest(4697, "Darkheart", "Brightpool Guardian");
        Story.MapItemQuest(4697, "Darkheart", 4055, 7);

        // Cleanse the Grove
        Story.KillQuest(4698, "Darkheart", new[] { "Tainted Earth", "Toxic Grove Spider", "Mutated Leech" });

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

        // We Have Some Stragglers
        Story.MapItemQuest(4799, "Gaiazor", 4204, 6);
        Bot.Wait.ForPickup("Ravinos Token I");

        // Too Many Nasties
        Story.KillQuest(4800, "Gaiazor", new[] { "Wolfwood", "Wisterrora", "Tree Golem" });
        Bot.Wait.ForPickup("Ravinos Token II");

        // But...Our Stuff!
        Story.KillQuest(4801, "Gaiazor", new[] { "Tree Golem", "Wolfwood", "Wisterrora" });
        Bot.Wait.ForPickup("Ravinos Token III");

        // Like One Of Those Toddler Gates
        Story.KillQuest(4802, "Gaiazor", new[] { "Tree Golem", "Wisterrora" });
        Story.MapItemQuest(4802, "Gaiazor", 4205, 5);
        Bot.Wait.ForPickup("Ravinos Token IV");

        // It's Too Bad I'm Poisonous Now
        Story.KillQuest(4803, "Gaiazor", "Toxic Grove Spider");
        Story.KillQuest(4803, "bloodtusk", "Trollola Plant");
        Story.KillQuest(4803, "firestorm", "Sulfur Imp");
        Bot.Wait.ForPickup("Ravinos Token V");

        // Talk to Lapis
        Story.MapItemQuest(4804, "Gaiazor", 4206);

    }

    public void LapisPart2()
    {
        if (Core.isCompletedBefore(4808))
            return;
        Core.AddDrop("Lapis Token I", "Lapis Token II", "Lapis Token III");

        // Better Than A Magic 8-Ball
        Story.KillQuest(4805, "Gaiazor", new[] { "Wolfwood", "Wisterrora" });
        Bot.Wait.ForPickup("Lapis Token I");

        // Outside Assistance
        if (!Story.QuestProgression(4806))
        {
            Core.EnsureAccept(4806);
            if (!Core.CheckInventory("Sparrow's Blood"))
                Dailys.SparrowsBlood();
            Core.HuntMonster("Gaiazor", "Wolfwood", "Wolfwood Twigs ", 7);
            Core.HuntMonster("Gaiazor", "Wisterrora", "Drop of Wisterorra Ichor");
            Bot.Wait.ForPickup("Lapis Token II");
            Core.EnsureComplete(4806);
        }

        // Even a Noob Can Do It
        Story.MapItemQuest(4807, "Gaiazor", 4209);
        Story.MapItemQuest(4807, "Gaiazor", 4208);
        Story.MapItemQuest(4807, "Gaiazor", 4207);

        // Return to Ravinos
        Story.MapItemQuest(4808, "Gaiazor", 4210);

    }

    public void RavinosBrightgladePart3()
    {
        if (Core.isCompletedBefore(4804))
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
        Dailys.CheckDaily(4471);
        {
            Core.EnsureAccept(4471);
            Core.HuntMonster("Brightoak", "Bright Treeant", "Treeant Trunk", 5);
            Core.HuntMonster("Brightoak", "Wolfwood", "Wolfwood Fur", 7);
            Core.EnsureComplete(4471);
        }

        // Some Disassembly Required   
        if (Dailys.CheckDaily(4472))
        {
            Core.EnsureAccept(4472);
            Core.HuntMonster("Brightoak", "Brightpool Guardian", "Disciplined Guardian", 7);
            Core.EnsureComplete(4472);
        }

    }
}