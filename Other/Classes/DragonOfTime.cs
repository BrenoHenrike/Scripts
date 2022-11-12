//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Other/Weapons/GoldenBladeOfFate.cs
//cs_include Scripts/Other/Weapons/PinkBladeofDestruction.cs
//cs_include Scripts/Story/Doomwood/DoomwoodPart3.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQoM.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Other/MysteriousEgg.cs
//cs_include Scripts/Story/Summer2015AdventureMap/CoreSummer.cs
//cs_include Scripts/Story/Borgars.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using Skua.Core.Interfaces;

public class DragonOfTime
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreStory Story = new();
    public CoreDarkon Darkon = new();
    public GoldenBladeOfFate GBoF = new();
    public PinkBladeOfDestruciton PBoD = new();
    public CoreQOM QOM = new();
    public CoreToD TOD = new();
    public MysteriousEgg Egg = new();
    public CoreSummer Coll = new();
    public Borgars Borgars = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetDoT();

        Core.SetOptions(false);
    }

    private string[] Extras = { "Dragon of Time Horns", "Dragon of Time Horns + Ponytail", "Dragon of Time Wings + Tail" };

    public void GetDoT(bool rankUpClass = true, bool doExtra = true)
    {
        if ((!doExtra && Core.CheckInventory("Dragon of Time")) || (doExtra && Core.CheckInventory(Extras)))
            return;

        Story.PreLoad(this);

        // Acquiring Ancient Secrets 7716
        if (!Story.QuestProgression(7716))
        {
            Core.EnsureAccept(7716);

            Core.EquipClass(ClassType.Farm);
            Bot.Quests.UpdateQuest(4614);
            Core.KillMonster("mummies", "Enter", "Spawn", "Mummy", "Lost Hieroglyphic", 30, false);

            Farm.LoremasterREP(4);

            Core.BuyItem("librarium", 651, "Myths of Lore");

            Core.KillMonster("timelibrary", "FrameAQ", "Left", "*", "Historia Page", 100, false);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("kingcoal", "Frost King", "Frost King's Story", isTemp: false);

            Core.KillMonster("baconcatyou", "Enter", "Spawn", "*", "Your Own Memories", isTemp: false, publicRoom: true);

            Story.ChainQuest(7716);
            Bot.Wait.ForPickup("*");
            Core.ToBank("Dragon of Time Helm", "Dragon of Time Ponytail");
        }

        // Time to Train Yourself 7717
        if (!Story.QuestProgression(7717))
        {
            Core.EnsureAccept(7717);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("dragonchallenge", "Desoloth the Final", "Desoloth's Destructive Aura", isTemp: false, publicRoom: true);

            Bot.Quests.UpdateQuest(899);
            Core.HuntMonster("blindingsnow", "Nythera", "Nythera's Patience", isTemp: false);

            Core.AddDrop("Key of Greed");
            Adv.BoostHuntMonster("greed", "Goregold", "Goregold's Luck", isTemp: false, publicRoom: true);

            Core.HuntMonster("darkplane", "Victorious", "Victorious's Dignity", isTemp: false);

            Core.HuntMonster("trigoras", "Trigoras", "Trigoras's Tenacity", 3, false);

            Story.ChainQuest(7717);
            Bot.Wait.ForPickup("*");
            Core.ToBank("Dragon of Time Runes", "Dragon of Time Wings", "Dragon of Time Tail");
        }

        // Do You Have the Time? 7718
        if (!Story.QuestProgression(7718))
        {
            Farm.Experience(31);
            Core.EnsureAccept(7718);

            GBoF.GetGBoF();

            PBoD.GetPBoD();

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("underworld", "Laken", "Cross-Era Stabilizer", isTemp: false);

            if (!Core.CheckInventory("Chronomancer's Codex"))
            {
                Core.HuntMonster("mqlesson", "Dragonoid", "Dragonoid of Hours", isTemp: false);
                Core.HuntMonster("timespace", "Chaos Lord Iadoa", "Chronomancer's Codex", isTemp: false);
            }

            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("arena", "Timestream Rider", "Timestream String", 100, false);

            Story.ChainQuest(7718);
            Bot.Wait.ForPickup("*");
            Core.ToBank("Dragon of Time FangBlade", "Dual Dragon of Time FangBlades");
        }

        // Through the Wormhole 7719
        if (!Story.QuestProgression(7719))
        {
            Farm.Experience(40);
            Core.EnsureAccept(7719);

            Core.EquipClass(ClassType.Solo);
            Adv.BoostHuntMonster("cathedral", "Incarnation of Time", "Time Loop Broken", isTemp: false, publicRoom: true);

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("portalwar", "r4", "Right", "*", "Anomaly Silenced", 100, false);

            Core.HuntMonster("portalmaze", "ChronoLord", "Chronolord Stopped", 50, false);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("ubear", "Cornholio", "Is This a Wormhole?", isTemp: false);

            Story.ChainQuest(7719);
            Bot.Wait.ForPickup("*");
            Core.ToBank("Dragon of Time Armor");
        }

        // Rend 7720
        if (!Story.QuestProgression(7720))
        {
            Farm.Experience(60);
            Core.EnsureAccept(7720);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("lairdefend", "Dragon Summoner", "Dimensional Dragon Portal", 2, false);

            Core.HuntMonster("bosschallenge", "Grievous Inbunche", "Brutal Slash Studied", 10, isTemp: false, publicRoom: true);

            Adv.BoostKillMonster("hydrachallenge", "h90", "Left", 3778, "Epic Hydra Fang", 125, false);

            Story.ChainQuest(7720);
            Bot.Wait.ForPickup("*");
            Core.ToBank("Dragon of Time Daggers", "Dragon of Time Cleaver");
        }

        // Confluence of Fates 7721
        if (!Story.QuestProgression(7721))
        {
            Farm.Experience(60);
            Core.EnsureAccept(7721);

            Core.EquipClass(ClassType.Solo);
            Adv.BoostHuntMonster("ivoliss", "Ivoliss", "Sword of Voids", isTemp: false);
            Bot.Wait.ForPickup("Sword of Voids");

            Darkon.FarmReceipt(100);

            QOM.TheReshaper();
            if (!Core.CheckInventory("Semiramis Feather"))
            {
                Core.AddDrop("Semiramis Feather");
                // Take Down Terrane 6286
                Core.EnsureAccept(6286);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("guardiantree", "Terrane", "Terrane Defeated");
                Core.EnsureComplete(6286);
                Bot.Wait.ForPickup("Semiramis Feather");
            }

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("aqw3d", "r13", "Bottom", "*", "Cross-Dimensional Weapons", 300, isTemp: false, publicRoom: true);

            TOD.ShiftingPyramid();
            if (!Core.CheckInventory("Starlight Singularity"))
            {
                Core.AddDrop("Starlight Singularity");
                // Serpent of the Stars 5186
                Core.EnsureAccept(5186);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("whitehole", "Mehensi Serpent", "Mehen Slain");
                Core.EnsureComplete(5186);
                Bot.Wait.ForPickup("Starlight Singularity");
            }

            Coll.Collector();
            Core.BuyItem("collection", 325, "Collectible Collector");
            Bot.Wait.ForPickup("Collectible Collector");

            Story.ChainQuest(7721);

            Bot.Wait.ForPickup("*");
            Core.ToBank("Ascended Dragon of Time Runes", "Runes Of Time");
        }

        // Dragon's Will 7722
        if (!Story.QuestProgression(7722))
        {
            Farm.Experience(65);
            Core.EnsureAccept(7722);

            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("moonlab", "Slime Mold", "Unyielding Slime", 300, false);

            Core.EquipClass(ClassType.Solo);
            Adv.BoostHuntMonster("bosschallenge", "Mutated Void Dragon", "Omnipotent Cells", 20, false, publicRoom: true);

            Adv.BoostHuntMonster("underlair", "ArchFiend Dragonlord", "Dragon's Plasma", 20, false, publicRoom: true);

            Core.HuntMonster("chaoskraken", "Chaos Kraken", "Chaotic Invertebrae", 20, false, publicRoom: true);

            Bot.Quests.UpdateQuest(9, 159);
            Adv.BoostHuntMonster("towerofdoom9", "Dread Fang", "Cryostatic Essence", 20, false, publicRoom: true);

            Adv.BoostHuntMonster("castleroof", "Ultra Chaos Dragon", "Salvaged Chaos Dragon Biomass", 20, false, publicRoom: true);

            Story.ChainQuest(7722);
            Bot.Wait.ForPickup("*");
            Core.ToBank("Dragon of Time Reaper", "Dragon of Time WingBlade");
        }

        // Burning Fates 7723
        if (!Story.QuestProgression(7723))
        {
            Farm.Experience(70);
            Core.EnsureAccept(7723);

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("volcano", "r10", "Left", "Fire Imp", "Fire Essence", 3000, false);

            Core.EquipClass(ClassType.Solo);
            Adv.BoostHuntMonster("charredplains", "Akriloth", "Akriloth's Flametongue", 100, false, publicRoom: true);

            Adv.BoostHuntMonster("ultraphedra", "Ultra Phedra", "Immortal Embers", 50, false, publicRoom: true);

            Adv.BoostHuntMonster("thevoid", "Reaper", "Ashes from the Void Realm", 50, false, publicRoom: true);

            Story.ChainQuest(7723);
            Bot.Wait.ForPickup("*");
            Core.ToBank("Ascended Dragon of Time");
        }

        // Hero's Heartbeat 7724
        if (!Story.QuestProgression(7724) || !Core.CheckInventory("Dragon of Time"))
        {
            Farm.Experience(75);
            if (!Core.CheckInventory("Blade of Awe"))
                Farm.BladeofAweREP(6, true);
            Core.EnsureAccept(7724);

            Egg.GetMysteriousEgg();

            Core.EquipClass(ClassType.Solo);
            Bot.Quests.UpdateQuest(3880);
            Core.KillMonster("chaoslord", "r2", "Left", "*", "Conquered Past", isTemp: false, publicRoom: true);

            Bot.Quests.UpdateQuest(10, 159);
            Adv.BoostHuntMonster("towerofdoom10", "Slugbutter", "Slugbutter Trophy", 100, false, publicRoom: true);

            Adv.BoostHuntMonster("icestormarena", "Warlord Icewing", "Icewing's Laurel", 30, false, publicRoom: true);

            Story.ChainQuest(7724);
            Bot.Wait.ForPickup("Dragon of Time");
            Bot.Wait.ForPickup("*");
            Core.ToBank("Ascended Dragon of Time Morph", "Ascended Dragon of Time Wings");
        }

        if (rankUpClass)
            Adv.rankUpClass("Dragon of Time");

        // I'm Loving It My Way 7725
        if (doExtra)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(Extras, toInv: false))
            {
                Farm.Experience(75);
                Core.EnsureAccept(7725);

                Borgars.StoryLine();
                Core.AddDrop("Burger Buns");
                Core.EquipClass(ClassType.Solo);

                if (!Core.CheckInventory("Borgar"))
                {
                    bool LoggedBefore = false;
                    while (!Bot.ShouldExit && !Core.CheckInventory("Burger Buns", 5))
                    {
                        // Burglinster's Revenge 7522
                        Core.EnsureAccept(7522);
                        Core.HuntMonster("borgars", "Burglinster", "Burglinster Cured", log: !LoggedBefore);
                        Core.EnsureComplete(7522);
                        Bot.Wait.ForPickup("Burger Buns");
                        LoggedBefore = true;
                    }
                }
                Core.BuyItem("borgars", 1884, 54650, shopItemID: 7387);

                Core.EnsureCompleteChoose(7725, Extras);
                Bot.Wait.ForPickup("*");
            }
            Core.ToBank("Dragon of Time Horns", "Dragon of Time Horns + Ponytail", "Dragon of Time Wings + Tail");
        }
    }
}