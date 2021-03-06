//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class Table
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions(); 
        
        DoAll();

        Core.SetOptions(false);
    }
    public void DoAll()
    {
        if (Core.isCompletedBefore(737))
        {
            Core.Logger("Table storyline has already been completed.");
            return;
        }

        Story.PreLoad();

        //Property Appraiser (717)
        Story.MapItemQuest(717, "giant", 119);
        //Pain in the Grass (718)
        Story.KillQuest(718, "giant", "Red Ant");
        //Dust Busting (719)
        Story.KillQuest(719, "giant", "Dust Bunny");
        //Cat-astrophe (720)
        Story.KillQuest(720, "giant", "Giant Cat");
        //Skynner's List (721)
        Story.KillQuest(721, "smuurvil", "Smuurvil");
        //A Mushy Situation (722)
        Story.MapItemQuest(722, "smuurvil", 122, 12);
        //W-Tea F (723)
        Story.KillQuest(723, "smuurvil", "Smuurvil|Smuurvilette");
        //A Skunkweed By Any Other Name... (724)
        Story.KillQuest(724, "smuurvil", "Smuurvilette");
        //There Is No Spoon (725)
        Story.KillQuest(725, "smuurvil", "Papa Smuurvil");
        //Spare Parts (737)
        if(!Story.QuestProgression(737))
        {
            Core.EnsureAccept(737);
            for (int i = 123; i <= 128; i++)
                Core.GetMapItem(i, map: "table");
            Core.EnsureComplete(737);
        }

    }
}
