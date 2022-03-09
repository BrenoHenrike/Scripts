//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
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
//cs_include Scripts/Story/MysteriousDungeon.cs
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

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetAwe();

        Core.SetOptions(false);
    }

    public void GetAwe()
    {
        Core.EquipClass(ClassType.Farm);

        //The Dawn of Lore
        if (!Story.QuestProgression(8035))
        {
            Farm.Experience(25);
            Core.EnsureAccept(8035);
            Core.KillMonster("uppercity", "r3", "Left", "Chaos Egg", "Fossilized Egg Yolk", 12);
            Story.UpdateQuest(537);
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
            ValothsCannonOfDoom();
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

    public void ValothsCannonOfDoom()
    {
        if (Core.CheckInventory("Valoth's Cannon of Doom"))
            return;

        Core.AddDrop("Valoth's Cannon of Doom");

        if (!Core.CheckInventory("Valoth's Broken Cannon"))
        {
            Farm.Gold(5000000);
            Core.BuyItem("crashruins", 1212, "Valoth's Broken Cannon");
        }

        Core.EnsureAccept(8043);
        J6.J6();
        Core.BuyItem("hyperspace", 603, "Peanut");
        Floozer();
        Core.EnsureComplete(8043);
    }

    public void Floozer()
    {
        if (Core.CheckInventory("Floozer"))
            return;

        Core.AddDrop("Floozer", "Ice Diamond", "Dark Bloodstone", "Songstone", "Butterfly Sapphire", "Understone", "Rainbow Moonstone");

        //Star of the Sandsea
        Story.KillQuest(7277, "wanders", "Kalestri Worshiper", GetReward: false);

        //Ice Diamond
        if (!Story.QuestProgression(7278))
        {
            if (!Core.CheckInventory("Ice Diamond"))
            {
                Core.EnsureAccept(7278, 7279);
                Core.HuntMonster("kingcoal", "Snow Golem", "Frozen Coal", 10);
                Core.EnsureComplete(7279);
                Bot.Wait.ForPickup("Ice Diamond");
            }
            Core.EnsureComplete(7278);
        }

        //Dark Bloodstone
        if (!Story.QuestProgression(7280))
        {
            if (!Core.CheckInventory("Dark Bloodstone"))
            {
                Core.EnsureAccept(7280, 7281);
                Core.HuntMonster("safiria", "Blood Maggot", "Blood Gem", 10);
                Core.EnsureComplete(7281);
                Bot.Wait.ForPickup("Dark Bloodstone");
            }
            Core.EnsureComplete(7280);
        }

        //Doomstone
        Story.KillQuest(7282, "brightfall", "Painadin Overlord", GetReward: false);

        //Void Opal
        Story.KillQuest(7283, "timevoid", "Unending Avatar", GetReward: false);

        //Mana Crystal
        Story.MapItemQuest(7284, "downward", 6908, GetReward: false);

        //Songstone
        if (!Story.QuestProgression(7285))
        {
            if (!Core.CheckInventory("Songstone"))
            {
                Core.EnsureAccept(7285, 7297);
                Core.GetMapItem(6909, 15, "mythsong");
                Core.EnsureComplete(7297);
                Bot.Wait.ForPickup("Songstone");
            }
            Core.EnsureComplete(7285);
        }

        //Butterfly Sapphire
        if (!Story.QuestProgression(7286))
        {
            if (!Core.CheckInventory("Butterfly Sapphire"))
            {
                Core.EnsureAccept(7286);
                Core.HuntMonster("bloodtusk", "Trollola Plant", "Butterfly Bloom", 15);
                Core.EnsureComplete(7287);
                Bot.Wait.ForPickup("Butterfly Sapphire");
            }
            Core.EnsureComplete(7286);

        }

        //Understone
        if (!Story.QuestProgression(7288))
        {
            if (!Core.CheckInventory("Understone"))
            {
                Under.Understone(1);
                Bot.Wait.ForPickup("Understone");
                Core.ChainComplete(7288);
            }
        }

        //Rainbow Moonstone
        if (Story.QuestProgression(7290) || !Core.CheckInventory("Floozer"))
        {
            if (!Core.CheckInventory("Rainbow Moonstone"))
            {
                Core.EnsureAccept(7290, 7291);
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
    }
}