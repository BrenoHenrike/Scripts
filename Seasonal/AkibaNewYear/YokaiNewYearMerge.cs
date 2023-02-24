/*
name: Yokai New Year Shop
description: Farms the materials needed for Yokai New Year Shop.
tags: yokai new year,akibacny,seasonal,akiba new year, merge shop
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class YokaiNewYearMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Emblem of Righteousness", "Lucky Red Envelope", "Super Lucky Red Envelope", "Emblem of Good Luck", "Emblem of Knowledge", "Emblem of Longevity", "Luckier Red Envelope", "Bunny", "Gold Medallion", "Lunar Firecracker", "Fuchsia Dye", "Magenta Dye" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("akibacny"))
            return;
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("akibacny", 252, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Emblem of Righteousness":
                case "Emblem of Good Luck":
                case "Emblem of Knowledge":
                case "Emblem of Longevity":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Core.IsMember)
                    {
                        Core.RegisterQuests(1582);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("mafic", "Mafic Dragon", "Magmas Spirit");
                            Bot.Wait.ForPickup(req.Name);
                        }
                    }
                    else
                    {
                        Core.RegisterQuests(1584);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("j6", "Sketchy Dragon", "Scrawl Spirit");
                            Bot.Wait.ForPickup(req.Name);
                        }
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Lucky Red Envelope":
                case "Super Lucky Red Envelope":
                case "Luckier Red Envelope":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Core.IsMember)
                    {
                        if (!Core.CheckInventory("Emblem of Longevity"))
                        {
                            Core.AddDrop("Emblem of Longevity");
                            Core.EnsureAccept(954);
                            Core.HuntMonster("mountfrost", "Snow Golem", "Icy Amulet", 5);
                            Core.HuntMonster("mountfrost", "Frostwyrm Rider", "Water Amulet", 5);
                            Core.EnsureComplete(954);
                        }
                        Core.RegisterQuests(955);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("creatures", "Black Tortoise", req.Name, quant);
                            Bot.Wait.ForPickup(req.Name);
                        }
                    }
                    else
                    {
                        Core.RegisterQuests(1584);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("j6", "Sketchy Dragon", req.Name, quant);
                            Bot.Wait.ForPickup(req.Name);
                        }
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Bunny":
                    Adv.BuyItem("ariapet", 12, "Bunny");
                    break;

                case "Gold Medallion":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("akibacny", "Hinezumi", req.Name, quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Lunar Firecracker":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(7923);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("akibacny", "Lu Niu", req.Name, quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Fuchsia Dye":
                case "Magenta Dye":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    if (!Core.IsMember)
                    {
                        Core.RegisterQuests(1489);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("sandsea", "Cactus Creeper", "Fandango Flower", 5);
                            Core.HuntMonster("wanders", "Lotus Spider", "Lotus Flower", 4);
                            Bot.Wait.ForPickup(req.Name);
                        }
                    }
                    else
                    {
                        Core.RegisterQuests(1490);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            Core.HuntMonster("uppercity", "Rhino Beetle", "Carmine Pigment", 4);
                            Core.HuntMonster("doomwood", "Doomwood Treeant", "Cerise Flower", 3);
                            Core.HuntMonster("voltaire", "Fishbones", "Anthurium Flower");
                            Bot.Wait.ForPickup(req.Name);
                        }
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("33676", "Chinese Umbrella", "Mode: [select] only\nShould the bot buy \"Chinese Umbrella\" ?", false),
        new Option<bool>("33636", "Stenciled Fan", "Mode: [select] only\nShould the bot buy \"Stenciled Fan\" ?", false),
        new Option<bool>("33677", "Deadly Fan", "Mode: [select] only\nShould the bot buy \"Deadly Fan\" ?", false),
        new Option<bool>("33637", "Colorful Stenciled Fan", "Mode: [select] only\nShould the bot buy \"Colorful Stenciled Fan\" ?", false),
        new Option<bool>("33638", "Colorful Deadly Fan", "Mode: [select] only\nShould the bot buy \"Colorful Deadly Fan\" ?", false),
        new Option<bool>("33639", "Dynasty's Destiny Blade", "Mode: [select] only\nShould the bot buy \"Dynasty's Destiny Blade\" ?", false),
        new Option<bool>("33640", "Descendant of the Dragon", "Mode: [select] only\nShould the bot buy \"Descendant of the Dragon\" ?", false),
        new Option<bool>("33642", "Helm of the Dragon Dance", "Mode: [select] only\nShould the bot buy \"Helm of the Dragon Dance\" ?", false),
        new Option<bool>("33643", "Dragon Dance Tail", "Mode: [select] only\nShould the bot buy \"Dragon Dance Tail\" ?", false),
        new Option<bool>("33641", "Gold FooPup", "Mode: [select] only\nShould the bot buy \"Gold FooPup\" ?", false),
        new Option<bool>("33644", "Red FooPup", "Mode: [select] only\nShould the bot buy \"Red FooPup\" ?", false),
        new Option<bool>("33645", "Blue FooPup", "Mode: [select] only\nShould the bot buy \"Blue FooPup\" ?", false),
        new Option<bool>("33646", "Dragon Dance Fire Stick", "Mode: [select] only\nShould the bot buy \"Dragon Dance Fire Stick\" ?", false),
        new Option<bool>("33647", "Lucky Coin Katana", "Mode: [select] only\nShould the bot buy \"Lucky Coin Katana\" ?", false),
        new Option<bool>("33649", "Lunisolar Katana", "Mode: [select] only\nShould the bot buy \"Lunisolar Katana\" ?", false),
        new Option<bool>("33650", "Blue FooDog", "Mode: [select] only\nShould the bot buy \"Blue FooDog\" ?", false),
        new Option<bool>("33651", "Red FooDog", "Mode: [select] only\nShould the bot buy \"Red FooDog\" ?", false),
        new Option<bool>("33654", "Gold FooDog", "Mode: [select] only\nShould the bot buy \"Gold FooDog\" ?", false),
        new Option<bool>("33652", "Red FooDog Morph", "Mode: [select] only\nShould the bot buy \"Red FooDog Morph\" ?", false),
        new Option<bool>("33653", "White FooDog Morph", "Mode: [select] only\nShould the bot buy \"White FooDog Morph\" ?", false),
        new Option<bool>("6855", "Year of the Rabbit Warrior", "Mode: [select] only\nShould the bot buy \"Year of the Rabbit Warrior\" ?", false),
        new Option<bool>("6856", "Warrior of the Rabbit Helm", "Mode: [select] only\nShould the bot buy \"Warrior of the Rabbit Helm\" ?", false),
        new Option<bool>("6857", "Year of the Rabbit Morph", "Mode: [select] only\nShould the bot buy \"Year of the Rabbit Morph\" ?", false),
        new Option<bool>("6858", "Cuniculus Decimator", "Mode: [select] only\nShould the bot buy \"Cuniculus Decimator\" ?", false),
        new Option<bool>("6859", "Rabbit Cannon", "Mode: [select] only\nShould the bot buy \"Rabbit Cannon\" ?", false),
        new Option<bool>("6860", "Dragon Bunny", "Mode: [select] only\nShould the bot buy \"Dragon Bunny\" ?", false),
        new Option<bool>("6861", "Golden Rabbit Longsword", "Mode: [select] only\nShould the bot buy \"Golden Rabbit Longsword\" ?", false),
        new Option<bool>("6862", "Golden Rabbit Daggers", "Mode: [select] only\nShould the bot buy \"Golden Rabbit Daggers\" ?", false),
        new Option<bool>("33655", "Serpent Parade Violet", "Mode: [select] only\nShould the bot buy \"Serpent Parade Violet\" ?", false),
        new Option<bool>("33656", "Serpent Parade Red", "Mode: [select] only\nShould the bot buy \"Serpent Parade Red\" ?", false),
        new Option<bool>("33657", "Serpent Parade Green", "Mode: [select] only\nShould the bot buy \"Serpent Parade Green\" ?", false),
        new Option<bool>("33658", "Serpent Parade Blue", "Mode: [select] only\nShould the bot buy \"Serpent Parade Blue\" ?", false),
        new Option<bool>("33751", "Horse Tail Katana", "Mode: [select] only\nShould the bot buy \"Horse Tail Katana\" ?", false),
        new Option<bool>("33752", "Dual Horse Tail Katanas", "Mode: [select] only\nShould the bot buy \"Dual Horse Tail Katanas\" ?", false),
        new Option<bool>("33661", "Celebration Katana", "Mode: [select] only\nShould the bot buy \"Celebration Katana\" ?", false),
        new Option<bool>("33662", "Dual Celebration Katanas", "Mode: [select] only\nShould the bot buy \"Dual Celebration Katanas\" ?", false),
        new Option<bool>("33663", "Parade Staff Red", "Mode: [select] only\nShould the bot buy \"Parade Staff Red\" ?", false),
        new Option<bool>("33664", "Parade Staff Violet", "Mode: [select] only\nShould the bot buy \"Parade Staff Violet\" ?", false),
        new Option<bool>("33665", "Parade Staff Blue", "Mode: [select] only\nShould the bot buy \"Parade Staff Blue\" ?", false),
        new Option<bool>("33666", "Parade Staff Green", "Mode: [select] only\nShould the bot buy \"Parade Staff Green\" ?", false),
        new Option<bool>("33667", "Parade Violet Mask", "Mode: [select] only\nShould the bot buy \"Parade Violet Mask\" ?", false),
        new Option<bool>("33668", "Parade Blue Mask", "Mode: [select] only\nShould the bot buy \"Parade Blue Mask\" ?", false),
        new Option<bool>("33669", "Parade Red Mask", "Mode: [select] only\nShould the bot buy \"Parade Red Mask\" ?", false),
        new Option<bool>("33670", "Parade Green Mask", "Mode: [select] only\nShould the bot buy \"Parade Green Mask\" ?", false),
        new Option<bool>("33671", "Parade Red Cloak", "Mode: [select] only\nShould the bot buy \"Parade Red Cloak\" ?", false),
        new Option<bool>("33672", "Parade Green Cloak", "Mode: [select] only\nShould the bot buy \"Parade Green Cloak\" ?", false),
        new Option<bool>("33673", "Parade Blue Cloak", "Mode: [select] only\nShould the bot buy \"Parade Blue Cloak\" ?", false),
        new Option<bool>("33674", "Parade Violet Cloak", "Mode: [select] only\nShould the bot buy \"Parade Violet Cloak\" ?", false),
        new Option<bool>("42637", "Gold Parade Costume", "Mode: [select] only\nShould the bot buy \"Gold Parade Costume\" ?", false),
        new Option<bool>("42638", "Gold Parade Cloak", "Mode: [select] only\nShould the bot buy \"Gold Parade Cloak\" ?", false),
        new Option<bool>("42639", "Gold Parade Helm", "Mode: [select] only\nShould the bot buy \"Gold Parade Helm\" ?", false),
        new Option<bool>("42640", "Gold Parade Staff", "Mode: [select] only\nShould the bot buy \"Gold Parade Staff\" ?", false),
        new Option<bool>("67798", "Nian Lantern 22", "Mode: [select] only\nShould the bot buy \"Nian Lantern 22\" ?", false),
        new Option<bool>("59292", "Colorful Akiban Hanfu", "Mode: [select] only\nShould the bot buy \"Colorful Akiban Hanfu\" ?", false),
        new Option<bool>("59293", "Decorated Colorful Akiban Hanfu", "Mode: [select] only\nShould the bot buy \"Decorated Colorful Akiban Hanfu\" ?", false),
        new Option<bool>("59310", "Xi Qi's Claymore", "Mode: [select] only\nShould the bot buy \"Xi Qi's Claymore\" ?", false),
        new Option<bool>("59296", "Akiban Hanfu", "Mode: [select] only\nShould the bot buy \"Akiban Hanfu\" ?", false),
        new Option<bool>("59297", "Decorated Akiban Hanfu", "Mode: [select] only\nShould the bot buy \"Decorated Akiban Hanfu\" ?", false),
        new Option<bool>("10771", "Cerise Dragon Spirit", "Mode: [select] only\nShould the bot buy \"Cerise Dragon Spirit\" ?", false),
        new Option<bool>("10782", "Cerise Dragon Spirit Helm", "Mode: [select] only\nShould the bot buy \"Cerise Dragon Spirit Helm\" ?", false),
        new Option<bool>("10775", "Mask of the Cerise Dragon", "Mode: [select] only\nShould the bot buy \"Mask of the Cerise Dragon\" ?", false),
        new Option<bool>("10772", "Cerulean Dragon Spirit", "Mode: [select] only\nShould the bot buy \"Cerulean Dragon Spirit\" ?", false),
        new Option<bool>("10780", "Cerulean Dragon Spirit Helm", "Mode: [select] only\nShould the bot buy \"Cerulean Dragon Spirit Helm\" ?", false),
        new Option<bool>("10777", "Mask of the Cerulean Dragon", "Mode: [select] only\nShould the bot buy \"Mask of the Cerulean Dragon\" ?", false),
        new Option<bool>("10773", "Ebony Dragon Spirit", "Mode: [select] only\nShould the bot buy \"Ebony Dragon Spirit\" ?", false),
        new Option<bool>("10781", "Ebony Dragon Spirit Helm", "Mode: [select] only\nShould the bot buy \"Ebony Dragon Spirit Helm\" ?", false),
        new Option<bool>("10778", "Mask of the Ebony Dragon", "Mode: [select] only\nShould the bot buy \"Mask of the Ebony Dragon\" ?", false),
        new Option<bool>("10774", "Legendary Dragon Spirit", "Mode: [select] only\nShould the bot buy \"Legendary Dragon Spirit\" ?", false),
        new Option<bool>("10779", "Legendary Dragon Spirit Helm", "Mode: [select] only\nShould the bot buy \"Legendary Dragon Spirit Helm\" ?", false),
        new Option<bool>("10776", "Mask of the Legendary Dragon", "Mode: [select] only\nShould the bot buy \"Mask of the Legendary Dragon\" ?", false),
        new Option<bool>("10748", "Sakura Dragon Spirit", "Mode: [select] only\nShould the bot buy \"Sakura Dragon Spirit\" ?", false),
        new Option<bool>("10750", "Sakura Dragon Spirit Morph", "Mode: [select] only\nShould the bot buy \"Sakura Dragon Spirit Morph\" ?", false),
        new Option<bool>("10749", "Sakura Dragon Spirit Mask", "Mode: [select] only\nShould the bot buy \"Sakura Dragon Spirit Mask\" ?", false),
    };
}
