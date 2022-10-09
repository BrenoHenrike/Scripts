//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class ThatStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(7179))
            return;

        Story.PreLoad(this);

        //Yeti Witual 7167
        Story.MapItemQuest(7167, "that", 6790, 8);
        Story.KillQuest(7167, "that", "Candy Goblin");

        //Get the Fur 7168
        Story.KillQuest(7168, "that", "Moglinster");

        //Vapor Needed 7169
        Story.KillQuest(7169, "that", "Mystcroft Ghost");

        //Trap the Flame 7170
        Story.KillQuest(7170, "that", "Will o' The Wisp");

        //Down the Well 7171
        Story.MapItemQuest(7171, "that", 6791, 1);

        //Cweepie-cwawlie Time 7172
        Story.KillQuest(7172, "that", new[] { "Well Spider", "Congealed Fear" });

        //Gwoop Destruction 7173
        Story.KillQuest(7173, "that", "Congealed Fear");

        //Seawch for Bubble 7174
        Story.MapItemQuest(7174, "that", 6792, 6);
        Story.MapItemQuest(7174, "that", 6793, 1);

        //Free Bubble! 7175
        Story.KillQuest(7175, "that", "Congealed Fear");
        Story.MapItemQuest(7175, "that", 6794, 1);
        
        //Echo... echo... echo 7176
        Story.KillQuest(7176, "that", "Echo of That");

        //Gather Hope 7177
        Story.KillQuest(7177, "that", "Shattered Hope");

        //Bweak the Blacklights 7178
        Story.KillQuest(7178, "that", "Blacklights");

        //Get Wid of THAT 7179
        Story.KillQuest(7179, "that", "That");
    }
}
