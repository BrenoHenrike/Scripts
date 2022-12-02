//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Extinction
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(3864))
            return;

        Story.PreLoad(this);

    //Chopped HAMM 3855
    Story.MapItemQuest(3855, "extinction", 2956, 5);
    Story.KillQuest(3855, "extinction", "Control Panel");

    //Bad Dog 3856
    Story.KillQuest(3856, "extinction", "Cyworg");

    //Pink Slime Yum! 3857
    Story.KillQuest(3857, "extinction", new[] { "Pink Slime", "Gelatinous Slime" });

    //Processed What? 3858
    Story.MapItemQuest(3858, "extinction", 2957, 5);

    //Another Locked Door 3859
    Story.KillQuest(3859, "extinction", "Slimed Drone");

    //This Factory is On Fire 3860
    Story.KillQuest(3860, "extinction", "Freezer Drone");

    //Frozen... Food? 3861
    Story.MapItemQuest(3861, "extinction", 2958, 5);

    //Fight Fire With Flammable Ingredients 3862
    Story.KillQuest(3862, "extinction", "Lard");

    //The Last Lock 3863
    Story.KillQuest(3863, "extinction", "Freezer Drone");

    //SN.O.W. Fall 3864
    Story.KillQuest(3864, "extinction", "SN.O.W.");
    }
}