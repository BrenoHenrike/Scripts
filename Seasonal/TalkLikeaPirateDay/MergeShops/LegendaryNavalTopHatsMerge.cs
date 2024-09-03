/*
name: Legendary Naval Top Hats Merge [Member]
description: This bot will farm the items belonging to the selected mode for the Legendary Naval Top Hats Merge [1919] in /pirates
tags: tlapd, talk-like-a-pirate-day, seasonal, legendary, naval, top, hats, merge, pirates, male, galactic, bearded, chronolord, chaos, blazing, icy, platinum, legion, red, rotting, skull, scallywag, sir, miss, mr, chronolady, missy, lady, mrs, lassy, cutie, femme, goggleless, brilliant, tophat, , doom, lass, chaotic, morph, flaming, toxic, beard
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/MergeShops/NavalTopHatMerge.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Options;

public class LegendaryNavalTopHatsMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private NavalTopHatMerge NTHM = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Stardust", "Legend Top Hat", "Facial Hair", "Gears", "Chaos Eye", "Breath of Flame", "Shard of Ice", "Icy Naval Top Hat", "Nugget of Platinum", "Blue Skull", "Red Cloth", "Zombie Flesh", "Pink Cloth", "Scrap of Cloth", "Toxic Flame", "Toxic Gas Mask" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("pirates", 1919, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Stardust":
                case "Facial Hair":
                case "Gears":
                case "Chaos Eye":
                case "Breath of Flame":
                case "Shard of Ice":
                case "Nugget of Platinum":
                case "Blue Skull":
                case "Red Cloth":
                case "Zombie Flesh":
                case "Pink Cloth":
                case "Toxic Flame":
                case "Toxic Gas Mask":
                    Core.FarmingLogger(req.Name, quant);
                    Adv.BuyItem("pirates", 724, req.Name, quant);
                    break;

                case "Legend Top Hat":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("pirates", "Fishman Soldier", req.Name, quant, false, false);
                    break;

                case "Icy Naval Top Hat":
                    NTHM.BuyAllMerge(req.Name);
                    break;

                case "Scrap of Cloth":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        foreach (int mon in new[] { 3, 7, 15, 10 })
                        {
                            Monster? M = Bot.Monsters.MapMonsters.FirstOrDefault(x => x != null && x.MapID == mon);
                            if (M == null)
                                continue;

                            if (Bot.Map.Name != "tlapd")
                                Core.Join("tlapd");
                            if (Bot.Player.Cell != M!.Cell)
                                Core.Jump(M.Cell);

                            if (M != null && M.HP >= 0)
                                Bot.Hunt.Monster(M.MapID);

                            if (Core.CheckInventory(req.Name, quant))
                                break;
                        }
                    }
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("25698", "Male Galactic Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Male Galactic Naval Top Hat\" ?", false),
        new Option<bool>("25699", "Bearded ChronoLord Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Bearded ChronoLord Naval Top Hat\" ?", false),
        new Option<bool>("25700", "Bearded Chaos Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Bearded Chaos Naval Top Hat\" ?", false),
        new Option<bool>("25701", "Bearded Blazing Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Bearded Blazing Naval Top Hat\" ?", false),
        new Option<bool>("25702", "Bearded Icy Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Bearded Icy Naval Top Hat\" ?", false),
        new Option<bool>("25703", "Bearded Galactic Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Bearded Galactic Naval Top Hat\" ?", false),
        new Option<bool>("25705", "Bearded Platinum Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Bearded Platinum Naval Top Hat\" ?", false),
        new Option<bool>("25706", "Bearded Legion Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Bearded Legion Naval Top Hat\" ?", false),
        new Option<bool>("25707", "Bearded Red Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Bearded Red Naval Top Hat\" ?", false),
        new Option<bool>("25708", "Bearded Rotting Top Naval Hat", "Mode: [select] only\nShould the bot buy \"Bearded Rotting Top Naval Hat\" ?", false),
        new Option<bool>("25709", "Bearded Rotting Skull Top Hat", "Mode: [select] only\nShould the bot buy \"Bearded Rotting Skull Top Hat\" ?", false),
        new Option<bool>("25710", "Bearded Scallywag Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Bearded Scallywag Naval Top Hat\" ?", false),
        new Option<bool>("25711", "ChronoLord Naval Top Hat", "Mode: [select] only\nShould the bot buy \"ChronoLord Naval Top Hat\" ?", false),
        new Option<bool>("25712", "Sir Legion Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Sir Legion Naval Top Hat\" ?", false),
        new Option<bool>("25713", "Miss Legion Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Miss Legion Naval Top Hat\" ?", false),
        new Option<bool>("25714", "Mr. Chaos Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Mr. Chaos Naval Top Hat\" ?", false),
        new Option<bool>("25715", "ChronoLady Naval Top Hat", "Mode: [select] only\nShould the bot buy \"ChronoLady Naval Top Hat\" ?", false),
        new Option<bool>("25716", "Blazing Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Blazing Naval Top Hat\" ?", false),
        new Option<bool>("25717", "Missy Scallywag Top Hat", "Mode: [select] only\nShould the bot buy \"Missy Scallywag Top Hat\" ?", false),
        new Option<bool>("25718", "Lady Rotting Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Lady Rotting Naval Top Hat\" ?", false),
        new Option<bool>("25719", "Miss Red Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Miss Red Naval Top Hat\" ?", false),
        new Option<bool>("25720", "Mrs. Platinum Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Mrs. Platinum Naval Top Hat\" ?", false),
        new Option<bool>("25793", "Lassy Icy Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Lassy Icy Naval Top Hat\" ?", false),
        new Option<bool>("25721", "Cutie Chaos Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Cutie Chaos Naval Top Hat\" ?", false),
        new Option<bool>("25722", "Femme Blazing Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Femme Blazing Naval Top Hat\" ?", false),
        new Option<bool>("25723", "Male Rotting Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Male Rotting Naval Top Hat\" ?", false),
        new Option<bool>("25724", "Mr. Red Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Mr. Red Naval Top Hat\" ?", false),
        new Option<bool>("25725", "Sir Platinum Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Sir Platinum Naval Top Hat\" ?", false),
        new Option<bool>("25726", "Miss Galactic Naval Top Hat", "Mode: [select] only\nShould the bot buy \"Miss Galactic Naval Top Hat\" ?", false),
        new Option<bool>("25730", "Goggle-less Bearded ChronoLord Top Hat", "Mode: [select] only\nShould the bot buy \"Goggle-less Bearded ChronoLord Top Hat\" ?", false),
        new Option<bool>("25731", "Goggle-less ChronoLady Top Hat", "Mode: [select] only\nShould the bot buy \"Goggle-less ChronoLady Top Hat\" ?", false),
        new Option<bool>("25732", "Goggle-less ChronoLord Top Hat", "Mode: [select] only\nShould the bot buy \"Goggle-less ChronoLord Top Hat\" ?", false),
        new Option<bool>("25579", "Brilliant Naval Tophat +3", "Mode: [select] only\nShould the bot buy \"Brilliant Naval Tophat +3\" ?", false),
        new Option<bool>("25580", "Brilliant Naval Tophat Locks +3", "Mode: [select] only\nShould the bot buy \"Brilliant Naval Tophat Locks +3\" ?", false),
        new Option<bool>("25581", "Doom Top Hat +3", "Mode: [select] only\nShould the bot buy \"Doom Top Hat +3\" ?", false),
        new Option<bool>("25582", "Doom Lass Tophat +3", "Mode: [select] only\nShould the bot buy \"Doom Lass Tophat +3\" ?", false),
        new Option<bool>("56022", "Chaotic Naval Top Hat + Locks Morph", "Mode: [select] only\nShould the bot buy \"Chaotic Naval Top Hat + Locks Morph\" ?", false),
        new Option<bool>("56023", "Chaotic Naval Top Hat Morph", "Mode: [select] only\nShould the bot buy \"Chaotic Naval Top Hat Morph\" ?", false),
        new Option<bool>("56537", "Flaming Toxic Top Hat", "Mode: [select] only\nShould the bot buy \"Flaming Toxic Top Hat\" ?", false),
        new Option<bool>("56392", "Flaming Toxic Top Hat + Beard", "Mode: [select] only\nShould the bot buy \"Flaming Toxic Top Hat + Beard\" ?", false),
        new Option<bool>("56390", "Flaming Toxic Top Hat + Mask", "Mode: [select] only\nShould the bot buy \"Flaming Toxic Top Hat + Mask\" ?", false),
        new Option<bool>("56539", "Flaming Toxic Naval Top Hat + Locks", "Mode: [select] only\nShould the bot buy \"Flaming Toxic Naval Top Hat + Locks\" ?", false),
        new Option<bool>("56541", "Flaming Toxic Top Hat + Locks + Mask", "Mode: [select] only\nShould the bot buy \"Flaming Toxic Top Hat + Locks + Mask\" ?", false),
    };
}
