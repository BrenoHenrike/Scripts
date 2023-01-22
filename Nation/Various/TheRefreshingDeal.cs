/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation\Various\PurifiedClaymoreOfDestiny.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class TheRefreshingDeal
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();
    public PurifiedClaymoreOfDestiny PCoD = new();

    public string OptionsStorage = "RefreshingDeal";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<QuestReward>("RewardChoice", "Choose Your Reward", "Gems or Totems)", QuestReward.None),
        new Option<bool>("BankItems", "Bank nation items at the end", "true/false", false),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.SetOptions();

        Deal();

        Core.SetOptions(false);
    }

    public void Deal(string item = null, int quant = 1)
    {
        if (!Core.CheckInventory(Nation.CragName))
        {
            Core.Logger($"{Nation.CragName} missing. stopping");
            return;
        }

        Nation.DragonSlayerReward(); //required
        PCoD.GetPCoD();

        Core.AddDrop("Gem of Nulgath", "Totem of Nulgath");
        Core.Logger($"Mode Selected: {Bot.Config.Get<QuestReward>("RewardChoice").ToString()} ");
        while (!Core.CheckInventory(item, quant))
        {
            Core.EnsureAccept(4777);
            Core.HuntMonster("graveyard", "Big Jack Sprat", "Bone Axe", isTemp: false);
            Nation.FarmBloodGem(2);
            Nation.FarmUni10(30);

            if (Bot.Config.Get<QuestReward>("RewardChoice") == QuestReward.Both)
            {
                if (!Bot.Inventory.IsMaxStack("Gem of Nulgath"))
                    Core.EnsureComplete(4777, 6136);
                else if (!Bot.Inventory.IsMaxStack("Totem of Nulgath"))
                    Core.EnsureComplete(4777, 5357);
                Bot.Wait.ForPickup(item);
            }
            else if (Bot.Config.Get<QuestReward>("RewardChoice") == QuestReward.Gems)
                Core.EnsureComplete(4777, 6136);
            else if (Bot.Config.Get<QuestReward>("RewardChoice") == QuestReward.Totems)
                Core.EnsureComplete(4777, 5357);
            Bot.Wait.ForPickup(item);
        }
        Core.CancelRegisteredQuests();
        Core.ToBank(Nation.bagDrops);
    }

    private enum QuestReward
    {
        Gems,
        Totems,
        Both,
        None
    }
}
