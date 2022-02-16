//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Other/Weapons/GoldenBladeOfFate.cs
//cs_include Scripts/Other/Weapons/PinkBladeofDestruction.cs
//cs_include Scripts/Story/Doomwood/DoomwoodPart3.cs
//cs_include Scripts/Story/QueenofMonsters/GuardianTree.cs
//cs_include Scripts/Story/ThroneofDarkness/05aSekt(ShiftingPyramid).cs
//cs_include Scripts/Story/7DeadlyDragons/MysteriousEgg.cs
//cs_include Scripts/Story/Collection.cs
//cs_include Scripts/Story/Borgars.cs
using RBot;

public class DragonOfTime
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDarkon Darkon = new CoreDarkon();
    public GoldenBladeOfFate GBoF = new GoldenBladeOfFate();
    public PinkBladeOfDestruciton PBoD = new PinkBladeOfDestruciton();
    public GuardianTree GT = new GuardianTree();
    public ShiftingPyramid SP = new ShiftingPyramid();
    public MysteriousEgg Egg = new MysteriousEgg();
    public Collection Coll = new Collection();
    public Borgars Borg = new Borgars();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetDoT();

        Core.SetOptions(false);
    }

    public void GetDoT(bool rankUpClass = true, bool doExtra = true)
    {
        if ((!doExtra && Core.CheckInventory("Dragon of Time")) || (doExtra && Core.CheckInventory("Dragon of Time Horns")))
            return;

        //Acquiring Ancient Secrets
        if (!Core.QuestProgression(7716))
        {
            Core.EnsureAccept(7716);

            Core.EquipClass(ClassType.Farm);
            Core.UpdateQuest(4614);
            Core.KillMonster("mummies", "Enter", "Spawn", "Mummy", "Lost Hieroglyphic", 30, false);

            Farm.LoremasterREP(4);
            Core.BuyItem("Librarium", 651, "Myths of Lore");

            Core.KillMonster("timelibrary", "FrameAQ", "Left", "*", "Historia Page", 100, false);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("kingcoal", "Frost King", "Frost King's Story", isTemp: false);

            Core.KillMonster("baconcatyou", "Enter", "Spawn", "*", "Your Own Memories", isTemp: false, publicRoom: true);

            Core.ChainQuest(7716);
        }

        //Time to Train Yourself
        if (!Core.QuestProgression(7717))
        {
            Core.EnsureAccept(7717);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("dragonchallenge", "Desoloth the Final", "Desoloth's Destructive Aura", isTemp: false, publicRoom: true);

            Core.UpdateQuest(899);
            Core.HuntMonster("blindingsnow", "Nythera", "Nythera's Patience", isTemp: false);

            Core.AddDrop("Key of Greed");
            Core.HuntMonster("greed", "Goregold", "Goregold's Luck", isTemp: false, publicRoom: true);

            Core.HuntMonster("darkplane", "Victorious", "Victorious's Dignity", isTemp: false);

            Core.HuntMonster("trigoras", "Trigoras", "Trigoras's Tenacity", 3, false);

            Core.ChainQuest(7717);
        }

        //Do You Have the Time?
        if (!Core.QuestProgression(7718))
        {
            Farm.Experience(31);
            Core.EnsureAccept(7718);

            GBoF.GetGBoF();

            PBoD.GetPBoD();

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("underworld", "Laken", "Cross-Era Stabiliser", isTemp: false);

            if (!Core.CheckInventory("Chronomancer's Codex"))
            {
                Core.HuntMonster("mqlesson", "Dragonoid", "Dragonoid of Hours", isTemp: false);
                Core.HuntMonster("timespace", "Iadoa", "Chronomancer's Codex", isTemp: false);
            }

            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("arena", "Timesteam Rider", "Timestream String", 100, false);

            Core.ChainQuest(7718);
        }

        //Through the Wormhole
        if (!Core.QuestProgression(7719))
        {
            Farm.Experience(40);
            Core.EnsureAccept(7719);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("cathedral", "Incarnation of Time", "Time Loop Broken", isTemp: false, publicRoom: true);

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("portalwar", "r4", "Right", "*", "Anomaly Silenced", 100, false);

            Core.HuntMonster("portalmaze", "ChronoLord", "Chronolord Stopped", 50, false);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("ubear", "Cornholio", "Is This a Wormhole?", isTemp: false);

            Core.ChainQuest(7719);
        }

        //Rend
        if (!Core.QuestProgression(7720))
        {
            Farm.Experience(60);
            Core.EnsureAccept(7720);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("lairdefend", "Dragon Summoner", "Dimensional Dragon Portal", 2, false);

            Core.HuntMonster("bosschallenge", "Grievous Inbunche", "Brutal Slash Studied", isTemp: false, publicRoom: true);

            Core.HuntMonster("hydrachallenge", "Hydra Head 90", "Epic Hydra Fang", 125, false);

            Core.ChainQuest(7720);
        }

        //Confluence of Fates
        if (!Core.QuestProgression(7721))
        {
            Farm.Experience(60);
            Core.EnsureAccept(7721);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("ivoliss", "Ivoliss", "Sword of Voids", isTemp: false);

            Darkon.FarmReceipt(100);

            GT.GuardianTreeQuests();
            if (!Core.CheckInventory("Semiramis Feather"))
            {
                Core.AddDrop("Semiramis Feather");
                Core.EnsureAccept(6286);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("guardiantree", "Terrane", "Terrane Defeated");
                Core.EnsureComplete(6286);
            }

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("aqw3d", "r13", "Bottom", "*", "Cross Dimensional Weapons", isTemp: false, publicRoom: true);

            SP.ShiftingPyramidSaga();
            if (!Core.CheckInventory("Starlight Singularity"))
            {
                Core.AddDrop("Starlight Singularity");
                Core.EnsureAccept(5186);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("whitehole", "Mehensi Serpent", "Mehen Slain");
                Core.EnsureComplete(5186);
            }

            Coll.CollectionStory();
            Core.BuyItem("collection", 325, "Collectible Collector");

            Core.ChainQuest(7721);
        }

        //Dragon's Will
        if (!Core.QuestProgression(7722))
        {
            Farm.Experience(65);
            Core.EnsureAccept(7722);

            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("moonlab", "Slime Mold", "Unyielding Slime", 300, false);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("bosschallenge", "Mutated Void Dragon", "Omnipotent Cells", 20, false, publicRoom: true);

            Core.HuntMonster("underlair", "ArchFiend Dragonlord", "Dragon's Plasma", 20, false, publicRoom: true);

            Core.HuntMonster("chaoskraken", "Chaos Kraken", "Chaotic Invertebrae", 20, false, publicRoom: true);

            Core.UpdateQuest(9, 159);
            Core.HuntMonster("towerofdoom9", "Dread Fang", "Cryostatic Essence", 20, false, publicRoom: true);

            Core.HuntMonster("castleroof", "Ultra Chaos Dragon", "Salvaged Chaos Dragon Biomass", 20, false, publicRoom: true);

            Core.ChainQuest(7722);
        }

        //Burning Fates
        if (!Core.QuestProgression(7723))
        {
            Farm.Experience(70);
            Core.EnsureAccept(7723);

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("volcano", "r10", "Left", "Fire Imp", "Fire Essence", 3000, false);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("charredplains", "Akriloth", "Akriloth's Flametongue", 100, false, publicRoom: true);

            Core.HuntMonster("ultraphedra", "Ultra Phedra", "Immortal Embers", 50, false, publicRoom: true);

            Core.HuntMonster("thevoid", "Reaper", "Ashes from the Void Realm", 50, false, publicRoom: true);

            Core.ChainQuest(7723);
        }

        //Hero's Heartbeat
        if (!Core.QuestProgression(7724))
        {
            Farm.Experience(75);
            if (!Core.CheckInventory("Blade of Awe"))
                Farm.BladeofAweREP(6, true);
            Core.EnsureAccept(7724);

            Egg.GetMysteriousEgg();

            Core.EquipClass(ClassType.Solo);
            Core.UpdateQuest(3880);
            Core.KillMonster("chaoslord", "r2", "Left", "*", "Conquered Past", isTemp: false, publicRoom: true);

            Core.UpdateQuest(10, 159);
            Core.HuntMonster("towerofdoom10", "Slugbutter", "Slugbutter Trophy", 100, false, publicRoom: true);

            Core.HuntMonster("icestormarena", "Warlord Icewing", "Icewing's Laurel", 30, false, publicRoom: true);

            Core.ChainQuest(7724);
        }

        //I'm Loving It My Way
        if (doExtra && !Core.QuestProgression(7725))
        {
            Farm.Experience(75);
            Core.EnsureAccept(7725);

            Borg.BorgarQuests();
            Core.AddDrop("Burger Buns");
            Core.EquipClass(ClassType.Solo);

            while (!Core.CheckInventory("Borgar") || !Core.CheckInventory("Burger Buns", 5))
            {
                Core.EnsureAccept(7522);
                Core.HuntMonster("borgars", "Burglinster", "Burglinster Cured");
                Core.EnsureComplete(7522);
                Bot.Wait.ForPickup("Burger Buns");
            }

            Core.BuyItem("borgars", 1884, "Borgar");

            Core.ChainQuest(7725);
        }

        if (rankUpClass)
            Farm.rankUpClass("Dragon of Time");
    }
}