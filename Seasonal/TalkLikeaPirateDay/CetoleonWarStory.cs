//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/HeartOfTheSeaStory.cs
using Skua.Core.Interfaces;

public class CetoleonWarStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public HeartOfTheSeaStory HeartOfTheSeaStory = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CetoleonWar();

        Core.SetOptions(false);
    }
    public string[] AllLoot =
    {
        "Skull-n-Bones Bandana",
        "Supernatural Skull Bandana",
        "Vampire Commander's Backblades",
        "Grislyfang Pirate's Tools",
        "Forgotten Cutlass",
        "Vampire Commander's Top Hat",
        "Vampire Commander's Top Hat + Locks",
        "Shaggy Vampire Commander's Top Hat",
        "Shaggy Vampire Commander's Top Hat + Locks",
        "Dual Forgotten Cutlasses",
        "Golden Naval Commander Hook",
        "Arachnid Commander's Sword",
        "Shipwrecked Captain's Rune",
        "Vampire Commander's Black Cutlass",
        "Golden Pirate Monkey",
        "Wrapped Captain's Tricorn",
        "Wrapped Captain's Tricorn + Locks",
        "Guncraft Commander's Hair",
        "Guncraft Commander's Locks",
        "Guncraft Commander's Flintlock",
        "Golden Commander's Rapier",
        "Golden Commander's Bandana",
        "Golden Commander's Back Cutlass",
        "Golden Commander's Cutlass",
        "Golden Commander's Reavers",
        "Skull-n-Ghostbones Flag",
        "Vampire Commander's Shaggy Hair",
        "Vampire Commander's Shaggy Locks"
    };

    public string[] GrislyFangLoot = { "Skull-n-Bones Bandana", "Supernatural Skull Bandana", "Vampire Commander's Backblades", "Grislyfang Pirate's Tools" };
    public string[] WreckersLoot = { "Forgotten Cutlass", "Vampire Commander's Top Hat", "Vampire Commander's Top Hat + Locks", "Shaggy Vampire Commander's Top Hat", "Shaggy Vampire Commander's Top Hat + Locks" };
    public string[] EngineersLoot = { "Dual Forgotten Cutlasses", "Golden Naval Commander Hook", "Arachnid Commander's Sword", "Shipwrecked Captain's Rune" };
    public string[] GunPowderLoot = { "Vampire Commander's Black Cutlass", "Golden Pirate Monkey", "Wrapped Captain's Tricorn", "Wrapped Captain's Tricorn + Locks" };
    public string[] SawLoot = { "Guncraft Commander's Hair", "Guncraft Commander's Locks", "Guncraft Commander's Flintlock", "Golden Commander's Rapier" };
    public string[] TentaclesLoot = { "Golden Commander's Bandana", "Golden Commander's Back Cutlass", "Golden Commander's Cutlass", "Golden Commander's Reavers" };
    public string[] RoachesLoot = { "Skull-n-Ghostbones Flag", "Vampire Commander's Shaggy Hair", "Vampire Commander's Shaggy Locks" };

    public void CetoleonWar()
    {
        if (!Core.isSeasonalMapActive("CetoleonWar"))
            return;

        if (Core.CheckInventory(AllLoot, toInv: false))
            return;

        HeartOfTheSeaStory.HeartOfTheSea();

        if (!Core.CheckInventory(GrislyFangLoot, toInv: false))
            Grislyfang();

        if (!Core.CheckInventory(WreckersLoot, toInv: false))
            Wreckers();

        if (!Core.CheckInventory(EngineersLoot, toInv: false))
            Engineers();

        if (!Core.CheckInventory(GunPowderLoot, toInv: false))
            GunPowder();

        if (!Core.CheckInventory(SawLoot, toInv: false))
            Saw();

        if (!Core.CheckInventory(TentaclesLoot, toInv: false))
            Tentacles();

        if (!Core.CheckInventory(RoachesLoot, toInv: false))
            Roaches();

        void Grislyfang()
        {
            //Grislyfang Doubloons 6523
            Core.AddDrop(GrislyFangLoot);
            Core.EquipClass(ClassType.Farm);
            Core.RegisterQuests(6523, 6524);
            while (!Bot.ShouldExit && (!Core.CheckInventory(GrislyFangLoot, toInv: false)))
                Core.HuntMonster("CetoleonWar", "Grislyfang Wrecker", "Grislyfang Doubloon", 5, log: false);
            Core.CancelRegisteredQuests();
            Core.ToBank(GrislyFangLoot);
        }

        void Wreckers()
        {
            //Stop the Wreckers, Fix the Ship 6524
            Core.AddDrop(WreckersLoot);
            Core.EquipClass(ClassType.Farm);
            Core.RegisterQuests(6524);
            while (!Bot.ShouldExit && (!Core.CheckInventory(WreckersLoot, toInv: false)))
            {
                Core.HuntMonster("CetoleonWar", "Grislyfang Wrecker", "Hammer", log: false);
                Core.HuntMonster("CetoleonWar", "Grislyfang Wrecker", "Plank", 5, log: false);
                Core.HuntMonster("CetoleonWar", "Grislyfang Wrecker", "Nails", 25, log: false);
            }
            Core.CancelRegisteredQuests();
            Core.ToBank(WreckersLoot);
        }

        void Engineers()
        {
            //Boil the Engineers 6525
            Core.AddDrop(EngineersLoot);
            Core.EquipClass(ClassType.Farm);
            Core.RegisterQuests(6525);
            while (!Bot.ShouldExit && (!Core.CheckInventory(EngineersLoot, toInv: false)))
                Core.HuntMonster("CetoleonWar", "Grislyfang Engineer", "Engineers Slain", 3, log: false);
            Core.CancelRegisteredQuests();
            Core.ToBank(EngineersLoot);
        }

        void GunPowder()
        {
            //Grab the Gunpowder 6526
            Core.AddDrop(GunPowderLoot);
            Core.EquipClass(ClassType.Farm);
            Core.RegisterQuests(6526);
            while (!Bot.ShouldExit && (!Core.CheckInventory(GunPowderLoot, toInv: false)))
                Core.HuntMonster("CetoleonWar", "Grislyfang Musketeer", "Grislyfang Gunpowder", 5, log: false);
            Core.CancelRegisteredQuests();
            Core.ToBank(GunPowderLoot);
        }

        void Saw()
        {
            //Saw THIS! 6527
            Core.AddDrop(SawLoot);
            Core.EquipClass(ClassType.Solo);
            Core.RegisterQuests(6527);
            while (!Bot.ShouldExit && (!Core.CheckInventory(SawLoot, toInv: false)))
                Core.HuntMonster("CetoleonWar", "Captain Sawtooth", "Captain Sawtooth Defeated");
            Core.CancelRegisteredQuests();
            Core.ToBank(SawLoot);
        }

        void Tentacles()
        {
            //Remove the Tentacles 6528
            Core.AddDrop(TentaclesLoot);
            Core.EquipClass(ClassType.Farm);
            Core.RegisterQuests(6528);
            while (!Bot.ShouldExit && (!Core.CheckInventory(TentaclesLoot, toInv: false)))
                Core.HuntMonster("CetoleonWar", "Nomura's Sting", "Tentacle Slain", 5, log: false);
            Core.CancelRegisteredQuests();
            Core.ToBank(TentaclesLoot);
        }

        void Roaches()
        {
            //Ugh, Roaches 6529
            Core.AddDrop(RoachesLoot);
            Core.EquipClass(ClassType.Farm);
            Core.RegisterQuests(6529);
            while (!Bot.ShouldExit && (!Core.CheckInventory(RoachesLoot, toInv: false)))
                Core.HuntMonster("CetoleonWar", "Sea Roach", "Sea Roach Slain", 5, log: false);
            Core.CancelRegisteredQuests();
            Core.ToBank(RoachesLoot);
        }

        //Gel the Jellyfish 6530
        Story.KillQuest(6530, "CetoleonWar", "Nomura");

    }
}
