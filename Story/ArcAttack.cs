//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class ArcAttack
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        StoryLine();
        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(1169))
            return;

        Story.PreLoad(this);

        //CastleMania: Disharmony and Despair 1155
        Story.MapItemQuest(1155, "lab", 488);

        //Stringing Your Enemies Along 1156
        Story.KillQuest(1156, "lab", "Ant Giant|Giant Scorpion");

        //Out of Tune(ing Knobs) 1157
        Story.MapItemQuest(1157, "lab", 489, 3);
        Story.KillQuest(1157, "lab", "Ant Giant");

        //Code of Conduct-or 1158
        Story.KillQuest(1158, "lab", "Ant Giant|Giant Scorpion");

        //Sending Out an SOS to the World 1159
        Story.KillQuest(1159, "lab", "Ant Giant|Giant Scorpion");

        //This is FINAL ZAP! 1160
        Story.MapItemQuest(1160, "lab", 490, 6);

        //Wire You Doing This Again? 1161
        Story.MapItemQuest(1161, "lab", 491, 7);
        Story.KillQuest(1161, "lab", "Giant Scorpion");

        //Die, All of You! 1162
        Story.KillQuest(1162, "lab", "Ant Giant|Giant Scorpion");

        //Ample Amps Required 1163
        Story.MapItemQuest(1163, "lab", 492, 10);

        //Upward Over the Mountain 1164
        Story.MapItemQuest(1164, "mountain", 493);

        //Charging Up! 1169
        Story.MapItemQuest(1169, "mountain", 494);
    }
}
