//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class ZorbasPalace
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        ZorbasPalaceStory();

        Core.SetOptions(false);
    }

    public void ZorbasPalaceStory()
    {
        if (Core.isCompletedBefore(7484))
            return;

        //7474 | Get me a Drink
        Story.KillQuest(7474, "zorbaspalace", "Cactus Creeper");

        //7475 | MonkeyButt Coffee
        Story.KillQuest(7475, "zorbaspalace", "Sand Monkey");

        //7476 | Flyer Time
        Story.MapItemQuest(7476, "zorbaspalace", 7301, 10);

        //7477 | Stop the Enforcers
        Story.KillQuest(7477, "zorbaspalace", "Palace Enforcer");

        //7478 | New Clothes
        Story.KillQuest(7478, "zorbaspalace", "Cactus Creeper");

        //7479 | Get a Guard
        Story.KillQuest(7479, "zorbaspalace", "Thwompcat");

        //7480 | Pickpocket the Pickpockets
        Story.KillQuest(7480, "zorbaspalace", "Sand Monkey");

        //7481 | Get more Enforcers
        Story.KillQuest(7481, "zorbaspalace", "Palace Enforcer");

        //7482 | Find Memet
        Story.MapItemQuest(7476, "zorbaspalace", 7304);

        //7483 | Get the Lem-or
        Story.KillQuest(7481, "zorbaspalace", "Lem-or");

        //7484 | Kick Zorba's Butt!
        Story.KillQuest(7481, "zorbaspalace", "Zorba the Bakk");
    }
}
