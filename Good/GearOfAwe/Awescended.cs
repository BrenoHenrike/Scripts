//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
//cs_include Scripts/Good/GearOfAwe/ArmorOfAwe.cs
//cs_include Scripts/Good/GearOfAwe/HelmOfAwe.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
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
//cs_include Scripts/Nulgath/CoreNulgath.cs

using RBot;

public class Awescended
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAwe Awe = new CoreAwe();
    public CoreStory Story = new CoreStory();
    public ArmorOfAwe AweArmor = new ArmorOfAwe();
    public HelmOfAwe Helm = new HelmOfAwe();
    public SEP Pal = new SEP();
    public FandH FaH = new FandH();
    public SRoD SRoD = new SRoD();
    public GetSDD SDD = new GetSDD();
    public DrakathArmorBot Armor = new DrakathArmorBot();
    public SepulchuresOriginalHelm Seppy = new SepulchuresOriginalHelm();
    public ArchDoomKnight ADK = new ArchDoomKnight();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetAwe();

        Core.SetOptions(false);
    }

    public void GetAwe()
    {
        Core.AddDrop("O-dokuro's Tooth", "The Supreme Arcane Staff", "Blinding Light of Destiny Handle",
        "ShadowReaper of Doom", "Blade of Awe", "Mirror Shield Fragment", "Mirror Realm Token", 
        "Undead Paladin Token", "Vaden's Helm", "Vaden Helm Token", "Armor of Zular", "Fortitude + Hubris", 
        "Shadow Dragon Defender", "Silver Exalted Paladin", "Valoth's Cannon of Doom", "Bin Jett's Salvaged Armor Part");
        Farm.Experience(25);
        Core.EquipClass(ClassType.Farm);
        if (!Story.QuestProgression(8036))
        {
            Core.EnsureAccept(8035);
            Core.KillMonster("uppercity", "r3", "Left", "Chaos Egg", "Fossilized Egg Yolk", 12);
            Core.KillMonster("lycanwar", "Boss", "Left", "Edvard", "Stone Mask");
            Core.KillMonster("pyramid", "r5", "Left", "Mummy", "Mummified Bone", 6);
            Core.KillMonster("ravinetemple", "r11", "Left", "*", "Iron Head", 4);
            Core.EnsureComplete(8035);
        }
        if(!Story.QuestProgression(8037))
        {
            Core.EnsureAccept(8036);
            Core.KillMonster("deathsrealm", "Frame3", "Down", "Undead Mage", "Enchanted Manuscript", 8);
            Core.KillMonster("citadel", "m14", "Left", "Grand Inquisitor", "Rite of Renewal");
            Core.KillMonster("marsh", "Forest3", "Left", "Dark Witch", "Coven's Sigil");
            Core.KillMonster("gilead", "r4", "Left", "*", "Spell Stone", 10);
            Core.EnsureComplete(8036);
        }
        if(!Story.QuestProgression(8038))
        {
            Core.EnsureAccept(8037);
            Core.KillMonster("thunderfang", "r2", "Left", "Energy Elemental", "Supercharged Gem", 8);
            Core.HuntMonster("lab", "Frank", "Lightning Capacitor", 5);
            Core.KillMonster("boxes", "Boss", "Center", "Sneeviltron", "Wooden Control Panel");
            Core.KillMonster("mqlesson", "Boss", "Left", "Dragonoid", "Dragonoid Core");
            Core.EnsureComplete(8037);
        }
        if(!Story.QuestProgression(8039))
        {
            Core.EnsureAccept(8038);
            Core.KillMonster("yokaiwar", "Boss", "Left", "O-Dokuro's Head", "O-Dokuro's Tooth", isTemp: false);
            Core.KillMonster("wardwarf", "r4", "Left", "D'wain Jonsen", "D'wain Jonsen's Stinger");
            Core.KillMonster("mythsongwar", "War2", "Left", "*", "Music Pirate's Instrument of War", 6);
            Core.HuntMonster("shadowfallwar", "Noxus", "Noxus' Necromancy Robe");
            Core.EnsureComplete(8038);
        }
            Story.KillQuest(8039, "crashsite", new[] { "Barrier Bot", "Dwakel Warrior", "Mithril Man", "ProtoSartorium" });
        Farm.Experience(50);
        if(!Story.QuestProgression(8041))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(8040);
            Core.KillMonster("ledgermayne", "Boss", "Left", "Ledgermayne", "The Supreme Arcane Staff", isTemp: false);
            Core.BuyItem("doomwood", 276, "Blinding Light of Destiny Handle");
            SRoD.ShadowReaperOfDoom();
            Farm.BladeofAweREP(10, true);
            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("cornelis", "Side1", "Left", "*", "Mirror Shield Fragment", 50, false);
            Core.EnsureComplete(8040);
        }
        Farm.Experience(75);
        if(!Story.QuestProgression(8042))
        {
            Core.EnsureAccept(8041);
            if (!Core.CheckInventory("Vaden's Helm"))
            {
                Core.EquipClass(ClassType.Solo);
                Core.KillMonster("bonecastlec", "r25", "Bottom", "Vaden", "Vaden Helm Token", 333, false);
                Core.BuyItem("bonecastlec", 1242, "Vaden's Helm");
            }
            Core.EquipClass(ClassType.Farm);
            Awe.ArmorOfZular();
            FaH.FortitudeAndHubris();
            SDD.ShadowDragonDefender();
            Pal.SilverExaltedPaladin();
            Awe.ValothsCannonOfDoom();
            Core.EnsureComplete(8041);
        }
        Farm.Experience(100);
        Core.EnsureAccept(8042);
        Armor.DrakathOriginalArmor();
        AweArmor.GetArmor();
        Helm.GetHoA();
        Seppy.DoAll();
        ADK.DoAll();
        Core.KillMonster("ectocave", "Boss", "Left", "*", "Bin Jett's Salvaged Armor Part", 50, false);
        Core.EnsureComplete(8042);
    }

    
}