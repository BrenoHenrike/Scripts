/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;
using System.Collections.Generic;

public class AlchemyREP
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public string OptionsStorage = "AlchemyREP";
    public List<IOption> Options = new List<IOption>()
    {
        new Option<bool>("goldMethod", "use Gold?", "Using gold (cost 7.000.000 without boosts rank 1 to 10)", false)
    };

    //Gold Voucher 500k x6 (3.000.000 Gold) => 30x Dragon Runestone => Dragon Scale & Ice Vapor x30
    //Rank 1 to 10 => 6.100.000 Gold w/out boost											 (7KK WITH THE BOT)
    //Rank 1 to 10 => 4.900.000 Gold 25% boost (Cape of Awe)								 (5KK WITH THE BOT)
    //Rank 1 to 10 => 3.100.000 Gold REP Boost											     (4KK WITH THE BOT)
    //Rank 1 to 10 => 2.500.000 Gold REP Boost + 25% boost (Cape of Awe)					 (3KK WITH THE BOT)
    //Rank 1 to 10 => 1.300.000 Gold Server REP Boost + REP Boost + 25% boost (Cape of Awe)  (2KK WITH THE BOT)

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Farm.AlchemyREP(10, Bot.Config.Get<bool>("goldMethod"));

        Core.SetOptions(false);
    }
}
