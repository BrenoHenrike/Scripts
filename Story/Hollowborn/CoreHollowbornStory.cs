/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CoreHollowbornStory
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
        ShadowslayerD();
        Shadowrealm();
    }

    public void Trygve()
    {
        if (Core.isCompletedBefore(8298))
            return;

        Story.PreLoad(this);

        //Wunjo, Reversed (8289)
        Story.KillQuest(8289, "trygve", "Vindicator Recruit");

        //Berkana (8290)
        Story.KillQuest(8290, "trygve", "Blood Eagle");
        Story.MapItemQuest(8290, "trygve", 9036);

        //Algiz, Reversed (8291)
        Story.KillQuest(8291, "trygve", "Vindicator Recruit");
        Story.MapItemQuest(8291, "trygve", 9037);

        //Gebo (8292)
        Story.KillQuest(8292, "trygve", "Rune Boar");
        Story.MapItemQuest(8292, "trygve", 9038);

        //Eihwaz (8293)
        Story.MapItemQuest(8293, "trygve", 9039, 3);
        Story.KillQuest(8293, "trygve", "Vindicator Recruit");

        //Hagalaz (8294)
        Story.MapItemQuest(8294, "trygve", 9040, 8);

        //Mannaz (8295)
        Story.KillQuest(8295, "trygve", new[] { "Rune Boar", "Blood Eagle" });

        //Thurisaz (8296)
        Story.KillQuest(8296, "trygve", "Vindicator Recruit");

        //Othala (8297)
        Story.KillQuest(8297, "trygve", new[] { "Blood Eagle", "Vindicator Recruit" });

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

    public void ShadowslayerD()
    {
        if (Core.isCompletedBefore(9489))
            return;

        Story.PreLoad(this);

        // Remnant of Will 9487
        Story.KillQuest(9487, "hbchallenge", "Sentient Hollow");
        // Stake Your Life 9488
        Story.KillQuest(9488, "hbchallenge", "Hollowborn Vampire");
        // Hollow Howl 9489     
        Story.KillQuest(9489, "hbchallenge", "Hollowborn Lycan");
    }

    public void Shadowrealm()
    {

        if (Core.isCompletedBefore(9793))
            return;

        Story.PreLoad(this);

        // The First Crumb (9783)
        Story.KillQuest(9783, "bonecastle", "Undead Guard");

        // Return to Lifeblood (9784)
        Story.KillQuest(9784, "lycan", "Sanguine");

        // Nohairatu (9785)
        Story.KillQuest(9785, "umbral", "Rapaxi");

        // Bony Hodgepodge (9786)
        Story.KillQuest(9786, "battleundera", "Bone Terror");

        // Red Risk (9787)
        if (!Story.QuestProgression(9787))
        {
            Core.EnsureAccept(9787);
            Core.KillMonster("shadowrise", "r15", "Left", "Infernal Warrior", "Marred Armor Piece");
            Core.EnsureComplete(9787);
        }
        // Story.KillQuest(9787, "shadowrise", "Infernal Warrior");

        // Knuckle Levity (9788)
        if (!Story.QuestProgression(9788))
        {
            Core.EquipClass(ClassType.Solo);
            Story.KillQuest(9788, "dagerecruit", "Nuckelavee");
        }

        // Grudge Ghost (9790)
        if (!Story.QuestProgression(9790))
        {
            Core.EquipClass(ClassType.Solo);
            Story.KillQuest(9790, "darkally", "Underfiend");
        }

        // Inverted Expectation (9791)
        if (!Story.QuestProgression(9791))
        {
            Core.EquipClass(ClassType.Solo);
            Story.KillQuest(9791, "yasaris", "Avatar of Anubyx");
        }

        // Die Angry (9792)
        if (!Story.QuestProgression(9792))
        {
            Core.EquipClass(ClassType.Solo);
            Story.KillQuest(9792, "wrath", "Gorgorath");
        }

        // Treasure Death (9793)
        Story.MapItemQuest(9793, "greed", 13314);
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
