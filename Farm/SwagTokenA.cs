/*
name: Swag Token A
description: Farms Swag Token As with mem and non-mem methods
tags: swag token a, swag token b, swag token c, swag token d
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
public class SwagTokenA
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Super-Fan Swag Token A", "Super-Fan Swag Token B", "Super-Fan Swag Token C", "Super-Fan Swag Token D" });
        Core.SetOptions();

        GetToken();
        Core.SetOptions(false);
    }

    public void GetToken()
    {
        if (Core.IsMember)
            Farm.SwagTokenA();
        else Farm.SwagTokenAF2p(); 
    }
}
