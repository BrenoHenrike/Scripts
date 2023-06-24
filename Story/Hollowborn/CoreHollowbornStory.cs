/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class HollowbornStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void DoAll()
    {
        Trygve();
        NeoFortress();
    }

    public void Trygve()
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

    public void NeoFortress()
    {
        if (Core.isCompletedBefore(9290))
            return;
            
        Trygve();

        Story.PreLoad(this);

        //Watch the Light 9281
        Story.MapItemQuest(9281, "neofortress", 11806, 9);

        //Uprootment Recruitment 9282
        Story.KillQuest(9282, "neofortress", "Vindicator Recruit");

        //Endless Hounding 9283
        Story.KillQuest(9283, "neofortress", "Vindicator Hound");

        //Mystery Creature 9284
        Story.KillQuest(9284, "neofortress", "Vindicator Beast");

        //Retrieve the Keys 9285
        Story.KillQuest(9285, "neofortress", "Vindicator Soldier");

        //Free the Prisoners 9286
        Story.MapItemQuest(9286, "neofortress", 11807, 5);

        //Vindicator General 9287
        Story.KillQuest(9287, "neofortress", "Vindicator General");

        //De-dicated 9288
        Story.KillQuest(9288, "neofortress", "Vindicator Recruit");

        //Get into the Chambers 9289
        Story.KillQuest(9289, "neofortress", "Vindicator General");

        //Tales from the Past 9290
        Story.MapItemQuest(9290, "neofortress", 11808);
    }


    // I'll put it here in case it turns out its needed
    // public void ShadowrRealm()
    // {
    //     if (Core.isCompletedBefore(3182))
    //         return;

    //     Story.PreLoad(this);

    //     [[[Shadowrealm]]] un related quest but releted to hollowborn

    //     Key to the ShadowLord 3182
    //     if (!Story.QuestProgression(3182))
    //     {
    //         Core.EnsureAccept(3182);
    //         Core.HuntMonster("shadowrealmpast", "Pure Shadowscythe", "Source of Luminance", 50, false);
    //         Core.EnsureComplete(3182);
    //     }
    // }
}
