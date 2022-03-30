//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class CoreExtriki
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void CompleteCoreExtriki()
    {
        //Progress Check
        if (Core.isCompletedBefore(5847))
            return;

        //Preload Quests
        Story.PreLoad();

        TheRift();
        CharredPath();
        Underglade();
        Extriki();

    }

    public void TheRift()
    {
        //What Happened to Baldric?
        Story.MapItemQuest(5791, "therift", 5228);
    }

    public void CharredPath()
    {
        //Progress Check
        if (Core.isCompletedBefore(5836))
            return;

        //Quests Here Bug Out So This is Needed.
        Core.AcceptandCompleteTries = 3;

        //Preload Quests
        Story.PreLoad();

        //Get a Monitor
        Story.MapItemQuest(5803, "charredpath", 5248);

        //Follow the Pattern.. He Ne Ar Kr
        Story.KillQuest(5804, "crashsite", "Dwakel Blaster|Dwakel Warrior|Flamethrower Dwakel");
        Story.MapItemQuest(5804, "crashsite", 5249);

        //The PTM is Ready!
        Story.KillQuest(5805, "charredpath", new[] { "Noxious Fumes", "Toxic Bile" });
        Story.MapItemQuest(5805, "charredpath", 5256);

        //Save the Creatures
        Story.KillQuest(5806, "charredpath", "Ragewing");
        Story.MapItemQuest(5806, "charredpath", 5250, 6);

        //Clear the Treeants
        Story.KillQuest(5807, "charredpath", "Toxic Treeant");
        Story.MapItemQuest(5807, "charredpath", 5251, 3);

        //Excise the Infection
        Story.KillQuest(5808, "charredpath", "Infected Hare");

        //Root out the Plague
        Story.KillQuest(5809, "charredpath", "Plague Spreader");
        Story.MapItemQuest(5809, "charredpath", 5252, 6);

        //I Said Yes, Yes, Yes
        Story.MapItemQuest(5810, "charredpath", 5255, AutoCompleteQuest: false);

        //Rally the Mages
        Story.KillQuest(5811, "therift", "Mana Chest");
        Story.MapItemQuest(5811, "therift", 5253, 4);

        //Wisteria Hysteria
        Story.KillQuest(5812, "charredpath", "Toxic Wisteria");

        //Confront Extriki
        Story.MapItemQuest(5813, "charredpath", 5254, AutoCompleteQuest: false);

        //Remove the Bile
        Story.KillQuest(5819, "charredpath", "Toxic Bile|Noxious Fumes");

        //Get the Plague (Crystal)
        Story.KillQuest(5820, "charredpath", "Plague Spreader");

        //Grab the Growth
        Story.KillQuest(5821, "charredpath", "Infected Hare|Ragewing");

        //Treeants for Wood
        Story.KillQuest(5822, "farm", "Treeant");

        //Sand sounds better than Litter...
        Story.KillQuest(5823, "baconcat", "Litter Elemental");

        //Destroy the Zognax
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(5824, "charredpath", "Zognax");

        //Bandages Needed
        Story.KillQuest(5830, "charredpath", "Ravenous Parasite");

        //Shinies!
        Story.KillQuest(5831, "skytower", new[] { "Sunstone", "Moonstone", "Star Sapphire" });

        //Plushies
        Story.KillQuest(5832, "sewerpink", "Cutie Grumbley");

        //Sleepies
        if (!Story.QuestProgression(5833))
        {
            Core.EnsureAccept(5833);

            if (!Core.CheckInventory("Black Metal Cold Brew"))
            {
                Core.AddDrop("Black Metal Cold Brew");
                Core.EnsureAccept(5834);
                Core.HuntMonster("therift", "Mana Chest", "Liquid Mana", 4);
                Core.HuntMonster("therift", "Ravenous Parasite", "Parasite \"Spice\"", 4);
                Core.EnsureComplete(5834);
            }

            Core.EnsureComplete(5833);
        }

        //What's Next?
        Story.MapItemQuest(5835, "charredpath", 5270, AutoCompleteQuest: false);

        //Make a Bed
        Story.KillQuest(5836, "charredpath", "Pustulisk");
    }

    public void Underglade()
    {
        //Progress Check
        if (Core.isCompletedBefore(5846))
            return;

        //Quests Here Bug Out So This is Needed.
        Core.AcceptandCompleteTries = 3;

        //Preload Quests
        Story.PreLoad();

        //Talk to Ravinos
        Story.MapItemQuest(5837, "underglade", 5271, AutoCompleteQuest: false);

        //Into the Underglade
        Story.KillQuest(5838, "underglade", new[] { "Forest Spirit", "Tree Nymph" });

        //Clear the Spores
        Story.KillQuest(5839, "underglade", "Slime Spore");

        //Cleanse the Walls
        Story.KillQuest(5840, "underglade", "Forest Spirit|Tree Nymph");
        Story.MapItemQuest(5840, "underglade", 5272, 8);

        //Expose the Entrance
        Story.KillQuest(5841, "underglade", "Blackened Earth");

        //Gather the Glows
        Story.KillQuest(5842, "underglade", "Luminous Fungus");

        //More Corruption Revealed
        Story.KillQuest(5843, "underglade", "Forest Spirit|Tree Nymph");
        Story.MapItemQuest(5843, "underglade", 5273, 6);

        //The Goblin Threat
        Story.KillQuest(5844, "underglade", "Twisted Goblin");

        //Get an Offering
        Story.KillQuest(5845, "underglade", "Gemstone Elemental");

        //The Heart of the Glade
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(5846, "underglade", "Lunamoss");
    }

    public void Extriki()
    {
        //Defeat Extriki
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(5847, "extriki", "Extriki");
    }
}