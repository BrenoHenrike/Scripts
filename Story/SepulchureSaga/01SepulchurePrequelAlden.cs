//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class SepulchurePrequelAlden
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
        if (Core.isCompletedBefore(6342))
        {

            Core.Logger("You have already completed SelpulchurePrequel Storyline");
            return;
        }

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Solo);

        //Who goes there? 6332
        Story.MapItemQuest(6332, "Scarsgarde", 5860);

        //The Taint Spreads 6333
        Story.MapItemQuest(6333, "Scarsgarde", 5864, 6);
        Story.KillQuest(6333, "Scarsgarde", "VenomWing");

        //Beauty Twisted 6334
        Story.KillQuest(6334, "Scarsgarde", "Garde Grif");

        //Element of Surprise 6335
        Story.MapItemQuest(6335, "Scarsgarde", 5865, 5);
        Story.KillQuest(6335, "Scarsgarde", "Tree");

        //(Take the) Watch Out 6336
        Story.KillQuest(6336, "Scarsgarde", new[] { "Garde Watch", "Garde Pikeman" });

        //False Hoods 6337
        Story.KillQuest(6337, "Scarsgarde", new[] { "Garde Watch", "Garde Watch", "Garde Watch", "Garde Watch", "Garde Watch", "Garde Watch", "Garde Watch" });

        //Pass for Real 6338
        Story.MapItemQuest(6338, "Scarsgarde", new[] { 5866, 5867 });
        Story.KillQuest(6338, "Scarsgarde", new[] { "Garde Knight", "Garde Pikeman", "Garde Knight" });

        //Hidden in Plain Sight 6339
        Story.MapItemQuest(6339, "Scarsgarde", 5868, 8);
        Story.MapItemQuest(6339, "Scarsgarde", 5869);

        //Stay Strong Keep Steady 6340
        Story.KillQuest(6340, "Scarsgarde", new[] { "Garde Knight", "Garde Pikeman" });

        //The Final Fight 6341
        Story.KillQuest(6341, "Scarsgarde", "Garde Captain");

        //Arm the Army 6342
        Story.KillQuest(6342, "Scarsgarde", new[] { "Garde Watch", "Garde Pikeman", "Garde Knight" });
    }
}