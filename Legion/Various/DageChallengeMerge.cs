/*
name: Dage Challenge Merge
description: This bot will farm the items belonging to the selected mode for the Dage Challenge Merge [2118] in /dage
tags: dage, challenge, merge, dage, avarice, legion, legions, scarf, scythe, luxuria, eye, virgil, vital, exanima
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
//cs_include Scripts/Legion/HeadOfTheLegionBeast.cs
//cs_include Scripts/Story/Legion/DageChallengeStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DageChallengeMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private CoreStory Story = new();
    private CoreLegion Legion = new();
    private SevenCircles Circles = new();
    private HeadoftheLegionBeast HOTLB = new();
    private DageChallengeStory DageChallenge = new();
    private static CoreAdvanced sAdv = new();


    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Underworld Laurel", "Underworld Medal", "Underworld Accolade", "Avarice of the Legion's Skull", "Avarice of the Legion's Hood", "Dage the Evil Insignia" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Legion.SoulForgeHammer();
        Circles.CirclesWar(); //Required to turnin & accept the SoH quests
        DageChallenge.DageChallengeQuests(); //to unlock mergeshop

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("dage", 2118, findIngredients, buyOnlyThis, buyMode: buyMode);

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
                    bool shouldStop = !Adv.matsOnly || !dontStopMissingIng;
                    Core.Logger($"The bot hasn't been taught how to get {req.Name}." + (shouldStop ? " Please report the issue." : " Skipping"), messageBox: shouldStop, stopBot: shouldStop);
                    break;
                #endregion

                case "Underworld Laurel":
                    while (!Bot.ShouldExit && !Core.CheckInventory("Underworld Laurel", quant))
                    {
                        Core.EnsureAccept(8544);
                        Core.HuntMonster("Dage", "Dage the Evil", "Dage Dueled");
                        Core.EnsureComplete(8544);
                        Bot.Wait.ForPickup("Underworld Laurel");
                    }
                    break;

                case "Underworld Medal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory("Underworld Medal", quant))
                    {
                        Core.EnsureAccept(8545);
                        Legion.ApprovalAndFavor(0, 200);
                        Legion.ObsidianRock();
                        HOTLB.SoulsHeresy(30);
                        Core.EnsureComplete(8545);
                        Bot.Wait.ForPickup("Underworld Medal");
                    }
                    break;

                case "Underworld Accolade":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(8546);
                        Core.HuntMonster("legionarena", "legion fiend rider", "Fiend Rider's Approval");
                        Core.HuntMonster("frozenlair", "lich lord", "Lich Lord's Approval");
                        Core.HuntMonster("dagefortress", "Grrrberus", "Grrrberus's Grr Grrr");
                        Core.EnsureComplete(8546);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Avarice of the Legion's Hood":
                case "Avarice of the Legion's Skull":
                    Core.AddDrop("Avarice of the Legion's Hood", "Avarice of the Legion's Skull");
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("dage", "Dage the Evil", req.Name, quant, req.Temp);
                    break;

                case "Dage the Evil Insignia":
                    if (!Core.CheckInventory(req.Name, quant))
                        Core.Logger($"Player does not have required amount of insignias [x{quant}]", stopBot: true);
                    Core.Logger($"Insignias [x{quant}] found, continueing");
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("68433", "Avarice of the Legion", "Mode: [select] only\nShould the bot buy \"Avarice of the Legion\" ?", false),
        new Option<bool>("68434", "Avarice of the Legion's Helm", "Mode: [select] only\nShould the bot buy \"Avarice of the Legion's Helm\" ?", false),
        new Option<bool>("68437", "Avarice of the Legion's Scarf", "Mode: [select] only\nShould the bot buy \"Avarice of the Legion's Scarf\" ?", false),
        new Option<bool>("68438", "Avarice of the Legion's Scythe", "Mode: [select] only\nShould the bot buy \"Avarice of the Legion's Scythe\" ?", false),
        new Option<bool>("68440", "Luxuria of the Legion", "Mode: [select] only\nShould the bot buy \"Luxuria of the Legion\" ?", false),
        new Option<bool>("68442", "Eye of Luxuria Runes", "Mode: [select] only\nShould the bot buy \"Eye of Luxuria Runes\" ?", false),
        new Option<bool>("68443", "Virgil of the Legion", "Mode: [select] only\nShould the bot buy \"Virgil of the Legion\" ?", false),
        new Option<bool>("68444", "Virgil of the Legion's Helm", "Mode: [select] only\nShould the bot buy \"Virgil of the Legion's Helm\" ?", false),
        new Option<bool>("68445", "Virgil of the Legion's Cape", "Mode: [select] only\nShould the bot buy \"Virgil of the Legion's Cape\" ?", false),
        new Option<bool>("68446", "Virgil of the Legion's Staff", "Mode: [select] only\nShould the bot buy \"Virgil of the Legion's Staff\" ?", false),
        new Option<bool>("73357", "Vital Exanima", "Mode: [select] only\nShould the bot buy \"Vital Exanima\" ?", false),
    };
}
