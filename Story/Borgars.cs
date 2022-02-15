//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs

//cs_include Scripts/CoreFile(Or folder)/Filename.cs

using RBot;

public class Borgars //you can rename this anything you want it will be the "Class" you refference elsewhere
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();


    public void ScriptMain(ScriptInterface bot) //DO NOT RENAME THIS
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(7522))
            return;

        Core.AddDrop("Slice of Cake");

        //P1 - START
        //Map: Yulgar
        // Beef Up
        Core.KillQuest(7510, "extinction", new[] { "Lard", "Freezer Drone" });
        // Creamy!
        Core.KillQuest(7511, "battlefowl", "Chickencow");
        // Sacchar-Imp
        Core.KillQuest(7512, "freakitiki", "Sugar Imp");
        // Salty Balboa
        Core.KillQuest(7513, "stalagbite", "Balboa");
        // Filet o' Fishwing
        Core.KillQuest(7514, "pirates", "Fishwing");
        // A Potion Master
        Core.MapItemQuest(7515, "Arcangrove", 7370);

        //P2 - Requirements: Must have completed the 'A Potion Master' quest.
        //Map: arcangrove
        // A Weird Gourmet - 7516
        Core.MapItemQuest(7516, "thespan", 7371);

        //Part.Getcake - Requirements: Must have completed the 'A Weird Gourmet' quest.
        //Map: portalmaze
        // The Cake is NOT a lie! -- not sure how else todo this
        if (!Core.QuestProgression(7517))
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
        Core.MapItemQuest(7519, "brightfortress", 7372);

        //P5 - Requirements: Must have completed the 'A Health Nut' quest.
        //Map: brightfortress
        // The Importance Of Staying Healthy
        if (!Core.QuestProgression(7520))
        {
            Core.EnsureAccept(7520);
            Core.HuntMonster("brightfortress", "Brightscythe Reaver", "Kale", 4);
            Core.HuntMonster("brightfortress", "Brightscythe Reaver", "Quinoa", 4);
            Core.EnsureComplete(7520);
        }
        // An Aspiring Burgermonger
        Core.MapItemQuest(7521, "borgars", 7373);

        //P6 - Requirements: Must have completed the 'An Aspiring Burgermonger' quest.
        //Map:
        // Burglinster's Revenge
        Core.KillQuest(7522, "borgars", "Burglinster");
    }
}