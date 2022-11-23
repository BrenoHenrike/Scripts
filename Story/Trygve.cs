//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Trygve
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
        TrygveStory();
        ShadowrRealm();
    }
    public void TrygveStory()
    {
        if (Core.isCompletedBefore(8298))
            return;

        Story.PreLoad(this);

        //Wunjo, Reversed (8289)
        Story.KillQuest(8289, "trygve", "Vindicator Recruit|Vindicator Soldier");
        //Berkana (8290)
        Story.KillQuest(8290, "trygve", "Blood Eagle");
        Story.MapItemQuest(8290, "trygve", 9036);
        //Algiz, Reversed (8291)
        Story.KillQuest(8291, "trygve", "Vindicator Recruit|Vindicator Soldier");
        Story.MapItemQuest(8291, "trygve", 9037);
        //Gebo (8292)
        Story.KillQuest(8292, "trygve", "Rune Boar");
        Story.MapItemQuest(8292, "trygve", 9038);
        //Eihwaz (8293)
        Story.MapItemQuest(8293, "trygve", 9039, 3);
        Story.KillQuest(8293, "trygve", "Vindicator Recruit|Vindicator Soldier");
        //Hagalaz (8294)
        Story.MapItemQuest(8294, "trygve", 9040, 8);
        //Mannaz (8295)
        Story.KillQuest(8295, "trygve", new[] { "Rune Boar", "Blood Eagle" });
        //Thurisaz (8296)
        Story.KillQuest(8296, "trygve", "Vindicator Recruit|Vindicator Soldier");
        //Othala (8297)
        Story.KillQuest(8297, "trygve", new[] { "Blood Eagle|Rune Boar", "Vindicator Recruit|Vindicator Soldier" });
        //Isa, Reversed (8298)
        Story.KillQuest(8298, "trygve", "Gramiel");

    }

    public void ShadowrRealm()
    {
        if (Core.isCompletedBefore(3182))
            return;

        Story.PreLoad(this);

        // [[[Shadowrealm]]] un related quest but releted to hollowborn

        //Key to the ShadowLord 3182
        if (!Story.QuestProgression(3182))
        {
            Core.EnsureAccept(3182);
            Core.HuntMonster("shadowrealmpast", "Pure Shadowscythe", "Source of Luminance", 50, false);
            Core.EnsureComplete(3182);
        }
    }

}