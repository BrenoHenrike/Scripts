//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class GlaceraStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        FrozenTower();
        FrozenRuins();
        Glacera();
        FrozenRuins2();
        Northstar();
        Glacera1();
        FrostRuins1();
        IceWindPass();
        IceDungeon();
    }

    public void FrozenTower()
    {
        if (Core.isCompletedBefore(3941))
            return;

        Story.PreLoad(this);

        // Seek the Tower
        Story.MapItemQuest(3907, "frozentower", 3022);

        // A n-Ice Beginning
        Story.KillQuest(3908, "frozentower", "Polar Elemental");

        // Search for Syrrus
        Story.MapItemQuest(3909, "frozentower", 3019);

        // Building the Base
        Story.MapItemQuest(3910, "frozentower", 3004, 13);

        // Refugee Roundup
        Story.KillQuest(3911, "frozentower", MonsterNames: new[] { "Frostwyrm", "Frostwyrm" });

        // Retrieve the Water Starstone
        Story.KillQuest(3912, "frozentower", "FrostDeep Dweller");

        // Magical Attraction
        Story.MapItemQuest(3913, "frozentower", 3005, 13);

        // Frozen Blood
        Story.KillQuest(3914, "frozentower", "Twisted Ice");

        // Retrieve the Fire Starstone
        Story.MapItemQuest(3915, "frozentower", 3006);

        // Defend the Tower!
        Story.KillQuest(3916, "frozentower", "Polar Elemental");

        // Refugee Rescue Run
        Story.MapItemQuest(3917, "frozentower", 3007, 6);

        // Retrieve the Earth Starston
        Story.MapItemQuest(3918, "frozentower", 3013);

        // Polar Penetration and Progress
        Story.MapItemQuest(3919, "frozentower", 3008, 6);
        Story.KillQuest(3919, "frozentower", "Polar Elemental");

        // Save the Astronomer Apprentice
        Story.MapItemQuest(3920, "frozentower", 3020);
        Story.KillQuest(3920, "frozentower", new[] { "Ice Wolf", "Polar Elemental" });

        // Glacial Elixir
        Story.MapItemQuest(3921, "frozentower", 3017, 6);
        Story.KillQuest(3921, "frozentower", "FrostDeep Dweller");

        // Retrieve the Energy Starstone
        Story.KillQuest(3922, "frozentower", "Polar Elemental");

        // Marking the Future
        Story.KillQuest(3923, "frozentower", "Frostwyrm");

        //Glacial Shift
        Story.MapItemQuest(3924, "frozentower", 3009, 6);

        // Divination Draft
        Story.MapItemQuest(3925, "frozentower", 3012, 4);
        Story.MapItemQuest(3925, "frozentower", 3011, 4);
        Story.KillQuest(3925, "frozentower", new[] { "Arctic Eel", "Frostwyrm" });

        // Retrieve the Light StarStone
        Story.MapItemQuest(3926, "frozentower", 3021, 4);

        // The Future is Bright
        Story.MapItemQuest(3927, "frozentower", 3014, 7);

        // Bled Bone Dry
        Story.KillQuest(3928, "frozentower", "Arctic Eel");

        // Chill of Fear
        Story.KillQuest(3929, "frozentower", "Polar Elemental");

        // Retrieve the Darkness Starstone
        Story.KillQuest(3930, "frozentower", "Twisted Ice");

        // Web of Fear
        Story.KillQuest(3931, "frozentower", "Frostwyrm");

        // Frozen in Time
        Story.MapItemQuest(3932, "frozentower", 3016, 13);

        // Heart of the Matter
        Story.KillQuest(3933, "frozentower", "Ice Wolf");

        // Retrieve the Wind Starstone
        Story.KillQuest(3934, "frozentower", "Rotten Ice");

        // Create the Gate
        Story.MapItemQuest(3935, "frozentower", 3018, 13);
        Story.KillQuest(3935, "frozentower", "Ice Wolf");

        // Drive Back the Invaders
        Story.KillQuest(3936, "frozentower", "Frost Invader");

        // Defeat the FrostSpawn Invaders
        Story.KillQuest(3937, "frozentower", "Frost Fangbeast");

        // FangBeast Bash-up
        Story.KillQuest(3941, "frozentower", "Frost Fangbeast");
    }

    public void FrozenRuins()
    {
        if (Core.isCompletedBefore(3946))
            return;

        Story.PreLoad(this);

        // FrozenRuins

        // Ravage the Reapers
        Story.KillQuest(3942, "frozenruins", "Frost Reaper");

        // Oh the Humanity
        Story.KillQuest(3943, "frozenruins", "Frost Reaper");

        // Close the Gate
        Story.KillQuest(3944, "frozenruins", "Frost Reaper");

        // Form the Lock
        Story.KillQuest(3945, "frozenruins", "Frozen Moglinster");

        Story.MapItemQuest(3945, "frozenruins", 3050, 10);
        // Glacera
        if (!Story.QuestProgression(3946))
        {
            Core.EnsureAccept(3946);
            if (!Core.CheckInventory(27357))
                Core.HuntMonster("frozenruins", "Frost Reaper", "Mercury");
            Core.EnsureComplete(3946);
        }
    }

    public void Glacera()
    {
        if (Core.isCompletedBefore(3950))
            return;

        // Glaera     

        //A Frost Welcome
        if (!Story.QuestProgression(3947))
        {
            Core.EnsureAccept(3947);
            Core.Join("glacera");
            Core.GetMapItem(3048, 1, "glacera");
            Core.EnsureComplete(3947);
        }

        // Key to the Fortress
        Story.KillQuest(3948, "glacera", "Frost Invader");

        // Ravage the Reapers
        Story.MapItemQuest(3949, "glacera", 3049, 6);

        // Oh the Humanity
        Story.MapItemQuest(3950, "glacera", 3047);

    }

    public void FrozenRuins2()
    {
        if (Core.isCompletedBefore(3954))
            return;

        // FrozenRuins encore

        // Rescue the Refugees
        Story.KillQuest(3951, "frozenruins", new[] { "Frost Invader", "Frozen Moglinster" });

        // Defeat the Fangbeasts
        Story.KillQuest(3952, "frozenruins", "Frost Fangbeast");

        // Destroy the Frost Reapers
        Story.KillQuest(3953, "frozenruins", "Frost Reaper");

        // FrostSpawn General Takedown
        Story.KillQuest(3954, "frozenruins", "Frost General");
    }


    public void Northstar()
    {
        if (Core.isCompletedBefore(3971))
            return;

        // Northstar

        // From Refugee to Enemy
        Story.KillQuest(3958, "northstar", new[] { "Frost Invader", "Monstrous Refugee" });

        // Fangs and Blades
        Story.KillQuest(3959, "northstar", new[] { "Frost Fangbeast", "Frost Invader" });

        // Reaping the Refugees
        Story.KillQuest(3960, "northstar", "Frost Reaper");

        // Saving Syrrus' Spirit
        Story.MapItemQuest(3961, "northstar", 3060, 5); //plush bear
        Story.MapItemQuest(3961, "northstar", 3061, 7); //snowdrop blossom
        Story.MapItemQuest(3961, "northstar", 3073, 5); //journal page
        Story.KillQuest(3961, "northstar", "Frost Reaper"); //loadstone peice

        // It's a Trap!
        Story.MapItemQuest(3972, "northstar", 3063, 10);

        // Feast or Famine
        Story.KillQuest(3973, "northstar", new[] { "Frost Fangbeast", "Frost Invader", "Frost Reaper", "Frost Superreaper", "Monstrous Refugee" });

        // Decipher the Freezing
        Story.KillQuest(3974, "northstar", new[] { "Frost Fangbeast", "Monstrous Refugee", "Frost Fangbeast", "Frost Invader", "Frost Reaper", "Monstrous Refugee" });

        // A New Frost Monster
        Story.KillQuest(3970, "northstar", "The Queen's Gift");

        // Defeat Karok!
        Story.KillQuest(3971, "northstar", "Karok the Fallen");
    }


    public void Glacera1()
    {
        if (Core.isCompletedBefore(3950))
            return;

        // Key to the Fortress
        Story.KillQuest(3948, "Glacera", "mob");
        // Breaking Boulders
        Story.MapItemQuest(3949, "Glacera", 3049, 6);
        // The Scythe of Vengeance
        Story.MapItemQuest(3950, "Glacera", 3047);
    }

    public void FrostRuins1()
    {
        if (Core.isCompletedBefore(3954))
            return;

        // Rescue the Refugees
        Story.KillQuest(3951, "frozenruins", "Frost Invader|Frozen Moglinster");

        // Defeat the Fangbeasts
        Story.KillQuest(3952, "frozenruins", "Frost Fangbeast");

        // Destroy the Frost Reapers
        Story.KillQuest(3953, "frozenruins", "Frost Reaper");

        // FrostSpawn General Takedown        
        Story.KillQuest(3954, "frozenruins", "Frost General");
    }

    public void IceWindPass()
    {
        if (Core.isCompletedBefore(5601))
            return;

        // Where is Karok?
        Story.MapItemQuest(5587, "IceWindPass", 5074, 5);

        // Cloaking Spell
        Story.KillQuest(5588, "IceWindPass", "Glacial Elemental");

        // Splattered Mana
        Story.MapItemQuest(5589, "IceWindPass", 5075, 5);
        Story.KillQuest(5589, "IceWindPass", "Glacial Elemental");

        // Dispell the Spell 
        Story.KillQuest(5590, "IceWindPass", "Polar Golem");

        // Catch Up to Karok 
        Story.KillQuest(5591, "IceWindPass", "Frost Invader");

        // Blast the Frostspawn Symbiote
        Story.KillQuest(5592, "IceWindPass", "Frostspawn Symbiote");

        // Keep Going!
        Story.KillQuest(5593, "IceWindPass", "Frost Invader|Frostspawn Troll");

        // Take it Down 
        Story.KillQuest(5594, "IceWindPass", "Frostspawn Horror");

        // Keep the Frostspawn Away!
        Story.KillQuest(5595, "IceWindPass", new[] { "Frostspawn Troll", "Frost Invader" });

        // Take a Break from Fighting
        Story.KillQuest(5596, "IceWindPass", new[] { "Polar Golem", "Glacial Elemental" });

        // Fight For Karok! & Fight For Kezeroth! + Mega   
        if (!Bot.Quests.IsUnlocked(5601))
        {
            Core.EnsureAccept(new[] { 5597, 5598, 5599, 5600 });
            Core.KillMonster("icewindwar", "r3", "Left", "*", "FrostSpawn Medal", 10);
            Core.KillMonster("icewindwar", "r3", "Left", "*", "Mega Frostspawn Medal", 5);
            Core.KillMonster("icewindwar", "r3", "Left", "*", "World Ender Medal", 10);
            Core.KillMonster("icewindwar", "r3", "Left", "*", "Mega World Ender Medal", 5);
            Core.EnsureComplete(new[] { 5597, 5598, 5599, 5600 });
        }

        // What is THAT?
        Story.KillQuest(5601, "icewindwar", "Soricomorpha");
    }

    public void IceDungeon()
    {
        if (Core.isCompletedBefore(7841))
            return;

        // Cold Open
        Story.KillQuest(7832, "IceDungeon", new[] { "Frosted Banshee", "Frozen Undead", "Ice Symbiote" });

        // Frost Fight
        Story.KillQuest(7833, "IceDungeon", new[] { "Spirit of Ice", "Ice Crystal", "Frigid Spirit" });

        // Freeze Frame
        Story.KillQuest(7834, "IceDungeon", new[] { "Living Ice", "Crystallized Elemental", "Frozen Demon" });

        // Glace's Trial
        Story.KillQuest(7835, "IceDungeon", "Image of Glace");

        // Abel's Trial
        Story.KillQuest(7836, "IceDungeon", "Abel");

        // The Final Trial   
        Story.KillQuest(7837, "IceDungeon", "Shade of Kyanos");

        // Basic Ingredients
        Story.KillQuest(7838, "IceDungeon", new[] { "Frosted Banshee", "Frozen Undead", "Ice Symbiote" });

        // Cool Flavor
        Story.KillQuest(7839, "IceDungeon", new[] { "Spirit of Ice", "Ice Crystal", "Frigid Spirit" });

        // Chilled to Perfection
        Story.KillQuest(7840, "IceDungeon", new[] { "Living Ice", "Crystallized Elemental", "Frozen Demon" });

        // Icing on the Cake  
        Story.KillQuest(7841, "IceDungeon", new[] { "Image of Glace", "Abel", "Shade of Kyanos" });
    }
}
