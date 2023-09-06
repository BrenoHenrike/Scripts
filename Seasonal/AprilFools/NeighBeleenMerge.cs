/*
name: NeighBeleen Merge
description: This script farms all the materials needed for NeighBeleen Merge in magicmeaderp.
tags: seasonal, april fools, magicmeaderp, neighbeleen, merge
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal\AprilFools\MagicMeaderp.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class NeighBeleenMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private Magicmeadow MM = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Furry Ticket", "Floofy Ticket", "Fancy Ticket", "Hoppy Ticket", "Mushy Ticket", "Pwny Ticket", "Slobber Ticket" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        MM.CompleteStory();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("magicmeaderp", 2251, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Furry Ticket":
                case "Slobber Ticket":
                case "Floofy Ticket":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9185);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.GetMapItem(11466, map: "magicmeaderp");
                        Core.GetMapItem(11467, map: "magicmeaderp");
                        Core.GetMapItem(11468, map: "magicmeaderp");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Fancy Ticket":
                case "Hoppy Ticket":
                case "Mushy Ticket":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9186);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.GetMapItem(11469, map: "magicmeaderp");
                        Core.GetMapItem(11470, map: "magicmeaderp");
                        Core.GetMapItem(11471, map: "magicmeaderp");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Pwny Ticket":
                case "Bony Ticket":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9187);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.GetMapItem(11472, map: "magicmeaderp");
                        Core.GetMapItem(11473, map: "magicmeaderp");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("77251", "Derpy Bubblegum Kitteh Pet", "Mode: [select] only\nShould the bot buy \"Derpy Bubblegum Kitteh Pet\" ?", false),
        new Option<bool>("77252", "Derpy Citrus Kitteh Pet", "Mode: [select] only\nShould the bot buy \"Derpy Citrus Kitteh Pet\" ?", false),
        new Option<bool>("77253", "Derpy Lime Kitteh Pet", "Mode: [select] only\nShould the bot buy \"Derpy Lime Kitteh Pet\" ?", false),
        new Option<bool>("77254", "Derpy Icy Kitteh Pet", "Mode: [select] only\nShould the bot buy \"Derpy Icy Kitteh Pet\" ?", false),
        new Option<bool>("77255", "Derpy Berry Kitteh Pet", "Mode: [select] only\nShould the bot buy \"Derpy Berry Kitteh Pet\" ?", false),
        new Option<bool>("77256", "Derpy Prism Kitteh Pet", "Mode: [select] only\nShould the bot buy \"Derpy Prism Kitteh Pet\" ?", false),
        new Option<bool>("77257", "Derpy Bubblegum Foxx Pet", "Mode: [select] only\nShould the bot buy \"Derpy Bubblegum Foxx Pet\" ?", false),
        new Option<bool>("77258", "Derpy Citrus Foxx Pet", "Mode: [select] only\nShould the bot buy \"Derpy Citrus Foxx Pet\" ?", false),
        new Option<bool>("77259", "Derpy Lime Foxx Pet", "Mode: [select] only\nShould the bot buy \"Derpy Lime Foxx Pet\" ?", false),
        new Option<bool>("77260", "Derpy Icy Foxx Pet", "Mode: [select] only\nShould the bot buy \"Derpy Icy Foxx Pet\" ?", false),
        new Option<bool>("77261", "Derpy Berry Foxx Pet", "Mode: [select] only\nShould the bot buy \"Derpy Berry Foxx Pet\" ?", false),
        new Option<bool>("77262", "Derpy Prism Foxx Pet", "Mode: [select] only\nShould the bot buy \"Derpy Prism Foxx Pet\" ?", false),
        new Option<bool>("77263", "Derpy Bubblegum Birb Pet", "Mode: [select] only\nShould the bot buy \"Derpy Bubblegum Birb Pet\" ?", false),
        new Option<bool>("77264", "Derpy Citrus Birb Pet", "Mode: [select] only\nShould the bot buy \"Derpy Citrus Birb Pet\" ?", false),
        new Option<bool>("77265", "Derpy Lime Birb Pet", "Mode: [select] only\nShould the bot buy \"Derpy Lime Birb Pet\" ?", false),
        new Option<bool>("77266", "Derpy Icy Birb Pet", "Mode: [select] only\nShould the bot buy \"Derpy Icy Birb Pet\" ?", false),
        new Option<bool>("77267", "Derpy Berry Birb Pet", "Mode: [select] only\nShould the bot buy \"Derpy Berry Birb Pet\" ?", false),
        new Option<bool>("77268", "Derpy Prism Birb Pet", "Mode: [select] only\nShould the bot buy \"Derpy Prism Birb Pet\" ?", false),
        new Option<bool>("77269", "Derpy Bubblegum Bunbun Pet", "Mode: [select] only\nShould the bot buy \"Derpy Bubblegum Bunbun Pet\" ?", false),
        new Option<bool>("77270", "Derpy Citrus Bunbun Pet", "Mode: [select] only\nShould the bot buy \"Derpy Citrus Bunbun Pet\" ?", false),
        new Option<bool>("77271", "Derpy Lime Bunbun Pet", "Mode: [select] only\nShould the bot buy \"Derpy Lime Bunbun Pet\" ?", false),
        new Option<bool>("77272", "Derpy Icy Bunbun Pet", "Mode: [select] only\nShould the bot buy \"Derpy Icy Bunbun Pet\" ?", false),
        new Option<bool>("77273", "Derpy Berry Bunbun Pet", "Mode: [select] only\nShould the bot buy \"Derpy Berry Bunbun Pet\" ?", false),
        new Option<bool>("77274", "Derpy Prism Bunbun Pet", "Mode: [select] only\nShould the bot buy \"Derpy Prism Bunbun Pet\" ?", false),
        new Option<bool>("77275", "Derpy Bubblegum ShrOoOm Pet", "Mode: [select] only\nShould the bot buy \"Derpy Bubblegum ShrOoOm Pet\" ?", false),
        new Option<bool>("77276", "Derpy Citrus ShrOoOm Pet", "Mode: [select] only\nShould the bot buy \"Derpy Citrus ShrOoOm Pet\" ?", false),
        new Option<bool>("77277", "Derpy Lime ShrOoOm Pet", "Mode: [select] only\nShould the bot buy \"Derpy Lime ShrOoOm Pet\" ?", false),
        new Option<bool>("77278", "Derpy Icy ShrOoOm Pet", "Mode: [select] only\nShould the bot buy \"Derpy Icy ShrOoOm Pet\" ?", false),
        new Option<bool>("77279", "Derpy Berry ShrOoOm Pet", "Mode: [select] only\nShould the bot buy \"Derpy Berry ShrOoOm Pet\" ?", false),
        new Option<bool>("77280", "Derpy Prism ShrOoOm Pet", "Mode: [select] only\nShould the bot buy \"Derpy Prism ShrOoOm Pet\" ?", false),
        new Option<bool>("77345", "Derpy Bubblegum Pupper Pet", "Mode: [select] only\nShould the bot buy \"Derpy Bubblegum Pupper Pet\" ?", false),
        new Option<bool>("77346", "Derpy Citrus Pupper Pet", "Mode: [select] only\nShould the bot buy \"Derpy Citrus Pupper Pet\" ?", false),
        new Option<bool>("77347", "Derpy Lime Pupper Pet", "Mode: [select] only\nShould the bot buy \"Derpy Lime Pupper Pet\" ?", false),
        new Option<bool>("77348", "Derpy Icy Pupper Pet", "Mode: [select] only\nShould the bot buy \"Derpy Icy Pupper Pet\" ?", false),
        new Option<bool>("77349", "Derpy Berry Pupper Pet", "Mode: [select] only\nShould the bot buy \"Derpy Berry Pupper Pet\" ?", false),
        new Option<bool>("77350", "Derpy Prism Pupper Pet", "Mode: [select] only\nShould the bot buy \"Derpy Prism Pupper Pet\" ?", false),
        new Option<bool>("77281", "Derpy Anger Birb Pet", "Mode: [select] only\nShould the bot buy \"Derpy Anger Birb Pet\" ?", false),
        new Option<bool>("77282", "Derpy Blepper Kitteh Pet", "Mode: [select] only\nShould the bot buy \"Derpy Blepper Kitteh Pet\" ?", false),
        new Option<bool>("77288", "Derpy Smol Floofer Pet", "Mode: [select] only\nShould the bot buy \"Derpy Smol Floofer Pet\" ?", false),
        new Option<bool>("77289", "Derpy Heckin Woofer Pet", "Mode: [select] only\nShould the bot buy \"Derpy Heckin Woofer Pet\" ?", false),
        new Option<bool>("77290", "Hurrpa Bubblegum Kitteh Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Bubblegum Kitteh Cape\" ?", false),
        new Option<bool>("77291", "Hurrpa Citrus Kitteh Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Citrus Kitteh Cape\" ?", false),
        new Option<bool>("77292", "Hurrpa Lime Kitteh Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Lime Kitteh Cape\" ?", false),
        new Option<bool>("77293", "Hurrpa Icy Kitteh Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Icy Kitteh Cape\" ?", false),
        new Option<bool>("77294", "Hurrpa Berry Kitteh Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Berry Kitteh Cape\" ?", false),
        new Option<bool>("77295", "Hurrpa Prism Kitteh Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Prism Kitteh Cape\" ?", false),
        new Option<bool>("77296", "Hurrpa Bubblegum Foxx Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Bubblegum Foxx Cape\" ?", false),
        new Option<bool>("77297", "Hurrpa Citrus Foxx Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Citrus Foxx Cape\" ?", false),
        new Option<bool>("77298", "Hurrpa Lime Foxx Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Lime Foxx Cape\" ?", false),
        new Option<bool>("77299", "Hurrpa Icy Foxx Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Icy Foxx Cape\" ?", false),
        new Option<bool>("77300", "Hurrpa Berry Foxx Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Berry Foxx Cape\" ?", false),
        new Option<bool>("77301", "Hurrpa Prism Foxx Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Prism Foxx Cape\" ?", false),
        new Option<bool>("77302", "Hurrpa Bubblegum Birb Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Bubblegum Birb Cape\" ?", false),
        new Option<bool>("77303", "Hurrpa Citrus Birb Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Citrus Birb Cape\" ?", false),
        new Option<bool>("77304", "Hurrpa Lime Birb Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Lime Birb Cape\" ?", false),
        new Option<bool>("77305", "Hurrpa Icy Birb Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Icy Birb Cape\" ?", false),
        new Option<bool>("77306", "Hurrpa Berry Birb Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Berry Birb Cape\" ?", false),
        new Option<bool>("77307", "Hurrpa Prism Birb Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Prism Birb Cape\" ?", false),
        new Option<bool>("77308", "Hurrpa Bubblegum Bunbun Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Bubblegum Bunbun Cape\" ?", false),
        new Option<bool>("77309", "Hurrpa Citrus Bunbun Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Citrus Bunbun Cape\" ?", false),
        new Option<bool>("77310", "Hurrpa Lime Bunbun Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Lime Bunbun Cape\" ?", false),
        new Option<bool>("77311", "Hurrpa Icy Bunbun Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Icy Bunbun Cape\" ?", false),
        new Option<bool>("77312", "Hurrpa Berry Bunbun Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Berry Bunbun Cape\" ?", false),
        new Option<bool>("77313", "Hurrpa Prism Bunbun Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Prism Bunbun Cape\" ?", false),
        new Option<bool>("77314", "Hurrpa Bubblegum ShrOoOm Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Bubblegum ShrOoOm Cape\" ?", false),
        new Option<bool>("77315", "Hurrpa Citrus ShrOoOm Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Citrus ShrOoOm Cape\" ?", false),
        new Option<bool>("77316", "Hurrpa Lime ShrOoOm Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Lime ShrOoOm Cape\" ?", false),
        new Option<bool>("77317", "Hurrpa Icy ShrOoOm Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Icy ShrOoOm Cape\" ?", false),
        new Option<bool>("77318", "Hurrpa Berry ShrOoOm Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Berry ShrOoOm Cape\" ?", false),
        new Option<bool>("77319", "Hurrpa Prism ShrOoOm Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Prism ShrOoOm Cape\" ?", false),
        new Option<bool>("77351", "Hurrpa Bubblegum Pupper Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Bubblegum Pupper Cape\" ?", false),
        new Option<bool>("77352", "Hurrpa Citrus Pupper Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Citrus Pupper Cape\" ?", false),
        new Option<bool>("77353", "Hurrpa Lime Pupper Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Lime Pupper Cape\" ?", false),
        new Option<bool>("77354", "Hurrpa Icy Pupper Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Icy Pupper Cape\" ?", false),
        new Option<bool>("77355", "Hurrpa Berry Pupper Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Berry Pupper Cape\" ?", false),
        new Option<bool>("77356", "Hurrpa Prism Pupper Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Prism Pupper Cape\" ?", false),
        new Option<bool>("77320", "Hurrpa Anger Birb Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Anger Birb Cape\" ?", false),
        new Option<bool>("77321", "Hurrpa Blepper Kitteh Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Blepper Kitteh Cape\" ?", false),
        new Option<bool>("77327", "Hurrpa Smol Floofer Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Smol Floofer Cape\" ?", false),
        new Option<bool>("77328", "Hurrpa Heckin Woofer Cape", "Mode: [select] only\nShould the bot buy \"Hurrpa Heckin Woofer Cape\" ?", false),
    };
}
