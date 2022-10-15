//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
//cs_include Scripts/Evil/SepulchuresOriginalHelm.cs
//cs_include Scripts/Other/Materials/DarknessShard.cs
//cs_include Scripts/Story/Doomwood/AQWZombies.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Nation/Various/TarosManslayer.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/Various/PurifiedClaymoreOfDestiny.cs

using Skua.Core.Interfaces;
using Skua.Core.Options;

public class CyseroItemUpgrade
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreBLOD BLOD = new();
    public CoreAdvanced Adv = new();
    public DarknessShard DS = new();
    public SepulchuresOriginalHelm Seppy = new();
    public TarosManslayer TarosManslayer = new();

    public string OptionsStorage = "FarmerJoePet";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        new Option<bool>("SkipOption", "Skip this window next time", "You will be able to return to this screen via [Options] -> [Script Options] if you wish to change anything.", false),
        new Option<bool>("PolishedBlod", "Polished Blinding Light of Destiny", "Finishes Cysero Quest \"Upgrade the Blinding Light of Destiny (7063)\" to get you \"Polished Blinding Light of Destiny\"", false),
        new Option<bool>("ToxicPlagueSpear", "Toxic Plague Spear", "Finishes Cysero Quest \"Upgrade the Plague Spear (7064)\" to get you \"Toxic Plague Spear\"", false),
        new Option<bool>("BurningPhoenixBlade", "Burning Phoenix Blade", "Finishes Cysero Quest \"Upgrade the Phoenix Blade (7065)\" to get you \"Burning Phoenix Blade\"", false),
        new Option<bool>("OdokuroBlight", "O-dokuro's Blight", "Finishes Cysero Quest \"Upgrade O-Dokuro Blade (7066)\" to get you \"O-dokuro's Blight\"", false),
        new Option<bool>("PolishedManslayer", "Polished Manslayer", "Finishes Cysero Quest \"Upgrade Taro’s Manslayer (7067)\" to get you \"Polished Manslayer\"", false),
        new Option<bool>("CursedDoomBlade", "Cursed DoomBlade", "Finishes Cysero Quest \"Upgrade Sepulchure’s Undead Blade (7068)\" to get you \"Cursed DoomBlade\"", false),
        new Option<bool>("RebornSepulchureHelm", "Reborn Sepulchure's Helm", "Finishes Cysero Quest \"Upgrade Sepulchure’s Original Helm (7069)\" to get you \"Reborn Sepulchure's Helm\"", false),
        new Option<bool>("GetAll", "GetAllUpgrades", "Finishes All Cysero Item Upgrade Quests to Get you all of the  rewards", false),

    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        ScriptOtions();

        Core.SetOptions(false);
    }

    public void ScriptOtions()
    {
        if (!Bot.Config.Get<bool>("SkipOption"))
            Bot.Config.Configure();

        if (Bot.Config.Get<bool>("PolishedBlod"))
            GetPolishedBLoD();

        if (Bot.Config.Get<bool>("ToxicPlagueSpear"))
            GetToxicPlagueSpear();

        if (Bot.Config.Get<bool>("BurningPhoenixBlade"))
            GetBurningPhoenixBlade();

        if (Bot.Config.Get<bool>("OdokuroBlight"))
            GetOdokuroBlight();

        if (Bot.Config.Get<bool>("PolishedManslayer"))
            GetPolishedManslayer();

        if (Bot.Config.Get<bool>("CursedDoomBlade"))
            GetCursedDoomBlade();

        if (Bot.Config.Get<bool>("RebornSepulchureHelm"))
            GetRebornSepulchureHelm();

        if (Bot.Config.Get<bool>("GetAll"))
            GetAll();
    }

    public void GetAll()
    {
        GetPolishedBLoD();
        GetToxicPlagueSpear();
        GetBurningPhoenixBlade();
        GetOdokuroBlight();
        GetPolishedManslayer();
        GetCursedDoomBlade();
        GetRebornSepulchureHelm();
    }

    public void GetPolishedBLoD()
    {
        if (Core.CheckInventory("Polished Blinding Light of Destiny", toInv: false))
            return;

        BLOD.DoAll();

        Core.AddDrop("Polished Blinding Light of Destiny");

        //Upgrade the Blinding Light of Destiny 7063

        Core.EnsureAccept(7063);

        Farm.BattleUnderB("Undead Energy", 3000);
        Adv.BuyItem("alchemyacademy", 2114, "Bright Tonic", 10, 10);
        Core.HuntMonster("doomwoodforest", "Undead Paladin", "Purification Orb", 10, false);
        Core.KillMonster("doomwoodforest", "r7", "Up", "*", "Shoelace of a Fallen Paladin", 3, false);
        Core.HuntMonster("therift", "Plague Spreader", "Slimed Sigil", 75, false);
        Core.HuntMonster("lightguardwar", "Sigrid Sunshield", "Medal of Justice", 150, false);

        Core.EnsureComplete(7063);
        Bot.Wait.ForPickup("Polished Blinding Light of Destiny");
    }

    public void GetToxicPlagueSpear()
    {
        if (Core.CheckInventory("Toxic Plague Spear", toInv: false))
            return;

        if (!Core.CheckInventory("Undead Plague Spear"))
            Core.HuntMonster("Graveyard", "Big Jack Sprat", "Undead Plague Spear", isTemp: false);

        Core.AddDrop("Toxic Plague Spear");

        Bot.Quests.UpdateQuest(3484);
        while (!Bot.ShouldExit && !Core.CheckInventory("Toxic Plague Spear"))
        {
            //Upgrade the Plague Spear 7064

            Core.EnsureAccept(7064);

            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("Swallowed", "Germs", "Botulinum", 6);
            Core.HuntMonster("Artixpointe", "Enchanted Sushi", "Pufferfish Sushi", 3);
            Core.HuntMonster("TowerofDoom10", "Dreadroom", "Dried Dreadroom", 5);

            Core.EnsureComplete(7064);
            Bot.Wait.ForPickup("Toxic Plague Spear");
        }
    }

    public void GetBurningPhoenixBlade()
    {
        if (Core.CheckInventory("Burning Phoenix Blade", toInv: false))
            return;

        if (!Core.CheckInventory("Phoenix Blade"))
            Core.HuntMonster("lair", "Red Dragon", "Phoenix Blade", isTemp: false);

        Core.AddDrop("Burning Phoenix Blade");


        while (!Bot.ShouldExit && !Core.CheckInventory("Burning Phoenix Blade"))
        {
            //Upgrade the Phoenix Blade 7065

            Core.EnsureAccept(7065);

            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("Lair", "Bronze Draconian", "Wisp of Dragonspirit", 12);
            Core.HuntMonster("Lair", "Dark Draconian", "Crystallized Flame");
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("underlair", "ArchFiend DragonLord", "Void Scale", 13, isTemp: false);

            Core.EnsureComplete(7065);
            Bot.Wait.ForPickup("Burning Phoenix Blade");
        }
    }

    public void GetOdokuroBlight()
    {
        if (Core.CheckInventory("O-dokuro's Blight", toInv: false))
            return;

        if (!Core.CheckInventory("O-dokuro Blade"))
            Core.HuntMonster("Odokuro", "O-dokuro", "O-dokuro Blade", isTemp: false);

        Core.AddDrop("O-dokuro's Blight");


        while (!Bot.ShouldExit && !Core.CheckInventory("O-dokuro's Blight"))
        {
            //Upgrade O-Dokuro Blade 7066

            Core.EnsureAccept(7066);

            Core.EquipClass(ClassType.Farm);
            Farm.BattleUnderB("Bone Dust", 25);
            Core.HuntMonster("Embersea", "Storm Scout", "Polish");
            Core.HuntMonster("Embersea", "Flame Soldier", "Cloth");

            Core.EnsureComplete(7066);
            Bot.Wait.ForPickup("O-dokuro's Blight");
        }
    }

    public void GetPolishedManslayer()
    {
        if (Core.CheckInventory("Polished Manslayer", toInv: false))
            return;

        if (!Core.CheckInventory("Taro's Manslayer"))
            TarosManslayer.GuardianTaro(true);

        Core.AddDrop("Polished Manslayer");


        while (!Bot.ShouldExit && !Core.CheckInventory("Polished Manslayer"))
        {
            //Upgrade Taro’s Manslayer 7067

            Core.EnsureAccept(7067);

            Core.EquipClass(ClassType.Farm);
            Farm.BattleUnderB("Bone Dust", 25);
            Core.HuntMonster("Bloodtusk ", "Crystal Rock", "Polished Rocks", 3);
            Core.HuntMonster("Bloodtusk ", "Crystal Rock", "Precious Gemstone", 3);
            Core.HuntMonster("DarkFortress", "Wilhelm", "Ultra Shifting Plane Gem", 15, isTemp: false);

            Core.EnsureComplete(7067);
            Bot.Wait.ForPickup("Polished Manslayer");
        }
    }

    public void GetCursedDoomBlade()
    {
        if (Core.CheckInventory("Cursed DoomBlade", toInv: false))
            return;

        if (!Core.CheckInventory("Sepulchure's Undead Blade"))
        {
            Bot.ShowMessageBox($"The bot is about to buy \"Sepulchure's Undead Blade\", which costs 2500 AC, do you accept this?", "Warning: Costs AC!", true);
            Adv.BuyItem("museum", 580, "Sepulchure's Undead Blade");
        }

        Core.AddDrop("Cursed DoomBlade");


        while (!Bot.ShouldExit && !Core.CheckInventory("Cursed DoomBlade"))
        {
            //Upgrade Sepulchure’s Undead Blade 7068

            Core.EnsureAccept(7068);

            DS.GetShard(1);
            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("shadowfallwar", "Garden1", "Bottom", "Bonemuncher", "Ultimate Darkness Gem", 50, isTemp: false);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("frozenlair", "Lich Lord", "Necrotic Orb", 100, isTemp: false);
            Core.HuntMonster("underworld", "Frozen Pyromancer", "Flaming Skull", 50, isTemp: false);

            Core.EnsureComplete(7068);
            Bot.Wait.ForPickup("Cursed DoomBlade");
        }
    }

    public void GetRebornSepulchureHelm()
    {
        if (Core.CheckInventory("Reborn Sepulchure's Helm", toInv: false))
            return;

        Seppy.DoAll();

        Core.AddDrop("Reborn Sepulchure's Helm", "Ultimate Darkness Gem");

        //Upgrade Sepulchure’s Original Helm 7069
        Core.EnsureAccept(7069);

        DS.GetShard(1);
        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("shadowfallwar", "Garden1", "Bottom", "Bonemuncher", "Ultimate Darkness Gem", 75, isTemp: false);
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("frozenlair", "Lich Lord", "Necrotic Orb", 150, isTemp: false);
        Core.HuntMonster("underworld", "Frozen Pyromancer", "Flaming Skull", 100, isTemp: false);

        Core.EnsureComplete(7069);
        Bot.Wait.ForPickup("Reborn Sepulchure's Helm");
    }

}

