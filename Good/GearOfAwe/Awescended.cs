//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
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
//cs_include Scripts/Story/ThroneofDarkness/07bStranger(MysteriousDungeon).cs
//cs_include Scripts/Story/Doomwood/AQWZombies.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/7DeadlyDragons/MysteriousEgg.cs
//cs_include Scripts/Story/7DeadlyDragons/ShadowDragonDefender.cs
//cs_include Scripts/Chaos/DrakathArmorBot.cs
//cs_include Scripts/Evil/SepulchuresOriginalHelm.cs
//cs_include Scripts/Evil/ADK.cs
using RBot;

public class Awescended
{
    public ScriptInterface Bot => ScriptInterface.Instance;
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

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetAwe();

        Core.SetOptions(false);
    }

    public void GetAwe()
    {
        if (Core.CheckInventory(new[] { "Awescended", "Awescended Omni Armblades", "Awescended Omni Cowl", "Awescended Omni Wings" }))
            return;

        Story.PreLoad();

        //The Dawn of Lore
        if (!Story.QuestProgression(8035))
        {
            Farm.Experience(25);
            Core.EnsureAccept(8035);
            Core.KillMonster("uppercity", "r3", "Left", "Chaos Egg", "Fossilized Egg Yolk", 12);
            Bot.Quests.UpdateQuest(537);
            Core.Join("lycanwar", "Boss", "Left");
            Core.KillMonster("lycanwar", "Boss", "Left", "Edvard", "Stone Mask");
            Core.KillMonster("pyramid", "r5", "Left", "Mummy", "Mummified Bone", 6);
            Core.KillMonster("ravinetemple", "r11", "Left", "*", "Iron Head", 4);
            Core.EnsureComplete(8035);
        }

        //Mystical Magics
        if (!Story.QuestProgression(8036))
        {
            Core.EnsureAccept(8036);
            Core.KillMonster("deathsrealm", "Frame3", "Down", "Undead Mage", "Enchanted Manuscript", 8);
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
            Core.HuntMonster("shadowfallwar", "Noxus", "Noxus' Necromancy Robe");
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
            Core.KillMonster("cornelis", "Side1", "Left", "*", "Mirror Shield Fragment", 50, false);
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
                Core.BuyItem("bonecastlec", 1242, "Vaden's Helm");
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
            ADK.DoAll();
            Armor.DrakathOriginalArmor();
            Core.KillMonster("ectocave", "Boss", "Left", "*", "Bin Jett's Salvaged Armor Part", 50, false);
            Core.EnsureComplete(8042);
        }

        Core.BuyItem("museum", 1994, "Awescended");
        Core.BuyItem("museum", 1994, "Awescended Omni Armblades");
        Core.BuyItem("museum", 1994, "Awescended Omni Cowl");
        Core.BuyItem("museum", 1994, "Awescended Omni Wings");
    }

    public void ArmorOfZular()
    {
        if (Core.CheckInventory("Armor of Zular"))
            return;

        Core.AddDrop("Armor of Zular", "Djinn's Essence");
        Core.EquipClass(ClassType.Farm);

        //Recovering the Fangs of the Lion
        if (!Story.QuestProgression(6153))
        {
            Core.EnsureAccept(6153);
            Core.KillMonster("mobius", "Slugfit", "Left", "Slugfit", "Fragment 1");
            Core.KillMonster("faerie", "TopRock", "Left", "*", "Fragment 2");
            Core.KillMonster("faerie", "Side4", "Right", "*", "Fragment 3");
            Core.KillMonster("faerie", "End", "Center", "Cyclops Warlord", "Fragment 4");
            Core.KillMonster("cornelis", "Side1", "Left", "*", "Fragment 5");
            Core.EnsureComplete(6153);
        }

        //Recovering the Claws of the Daeva
        if (!Story.QuestProgression(6154))
        {
            Core.EnsureAccept(6154);
            Core.KillMonster("arcangrove", "Left", "Left", "*", "Fragment 6");
            Core.KillMonster("cloister", "r8", "Left", "*", "Fragment 7");
            Core.KillMonster("gilead", "r5", "Right", "Bubblin", "Fragment 8");
            Core.KillMonster("natatorium", "r2", "Left", "Merdraconian", "Fragment 9");
            Core.KillMonster("mafic", "r6", "Left", "*", "Fragment 10");
            Core.EnsureComplete(6154);
        }

        //Recovering the Light of the Serpent
        if (!Story.QuestProgression(6155))
        {
            Core.EnsureAccept(6155);
            Core.KillMonster("mythsong", "Hill", "Left", "*", "Fragment 11");
            Core.KillMonster("palooza", "Act3", "Left", "Rock Lobster", "Fragment 12");
            Core.KillMonster("palooza", "Act2", "Left", "Stinger", "Fragment 13");
            Core.KillMonster("palooza", "Act3", "Left", "Mozard", "Fragment 15");
            Core.KillMonster("beehive", "r5", "Left", "*", "Fragment 14");
            Core.EnsureComplete(6155);
        }

        //Recovering the Pike of the Shimmering Sands
        if (!Story.QuestProgression(6156))
        {
            Core.EnsureAccept(6156);
            Core.KillMonster("forestchaos", "Boss", "Left", "*", "Fragment 16");
            Core.KillMonster("guru", "Field2", "Left", "*", "Fragment 17");
            Core.KillMonster("marsh", "Forest3", "Left", "Dark Witch", "Fragment 18");
            Core.KillMonster("marsh", "Forest3", "Left", "Spider", "Fragment 19");
            Core.KillMonster("marsh2", "End", "Left", "Soulseeker", "Fragment 20");
            Core.EnsureComplete(6156);
        }

        //Recovering the Reavers of the Gilded Sun
        if (!Story.QuestProgression(6157))
        {
            Core.EnsureAccept(6157);
            Core.KillMonster("pirates", "End", "Left", "Shark Bait", "Fragment 21");
            Core.KillMonster("pirates", "End", "Left", "Fishwing", "Fragment 25");
            Core.KillMonster("yokairiver", "r2", "Left", "Kappa Ninja", "Fragment 22");
            Core.KillMonster("bamboo", "Enter", "Spawn", "*", "Fragment 23");
            Core.KillMonster("yokaiwar", "War2", "Left", "Samurai Nopperabo", "Fragment 24");
            Core.EnsureComplete(6157);
        }

        //Gauntlet of Monsters
        if (!Story.QuestProgression(6158))
        {
            Core.EnsureAccept(6158);
            Core.EquipClass(ClassType.Solo);
            Core.KillMonster("doomkitten", "Enter", "Spawn", "*", "Potent DoomKitten Mana", publicRoom: true);
            Core.KillMonster("bloodtitan", "Ultra", "Left", "*", "Potent Blood Titan Mana");
            Core.HuntMonster("trigoras", "Trigoras", "Potent Trigoras Mana");
            Core.KillMonster("phoenixrise", "r8", "Left", "*", "Potent CinderClaw Mana");
            Core.KillMonster("thevoid", "r16", "Left", "*", "Potent Reaper Mana", publicRoom: true);
            Core.EnsureComplete(6158);
        }

        //Gauntlet of Drakels
        Story.MapItemQuest(6159, "djinngate", 5571, 5, false);

        Core.EquipClass(ClassType.Farm);
        //Gauntlet of Generals
        Story.KillQuest(6160, "djinngate", "Harpy|Lamia");
    }
}