/*
name: Yulgar’s Dual Wield Drop Off Merge
description: This bot will farm the items belonging to the selected mode for the Yulgar’s Dual Wield Drop Off Merge [1312] in /nostalgiaquest
tags: yulgar’s, dual, wield, drop, off, merge, nostalgiaquest, boom, went, dynamite, thewicked, overlords, doomblade, blessed, coffee, cup, party, slasher, birthday, rapier, skulls, frostbite, a, rock, phoenix, nulgath, shadow, spear, guardian, virtue, leviasea, iron, dreadsaw, blood, destruction, painsaw, eidolon, hanzamune, dragon, koi, ugly, stick, balrog, legendary, magma, saw, overfiend, bone, honor, guards, ceremonial, legion, alteons, pride, ddog, sea, serpent, eternity, blinding, light, destiny, crystal, claymore, dark, soulreaper, grumpy, warhammer, maximillians, whip, warpforce, war, shovel, k, godly, mace, ancients, grand, inquisitor, kneecapper, morning, star, black, knight, cruel, midnight, platinum, big, golden, hydra, crusader, bloodriver, breaker, reignbringer, balors, cruelty, default, mighty, dragons, single, butcher, knife, mystic, pencil, endless, scribbles, kroms, brutality, abaddons, terror, awe, burning, abezeth, necrotic, doom, burn, it, down, dragonblade, shadowreaper, cyseros, potato, kuros, wrath, lilith, katana, mammoth, crusher, prismatic, corpse, maker, excavated, glaive, fate, hex, shadowworn, bane, hollowborn, oblivion, revontheus
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Other/WeaponReflection.cs
//cs_include Scripts/Nation/Various/JuggernautItems.cs
//cs_include Scripts/Nation/Various/DragonBlade[mem].cs
//cs_include Scripts/Other/Weapons/ShadowReaperOfDoom.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Nation/Various/TheLeeryContract[Member].cs
//cs_include Scripts/Other/Weapons/GoldenBladeOfFate.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Hollowborn/HollowbornOblivionBlade.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Nation/Various/TarosManslayer.cs
//cs_include Scripts/Nation/Various/PurifiedClaymoreOfDestiny.cs
//cs_include Scripts/Nation/MergeShops/NulgathDiamondMerge.cs
//cs_include Scripts/Other/MergeShops/SpiritHunterMerge[Mem].cs
//cs_include Scripts/Story/Bludrut.cs
//cs_include Scripts/Other/MergeShops/YulgarsDualWieldMerge.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class YulgarsDualWieldDropOffMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private YulgarsDualWieldMerge YDWM = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Dual Boom Went The Dynamite", "Dual TheWicked", "Dual Overlord's DoomBlade", "Dual Blessed Coffee Cup", "Dual Party Slasher Birthday Sword", "Dual Rapier of Skulls", "Dual Frostbite", "Dual Rocks", "Dual Phoenix Blade of Nulgath", "Dual Shadow Spear of Nulgath", "Dual Guardian of Virtue", "Dual Leviasea Sword", "Dual Iron Dreadsaw", "Dual Blood Axe Of Destruction", "Dual PainSaw of Eidolon", "Dual Hanzamune Dragon Koi Blade", "Dual Ugly Stick", "Dual Balrog Blade", "Dual Legendary Magma Sword", "Dual Dragon Saw", "Dual Overfiend Blade of Nulgath", "Dual Bone Sword", "Dual Honor Guard's Blade", "Dual Ceremonial Legion Blade", "Dual Alteon's Pride", "Dual Ddog Sea Serpent Sword", "Dual Eternity Blade", "Dual Blinding Light of Destiny", "Dual Crystal Claymore", "Dual Dark Crystal Claymore", "Dual Soulreaper of Nulgath", "Dual Grumpy Warhammer", "Dual Crystal Phoenix Blade of Nulgath", "Dual Maximillian's Whip", "Dual WarpForce War Shovel 20K", "Dual Godly Mace of the Ancients", "Dual Mace of the Grand Inquisitor", "Dual KneeCappers", "Dual Morning Stars", "Dual Axe of the Black Knight", "Dual Cruel Axe of Midnight", "Dual Platinum Axe of Destiny", "Dual Star Sword", "Dual Big 100K", "Dual Golden Phoenix Sword", "Dual Hydra Blades", "Dual Crusader Sword", "Dual Bloodrivers", "Dual Star Sword Breaker", "Dual ReignBringers", "Dual Balor's Cruelty", "Dual Default Sword", "Dual Iron Spears", "Dual Mighty Sword Of The Dragons", "Butcher Knife", "Dual Pencil of Endless Scribbles", "Dual Krom's Brutalities", "Dual Abaddon's Terrors", "Dual Blades of Awe", "Dual Burning Blades Of Abezeth", "Dual Necrotic Swords of Doom", "Dual Burn it Down Staves", "Phoenix Blades", "Dual Shadow Terror Axes", "Dual DragonBlades of Nulgath", "Dual ShadowReapers Of Doom", "Cysero's Potatoes", "Dual Kuro's Wrath", "Dual Lilith Katana", "Dual Mammoth Crusher Blade", "Dual Light Prismatic Katana", "Dual Corpse Maker of Nulgath", "Dual Excavated Glaive: Sword", "Dual Golden Blade of Fate", "Dual Hex Blade of Nulgath", "Dual Shadowworn", "Dual Bane of Nulgath", "Dual Hollowborn Oblivion Blade", "Dual Katana of Revontheus" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("nostalgiaquest", 1312, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Dual Boom Went The Dynamite":
                case "Dual TheWicked":
                case "Dual Overlord's DoomBlade":
                case "Dual Blessed Coffee Cup":
                case "Dual Party Slasher Birthday Sword":
                case "Dual Rapier of Skulls":
                case "Dual Frostbite":
                case "Dual Rocks":
                case "Dual Phoenix Blade of Nulgath":
                case "Dual Shadow Spear of Nulgath":
                case "Dual Guardian of Virtue":
                case "Dual Leviasea Sword":
                case "Dual Iron Dreadsaw":
                case "Dual Blood Axe Of Destruction":
                case "Dual PainSaw of Eidolon":
                case "Dual Hanzamune Dragon Koi Blade":
                case "Dual Ugly Stick":
                case "Dual Balrog Blade":
                case "Dual Legendary Magma Sword":
                case "Dual Dragon Saw":
                case "Dual Overfiend Blade of Nulgath":
                case "Dual Bone Sword":
                case "Dual Honor Guard's Blade":
                case "Dual Ceremonial Legion Blade":
                case "Dual Alteon's Pride":
                case "Dual Ddog Sea Serpent Sword":
                case "Dual Eternity Blade":
                case "Dual Blinding Light of Destiny":
                case "Dual Crystal Claymore":
                case "Dual Dark Crystal Claymore":
                case "Dual Soulreaper of Nulgath":
                case "Dual Grumpy Warhammer":
                case "Dual Crystal Phoenix Blade of Nulgath":
                case "Dual Maximillian's Whip":
                case "Dual WarpForce War Shovel 20K":
                case "Dual Godly Mace of the Ancients":
                case "Dual Mace of the Grand Inquisitor":
                case "Dual KneeCappers":
                case "Dual Morning Stars":
                case "Dual Axe of the Black Knight":
                case "Dual Cruel Axe of Midnight":
                case "Dual Platinum Axe of Destiny":
                case "Dual Star Sword":
                case "Dual Big 100K":
                case "Dual Golden Phoenix Sword":
                case "Dual Hydra Blades":
                case "Dual Crusader Sword":
                case "Dual Bloodrivers":
                case "Dual Star Sword Breaker":
                case "Dual ReignBringers":
                case "Dual Balor's Cruelty":
                case "Dual Default Sword":
                case "Dual Iron Spears":
                case "Dual Mighty Sword Of The Dragons":
                case "Butcher Knife":
                case "Dual Pencil of Endless Scribbles":
                case "Dual Krom's Brutalities":
                case "Dual Abaddon's Terrors":
                case "Dual Blades of Awe":
                case "Dual Burning Blades Of Abezeth":
                case "Dual Necrotic Swords of Doom":
                case "Dual Burn it Down Staves":
                case "Phoenix Blades":
                case "Dual Shadow Terror Axes":
                case "Dual DragonBlades of Nulgath":
                case "Dual ShadowReapers Of Doom":
                case "Cysero's Potatoes":
                case "Dual Kuro's Wrath":
                case "Dual Mammoth Crusher Blade":
                case "Dual Lilith Katana":
                case "Dual Light Prismatic Katana":
                case "Dual Corpse Maker of Nulgath":
                case "Dual Excavated Glaive: Sword":
                case "Dual Golden Blade of Fate":
                case "Dual Hex Blade of Nulgath":
                case "Dual Shadowworn":
                case "Dual Bane of Nulgath":
                case "Dual Hollowborn Oblivion Blade":
                case "Dual Katana of Revontheus":
                    YDWM.BuyAllMerge(req.Name);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("12060", "Boom Went The Dynamite", "Mode: [select] only\nShould the bot buy \"Boom Went The Dynamite\" ?", false),
        new Option<bool>("13041", "TheWicked", "Mode: [select] only\nShould the bot buy \"TheWicked\" ?", false),
        new Option<bool>("10150", "Overlord's DoomBlade", "Mode: [select] only\nShould the bot buy \"Overlord's DoomBlade\" ?", false),
        new Option<bool>("36618", "Blessed Coffee Cup", "Mode: [select] only\nShould the bot buy \"Blessed Coffee Cup\" ?", false),
        new Option<bool>("2033", "Party Slasher Birthday Sword", "Mode: [select] only\nShould the bot buy \"Party Slasher Birthday Sword\" ?", false),
        new Option<bool>("19980", "Rapier of Skulls", "Mode: [select] only\nShould the bot buy \"Rapier of Skulls\" ?", false),
        new Option<bool>("6522", "Frostbite", "Mode: [select] only\nShould the bot buy \"Frostbite\" ?", false),
        new Option<bool>("9430", "A Rock", "Mode: [select] only\nShould the bot buy \"A Rock\" ?", false),
        new Option<bool>("4836", "Phoenix Blade of Nulgath", "Mode: [select] only\nShould the bot buy \"Phoenix Blade of Nulgath\" ?", false),
        new Option<bool>("5048", "Shadow Spear of Nulgath", "Mode: [select] only\nShould the bot buy \"Shadow Spear of Nulgath\" ?", false),
        new Option<bool>("5828", "Guardian of Virtue", "Mode: [select] only\nShould the bot buy \"Guardian of Virtue\" ?", false),
        new Option<bool>("7667", "Leviasea Sword", "Mode: [select] only\nShould the bot buy \"Leviasea Sword\" ?", false),
        new Option<bool>("33164", "Iron Dreadsaw", "Mode: [select] only\nShould the bot buy \"Iron Dreadsaw\" ?", false),
        new Option<bool>("36465", "Blood Axe Of Destruction", "Mode: [select] only\nShould the bot buy \"Blood Axe Of Destruction\" ?", false),
        new Option<bool>("5757", "PainSaw of Eidolon", "Mode: [select] only\nShould the bot buy \"PainSaw of Eidolon\" ?", false),
        new Option<bool>("2238", "Hanzamune Dragon Koi Blade", "Mode: [select] only\nShould the bot buy \"Hanzamune Dragon Koi Blade\" ?", false),
        new Option<bool>("815", "Ugly Stick", "Mode: [select] only\nShould the bot buy \"Ugly Stick\" ?", false),
        new Option<bool>("71", "Balrog Blade", "Mode: [select] only\nShould the bot buy \"Balrog Blade\" ?", false),
        new Option<bool>("1006", "Legendary Magma Sword", "Mode: [select] only\nShould the bot buy \"Legendary Magma Sword\" ?", false),
        new Option<bool>("26", "Dragon Saw", "Mode: [select] only\nShould the bot buy \"Dragon Saw\" ?", false),
        new Option<bool>("6138", "Overfiend Blade of Nulgath", "Mode: [select] only\nShould the bot buy \"Overfiend Blade of Nulgath\" ?", false),
        new Option<bool>("47", "Bone Sword", "Mode: [select] only\nShould the bot buy \"Bone Sword\" ?", false),
        new Option<bool>("14126", "Honor Guard's Blade", "Mode: [select] only\nShould the bot buy \"Honor Guard's Blade\" ?", false),
        new Option<bool>("16431", "Ceremonial Legion Blade", "Mode: [select] only\nShould the bot buy \"Ceremonial Legion Blade\" ?", false),
        new Option<bool>("2774", "Alteon's Pride", "Mode: [select] only\nShould the bot buy \"Alteon's Pride\" ?", false),
        new Option<bool>("4766", "Ddog Sea Serpent Sword", "Mode: [select] only\nShould the bot buy \"Ddog Sea Serpent Sword\" ?", false),
        new Option<bool>("23689", "Eternity Blade", "Mode: [select] only\nShould the bot buy \"Eternity Blade\" ?", false),
        new Option<bool>("14467", "Blinding Light of Destiny", "Mode: [select] only\nShould the bot buy \"Blinding Light of Destiny\" ?", false),
        new Option<bool>("1151", "Crystal Claymore", "Mode: [select] only\nShould the bot buy \"Crystal Claymore\" ?", false),
        new Option<bool>("1152", "Dark Crystal Claymore", "Mode: [select] only\nShould the bot buy \"Dark Crystal Claymore\" ?", false),
        new Option<bool>("4765", "Soulreaper of Nulgath", "Mode: [select] only\nShould the bot buy \"Soulreaper of Nulgath\" ?", false),
        new Option<bool>("1236", "Grumpy Warhammer", "Mode: [select] only\nShould the bot buy \"Grumpy Warhammer\" ?", false),
        new Option<bool>("6137", "Crystal Phoenix Blade of Nulgath", "Mode: [select] only\nShould the bot buy \"Crystal Phoenix Blade of Nulgath\" ?", false),
        new Option<bool>("2045", "Maximillian's Whip", "Mode: [select] only\nShould the bot buy \"Maximillian's Whip\" ?", false),
        new Option<bool>("1301", "WarpForce War Shovel 20K", "Mode: [select] only\nShould the bot buy \"WarpForce War Shovel 20K\" ?", false),
        new Option<bool>("866", "Godly Mace of the Ancients", "Mode: [select] only\nShould the bot buy \"Godly Mace of the Ancients\" ?", false),
        new Option<bool>("537", "Mace of the Grand Inquisitor", "Mode: [select] only\nShould the bot buy \"Mace of the Grand Inquisitor\" ?", false),
        new Option<bool>("10483", "KneeCapper", "Mode: [select] only\nShould the bot buy \"KneeCapper\" ?", false),
        new Option<bool>("55", "Morning Star", "Mode: [select] only\nShould the bot buy \"Morning Star\" ?", false),
        new Option<bool>("1546", "Axe of the Black Knight", "Mode: [select] only\nShould the bot buy \"Axe of the Black Knight\" ?", false),
        new Option<bool>("1545", "Cruel Axe of Midnight", "Mode: [select] only\nShould the bot buy \"Cruel Axe of Midnight\" ?", false),
        new Option<bool>("820", "Platinum Axe of Destiny", "Mode: [select] only\nShould the bot buy \"Platinum Axe of Destiny\" ?", false),
        new Option<bool>("74", "Star Sword", "Mode: [select] only\nShould the bot buy \"Star Sword\" ?", false),
        new Option<bool>("25", "Big 100K", "Mode: [select] only\nShould the bot buy \"Big 100K\" ?", false),
        new Option<bool>("2090", "Golden Phoenix Sword", "Mode: [select] only\nShould the bot buy \"Golden Phoenix Sword\" ?", false),
        new Option<bool>("57", "Hydra Blade", "Mode: [select] only\nShould the bot buy \"Hydra Blade\" ?", false),
        new Option<bool>("680", "Crusader Sword", "Mode: [select] only\nShould the bot buy \"Crusader Sword\" ?", false),
        new Option<bool>("2421", "Bloodriver", "Mode: [select] only\nShould the bot buy \"Bloodriver\" ?", false),
        new Option<bool>("2256", "Star Sword Breaker", "Mode: [select] only\nShould the bot buy \"Star Sword Breaker\" ?", false),
        new Option<bool>("53", "ReignBringer", "Mode: [select] only\nShould the bot buy \"ReignBringer\" ?", false),
        new Option<bool>("441", "Balor's Cruelty", "Mode: [select] only\nShould the bot buy \"Balor's Cruelty\" ?", false),
        new Option<bool>("1", "Default Sword", "Mode: [select] only\nShould the bot buy \"Default Sword\" ?", false),
        new Option<bool>("94", "Iron Spear", "Mode: [select] only\nShould the bot buy \"Iron Spear\" ?", false),
        new Option<bool>("22359", "Mighty Sword Of The Dragons", "Mode: [select] only\nShould the bot buy \"Mighty Sword Of The Dragons\" ?", false),
        new Option<bool>("41804", "Single Butcher Knife", "Mode: [select] only\nShould the bot buy \"Single Butcher Knife\" ?", false),
        new Option<bool>("1008", "Mystic Pencil of Endless Scribbles", "Mode: [select] only\nShould the bot buy \"Mystic Pencil of Endless Scribbles\" ?", false),
        new Option<bool>("370", "Krom's Brutality", "Mode: [select] only\nShould the bot buy \"Krom's Brutality\" ?", false),
        new Option<bool>("2307", "Abaddon's Terror", "Mode: [select] only\nShould the bot buy \"Abaddon's Terror\" ?", false),
        new Option<bool>("17585", "Blade of Awe", "Mode: [select] only\nShould the bot buy \"Blade of Awe\" ?", false),
        new Option<bool>("41453", "Burning Blade Of Abezeth", "Mode: [select] only\nShould the bot buy \"Burning Blade Of Abezeth\" ?", false),
        new Option<bool>("30629", "Necrotic Sword of Doom", "Mode: [select] only\nShould the bot buy \"Necrotic Sword of Doom\" ?", false),
        new Option<bool>("868", "Burn it Down", "Mode: [select] only\nShould the bot buy \"Burn it Down\" ?", false),
        new Option<bool>("300", "Phoenix Blade", "Mode: [select] only\nShould the bot buy \"Phoenix Blade\" ?", false),
        new Option<bool>("1961", "Shadow Terror Axe", "Mode: [select] only\nShould the bot buy \"Shadow Terror Axe\" ?", false),
        new Option<bool>("5483", "DragonBlade of Nulgath", "Mode: [select] only\nShould the bot buy \"DragonBlade of Nulgath\" ?", false),
        new Option<bool>("17488", "ShadowReaper Of Doom", "Mode: [select] only\nShould the bot buy \"ShadowReaper Of Doom\" ?", false),
        new Option<bool>("85139", "Cysero's Potato", "Mode: [select] only\nShould the bot buy \"Cysero's Potato\" ?", false),
        new Option<bool>("369", "Kuro's Wrath", "Mode: [select] only\nShould the bot buy \"Kuro's Wrath\" ?", false),
        new Option<bool>("6133", "Lilith Katana", "Mode: [select] only\nShould the bot buy \"Lilith Katana\" ?", false),
        new Option<bool>("456", "Mammoth Crusher Blade", "Mode: [select] only\nShould the bot buy \"Mammoth Crusher Blade\" ?", false),
        new Option<bool>("2715", "Light Prismatic Katana", "Mode: [select] only\nShould the bot buy \"Light Prismatic Katana\" ?", false),
        new Option<bool>("4764", "Corpse Maker of Nulgath", "Mode: [select] only\nShould the bot buy \"Corpse Maker of Nulgath\" ?", false),
        new Option<bool>("38412", "Excavated Glaive: Sword", "Mode: [select] only\nShould the bot buy \"Excavated Glaive: Sword\" ?", false),
        new Option<bool>("38356", "Golden Blade of Fate", "Mode: [select] only\nShould the bot buy \"Golden Blade of Fate\" ?", false),
        new Option<bool>("5052", "Hex Blade of Nulgath", "Mode: [select] only\nShould the bot buy \"Hex Blade of Nulgath\" ?", false),
        new Option<bool>("20878", "Shadowworn", "Mode: [select] only\nShould the bot buy \"Shadowworn\" ?", false),
        new Option<bool>("5529", "Bane of Nulgath", "Mode: [select] only\nShould the bot buy \"Bane of Nulgath\" ?", false),
        new Option<bool>("52597", "Hollowborn Oblivion Blade", "Mode: [select] only\nShould the bot buy \"Hollowborn Oblivion Blade\" ?", false),
        new Option<bool>("13027", "Katana of Revontheus", "Mode: [select] only\nShould the bot buy \"Katana of Revontheus\" ?", false),
    };
}
