/*
name: Farms Prismatic Seams
description: Farms the Prismatic Seams from Shadows of War
tags: Prismatic Seams, shadows of war
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/ShadowsOfWar/CoreSoWMats.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;

public class UnboundThread
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreSoWMats SOWM = new CoreSoWMats();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.Add("Prismatic Seams");

        Core.SetOptions();

        SOWM.PrismaticSeams();

        Core.SetOptions(false);
    }
}
