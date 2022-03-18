//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class CoreAstravia
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void CompleteCoreAstravia()
    {
        //Progress Check
        if (Core.isCompletedBefore(8395))
            return;

        //Preload Quests
        Story.PreLoad();

        Eridani();
        Astravia();
        AstraviaCastle();
        AstraviaJudgement();

    }

    public void Eridani()
    {
        //Progress Check
        if (Core.isCompletedBefore(7779))
            return;

        //Preload Quests
        Story.PreLoad();

        //Lost Breadcrumbs
        Story.KillQuest(7769, "eridani", "Maggot-Like Creature");
        Story.MapItemQuest(7769, "eridani", 7783);

        //Dirty Laundry
        Story.KillQuest(7770, "eridani", "Maggot-Like Creature");

        //Ergot Bread
        Story.KillQuest(7771, "eridani", "Rat-Like Creature");

        //Kids These Days
        Story.KillQuest(7772, "eridani", "Bat-Like Creature");

        //No Good Dead
        Story.KillQuest(7773, "eridani", "Wolf-Like Creature");

        //A Worn Book's Cover
        Story.KillQuest(7774, "eridani", new[] { "Rat-Like Creature", "Bat-Like Creature", "Maggot-Like Creature" });
        Story.MapItemQuest(7774, "eridani", 7784);

        //No Good Dead
        Story.KillQuest(7775, "eridani", "Wolf-Like Creature");
        Story.MapItemQuest(7775, "eridani", 7785);

        //The Black Goat in The Dark
        Story.KillQuest(7776, "eridani", new[] { "Creature 15", "Creature 16", "Creature 34" });

        //The Star Sinks into the Sea
        Story.KillQuest(7777, "eridani", new[] { "Creature 30", "Creature 31" });

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

        //Preload Quests
        Story.PreLoad();

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
        Story.KillQuest(7996, "astravia", "Creature 27");

        //Zugzwang
        Story.KillQuest(7997, "astravia", "Creature 28");

        //Dead Position
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(7998, "astravia", "Ti");

        //Desperado
        Story.KillQuest(7999, "astravia", "Creature 27|Creature 28");

        //Zugzwang
        Story.KillQuest(8000, "astravia", "The Moon");
    }

    //There are Bugged Monsters, so don't change any code here.
    public void AstraviaCastle()
    {
        //Progress Check
        if (Core.isCompletedBefore(8256))
            return;

        //Preload Quests
        Story.PreLoad();

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
        Story.PreLoad();

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
}