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

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(7522))
            return;

        Story.PreLoad(this);

        // Beef Up 7510
        Core.AddDrop("Slice of Cake");
        Story.KillQuest(7510, "extinction", new[] { "Lard", "Freezer Drone" });
       
        // Creamy! 7511
        Story.KillQuest(7511, "battlefowl", "Chickencow", false);
      
        // Sacchar-Imp 7512
        Story.KillQuest(7512, "freakitiki", "Sugar Imp", false);
      
        // Salty Balboa 7513
        Story.KillQuest(7513, "stalagbite", "Balboa");
       
        // Filet o' Fishwing 7514
        Story.KillQuest(7514, "pirates", "Fishwing", false);
      
        // A Potion Master 7515
        Story.MapItemQuest(7515, "arcangrove", 7370);

        // A Weird Gourmet 7516
        Story.MapItemQuest(7516, "thespan", 7371);

        // Piece of Cake 7517
        if (!Story.QuestProgression(7517))
        {
            Core.EnsureAccept(7517);
            // The Cake is NOT a lie! 7518
            Core.EnsureAccept(7518);
            Core.HuntMonster("portalmaze", "Time Wraith", "Time Fragment", 10);
            Bot.Wait.ForDrop("Slice of Cake");
            Core.EnsureComplete(7518);
            Core.EnsureComplete(7517);
        }

        // A Health Nut 7519
        Story.MapItemQuest(7519, "brightfortress", 7372);

        // The Importance Of Staying Healthy 7520
        if (!Story.QuestProgression(7520))
        {
            Core.EnsureAccept(7520);
            Core.HuntMonster("brightfortress", "Brightscythe Reaver", "Kale", 4);
            Core.HuntMonster("brightfortress", "Brightscythe Reaver", "Quinoa", 4);
            Core.EnsureComplete(7520);
        }
        // An Aspiring Burgermonger 7521
        Story.MapItemQuest(7521, "borgars", 7373);

        // Burglinster's Revenge 7522
        Story.KillQuest(7522, "borgars", "Burglinster", false);
    }
}