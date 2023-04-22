/*
name: Free Boosts [10mns]
description: Farms Free Boosts from "Reagents for Zifwin" (cp/gold/rep)
tags: reagents for zifwin, free, boost, class points, reputation, gold
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class FreeBoosts
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public string OptionsStorage = "Booster";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<int>("GoldBoostQuant", "Gold Boost Quant", "Input the number of The Type of Boost For the Bot to Get [Max 9,999]", 99),
        new Option<int>("ClassBoostQuant", "Class Boost Quant", "Input the number of The Type of Boost For the Bot to Get [Max 9,999]", 99),
        new Option<int>("RepBoostQuant", "Rep Boost Quant", "Input the number of The Type of Boost For the Bot to Get [Max 9,999]", 99),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetBoostsSelect(Bot.Config.Get<int>("GoldBoostQuant"), Bot.Config.Get<int>("ClassBoostQuant"), Bot.Config.Get<int>("RepBoostQuant"));

        Core.SetOptions(false);
    }

    public void GetBoostsSelect(int GoldBoostQuant, int CLassBoostQuant, int RepBoostQuant) //used when running this script itself.
    {
        Core.AddDrop("GOLD Boost! (10 min)", "CLASS Boost! (10 min)", "REPUTATION Boost! (10 min)");

        Core.Logger("Drops are \"randomly\" recieved, and may take awhile... be prepared if quants are high.");

        Core.FarmingLogger("GOLD Boost! (10 min)", GoldBoostQuant);
        Core.FarmingLogger("CLASS Boost! (10 min)", CLassBoostQuant);
        Core.FarmingLogger("REPUTATION Boost! (10 min)", RepBoostQuant);

        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(6208);
        while (!Bot.ShouldExit && !Core.CheckInventory("GOLD Boost! (10 min)", GoldBoostQuant) || !Core.CheckInventory("CLASS Boost! (10 min)", CLassBoostQuant) || !Core.CheckInventory("REPUTATION Boost! (10 min)", RepBoostQuant))
        {
            Core.KillMonster("nibbleon", "r10", "Left", "Dark Makai", "Moglinberries", 3, isTemp: false, log: false);
            Core.KillMonster("bloodtusk", "r4", "Left", "Trollola Plant", "Trollola Nectar", 2, isTemp: false, log: false);
            Core.KillMonster("mudluk", "r3", "Left", "*", "Nimblestem", isTemp: false, log: false);
        }
        Core.CancelRegisteredQuests();
    }
}

public enum Booster
{
    Gold = 7140,
    Class = 8877,
    REPUTATION = 8879,
};
