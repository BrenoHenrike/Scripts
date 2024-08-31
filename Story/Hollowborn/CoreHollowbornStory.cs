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
        NeoTower();
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

    public void NeoTower()
    {
        if (Core.isCompletedBefore(9864))
            return;

        NeoFortress();

        Story.PreLoad(this);

        #region Useable Monsters
        string[] UseableMonsters = new[]
        {
    "Dawn Vindicator Soldier ", // UseableMonsters[0],
	"Vindicator Beast", // UseableMonsters[1],
	"Vindicator Draconian", // UseableMonsters[2],
	"Vindicator Assassin", // UseableMonsters[3],
	"Assassin Shade", // UseableMonsters[4],
	"Vindicator BeastTamer", // UseableMonsters[5],
	"Vindicator Hound", // UseableMonsters[6],
	"Vindicator Crystal", // UseableMonsters[7],
	"Vindicator Priest", // UseableMonsters[8]
};
        #endregion Useable Monsters

        // 9855 | 24 Hour Graveyard Shift
        if (!Story.QuestProgression(9855))
        {
            Core.HuntMonsterQuest(9855
            , new (string? mapName, string? monsterName, ClassType classType)[] {
        ("neotower", UseableMonsters[0], ClassType.Solo) });
        }


        // 9856 | On Unwanted Wings
        if (!Story.QuestProgression(9856))
        {
            Core.HuntMonsterQuest(9856, new (string? mapName, string? monsterName, ClassType classType)[] {
        ("neotower", UseableMonsters[1], ClassType.Solo) });
        }


        // 9857 | Noble Dragonkin
        if (!Story.QuestProgression(9857))
        {
            Core.HuntMonsterQuest(9857, new (string? mapName, string? monsterName, ClassType classType)[] {
        ("neotower", UseableMonsters[2], ClassType.Solo) });
        }


        // 9858 | A Dire Horcdeal
        Story.MapItemQuest(9858, "neotower", 13581);
        Story.KillQuest(9858, "neotower", UseableMonsters[2]);


        // 9859 | Outclassin' the Assassin
        if (!Story.QuestProgression(9859))
        {
            Core.HuntMonsterQuest(9859, new (string? mapName, string? monsterName, ClassType classType)[] {
        ("neotower", UseableMonsters[3], ClassType.Solo) });
        }


        // 9860 | Mean Dog Walker
        if (!Story.QuestProgression(9860))
        {
            Core.HuntMonsterQuest(9860, new (string? mapName, string? monsterName, ClassType classType)[] {
        ("neotower", UseableMonsters[5], ClassType.Solo) });
        }


        // 9861 | Safe Keeping
        if (!Story.QuestProgression(9861))
        {
            Core.HuntMonsterQuest(9861, new (string? mapName, string? monsterName, ClassType classType)[] {
        ("neotower", UseableMonsters[1], ClassType.Solo) });
        }


        // 9862 | Seeds to be Reaped
        if (!Story.QuestProgression(9862))
        {
            Core.HuntMonsterQuest(9862, new (string? mapName, string? monsterName, ClassType classType)[] {
        ("neotower", UseableMonsters[0], ClassType.Solo) });
        }


        // 9863 | Ceremonies and Rituals
        if (!Story.QuestProgression(9863))
        {
            Core.HuntMonsterQuest(9863, new (string? mapName, string? monsterName, ClassType classType)[] {
        ("neotower", UseableMonsters[0], ClassType.Solo),
        ("neotower", UseableMonsters[1], ClassType.Solo) });
        }


        // 9864 | Selective Acknowledgement
        if (!Story.QuestProgression(9864))
        {
            Core.HuntMonsterQuest(9864, new (string? mapName, string? monsterName, ClassType classType)[] {
        ("neotower", UseableMonsters[8], ClassType.Solo) });
        }


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
