//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/OrbHunt.cs
using Skua.Core.Interfaces;

public class QueenBattle
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public OrbHunt OrbHunt = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        OrbHunt.OrbHuntSaga();

        if (Core.isCompletedBefore(8361))
            return;

        Story.PreLoad(this);

        //Forces of Chaorruption 8350
        Story.KillQuest(8350, "QueenBattle", "Chaorruption");

        //Phantom Chaos 8351
        Story.KillQuest(8351, "QueenBattle", "Chaos Ghost");

        //Spies of the Queen 8352
        Story.KillQuest(8352, "QueenBattle", "Chaos Sp-eye");

        //Portal of Chaos 8353
        Story.MapItemQuest(8353, "QueenBattle", 9204);
        Story.KillQuest(8353, "QueenBattle", "Chaos Ghost");

        //Slain Children 8354
        Story.KillQuest(8354, "QueenBattle", new[] { "Kolyaban Shade", "Horothotep Shade", "Sa-Laatan Shade", "Grou'luu Shade", "Extriki Shade" });

        //Dragons Felled By Chaos 8355
        Story.KillQuest(8355, "QueenBattle", "Chaos Dracolich");

        //The Queen's Generals 8356
        Story.KillQuest(8356, "QueenBattle", "Chaos General");

        //Portal of Chaos II 8357
        Story.MapItemQuest(8357, "QueenBattle", 9205);
        Story.KillQuest(8357, "QueenBattle", "Chaos General");

        //Guilt of the Past 8358
        Story.KillQuest(8358, "QueenBattle", "Chaotic Guilt");

        //A Giant of Chaos 8359
        Story.KillQuest(8359, "QueenBattle", "Chaos Giant");

        //The First Champion of Chaos 8360
        Story.KillQuest(8360, "QueenBattle", "Proto Chaos Champion");

        //The Queen of Monsters 8361
        Story.KillQuest(8361, "QueenBattle", "Queen of Monsters");
    }
}
