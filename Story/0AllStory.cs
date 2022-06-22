//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/7DeadlyDragons/00SevenDeadlyDragons.cs
//cs_include Scripts/Story/7DeadlyDragons/01Gluttony.cs
//cs_include Scripts/Story/7DeadlyDragons/02Pride.cs
//cs_include Scripts/Story/7DeadlyDragons/03Greed.cs
//cs_include Scripts/Story/7DeadlyDragons/04Sloth.cs
//cs_include Scripts/Story/7DeadlyDragons/05Lust.cs
//cs_include Scripts/Story/7DeadlyDragons/06Envy.cs
//cs_include Scripts/Story/7DeadlyDragons/07Wrath.cs
//cs_include Scripts/Story/7DeadlyDragons/MysteriousEgg.cs
//cs_include Scripts/Story/7DeadlyDragons/ShadowDragonDefender.cs
//cs_include Scripts/Story/Doomwood/AQWZombies.cs
//cs_include Scripts/Story/Doomwood/DoomwoodPart3.cs
//cs_include Scripts/Story/Doomwood/Necrodungeon.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
//cs_include Scripts/Story/Legion/DarkAlliance.cs
//cs_include Scripts/Story/Legion/DarkAlly.cs
//cs_include Scripts/Story/Legion/SeraphicWar.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
//cs_include Scripts/Story/Legion/WorldSoul.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/LordsofChaos/ChoasFinaleBonus[Mem]/DeadlyDungeon[Mem].cs
//cs_include Scripts/Story/LordsofChaos/ChoasFinaleBonus[Mem]/KillerCatacombs[Mem].cs
//cs_include Scripts/Story/LordsofChaos/ChoasFinaleBonus[Mem]/PyramidofPain[Mem].cs
//cs_include Scripts/Story/Nation/Bamboozle.cs
//cs_include Scripts/Story/Nation/CitadelRuins.cs
//cs_include Scripts/Story/Nation/Fiendshard.cs
//cs_include Scripts/Story/Nation/Originul.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQoM.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/BrightOak.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/LivingDungeon.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Story/Artixpointe.cs
//cs_include Scripts/Story/Banished.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Story/Borgars.cs
//cs_include Scripts/Story/Collection.cs
//cs_include Scripts/Story/DjinnGate.cs
//cs_include Scripts/Story/DoomVault.cs
//cs_include Scripts/Story/DoomVaultB.cs
//cs_include Scripts/Story/DragonFableOrigins.cs
//cs_include Scripts/Story/Glacera.cs
//cs_include Scripts/Story/J6Saga.cs
//cs_include Scripts/Story/Phoenixrise.cs
//cs_include Scripts/Story/TowerOfDoom.cs
//cs_include Scripts/Story/Tutorial.cs
//cs_include Scripts/Story/WarfuryTraining.cs
//cs_include Scripts/Story/XansLair.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class AllStory
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public CoreStory Story = new();
    public Complete7DeadlyDragons C7DD = new();
    public GetSDD SDD = new GetSDD();
    public AQWZombies AQWZombies = new();
    public DoomwoodPart3 DoomwoodPart3 = new();
    public NecroDungeon NecroDungeon = new();
    public CoreAstravia CoreAstravia = new();
    public DarkAlliance_Story DarkAlliance_Story = new();
    public DarkAlly_Story DarkAlly_Story = new();
    public SeraphicWar_Story SeraphicWar_Story = new();
    public SevenCircles SevenCircles = new();
    public WorldSoul WorldSoul = new();
    public Core13LoC LOC => new();
    public DeadlyDungeon DeadlyDungeon = new();
    public KillerCatacombs KillerCatacombs = new();
    public PyramidofPain PyramidofPain = new();
    public Bamboozle Bamboozle = new();
    public CitadelRuins CitadelRuins = new();
    public Fiendshard_Story Fiendshard_Story = new();
    public Originul_Story Originul_Story = new();
    public CoreQOM QOM => new();
    public BrightOak BrightOak = new();
    public LivingDungeon LivingDungeon = new();
    public CoreToD TOD = new();
    public Artixpointe Artixpointe = new();
    public Banished Banished = new();
    public BattleUnder BattleUnder = new();
    public Borgars Borgars = new();
    public Collection Collection = new();
    public DjinnGateStory DjinnGateStory = new();
    public DoomVaultA DoomVaultA = new();
    public DoomVaultB DoomVaultB = new();
    public DragonFableOrigins DragonFableOrigins = new();
    public GlaceraStory GlaceraStory = new();
    public J6Saga J6Saga = new();
    public PhoenixriseStory PhoenixriseStory = new();
    public TowerOfDoom TowerOfDoom = new();
    public Tutorial Tutorial = new();
    public WarTraining WarTraining = new();
    public XansLair Xans = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        CompleteAll();

        Core.SetOptions(false);
    }

    public void CompleteAll()
    {
        C7DD.CompleteToD();
        Core.Logger($"Saga: 7 Deadly Dragons - Complete");

        AQWZombies.Storyline();
        Core.Logger($"Story: AQW Zombies - Complete");

        DoomwoodPart3.StoryLine();
        Core.Logger($"Story: Doomwood Part 3 - Complete");

        NecroDungeon.NecrodungeonStoryLine();
        Core.Logger($"Story: Necro Dungeon - Complete");

        CoreAstravia.CompleteCoreAstravia();
        Core.Logger($"Saga: Astravia - Complete");

        DarkAlliance_Story.DarkAlliance_Questline();
        Core.Logger($"Story: Dark Alliance - Complete");

        DarkAlly_Story.DarkAlly_Questline();
        Core.Logger($"Story: Dark Ally - Complete");

        SeraphicWar_Story.SeraphicWar_Questline();
        Core.Logger($"Story: Seraphic War - Complete");

        SevenCircles.CirclesWar();
        Core.Logger($"Story: Seven Circles - Complete");

        WorldSoul.WorldSoulQuests();
        Core.Logger($"Story: World Soul - Complete");

        Adv.GearStore();
        LOC.Complete13LOC(true);
        Adv.GearStore(true);
        Core.Logger($"Saga: The 13 Lords of Chaos - Complete");
        if (Core.IsMember)
        {
            DeadlyDungeon.DeadlyDungeonQuest_Line();
            Core.Logger($"Story: Deadly Dungeon - Complete");

            KillerCatacombs.KillerCatacombs_Line();
            Core.Logger($"Story: Killer Catacombs - Complete");

            PyramidofPain.PyramidofPain_Line();
            Core.Logger($"Story: Pyramid of Pain - Complete");
        }
        Core.Logger($"Saga: The 13 Lords of Chaos (Extra) - Complete");

        Bamboozle.BamboozleQuest();
        Core.Logger($"Story: Bamboozle - Complete");

        CitadelRuins.DoAll();
        Core.Logger($"Story: Citadel Ruins - Complete");

        Fiendshard_Story.Fiendshard_Questline();
        Core.Logger($"Story: Fiendshard - Complete");

        QOM.CompleteEverything();
        Core.Logger($"Saga: Queen of Monsters - Complete");

        BrightOak.doall();
        Core.Logger($"BrightOak Complete");

        LivingDungeon.LivingDungeonStory();
        Core.Logger($"LivingDungeon Complete");

        TOD.CompleteToD();
        Core.Logger($"Throne of Darkness Complete");

        Artixpointe.OmniArtifact();
        Core.Logger($"Artixpointe Complete");

        Banished.doall();
        Core.Logger($"Banished Complete");

        BattleUnder.BattleUnderAll();
        Core.Logger($"Story: BattleUnder - Complete");

        Borgars.BorgarQuests();
        Core.Logger($"Story: Borgars - Complete");

        Collection.CollectionStory();
        Core.Logger($"Story: Collection - Complete");

        DjinnGateStory.DjinnGate();
        Core.Logger($"Story: Djinn Gate - Complete");

        DoomVaultA.StoryLine();
        Core.Logger($"Story: Doom Vault B - Complete");
        DoomVaultB.StoryLine();
        Core.Logger($"Story: Doom Vault A - Complete");

        DragonFableOrigins.DragonFableOriginsAll();
        Core.Logger($"Saga: Dragon Fable Origins - Complete");

        GlaceraStory.DoAll();
        Core.Logger($"Story: Glacera - Complete");

        J6Saga.J6();
        Core.Logger($"Sage: J6 - Complete");

        PhoenixriseStory.PhoenixAll();
        Core.Logger($"Story: Phoenixrise - Complete");

        TowerOfDoom.TowerProgress();
        Core.Logger($"Story: Tower Of Doom - Complete");

        WarTraining.StoryLine();
        Core.Logger($"Story: WarTraining - Complete");

        Xans.DoAll();
        Core.Logger($"Story: Xans Lair - Complete");
    }
}
