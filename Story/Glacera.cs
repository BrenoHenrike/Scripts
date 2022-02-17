//cs_include Scripts/CoreBots.cs
using RBot;

public class GlaceraStory
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
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
    }

    public void FrozenTower()
    {
        if (Core.isCompletedBefore(3941))
            return;

        Core.EquipClass(ClassType.Solo);


        // Seek the Tower
        Core.MapItemQuest(3907, "frozentower", 3022);

        // A n-Ice Beginning
        Core.KillQuest(3908, "frozentower", "Polar Elemental");

        // Search for Syrrus
        Core.MapItemQuest(3909, "frozentower", 3019);

        // Building the Base
        Core.MapItemQuest(3910, "frozentower", 3004, 13);

        // Refugee Roundup
        Core.KillQuest(3911, "frozentower", MonsterNames: new[] { "Frostwyrm", "Frostwyrm" });

        // Retrieve the Water Starstone
        Core.KillQuest(3912, "frozentower", "FrostDeep Dweller");

        // Magical Attraction
        Core.MapItemQuest(3913, "frozentower", 3005, 13);

        // Frozen Blood
        Core.KillQuest(3914, "frozentower", "Twisted Ice");

        // Retrieve the Fire Starstone
        Core.MapItemQuest(3915, "frozentower", 3006);

        // Defend the Tower!
        Core.KillQuest(3916, "frozentower", "Polar Elemental");

        // Refugee Rescue Run
        Core.MapItemQuest(3917, "frozentower", 3007, 6);

        // Retrieve the Earth Starston
        Core.MapItemQuest(3918, "frozentower", 3013);

        // Polar Penetration and Progress
        Core.KillQuest(3919, "frozentower", "Polar Elemental");
        Core.MapItemQuest(3919, "frozentower", 3008, 6);

        // Save the Astronomer Apprentice
        Core.KillQuest(3920, "frozentower", new[] { "Ice Wolf", "Polar Elemental" });
        Core.MapItemQuest(3920, "frozentower", 3020);

        // Glacial Elixir
        Core.KillQuest(3921, "frozentower", "FrostDeep Dweller");
        Core.MapItemQuest(3921, "frozentower", 3017, 6);

        // Retrieve the Energy Starstone
        Core.KillQuest(3922, "frozentower", "Polar Elemental");

        // Marking the Future
        Core.KillQuest(3923, "frozentower", "Frostwyrm");

        //Glacial Shift
        Core.MapItemQuest(3924, "frozentower", 3009, 6);

        // Divination Draft
        Core.KillQuest(3925, "frozentower", new[] { "Arctic Eel", "Frostwyrm" });
        Core.MapItemQuest(3925, "frozentower", 3012, 4);
        Core.MapItemQuest(3925, "frozentower", 3011, 4);

        // Retrieve the Light StarStone
        Core.MapItemQuest(3926, "frozentower", 3021, 4);

        // The Future is Bright
        Core.MapItemQuest(3927, "frozentower", 3014, 7);

        // Bled Bone Dry
        Core.KillQuest(3928, "frozentower", "Arctic Eel");

        // Chill of Fear
        Core.KillQuest(3929, "frozentower", "Polar Elemental");

        // Retrieve the Darkness Starstone
        Core.KillQuest(3930, "frozentower", "Twisted Ice");

        // Web of Fear
        Core.KillQuest(3931, "frozentower", "Frostwyrm");

        // Frozen in Time
        Core.MapItemQuest(3932, "frozentower", 3016, 13);

        // Heart of the Matter
        Core.KillQuest(3933, "frozentower", "Ice Wolf");

        // Retrieve the Wind Starstone
        Core.KillQuest(3934, "frozentower", "Rotten Ice");

        // Create the Gate
        Core.KillQuest(3935, "frozentower", "Ice Wolf");
        Core.MapItemQuest(3935, "frozentower", 3018, 13);

        // Drive Back the Invaders
        Core.KillQuest(3936, "frozentower", "Frost Invader");

        // Defeat the FrostSpawn Invaders
        Core.KillQuest(3937, "frozentower", "Frost Fangbeast");

        // FangBeast Bash-up
        Core.KillQuest(3941, "frozentower", "Frost Fangbeast");
    }

    public void FrozenRuins()
    {
        if (Core.isCompletedBefore(3946))
            return;

        // FrozenRuins

        // Ravage the Reapers
        Core.KillQuest(3942, "frozenruins", "Frost Reaper");

        // Oh the Humanity
        Core.KillQuest(3943, "frozenruins", "Frost Reaper");

        // Close the Gate
        Core.KillQuest(3944, "frozenruins", "Frost Reaper");

        // Form the Lock
        Core.KillQuest(3945, "frozenruins", "Frozen Moglinster");

        Core.MapItemQuest(3945, "frozenruins", 3050, 10);
        // Glacera
        Core.KillQuest(3946, "frozenruins", "Frost Reaper");
    }

    public void Glacera()
    {
        if (Core.isCompletedBefore(3950))
            return;

        // Glaera     

        //A Frost Welcome
        Core.EnsureAccept(3947);
        Core.MapItemQuest(3947, "glacera", 3048, 1);
        // Key to the Fortress
        Core.KillQuest(3948, "glacera", "Frost Invader");
        // Ravage the Reapers
        Core.MapItemQuest(3949, "glacera", 3049, 6);
        // Oh the Humanity
        Core.MapItemQuest(3950, "glacera", 3047, 1);
    }

    public void FrozenRuins2()
    {
        if (Core.isCompletedBefore(3954))
            return;

        // FrozenRuins encore

        // Rescue the Refugees
        Core.KillQuest(3951, "frozenruins", new[] { "Frost Invader", "Frozen Moglinster" });
        // Defeat the Fangbeasts
        Core.KillQuest(3952, "frozenruins", "Frost Fangbeast");
        // Destroy the Frost Reapers
        Core.KillQuest(3953, "frozenruins", "Frost Reaper");
        // FrostSpawn General Takedown
        Core.KillQuest(3954, "frozenruins", "Frost General");
    }

    public void Northstar()
    {
        if (Core.isCompletedBefore(3971))
            return;

        // Northstar

        // From Refugee to Enemy
        Core.KillQuest(3958, "northstar", new[] { "Frost Invader", "Monstrous Refugee" });
        // Fangs and Blades
        Core.KillQuest(3959, "northstar", new[] { "Frost Fangbeast", "Frost Invader" });
        // Reaping the Refugees
        Core.KillQuest(3960, "northstar", "Frost Reaper");
        // Saving Syrrus' Spirit
        Core.KillQuest(3961, "northstar", "Frost Reaper"); //loadstone peice
        Core.MapItemQuest(3961, "northstar", 3060, 5); //plush bear
        Core.MapItemQuest(3961, "northstar", 3061, 7); //snowdrop blossom
        Core.MapItemQuest(3961, "northstar", 3073, 5); //journal page
        // It's a Trap!
        Core.MapItemQuest(3972, "northstar", 3063, 10);
        // Feast or Famine
        Core.KillQuest(3973, "northstar", new[] { "Frost Fangbeast", "Frost Invader", "Frost Reaper", "Frost Superreaper", "Monstrous Refugee" });
        // Decipher the Freezing
        Core.KillQuest(3974, "northstar", new[] { "Frost Fangbeast", "Monstrous Refugee", "Frost Fangbeast", "Frost Invader", "Frost Reaper", "Monstrous Refugee" });
        // A New Frost Monster
        Core.KillQuest(3970, "northstar", "The Queen's Gift");
        // Defeat Karok!
        Core.KillQuest(3971, "northstar", "Karok the Fallen");
    }
}
