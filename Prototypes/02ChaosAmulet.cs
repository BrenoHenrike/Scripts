//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;
public class ChaosAmulet
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(7688))
        {
            Core.Logger("You have already completed this storyline");
            return;
        }
            

        Story.PreLoad();

        //Shadow Medals 7685
        Story.KillQuest(7685, "chaosamulet", "Shadowflame Berserker");

        //Mega Shadow Medals 7686
        Story.KillQuest(7686, "chaosamulet", "Shadowflame Berserker");

        //Defeat Goldun 7687
        Story.KillQuest(7687, "chaosamulet", "Goldun");

        //Goldun Wants Revenge 7688
        Story.KillQuest(7688, "chaosamulet", new[] { "Goldun", "Shadowflame Berserker" });

    }
}
