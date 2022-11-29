//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
//cs_include Scripts/Good/GearOfAwe/ArmorOfAwe.cs
//cs_include Scripts/Good/GearOfAwe/HelmOfAwe.cs
//cs_include Scripts/Good/SilverExaltedPaladin.cs
//cs_include Scripts/Other/Weapons/FortitudeAndHubris.cs
//cs_include Scripts/Other/Weapons/ShadowReaperOfDoom.cs
//cs_include Scripts/Story/J6Saga.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Story/Nation/Bamboozle.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Story/7DeadlyDragons/Extra/HatchTheEgg.cs
//cs_include Scripts/Other/MysteriousEgg.cs
//cs_include Scripts/Other/ShadowDragonDefender.cs
//cs_include Scripts/Chaos/DrakathsArmor.cs
//cs_include Scripts/Evil/SepulchuresOriginalHelm.cs
//cs_include Scripts/Evil/ADK.cs
//cs_include Scripts/Story\DjinnGate.cs
using Skua.Core.Interfaces;

public class Awescended
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreStory Story = new CoreStory();
    public CoreAdvanced Adv = new CoreAdvanced();
    public ArmorOfAwe AweArmor = new ArmorOfAwe();
    public HelmOfAwe Helm = new HelmOfAwe();
    public SEP Pal = new SEP();
    public FandH FaH = new FandH();
    public SRoD SRoD = new SRoD();
    public GetSDD SDD = new GetSDD();
    public DrakathArmorBot Armor = new DrakathArmorBot();
    public SepulchuresOriginalHelm Seppy = new SepulchuresOriginalHelm();
    public ArchDoomKnight ADK = new ArchDoomKnight();
    public J6Saga J6 = new J6Saga();
    public BattleUnder Under = new BattleUnder();
    public Bamboozle Bam = new Bamboozle();
    public DjinnGateStory Djinn = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAwe();

        Core.SetOptions(false);
    }

    public void GetAwe()
    {
        if (Core.CheckInventory(new[] { "Awescended", "Awescended Omni Armblades", "Awescended Omni Cowl", "Awescended Omni Wings" }))
            return;

        Story.PreLoad(this);

        //The Dawn of Lore
        if (!Story.QuestProgression(8035))
        {
            Farm.Experience(25);
            Core.EnsureAccept(8035);
            Core.KillMonster("uppercity", "r3", "Left", "Chaos Egg", "Fossilized Egg Yolk", 12);
            Bot.Quests.UpdateQuest(567);
            Core.KillMonster("lycanwar", "Boss", "Left", "Edvard", "Stone Mask");
            Bot.Quests.UpdateQuest(4614);
            Core.KillMonster("pyramid", "r5", "Left", "*", "Mummified Bone", 6);
            Core.KillMonster("ravinetemple", "r11", "Left", "*", "Iron Head", 4);
            Core.EnsureComplete(8035);
        }

        //Mystical Magics
        if (!Story.QuestProgression(8036))
        {
            Core.EnsureAccept(8036);
            Core.KillMonster("deathsrealm", "Frame3", "Down", "Undead Mage", "Enchanted Manuscript", 8);
            Core.EquipCached();

            Core.KillMonster("citadel", "m14", "Left", "Grand Inquisitor", "Rite of Renewal");
            Core.KillMonster("marsh", "Forest3", "Left", "Dark Witch", "Coven's Sigil");
            Core.KillMonster("gilead", "r4", "Left", "*", "Spell Stone", 10);
            Core.EnsureComplete(8036);
        }

        //Science Rules
        if (!Story.QuestProgression(8037))
        {
            Core.EnsureAccept(8037);
            Core.Join("thunderfang", "r2", "Left");
            Core.KillMonster("thunderfang", "r2", "Left", "Energy Elemental", "Supercharged Gem", 8);
            Core.HuntMonster("lab", "Frank", "Lightning Capacitor", 5);
            Core.KillMonster("boxes", "Boss", "Center", "Sneeviltron", "Wooden Control Panel");
            Core.KillMonster("mqlesson", "Boss", "Left", "Dragonoid", "Dragonoid Core");
            Core.EnsureComplete(8037);
        }

        //War: What is it Good For?
        if (!Story.QuestProgression(8038))
        {
            Core.EnsureAccept(8038);
            Core.KillMonster("yokaiwar", "Boss", "Left", "O-dokuro's Head", "O-dokuro's Tooth", isTemp: false);
            Core.KillMonster("wardwarf", "r4", "Left", "D'wain Jonsen", "D'wain Jonsen's Stinger");
            Core.KillMonster("mythsongwar", "War2", "Left", "*", "Music Pirate's Instrument of War", 6);
            Bot.Quests.UpdateQuest(1170);
            Core.KillMonster("shadowfallwar", "Inside", "Right", "Noxus", "Noxus' Necromancy Robe");
            Core.EnsureComplete(8038);
        }

        //Which Came First, the Dragon or the Dragonoid?
        Story.KillQuest(8039, "crashsite", new[] { "Dwakel Warrior", "Barrier Bot", "Mithril Man", "ProtoSartorium" });

        //Artifact Hunting
        if (!Story.QuestProgression(8040))
        {
            Farm.Experience(50);
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(8040);
            Adv.BoostKillMonster("ledgermayne", "Boss", "Left", "Ledgermayne", "The Supreme Arcane Staff", isTemp: false);
            Core.BuyItem("doomwood", 276, "Blinding Light of Destiny Handle");
            SRoD.ShadowReaperOfDoom();
            Farm.BladeofAweREP(10, true);
            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("cornelis", "Side1", "Left", "Gargoyle", "Mirror Shield Fragment", 50, false);
            Core.EnsureComplete(8040);
        }

        //What Does Lore Have in Store?
        if (!Story.QuestProgression(8041))
        {
            Farm.Experience(75);
            Core.EnsureAccept(8041);
            if (!Core.CheckInventory("Vaden's Helm"))
            {
                Core.EquipClass(ClassType.Solo);
                Core.KillMonster("bonecastlec", "r25", "Bottom", "Vaden", "Vaden Helm Token", 333, false);
                while (!Core.CheckInventory("Vaden's Helm"))
                {
                    Core.BuyItem("bonecastlec", 1242, "Vaden's Helm", shopItemID: 4363);
                    Bot.Sleep(500);
                }
            }
            Core.EquipClass(ClassType.Farm);
            ArmorOfZular();
            FaH.FortitudeAndHubris();
            SDD.ShadowDragonDefender();
            Pal.SilverExaltedPaladin();

            if (!Core.CheckInventory("Valoth's Cannon of Doom"))
            {
                Core.AddDrop("Valoth's Cannon of Doom");

                if (!Core.CheckInventory("Valoth's Broken Cannon"))
                {
                    Farm.Gold(5000000);
                    Core.BuyItem("crashruins", 1212, "Valoth's Broken Cannon");
                }

                Core.EnsureAccept(8043);
                J6.J6();
                Core.BuyItem("hyperspace", 603, "Peanut");
                Bam.BamboozleQuest();
                if (!Core.CheckInventory("Floozer"))
                {
                    Core.AddDrop("Floozer");
                    Core.EnsureAccept(7290);
                    if (!Core.CheckInventory("Rainbow Moonstone"))
                    {
                        Core.AddDrop("Rainbow Moonstone");
                        Core.EnsureAccept(7291);
                        Core.HuntMonster("earthstorm", "Sapphire Golem", "Chip of Sapphire");
                        Core.HuntMonster("earthstorm", "Ruby Golem", "Chip of Ruby");
                        Core.HuntMonster("earthstorm", "Emerald Golem", "Chip of Emerald");
                        Core.HuntMonster("earthstorm", "Diamond Golem", "Chip of Diamond");
                        Core.EnsureComplete(7291);
                        Bot.Wait.ForPickup("Rainbow Moonstone");
                    }
                    Core.EnsureComplete(7290);
                    Bot.Wait.ForPickup("Floozer");
                }
                Core.EnsureComplete(8043);
                Bot.Wait.ForPickup("Valoth's Cannon of Doom");
            }

            Core.EnsureComplete(8041);
        }

        //Awe-scention
        if (!Story.QuestProgression(8042))
        {
            Farm.Experience(100);
            Core.EnsureAccept(8042);
            AweArmor.GetArmor();
            Helm.GetHoA();
            Seppy.DoAll();
            Core.ToBank(Seppy.GravelynsDoomFireTokenItems);
            ADK.DoAll(true);
            Core.Unbank("Arch DoomKnight");
            Armor.DrakathOriginalArmor();

            // Relogin triggered by Drakath
            Core.EnsureAccept(8042);
            Core.HuntMonster("ectocave", "Ektorax", "Bin Jett's Salvaged Armor Part", 50, false);

            Core.Unbank($"GOLD Boost! (60 min)", "Doom GOLD Boost! (60 min)", "GOLD Boost! (20 min)");
            Bot.Boosts.UseGoldBoost = true;
            Adv.BestGear(GearBoost.gold);

            Core.EnsureComplete(8042);
            Bot.Boosts.UseGoldBoost = false;
        }

        Core.BuyItem("museum", 1994, "Awescended");
        Core.BuyItem("museum", 1994, "Awescended Omni Armblades");
        Core.BuyItem("museum", 1994, "Awescended Omni Cowl");
        Core.BuyItem("museum", 1994, "Awescended Omni Wings");
        Core.Logger("Awescended Set Bought, Congratulations!");
    }

    public void ArmorOfZular()
    {
        if (Core.CheckInventory("Armor of Zular"))
            return;

        Core.AddDrop("Armor of Zular", "Djinn's Essence");
        Core.EquipClass(ClassType.Farm);
        Djinn.DjinnGate();
        Core.Unbank("Armor of Zular");
    }
}