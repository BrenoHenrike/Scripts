//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class SepulchurePrequelLynaria
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
        if (Core.isCompletedBefore(6353))
        {

            Core.Logger("You have already completed SelpulchurePrequel Storyline");
            return;
        }

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Solo);

        //Who goes there? 6343
        Story.MapItemQuest(6343, "Scarsgarde", 5861);

        //The Taint Spreads 6344
        Story.MapItemQuest(6344, "Scarsgarde", 5864, 6);
        Story.KillQuest(6344, "Scarsgarde", "VenomWing");

        //Beauty Twisted 6345
        Story.KillQuest(6345, "Scarsgarde", "Garde Grif");

        //Element of Surprise 6346
        Story.MapItemQuest(6346, "Scarsgarde", 5865, 5);
        Story.KillQuest(6346, "Scarsgarde", "Tree");

        //(Take the) Watch Out 6347
        Story.KillQuest(6347, "Scarsgarde", new[] { "Garde Watch", "Garde Pikeman" });

        //False Hoods 6348
        Story.KillQuest(6348, "Scarsgarde", new[] { "Garde Watch", "Garde Watch", "Garde Watch", "Garde Watch", "Garde Watch", "Garde Watch", "Garde Watch" });

        //Pass for Real 6349
        Story.MapItemQuest(6349, "Scarsgarde", new[] { 5866, 5867 });
        Story.KillQuest(6349, "Scarsgarde", new[] { "Garde Knight", "Garde Pikeman", "Garde Knight" });

        //Hidden in Plain Sight 6350
        Story.MapItemQuest(6350, "Scarsgarde", 5868, 8);
        Story.MapItemQuest(6350, "Scarsgarde", 5869);

        //Stay Strong Keep Steady 6351
        Story.KillQuest(6351, "Scarsgarde", new[] { "Garde Knight", "Garde Pikeman" });

        //The Final Fight 6352
        Story.KillQuest(6352, "Scarsgarde", "Garde Captain");

        //Arm the Army 6353
        Story.KillQuest(6353, "Scarsgarde", new[] { "Garde Watch", "Garde Pikeman", "Garde Knight" });
    }
}