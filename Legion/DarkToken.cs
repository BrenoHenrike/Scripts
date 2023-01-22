/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Story/Legion/WorldSoul.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class DarkToken
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreLegion Legion = new CoreLegion();
    public WorldSoul WS = new WorldSoul();
    public CoreAdvanced Adv = new CoreAdvanced();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        FarmDarkToken();

        Core.SetOptions(false);
    }

    public void FarmDarkToken()
    {

        WS.WorldSoulQuests();
        Legion.DarkToken();
    }
}
