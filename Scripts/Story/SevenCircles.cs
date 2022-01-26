//cs_include Scripts/CoreBots.cs
using RBot;

public class SevenCircles
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
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
        if (!Bot.Quests.IsUnlocked(7973))
        {

            Core.EnsureAccept(7972);
            Core.GetMapItem(8206, 3, "sevencircles");
            Core.EnsureComplete(7972);
        }

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
        Core.KillQuest(7978, "sevencircles", "Luxuria", hasFollowup: false);
        Core.KillQuest(7978, "sevencircles", "Gluttony ", hasFollowup: false);
        Core.KillQuest(7978, "sevencircles", "Avarice", hasFollowup: false);
        Core.KillQuest(7978, "sevencircles", "Limbo Guard", hasFollowup: false);
    }
}