/*
name: Bonebreaker Merge
description: This bot will farm the items belonging to the selected mode for the Bonebreaker Merge [1051] in /bonebreak
tags: bonebreaker, merge, bonebreak, outcast, boneshattering, brokenbone, skull, bonepiercer, Membership
*/
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BonebreakerMergeMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreDailies Daily = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "BoneBreaker Medallion"});
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.IsMember)
        {
            Core.Logger("Membership Required.");
            return;
        }
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("bonebreak", 1051, findIngredients, buyOnlyThis, buyMode: buyMode);

        #region Dont edit this part
        void findIngredients()
        {
            ItemBase req = Adv.externalItem;
            int quant = Adv.externalQuant;
            int currentQuant = req.Temp ? Bot.TempInv.GetQuantity(req.Name) : Bot.Inventory.GetQuantity(req.Name);
            if (req == null)
            {
                Core.Logger("req is NULL");
                return;
            }

            switch (req.Name)
            {
                default:
                    bool shouldStop = Adv.matsOnly ? !dontStopMissingIng : true;
                    Core.Logger($"The bot hasn't been taught how to get {req.Name}." + (shouldStop ? " Please report the issue." : " Skipping"), messageBox: shouldStop, stopBot: shouldStop);
                    break;
                #endregion

                case "BoneBreaker Medallion":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant) && Daily.CheckDaily(3898))
                    {
                        Core.EnsureAccept(3898);
                        Core.HuntMonster("bonebreaker", "Undead Berserker", "Warrior Defeated", 5);
                        Cor.EnsureComplete(3898);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.ToBank(Core.QuestRewards(3898));
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("27272", "BoneBreaker Outcast", "Mode: [select] only\nShould the bot buy \"BoneBreaker Outcast\" ?", false),
        new Option<bool>("27215", "Axe of Bone-Shattering", "Mode: [select] only\nShould the bot buy \"Axe of Bone-Shattering\" ?", false),
        new Option<bool>("27213", "BrokenBone Skull", "Mode: [select] only\nShould the bot buy \"BrokenBone Skull\" ?", false),
        new Option<bool>("27214", "BonePiercer Spikes", "Mode: [select] only\nShould the bot buy \"BonePiercer Spikes\" ?", false),
    };
}
