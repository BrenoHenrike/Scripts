/*
name: The Refreshing Deal
description: This script farms Gems and Totems of Nulgath using "The Refreshing Deal" Quest.
tags: refreshing, deal, gem, totem, nulgath, nation, crag, bamboozle, quest
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/Various/PurifiedClaymoreOfDestiny.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class TheRefreshingDeal
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();
    public PurifiedClaymoreOfDestiny PCoD = new();

    public string OptionsStorage = "RefreshingDeal";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        new Option<int>("GemQuantity", "How many Gems of Nulgath?","Max Stack is 1000", 1000),
        new Option<int>("TotemQuantity", "How many Totems of Nulgath?","Max Stack is 100", 100),
        new Option<bool>("BankItems", "Bank nation items at the end", "true/false", false),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.SetOptions();

        Deal(Bot.Config!.Get<int>("GemQuantity"), Bot.Config.Get<int>("TotemQuantity"));

        Core.SetOptions(false);
    }

    public void Deal(int GemQuant, int TotemQuant)
    {
        if (!Core.CheckInventory(Nation.CragName))
        {
            Core.Logger($"{Nation.CragName} missing. stopping");
            return;
        }

        Nation.DragonSlayerReward(); //required
        PCoD.GetPCoD();

        Core.AddDrop(Core.QuestRewards(4777));

        if (GemQuant > 0)
        {
            Core.FarmingLogger("Gem of Nulgath", GemQuant);
            while (!Bot.ShouldExit && !Core.CheckInventory("Gem of Nulgath", GemQuant))
            {
                Core.EnsureAccept(4777);
                Core.HuntMonster("graveyard", "Big Jack Sprat", "Bone Axe", isTemp: false);
                Nation.FarmBloodGem(2);
                Nation.FarmUni10(30);
                Core.EnsureComplete(4777, 6136);
                Bot.Wait.ForPickup("Gem of Nulgath");
            }
        }


        if (TotemQuant > 0)
        {
            Core.FarmingLogger("Totem of Nulgath", TotemQuant);
            while (!Bot.ShouldExit && !Core.CheckInventory("Totem of Nulgath", TotemQuant))
            {
                Core.EnsureAccept(4777);
                Core.HuntMonster("graveyard", "Big Jack Sprat", "Bone Axe", isTemp: false);
                Nation.FarmBloodGem(2);
                Nation.FarmUni10(30);
                Core.EnsureComplete(4777, 5357);
                Bot.Wait.ForPickup("Totem of Nulgath");
            }

            if (Bot.Config!.Get<bool>("BankItems"))
                Core.ToBank(Nation.bagDrops);
        }
    }
}

