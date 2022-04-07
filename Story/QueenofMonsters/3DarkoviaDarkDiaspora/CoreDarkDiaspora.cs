using RBot;

public class CoreDarkDiaspora
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreFarms Farm = new CoreFarms();

    public void CompleteDarkDiaspora()
    {
        //Progress Check
        if (Core.isCompletedBefore(5416))
            return;

        //Preload Quests
        Story.PreLoad();

        DarkoviaInvasion();
        SafiriaInvasion();
        LycanInvasion();
        SafiriaInvasion2();

    }

    public void DarkoviaInvasion()
    {

        //Progress Check
        if (Core.isCompletedBefore(5503))
            return;

        //Preload Quests
        Story.PreLoad();

        //Hounds and Infernals and Imp, Oh my!
        Story.KillQuest(5487, "DarkoviaInvasion", new[] { "Underworld Hound", "Infernal Imp" });

        //Like Imps in A Pod
        Story.MapItemQuest(5488, "DarkoviaInvasion", 4905, 6);

        //A Grievous Threat
        Story.KillQuest(5489, "DarkoviaInvasion", "Grievous Fiend");

        //Undead Investigation
        Story.MapItemQuest(5490, "SafiriaInvasion", 4904);
    }

    public void SafiriaInvasion()
    {

        //Progress Check
        if (Core.isCompletedBefore(5496))
            return;

        //Preload Quests
        Story.PreLoad();

        //What We Need Is A Big Can Of Raid
        Story.KillQuest(5491, "SafiriaInvasion", new[] { "Fallen Knight", "Infernal Knight" });

        //Trapped!
        Story.MapItemQuest(5492, "SafiriaInvasion", 4895, 6);
        Story.MapItemQuest(5492, "SafiriaInvasion", 4896);

        //Won't Someone Think Of The Minions?
        Story.MapItemQuest(5493, "SafiriaInvasion", 4897, 9);

        //Here, Doggy Doggy
        Story.KillQuest(5494, "SafiriaInvasion", "Blood Maggot");
        Story.MapItemQuest(5494, "SafiriaInvasion", 4898);
        Story.MapItemQuest(5494, "SafiriaInvasion", 4899);

        //Ma'alech
        Story.KillQuest(5495, "SafiriaInvasion", "Ma'alech");

        //I'm Not Lycan This Situation
        Story.MapItemQuest(5496, "LycanInvasion", 4900);
    }

    public void LycanInvasion()
    {

        //Progress Check
        if (Core.isCompletedBefore(5500))
            return;

        //Preload Quests
        Story.PreLoad();

        //The Best Way To Slay An Infernal
        Story.KillQuest(5497, "LycanInvasion", new[] { "Fallen Knight", "Infernal Knight" });

        //A Dire Situation
        Story.KillQuest(5498, "LycanInvasion", "Dire Wolf|Hulking Dire Wolf");

        //I'd Lycan To Go Now
        Story.MapItemQuest(5499, "LycanInvasion", 4901);
        Story.MapItemQuest(5499, "LycanInvasion", 4903, 6);

        //Lord Balax'el
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(5500, "LycanInvasion", "Lord Balax'el");
    }

    public void SafiriaInvasion2()
    {

        //Progress Check
        if (Core.isCompletedBefore(5503))
            return;

        //Preload Quests
        Story.PreLoad();

        //Follow Lady Solani
        Story.KillQuest(5501, "SafiriaInvasion", "Fallen Knight|Infernal Knight");
        Story.MapItemQuest(5501, "SafiriaInvasion", 4902);

        //Revenant Slayer
        Story.KillQuest(5502, "SafiriaInvasion", new[] { "Revenant", "Shadow Imp" });

        //Noddharath
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(5503, "SafiriaInvasion", "Noddharath");
    }
}