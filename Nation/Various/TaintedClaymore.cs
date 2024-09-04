/*
name: Tainted Claymore
description: This script farms the Tainted Claymore weapon.
tags: claymore, nulgath, skew, warrior, blade, enchant, poisonous deal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
public class TaintedClaymore
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        PreReq();
        GetWep();
    }

    public void PreReq()
    {
        if (Core.isCompletedBefore(324))
            return;

        Story.PreLoad(this);

        //Adorable Sisters
        Story.MapItemQuest(319, "tavern", 56, 7);

        //Warm and Furry
        Story.KillQuest(320, "pines", "Pine Grizzly");

        //Shell Rock
        Story.KillQuest(321, "pines", "Red Shell Turtle");

        //Bear Facts
        Story.KillQuest(322, "pines", "Twistedtooth");

        //The Spittoon Saloon
        Story.KillQuest(324, "pines", "Red Shell Turtle");
    }

    public void GetWep()
    {
        if (Core.CheckInventory("Tainted Claymore"))
            return;

        Core.AddDrop("Tainted Claymore");

        Farm.EvilREP(8);

        Core.EnsureAccept(121);
        Core.HuntMonster("battleundera", "Undead Berserker", "Warrior Claymore Blade", isTemp: false);
        Core.EnsureComplete(121);
    }
}
