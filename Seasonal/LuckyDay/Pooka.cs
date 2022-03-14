//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class PookaStory
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        CompletePooka();

        Core.SetOptions(false);
    }

    public void CompletePooka()
    {
        //Progress Check
        if (Core.isCompletedBefore(7962))
            return;

        //Needed AddDrop
        Core.AddDrop("Amethyst Faerie Wings");
        
        //Luck Boost
        Story.KillQuest(7959, "pooka", "Sneevilchaun");

        //Safe Space
        Story.KillQuest(7960, "pooka", "Lucky Treeant");

        //Tricksy Gold
        Story.KillQuest(7961, "pooka", "Faerie");

        //You Feel Lucky?
        Story.KillQuest(7962, "pooka", "Pooka");
    }
}