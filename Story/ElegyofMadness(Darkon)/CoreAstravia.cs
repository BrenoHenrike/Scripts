//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CoreAstravia
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void CompleteCoreAstravia()
    {
        Eridani();
        Astravia();
        AstraviaCastle();
        AstraviaJudgement();
        EridaniPast();
        AstraviaPast();
        FirstObservatory();
        GenesisGarden();
        TheWorld();
    }

    public void Eridani()
    {
        //Progress Check
        if (Core.isCompletedBefore(7779))
            return;

        //Preload Quests
        Story.PreLoad(this);

        Core.Join("Garden");
        Bot.Sleep(2000);

        //Lost Breadcrumbs
        Story.KillQuest(7769, "eridani", "Maggot-Like Creature");
        Story.MapItemQuest(7769, "eridani", 7783);

        //Dirty Laundry
        if (!Story.QuestProgression(7770))
        {
            Core.EnsureAccept(7770);
            Core.KillMonster("eridani", "Enter", "Spawn", "Maggot-Like Creature", "Dress Scraps", 5);
            Core.EnsureComplete(7770);
        }

        //Ergot Bread
        if (!Story.QuestProgression(7771))
        {
            Core.EnsureAccept(7771);
            Core.KillMonster("eridani", "r10", "Right", "Rat-Like Creature", "Rats Exterminated", 5);
            Core.EnsureComplete(7771);
        }

        //Kids These Days
        if (!Story.QuestProgression(7772))
        {
            Core.EnsureAccept(7772);
            Core.KillMonster("eridani", "r10", "Right", "Bat-Like Creature", "Meat Cuts", 5);
            Core.EnsureComplete(7772);
        }

        //No Good Dead
        Story.KillQuest(7773, "eridani", "Wolf-Like Creature");

        //A Worn Book's Cover
        if (!Story.QuestProgression(7774))
        {
            Core.EnsureAccept(7774);
            Core.HuntMonsterMapID("eridani", 22|3, "Rat-Like Creature Slain");
            Core.HuntMonsterMapID("eridani", 13|24|29, "Bat-Like Creature Slain");
            Core.HuntMonsterMapID("eridani", 1, "Maggot-Like Creatures Slain", 2);
            Story.MapItemQuest(7774, "eridani", 7784);
        }

        //No Good Dead
        Story.KillQuest(7775, "eridani", "Wolf-Like Creature");
        Story.MapItemQuest(7775, "eridani", 7785);

        //The Black Goat in The Dark
        if (!Story.QuestProgression(7776))
        {
            Bot.Options.AttackWithoutTarget = true;
            Core.EnsureAccept(7776);
            Core.HuntMonsterMapID("eridani", 8, "Creature 15 Hunted");
            Core.HuntMonsterMapID("eridani", 11, "Creature 16 Hunted");
            Core.HuntMonsterMapID("eridani", 5, "Creature 34 Hunted", 7);
            Core.EnsureComplete(7776);
            Bot.Options.AttackWithoutTarget = false;
        }

        //The Star Sinks into the Sea
        if (!Story.QuestProgression(7777))
        {
            Bot.Options.AttackWithoutTarget = true;
            Core.EnsureAccept(7777);
            Core.HuntMonsterMapID("eridani", 17, "Creature 30 Hunted");
            Core.HuntMonsterMapID("eridani", 2, "Creature 31 Hunted");
            Core.EnsureComplete(7777);
            Bot.Options.AttackWithoutTarget = false;
        }

        //The Gentlest Truth
        Story.KillQuest(7778, "eridani", "Door");
        Story.MapItemQuest(7778, "eridani", 7786);

        //Time Moves the Moon
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(7779, "eridani", "Creature 6");
    }

    public void Astravia()
    {
        //Progress Check
        if (Core.isCompletedBefore(8000))
            return;

        Eridani();

        //Preload Quests
        Story.PreLoad(this);

        //Caro-Kann Defense
        Story.KillQuest(7993, "astravia", "Monstrous Dove");

        //Materialism
        Story.KillQuest(7994, "astravia", "Drago's Spy");

        //A Castled King
        Story.KillQuest(7995, "astravia", "Astravian Mercenary");
        Story.MapItemQuest(7995, "astravia", 8264);
        Story.MapItemQuest(7995, "astravia", 8265);
        Story.MapItemQuest(7995, "astravia", 8266);

        //Principle of Weakness
        if (!Story.QuestProgression(7996))
        {
            Core.EnsureAccept(7996);
            Core.KillMonster("astravia", "r6", "Bottom", "Creature 27", "Creature 27 Slain", 8);
            Core.EnsureComplete(7996);
        }

        //Zugzwang
        Story.KillQuest(7997, "astravia", "Creature 28");

        //Dead Position
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(7998, "astravia", "Ti");

        //Desperado
        Story.KillQuest(7999, "astravia", "Creature 27");

        //Zugzwang
        Story.KillQuest(8000, "astravia", "The Moon");
    }

    //There are Bugged Monsters, so don't change any code here.
    public void AstraviaCastle()
    {
        //Progress Check
        if (Core.isCompletedBefore(8256))
            return;

        Astravia();

        //Preload Quests
        Story.PreLoad(this);

        //Terpsichore, the Chained
        if (!Core.isCompletedBefore(8247))
        {
            Core.EnsureAccept(8247);
            Core.KillMonster("astraviacastle", "r3", "Left", "*", "Key Pieces", 9);
            Core.EnsureComplete(8247);
        }

        //Calliope, the Arrogant
        if (!Core.isCompletedBefore(8248))
        {
            Core.EnsureAccept(8248);
            Core.KillMonster("astraviacastle", "r3", "Left", "*", "Creatures Shoo'd", 6);
            Story.MapItemQuest(8248, "astraviacastle", 8891, 6);
        }


        //Euterpe, the Spiteful
        Story.KillQuest(8249, "astraviacastle", "Creature 20");
        Story.MapItemQuest(8249, "astraviacastle", 8892, 6);

        //Urania, the Origin
        if (!Core.isCompletedBefore(8250))
        {
            Core.EnsureAccept(8250);
            Core.KillMonster("astraviacastle", "r3", "Left", "*", "Creatures Repelled", 9);
            Story.MapItemQuest(8250, "astraviacastle", 8893);
        }

        //Polyhumnia, the Forgotten
        if (!Core.isCompletedBefore(8251))
        {
            Core.EnsureAccept(8251);
            Core.KillMonster("astraviacastle", "r3", "Left", "*", "Creatures Defeated", 6);
            Story.MapItemQuest(8251, "astraviacastle", 8894);
        }

        //Clio, the Liar
        Story.KillQuest(8252, "astraviacastle", "Creature 20");

        //Melpomene, the Inevitable
        Story.KillQuest(8253, "astraviacastle", "Astravian Royal Guard");

        //Erato, the Inconsolable
        Story.KillQuest(8254, "astraviacastle", "Storage Spider");
        Story.MapItemQuest(8254, "astraviacastle", 8895, 3);

        //Thalia, the Truth
        Story.MapItemQuest(8255, "astraviacastle", 8896);

        //Mnemosyne, the Tormentor
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(8256, "astraviacastle", "The Sun");
    }

    public void AstraviaJudgement()
    {
        //Progress Check
        if (Core.isCompletedBefore(8395))
            return;

        //Preload Quests
        Story.PreLoad(this);

        //Atata
        Story.MapItemQuest(8386, "astraviajudge", 9275, 6);

        //Samghata
        Story.KillQuest(8387, "astraviajudge", "Hand");

        //Hahava
        Story.KillQuest(8388, "astraviajudge", "Trumpeter");

        //Sanjiva
        if (!Core.isCompletedBefore(8389))
        {
            Core.EnsureAccept(8389);
            Core.KillMonster("astraviajudge", "r4", "Left", "Juror", "Jurors Repelled", 9);
            Core.EnsureComplete(8389);
        }


        //Kalasutra
        Story.KillQuest(8390, "astraviajudge", "Trumpeter");
        Story.MapItemQuest(8390, "astraviajudge", 9276);

        //Padma
        Story.KillQuest(8391, "astraviajudge", "Hand");
        Story.MapItemQuest(8391, "astraviajudge", 9277, 8);

        //Maharaurava
        Story.KillQuest(8392, "astraviajudge", "Trumpeter");

        //Raurava
        if (!Core.isCompletedBefore(8393))
        {
            Core.EnsureAccept(8393);
            Core.KillMonster("astraviajudge", "r4", "Left", "Juror", "Jurors Shoved", 9);
            Core.EnsureComplete(8393);
        }

        //Avici
        Story.KillQuest(8394, "astraviajudge", "Shades");
        Story.MapItemQuest(8394, "astraviajudge", 9278);

        //Mahapadma
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(8395, "astraviajudge", "La");
    }

    public void EridaniPast()
    {
        //Progress Check
        if (Core.isCompletedBefore(8530))
            return;

        AstraviaJudgement();

        //Preload Quests
        Story.PreLoad(this);

        //Dorian
        Story.MapItemQuest(8521, "eridanipast", 9681);

        //Hypophrygian
        Story.KillQuest(8522, "eridanipast", new[] { "Bandit", "Dog" });

        //Ionian
        Story.MapItemQuest(8523, "eridanipast", 9676, 8);

        //Lydian
        Story.KillQuest(8524, "eridanipast", "Bandit");

        //Mixolydian
        Story.KillQuest(8525, "eridanipast", "Bat");
        Story.MapItemQuest(8525, "eridanipast", 9677);

        //Hypomixolydian
        Story.MapItemQuest(8526, "eridanipast", 9678);
        Story.MapItemQuest(8526, "eridanipast", 9679);

        //Locrian
        Story.KillQuest(8527, "eridanipast", new[] { "Bat", "Dog" });

        //Aeolian
        Story.KillQuest(8528, "eridanipast", "Bandit");

        //Troubadour
        if (!Core.isCompletedBefore(8529))
        {
            Core.EnsureAccept(8529);
            Core.Join("eridanipast");
            Core.Jump("r10", "Left");
            Bot.Kill.Monster("*");
            Core.EnsureComplete(8529);
        }

        //Echoes
        if (!Story.QuestProgression(8530))
        {
            Core.EnsureAccept(8530);
            Story.MapItemQuest(8530, "eridanipast", 9680);
            Core.HuntMonster("eridanipast", "Bandit", "Bandit Remnants", 7);
            Core.EnsureComplete(8530);
        }
    }

    public void AstraviaPast()
    {
        //Progress Check
        if (Core.isCompletedBefore(8601))
            return;

        EridaniPast();

        //Preload Quests
        Story.PreLoad(this);

        //Fantaisie-Impromptu
        Story.MapItemQuest(8592, "astraviapast", 10017);
        Story.MapItemQuest(8592, "astraviapast", 10018);

        //Odile
        Story.KillQuest(8593, "astraviapast", "Astravian Soldier");

        //Revolutionary Etude
        Story.MapItemQuest(8594, "astraviapast", 10019, 7);

        //Tannhauser
        Story.KillQuest(8595, "astraviapast", "Panicked Citizen");

        //Tzigane
        Story.KillQuest(8596, "astraviapast", "Astravian Soldier");
        Story.MapItemQuest(8596, "astraviapast", 10020);

        //Ride of the Valkyrie
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(8597, "astraviapast", "Regulus");

        //Wedding March
        Story.KillQuest(8598, "astraviapast", "Titania");

        //From the New World
        Story.KillQuest(8599, "astraviapast", "Aurola");

        //Mov.VII Aquarium
        Story.MapItemQuest(8600, "astraviapast", 10021);

        //Liebestraume No. 3
        Story.KillQuest(8601, "astraviapast", "Forsaken Husk");
    }

    public void FirstObservatory()
    {
        //Progress Check
        if (Core.isCompletedBefore(8641))
            return;

        //Preload Quests
        Story.PreLoad(this);

        //Aries Tammuz
        Story.MapItemQuest(8630, "FirstObservatory", 10083);

        //Scorpio
        Story.KillQuest(8631, "FirstObservatory", "Astra Scorpio");

        //Leo
        Story.KillQuest(8632, "FirstObservatory", "Astra Leo");

        //Lugal-irra Meslamta-ea
        Story.MapItemQuest(8633, "FirstObservatory", 10084);
        Story.KillQuest(8633, "FirstObservatory", "Ancient Turret");

        //Virgo Shala
        Story.MapItemQuest(8634, "FirstObservatory", 10085);

        //Sagittarius Pabilsag
        Story.KillQuest(8635, "FirstObservatory", "Ancient Creature");

        //Libra Shamash
        Story.MapItemQuest(8636, "FirstObservatory", new[] { 10086, 10087 });

        //Auriga
        Story.KillQuest(8637, "FirstObservatory", new[] { "Ancient Creature", "Ancient Turret" });

        //Pisces Alrescha
        Story.MapItemQuest(8638, "FirstObservatory", new[] { 10088, 10089 });

        //Pisces Alpherg
        Story.MapItemQuest(8639, "FirstObservatory", 10090);

        //Pisces Ishtar
        Story.KillQuest(8640, "FirstObservatory", "Empress’ Finger");

        //Taurus Gugalanna
        Story.KillQuest(8641, "FirstObservatory", new[] { "Ancient Creature", "Ancient Turret", "Empress’ Finger" });
    }

    public void GenesisGarden()
    {
        if (Core.isCompletedBefore(8687))
            return;

        FirstObservatory();

        Story.PreLoad(this);

        //The Fool 8678
        Story.MapItemQuest(8678, "genesisgarden", 10196, 6);

        //The Magician 8679
        Story.KillQuest(8679, "genesisgarden", "Drago's Soldier");

        //The High Priestess 8680
        Story.MapItemQuest(8680, "genesisgarden", MapItemIDs: new[] { 10197, 10198, 10199 });

        //The Empress, Reversed 8681
        Story.KillQuest(8681, "genesisgarden", "Drago's Soldier");

        //The Emperor 8682
        Story.MapItemQuest(8682, "genesisgarden", MapItemIDs: new[] { 10200, 10201 });

        //The Hierophant 8683
        Story.MapItemQuest(8683, "genesisgarden", 10202, 5);

        //The Lovers, Reversed 8684
        Story.KillQuest(8684, "genesisgarden", new[] { "Ancient Creature", "Plant Beast" });

        //The Chariot 8685
        Story.MapItemQuest(8685, "genesisgarden", 10203);
        Story.KillQuest(8685, "genesisgarden", "Ancient Turret");

        //Strength 8686
        Story.KillQuest(8686, "genesisgarden", "Undead Humanoid");

        //The Hermit 8687
        Story.KillQuest(8687, "genesisgarden", "Ancient Mecha");
    }

    public void TheWorld()
    {
        if (Core.isCompletedBefore(8733))
            return;

        GenesisGarden();

        Story.PreLoad(this);

        //8723|Justice, Reversed
        Story.MapItemQuest(8723, "theworld", 10289);

        //8724|The Hanged Man, Reversed
        Story.KillQuest(8724, "theworld", "Nothingness");

        //8725|Death
        Story.KillQuest(8725, "theworld", "Re");

        //8726|Temperance, Reversed
        Story.KillQuest(8726, "theworld", "Fa");

        //8727|Temptation, Reversed
        Story.KillQuest(8727, "theworld", "Ti");

        //8728|The Tower
        Story.KillQuest(8728, "theworld", "So");

        //8729|The Star
        Story.KillQuest(8729, "theworld", "Nothingness");

        //8730|The Moon, Reversed
        Story.KillQuest(8730, "theworld", "So");

        //8731|The Sun, Reversed
        Story.KillQuest(8731, "theworld", "So");

        //8732|Judgement
        Story.KillQuest(8732, "theworld", "Darkon");

        //8733|The World
        Story.KillQuest(8733, "theworld", "Encore Darkon");
    }
}