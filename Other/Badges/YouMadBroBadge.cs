/*
name: YouMadBroBadge
description: Does the alcemy sh...t till it gets the badge
tags: alchemy, you mad bro, rep, badge
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class YouMadBroBadge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public string OptionsStorage = "YouMadBro";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
{
    new Option<bool>("Use Gold", "Use Gold?", "Buy farming mats?", true),
    CoreBots.Instance.SkipOptions,
};

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Dragon Runestone", "Ice Vapor" });
        Core.SetOptions();

        // Pass the "Use Gold" option to Badge method
        Badge(Bot.Config!.Get<bool>("Use Gold"));

        Core.SetOptions(false);
    }

    public void Badge(bool useGold)
    {
        // Check if the option is already set
        if (Core.HasWebBadge("UseGold"))
        {
            Core.Logger("Already have the Use Gold badge");
            return;
        }

        // Use the passed boolean parameter
        bool useGoldOption = useGold;

        Farm.AlchemyREP();
        Core.EquipClass(ClassType.Farm);
        while (!Bot.ShouldExit && !Core.HasWebBadge("UseGold"))
        {
            Core.AddDrop("Ice Vapor");
            Core.AddDrop(11475); //dragon scale (2 items items have this name hence the id)

            if (!useGoldOption)
            {
                Core.EquipClass(ClassType.Farm);
                Core.FarmingLogger("Dragon Scale", 30);
                while (!Core.CheckInventory(11475, 30))
                    Core.KillMonster("lair", "Hole", "Center", "*", isTemp: false, log: false);
                Core.KillMonster("lair", "Enter", "Spawn", "*", "Ice Vapor", 30, isTemp: false);
            }
            else
            {
                Farm.DragonRunestone(11);
                Core.BuyItem("alchemy", 397, 11475, 10, shopItemID: 1232);
                Core.BuyItem("alchemy", 397, 11478, 10, shopItemID: 1235);
            }

            //to make sure it always has 1 DRS
            Farm.AlchemyPacket("Dragon Scale", "Ice Vapor", trait: CoreFarms.AlchemyTraits.hOu);
        }
        Core.TrashCan("Dragon Scale", "Ice Vapor");
        Core.ToBank("Dragon Runestone", "Gold Voucher 100k");
    }

    // private string[] PotionsToSell = {"Life Potion", "Basic Crusader Elixir", "Basic Barrier Potion", "Basic Crusader Elixir", "Divine Elixir", "Barrier Potion", "Basic Barrier Potion", "Basic Divine Elixir", "Crusader Elixir"};
    private readonly string badge = "You mad bro?";
}