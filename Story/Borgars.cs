//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Borgars
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BorgarQuests();

        Core.SetOptions(false);
    }

    public void BorgarQuests()
    {
        if (Core.isCompletedBefore(7522))
            return;

        Story.PreLoad(this);

        Core.AddDrop("Slice of Cake");

        //P1 - START
        //Map: Yulgar
        // Beef Up
        Story.KillQuest(7510, "extinction", new[] { "Lard", "Freezer Drone" });
        // Creamy!
        Story.KillQuest(7511, "battlefowl", "Chickencow", false);
        // Sacchar-Imp
        Story.KillQuest(7512, "freakitiki", "Sugar Imp", false);
        // Salty Balboa
        Story.KillQuest(7513, "stalagbite", "Balboa");
        // Filet o' Fishwing
        Story.KillQuest(7514, "pirates", "Fishwing", false);
        // A Potion Master
        Story.MapItemQuest(7515, "Arcangrove", 7370);

        //P2 - Requirements: Must have completed the 'A Potion Master' quest.
        //Map: arcangrove
        // A Weird Gourmet - 7516
        Story.MapItemQuest(7516, "thespan", 7371);

        //Part.Getcake - Requirements: Must have completed the 'A Weird Gourmet' quest.
        //Map: portalmaze
        // The Cake is NOT a lie! -- not sure how else todo this
        if (!Story.QuestProgression(7517))
        {
            Core.EnsureAccept(7517);
            Core.EnsureAccept(7518);
            Core.HuntMonster("portalmaze", "Time Wraith", "Time Fragment", 10);
            Bot.Wait.ForDrop("Slice of Cake");
            Core.EnsureComplete(7518);
            Core.EnsureComplete(7517);
        }


        //P3 - Requirements: Must have completed the 'A Weird Gourmet' quest.
        //Map: thespan
        // Piece of Cake - 7517

        // A Health Nut
        Story.MapItemQuest(7519, "brightfortress", 7372);

        //P5 - Requirements: Must have completed the 'A Health Nut' quest.
        //Map: brightfortress
        // The Importance Of Staying Healthy
        if (!Story.QuestProgression(7520))
        {
            Core.EnsureAccept(7520);
            Core.HuntMonster("brightfortress", "Brightscythe Reaver", "Kale", 4);
            Core.HuntMonster("brightfortress", "Brightscythe Reaver", "Quinoa", 4);
            Core.EnsureComplete(7520);
        }
        // An Aspiring Burgermonger
        Story.MapItemQuest(7521, "borgars", 7373);

        //P6 - Requirements: Must have completed the 'An Aspiring Burgermonger' quest.
        //Map:
        // Burglinster's Revenge
        Story.KillQuest(7522, "borgars", "Burglinster", false);
    }
}