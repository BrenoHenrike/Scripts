/*
name: Void Chasm Merge
description: This bot will farm the items belonging to the selected mode for the Void Chasm Merge [2410] in /voidchasm
tags: void, chasm, merge, voidchasm, abyssal, gravity, ball, balls, midnight, fiend, nulgath, blood, moon, winged, bloodmoon, darksided, energized, archfiend, spines, runed, draconic, nation, double, inferno, prime, shard
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/VHL/CoreVHL.cs
//cs_include Scripts/Story/Nation/VoidRefuge.cs
//cs_include Scripts/Nation/MergeShops/VoidRefugeMerge.cs
//cs_include Scripts/Nation/AssistingCragAndBamboozle[Mem].cs
//cs_include Scripts/Story/Nation/Originul.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleDelve.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleSiege.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleDelveMerge.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
//cs_include Scripts/Nation/Various/TheLeeryContract[Member].cs
//cs_include Scripts/Nation/Various/JuggernautItems.cs
//cs_include Scripts/Nation/Various/VoidPaladin.cs
//cs_include Scripts/Evil/NSoD/CoreNSOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Nation/MergeShops/NulgathDiamondMerge.cs
//cs_include Scripts/Nation/Various/TarosManslayer.cs
//cs_include Scripts/Nation/Various/PurifiedClaymoreOfDestiny.cs
//cs_include Scripts/Nation/Various/VoidSpartan.cs
//cs_include Scripts/Story/Nation/Fiendshard.cs
//cs_include Scripts/Nation/Various/SwirlingTheAbyss.cs
//cs_include Scripts/Hollowborn/TradingandStuff(single).cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Nation/EmpoweringItems.cs
//cs_include Scripts/Other/Weapons/VoidAvengerScythe.cs
//cs_include Scripts/Nation/AFDL/NulgathDemandsWork.cs
//cs_include Scripts/Nation/AFDL/WillpowerExtraction.cs
//cs_include Scripts/Nation/Various/GoldenHanzoVoid.cs
//cs_include Scripts/Nation/MergeShops/DilligasMerge.cs
//cs_include Scripts/Nation/MergeShops/DirtlickersMerge.cs
//cs_include Scripts/Other/Weapons/WrathofNulgath.cs
//cs_include Scripts/Story\Legion\DarkWarLegionandNation.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/Nation/VoidChasm.cs
//cs_include Scripts/Nation/Various/ArchfiendDeathLord.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class VoidChasmMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    public CoreNation Nation = new();
    public VoidChasm VoidChasm = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Void Remnant", "Tainted Gem", "Essence of Nulgath" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        VoidChasm.Storyline();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("voidchasm", 2410, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Void Remnant":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.AddDrop("Void Remnant");
                        Core.EnsureAccept(9553);
                        Core.EquipClass(ClassType.Solo);
                        Core.KillMonster("voidchasm", "r10", "left", "Carcano", "Carcano's Teratoma");
                        Core.KillMonster("voidchasm", "r9", "left", "Carnage", "Bloodied Chainlink");
                        Core.EquipClass(ClassType.Farm);
                        Core.KillMonster("voidchasm", "r7", "left", "The Hushed", "Defunct Seal of Approval", 6);
                        Core.EnsureComplete(9553);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Tainted Gem":
                    Nation.FarmTaintedGem(quant);
                    break;

                case "Essence of Nulgath":
                    Nation.EssenceofNulgath(quant);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("83042", "Abyssal Gravity Ball", "Mode: [select] only\nShould the bot buy \"Abyssal Gravity Ball\" ?", false),
        new Option<bool>("83043", "Abyssal Gravity Balls", "Mode: [select] only\nShould the bot buy \"Abyssal Gravity Balls\" ?", false),
        new Option<bool>("83130", "Midnight Fiend of Nulgath", "Mode: [select] only\nShould the bot buy \"Midnight Fiend of Nulgath\" ?", false),
        new Option<bool>("83131", "Midnight Fiend Hood", "Mode: [select] only\nShould the bot buy \"Midnight Fiend Hood\" ?", false),
        new Option<bool>("83132", "Midnight Blood Moon", "Mode: [select] only\nShould the bot buy \"Midnight Blood Moon\" ?", false),
        new Option<bool>("83133", "Winged Midnight Fiend Cape", "Mode: [select] only\nShould the bot buy \"Winged Midnight Fiend Cape\" ?", false),
        new Option<bool>("83134", "Winged Bloodmoon Cape", "Mode: [select] only\nShould the bot buy \"Winged Bloodmoon Cape\" ?", false),
        new Option<bool>("83135", "Darksided Fiend Blade", "Mode: [select] only\nShould the bot buy \"Darksided Fiend Blade\" ?", false),
        new Option<bool>("83136", "Darksided Fiend Blades", "Mode: [select] only\nShould the bot buy \"Darksided Fiend Blades\" ?", false),
        new Option<bool>("83137", "Midnight Fiend Sword", "Mode: [select] only\nShould the bot buy \"Midnight Fiend Sword\" ?", false),
        new Option<bool>("83138", "Midnight Fiend Swords", "Mode: [select] only\nShould the bot buy \"Midnight Fiend Swords\" ?", false),
        new Option<bool>("83228", "Energized Archfiend Spines", "Mode: [select] only\nShould the bot buy \"Energized Archfiend Spines\" ?", false),
        new Option<bool>("83229", "Runed Archfiend Spines", "Mode: [select] only\nShould the bot buy \"Runed Archfiend Spines\" ?", false),
        new Option<bool>("69634", "Draconic Nation Double Axe", "Mode: [select] only\nShould the bot buy \"Draconic Nation Double Axe\" ?", false),
        new Option<bool>("69635", "Inferno Nation Double Axe", "Mode: [select] only\nShould the bot buy \"Inferno Nation Double Axe\" ?", false),
        new Option<bool>("83524", "Draconic Nation Double Axes", "Mode: [select] only\nShould the bot buy \"Draconic Nation Double Axes\" ?", false),
        new Option<bool>("83525", "Inferno Nation Double Axes", "Mode: [select] only\nShould the bot buy \"Inferno Nation Double Axes\" ?", false),
        new Option<bool>("70184", "Prime Fiend Shard", "Mode: [select] only\nShould the bot buy \"Prime Fiend Shard\" ?", false),
    };
}
