/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs

using Skua.Core.Interfaces;

public class UnderGroundLab
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        partofundergroundlabb();

        Core.SetOptions(false);
    }

    public void partofundergroundlabb()
    {
        if (Core.isCompletedBefore(3157))
            return;

        Story.PreLoad(this);
        Core.EquipClass(ClassType.Farm);

        Core.AddDrop("Unicorn Essence");

        // Hunt for the Brutal Intruder
        Story.MapItemQuest(3148, "undergroundlab", new[] { 2144, 2145, 2146, 2147, 2148 });

        // Brutal Detection Desired
        Story.MapItemQuest(3149, "undergroundlab", new[] { 2150, 2151 });
        Story.MapItemQuest(3149, "undergroundlabb", new[] { 2152, 2153 });

        // Silence the Green Screamers
        Story.KillQuest(3150, "undergroundlabb", "Green Screamer");

        // Soundbooth of Horrors
        Story.KillQuest(3151, "undergroundlabb", "Soundbooth Horror");

        // Skeletons in the Server Closet
        Story.KillQuest(3152, "undergroundlabb", "Closet Skeleton");

        // Server Gremlags
        Story.KillQuest(3153, "undergroundlabb", "Server Gremlin");

        // Window Slayer
        Story.KillQuest(3154, "undergroundlabb", "Window");

        // Gamer Fuel
        Story.KillQuest(3155, "undergroundlabb", new[] { "Invisible Ninjas", "Invisible Ninjas", "Invisible Ninjas", "Invisible Ninjas" });

        // Key to Doom
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(3156, "undergroundlabb", "Rabid Server Hamster");

        // UltraBrutal Battle
        Story.KillQuest(3157, "undergroundlabb", "Ultra Brutalcorn");
    }
}
