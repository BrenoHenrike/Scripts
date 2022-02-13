//cs_include Scripts/CoreBots.cs

using RBot;

public class SevenCircles
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        CirclesWar();

        Core.SetOptions(false);
    }

    public void Circles()
    {
        if (Core.isCompletedBefore(7978))
            return;

        Core.AddDrop("Indulgence");

        //Canto IV
        Core.KillQuest(7968, "sevencircles", "Limbo Guard");
        //Canto V
        Core.KillQuest(7969, "sevencircles", "Luxuria Guard");
        //Gone With the Wind
        Core.KillQuest(7970, "sevencircles", new[] { "Limbo Guard|Luxuria Guard", "Luxuria Guard", "Limbo Guard" });
        //Lest Ye Be Destroyed
        Core.KillQuest(7971, "sevencircles", "Luxuria");
        //Canto VI
        Core.MapItemQuest(7972, "sevencircles", 8206, 3);
        //HeckHound
        Core.KillQuest(7973, "sevencircles", "Gluttony Guard");
        //Glutton  for Punishment
        Core.KillQuest(7974, "sevencircles", "Gluttony");
        //Canto VII
        Core.KillQuest(7975, "sevencircles", "Avarice Guard");
        //Greed the Room
        Core.KillQuest(7976, "sevencircles", new[] { "Limbo Guard", "Luxuria Guard", "Gluttony Guard", "Avarice Guard" });
        //Ava-risky Business
        Core.KillQuest(7977, "sevencircles", "Avarice");
        //Cirlces of Fate
        Core.KillQuest(7978, "sevencircles", new[] { "Luxuria", "Gluttony", "Avarice", "Limbo Guard" });
    }

    public void CirclesWar()
    {
        if (Core.isCompletedBefore(7990))
            return;

        Circles();

        Core.AddDrop("Essence of Treachery", "Essence of Violence", "Souls of Heresy", "Essence of Wrath");

        //Guards of Wrath
        Core.KillQuest(7979, "sevencircleswar", "Wrath Guard");
        //War Medals
        Core.KillQuest(7980, "sevencircleswar", "Wrath Guard");
        //Mega War Medals
        Core.KillQuest(7981, "sevencircleswar", "Wrath Guard");
        //Wrath Against the Machine
        Core.KillQuest(7982, "sevencircleswar", "Wrath");
        //Blasphemy? Blasphe-you!
        Core.KillQuest(7983, "sevencircleswar", "Heresy Guard");
        //Violence's Gatekeeper
        Core.KillQuest(7984, "sevencircleswar", "Violence's Gatekeeper");
        //Meaningless Violence
        Core.KillQuest(7985, "sevencircleswar", "Violence Guard");
        //Geryon, Not Gary On!
        Core.KillQuest(7986, "sevencircleswar", "Geryon");
        //Violence
        Core.KillQuest(7987, "sevencircleswar", "Violence");
        //Where the Trea-sun Don't Shine
        Core.KillQuest(7988, "sevencircleswar", "Treachery Guard");
        //Hanged for Treason
        Core.KillQuest(7989, "sevencircleswar", "Treachery");
        //The Beast
        Core.KillQuest(7990, "sevencircleswar", "The Beast");
    }
}