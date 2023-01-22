/*
name: null
description: null
tags: null
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
        new Option<int>("GoldBoostQuant", "Gold Boost Quant", "Input the number of The Type of Boost For the Bot to Get", 99),
        new Option<int>("ClassBoostQuant", "Class Boost Quant", "Input the number of The Type of Boost For the Bot to Get", 99),
        new Option<int>("RepBoostQuant", "Rep Boost Quant", "Input the number of The Type of Boost For the Bot to Get", 99),
        new Option<Booster>("Booster", "Choose Your Boost Type", "Completes the Quest \"Zifwin the Colorful's Quest\", For a 10minute Booster of either Gold/Exp/Rep", Booster.All),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetBoostsSelect((int)Bot.Config.Get<Booster>("Booster"), Bot.Config.Get<int>("GoldBoostQuant"), Bot.Config.Get<int>("ClassBoostQuant"), Bot.Config.Get<int>("RepBoostQuant"));

        Core.SetOptions(false);
    }

    public void GetBoostsSelect(int Booster, int GoldBoostQuant, int CLassBoostQuant, int RepBoostQuant) //used when running this script itself.
    {
        Core.AddDrop("GOLD Boost! (10 min)", "CLASS Boost! (10 min)", "REPUTATION Boost! (10 min)");

        int ItemID = (int)Bot.Config.Get<Booster>("Booster");

        Core.FarmingLogger("GOLD Boost! (10 min)", Bot.Config.Get<int>("GoldBoostQuant"));
        Core.FarmingLogger("CLASS Boost! (10 min)", Bot.Config.Get<int>("ClassBoostQuant"));
        Core.FarmingLogger("REPUTATION Boost! (10 min)", Bot.Config.Get<int>("RepBoostQuant"));

        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(6208);
        while (!Bot.ShouldExit && !Core.CheckInventory(7140, Bot.Config.Get<int>("GoldBoostQuant")) || !Core.CheckInventory(8877, Bot.Config.Get<int>("ClassBoostQuant")) || !Core.CheckInventory(8879, Bot.Config.Get<int>("RepBoostQuant")))
        {
            Core.KillMonster("nibbleon", "r10", "Left", "Dark Makai", "Moglinberries", 3, isTemp: false, log: false);
            Core.KillMonster("bloodtusk", "r4", "Left", "Trollola Plant", "Trollola Nectar", 2, isTemp: false, log: false);
            Core.KillMonster("mudluk", "r3", "Left", "*", "Nimblestem", isTemp: false, log: false);
        }
        Core.CancelRegisteredQuests();
    }

    public void GetBoosts(int Booster, int GoldBoostQuant, int CLassBoostQuant, int RepBoostQuant) //used when using this void outside the script.
    {
        //used as such: FreeBoosts.GetBoosts((int)Booster.All, 25, 25, 25);

        Core.AddDrop("GOLD Boost! (10 min)", "CLASS Boost! (10 min)", "REPUTATION Boost! (10 min)");

        int ItemID = (int)Bot.Config.Get<Booster>("Booster");

        Core.FarmingLogger("GOLD Boost! (10 min)", Bot.Config.Get<int>("GoldBoostQuant"));
        Core.FarmingLogger("CLASS Boost! (10 min)", Bot.Config.Get<int>("ClassBoostQuant"));
        Core.FarmingLogger("REPUTATION Boost! (10 min)", Bot.Config.Get<int>("RepBoostQuant"));

        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(6208);
        while (!Bot.ShouldExit && !Core.CheckInventory(7140, GoldBoostQuant) || !Core.CheckInventory(8877, CLassBoostQuant) || !Core.CheckInventory(8879, RepBoostQuant))
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
    All
};
