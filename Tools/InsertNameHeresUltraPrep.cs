/*
name: Дж2सरतϛȠကỊⱣ
description: Not for everyday use
tags: Do, not, find, me
*/
#region includes
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Other\Various\Potions.cs
//cs_include Scripts/Farm\BuyScrolls.cs
//cs_include Scripts/Tools\BankAllItems.cs
//cs_include Scripts/Enhancement\UnlockForgeEnhancements.cs


//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Chaos/EternalDrakathSet.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQoM.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Evil/NSoD/CoreNSOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Legion/YamiNoRonin/CoreYnR.cs

//cs_include Scripts/Chaos/DrakathsArmor.cs
//cs_include Scripts/Good/ArchPaladin.cs
//cs_include Scripts/Good/Paladin.cs
//cs_include Scripts/Evil/SepulchuresOriginalHelm.cs
//cs_include Scripts/Legion/HeadOfTheLegionBeast.cs
//cs_include Scripts/Other/WarFuryEmblem.cs
//cs_include Scripts/Other/MysteriousEgg.cs
//cs_include Scripts/Other/Classes/DragonslayerGeneral.cs
//cs_include Scripts/Other/Weapons/GoldenBladeOfFate.cs
//cs_include Scripts/Other/Weapons/PinkBladeofDestruction.cs
//cs_include Scripts/Other/Armor/FireChampionsArmor.cs
//cs_include Scripts/Other/Classes/DragonOfTime.cs
//cs_include Scripts/Darkon/Various/PrinceDarkonsPoleaxePreReqs.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
//cs_include Scripts/Story/SepulchureSaga/CoreSepulchure.cs
//cs_include Scripts/Story/Summer2015AdventureMap/CoreSummer.cs
//cs_include Scripts/Story/Borgars.cs
//cs_include Scripts/Story/StarSinc.cs
//cs_include Scripts/Story/TowerOfDoom.cs
//cs_include Scripts/Story/XansLair.cs
//cs_include Scripts/Good/GearOfAwe/Awescended.cs
//cs_include Scripts/Nation/AFDL/NulgathDemandsWork.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Nation/Various/GoldenHanzoVoid.cs
//cs_include Scripts/Nation/AFDL/WillpowerExtraction.cs
//cs_include Scripts/Good/GearOfAwe/ArmorOfAwe.cs
//cs_include Scripts/Good/GearOfAwe/HelmOfAwe.cs
//cs_include Scripts/Good/SilverExaltedPaladin.cs
//cs_include Scripts/Other/Weapons/FortitudeAndHubris.cs
//cs_include Scripts/Other/Weapons/ShadowReaperOfDoom.cs
//cs_include Scripts/Story/J6Saga.cs
//cs_include Scripts/Story/Nation/Bamboozle.cs
//cs_include Scripts/Story/7DeadlyDragons/Extra/HatchTheEgg.cs
//cs_include Scripts/Other/ShadowDragonDefender.cs
//cs_include Scripts/Evil/ADK.cs
//cs_include Scripts/Story/DjinnGate.cs
//cs_include Scripts/Story/ThirdSpell.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/Yokai.cs
//cs_include Scripts/Legion/SwordMaster.cs
//cs_include Scripts/Dailies/LordOfOrder.cs
//cs_include Scripts/Story/Nation/CitadelRuins.cs
//cs_include Scripts/Story/DragonFableOrigins.cs
//cs_include Scripts/Other/Armor/MalgorsArmorSet.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/DeadLinesMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/ShadowflameFinaleMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/TimekeepMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/StreamwarMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/WorldsCoreMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/ManaCradleMerge.cs
//cs_include Scripts/ShadowsOfWar/CoreSoWMats.cs
//cs_include Scripts/Story/Nation/Fiendshard.cs

//cs_include Scripts/Nation/VHL/CoreVHL.cs
//cs_include Scripts/Story/Nation/VoidRefuge.cs
//cs_include Scripts/Nation/MergeShops/VoidRefugeMerge.cs
//cs_include Scripts/Nation/AssistingCragAndBamboozle[Mem].cs
//cs_include Scripts/Story/Nation/Originul.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleDelve.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleSiege.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleDelveMerge.cs
//cs_include Scripts/Nation/Various/TheLeeryContract[Member].cs
//cs_include Scripts/Nation/Various/JuggernautItems.cs
//cs_include Scripts/Nation/Various/VoidPaladin.cs
//cs_include Scripts/Nation/MergeShops/NulgathDiamondMerge.cs
//cs_include Scripts/Nation/Various/TarosManslayer.cs
//cs_include Scripts/Nation/Various/PurifiedClaymoreOfDestiny.cs
//cs_include Scripts/Nation/Various/VoidSpartan.cs
//cs_include Scripts/Nation/Various/SwirlingTheAbyss.cs
//cs_include Scripts/Hollowborn/TradingandStuff(single).cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Nation/EmpoweringItems.cs
//cs_include Scripts/Other/Weapons/VoidAvengerScythe.cs
//cs_include Scripts/Nation/MergeShops/DilligasMerge.cs
//cs_include Scripts/Nation/MergeShops/DirtlickersMerge.cs
//cs_include Scripts/Other/Weapons/WrathofNulgath.cs
//cs_include Scripts/Story/Legion/DarkWarLegionandNation.cs
//cs_include Scripts/Nation/Various/PrimeFiendShard.cs
//cs_include Scripts/Nation/Various/ArchfiendDeathLord.cs
//cs_include Scripts/Nation/MergeShops/VoidChasmMerge.cs
//cs_include Scripts/Story/Nation/VoidChasm.cs
//cs_include Scripts/Nation/MergeShops/NationMerge.cs
//cs_include Scripts/Nation\NationLoyaltyRewarded.cs
#endregion

using Skua.Core.Interfaces;
using Skua.Core.Options;

public class InsertNameHeresUltraPrep
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv = new();
    private CoreFarms Farm = new();
    private CoreStory Story = new();
    private CoreDailies Daily = new();
    public PotionBuyer PotionBuyer = new();
    private BuyScrolls Scroll = new();
    private BankAllItems BankAllItems = new();
    private UnlockForgeEnhancements UnlockForgeEnhancements = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "UltraPrep";

    public List<IOption> Options = new()
    {
        // Player options
        new Option<string>("Player1", "Player 1 name", "Username of Player 1", "Player1"),
        new Option<string>("Player1_Pet", "Player 1 pet", "Pet of Player 1", "Pet1"),
        new Option<string>("Player2", "Player 2 name", "Username of Player 2", "Player2"),
        new Option<string>("Player2_Pet", "Player 2 pet", "Pet of Player 2", "Pet2"),
        new Option<string>("Player3", "Player 3 name", "Username of Player 3", "Player3"),
        new Option<string>("Player3_Pet", "Player 3 pet", "Pet of Player 3", "Pet3"),
        new Option<string>("Player4", "Player 4 name", "Username of Player 4", "Player4"),
        new Option<string>("Player4_Pet", "Player 4 pet", "Pet of Player 4", "Pet4"),

        // Weapon and enhancement options
        new Option<string>("Dauntless", "Dauntless Weapon", "Weapon for Dauntless", ""),
        new Option<string>("Valiance", "Valiance Weapon", "Weapon for Valiance", ""),
        new Option<string>("Arcanas_Concerto", "Arcanas Concerto Weapon", "Weapon for Arcanas Concerto", ""),
        new Option<string>("Awe_Blast", "Awe Blast Weapon", "Weapon for Awe Blast", ""),
        new Option<string>("Praxis", "Praxis Weapon", "Weapon for Praxis", ""),
        new Option<string>("Ravenous", "Ravenous Weapon", "Weapon for Ravenous", ""),
        new Option<string>("Elysium", "Elysium Weapon", "Weapon for Elysium", ""),
        new Option<string>("Lacerate", "Lacerate Weapon", "Weapon for Lacerate", ""),
        new Option<string>("HealthVamp", "HealthVamp Weapon", "Weapon for HealthVamp", ""),

        // Helm options
        new Option<string>("Wizard Helm", "Wizard Helm", "Helm for Wizard", ""),
        new Option<string>("Luck Helm", "Luck Helm", "Helm for Luck", ""),
        new Option<string>("Forge Helm", "Forge Helm", "Helm for Forge", ""),

        // Cape options
        new Option<string>("Absolution", "Absolution Cape", "Cape for Absolution", ""),
        new Option<string>("Avarice", "Avarice Cape", "Cape for Avarice", ""),
        new Option<string>("Penitence", "Penitence Cape", "Cape for Penitence", ""),
        new Option<string>("Vainglory", "Vainglory Cape", "Cape for Vainglory", ""),
        new Option<string>("Lament", "Lament Cape", "Cape for Lament", ""),
    };


    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Example();

        Core.SetOptions(false);
    }

    public void Example()
    {
        Core.Logger("This script is made to help you prep for InsertNamehere's \"ULTRAS - INSERTNAMEHERE.gbot\" on `Grim Li`. First run and fill this script out, once its finished, you can Access your item (for easy copy paste) in `Documents > Skua > options > UltraPrep`, open that and u can simply copy paste names of items. Set your safe pot to: `Potent Malevolence Elixir` as its what this script grabs.");
        
        //prepare inventory
        Core.Logger("Preparing Inventory!!");

        // Dictionary to store players and their associated pets
        Dictionary<string, string> PlayersAndPets = new()
        {
            { Bot.Config!.Get<string>("Player1"), Bot.Config.Get<string>("Player1_Pet") },
            { Bot.Config!.Get<string>("Player2"), Bot.Config.Get<string>("Player2_Pet") },
            { Bot.Config!.Get<string>("Player3"), Bot.Config.Get<string>("Player3_Pet") },
            { Bot.Config!.Get<string>("Player4"), Bot.Config.Get<string>("Player4_Pet") }
        };

        // Retrieve options excluding those related to players
        var blacklistOptions = Options
            .Where(o => !o.Name.StartsWith("Player", StringComparison.OrdinalIgnoreCase))
            .Select(o => Bot.Config!.Get<string>(o.Name));

        // Retrieve the pet associated with the current player
        var currentPlayerPets = new List<string>();
        foreach (var kvp in PlayersAndPets)
        {
            if (Bot.Player.Username != kvp.Key)
                continue;

            currentPlayerPets.Add(kvp.Value);
        }

        // Combine blacklist options and the current player's pets
        var combinedBlacklist = blacklistOptions.Concat(currentPlayerPets);

        // Bank all items with the generated blacklist
        BankAllItems.BankAll(true, false, false, string.Join(",", combinedBlacklist));

        #region  Unbanking Required items
        // Iterate through options and unbank items for each option except those starting with "player"
        foreach (var option in Options)
        {
            if (option.Name.StartsWith("player", StringComparison.OrdinalIgnoreCase))
                continue; // Skip options starting with "player"

            string itemName = Bot.Config!.Get<string>(option.Name);
            Core.Unbank(itemName);
        }

        // Dictionary with players and their corresponding classes
        Dictionary<string, List<string>> PlayersandItems = new()
        {
            { Bot.Config!.Get<string>("Player1"), new List<string> { "Legion Revenant", "ArchPaladin", "StoneCrusher", Core.CheckInventory("Chaos Avenger") ? "Chaos Avenger" : "Verus DoomKnight", Bot.Config.Get<string>("Player1_Pet") } },
            { Bot.Config!.Get<string>("Player2"), new List<string> { "Quantum Chronomancer", "Legion Revenant", Core.CheckInventory("Chaos Avenger") ? "Chaos Avenger" : "Verus DoomKnight", "StoneCrusher", Bot.Config.Get<string>("Player2_Pet") } },
            { Bot.Config!.Get<string>("Player3"), new List<string> { "Lord Of Order", "Legion Revenant", Bot.Config.Get<string>("Player3_Pet") } },
            { Bot.Config!.Get<string>("Player4"), new List<string> { "ArchPaladin", "LightCaster", "Verus DoomKnight", Bot.Config.Get<string>("Player4_Pet") } }
        };

        // Iterate through the dictionary
        foreach (var player in PlayersandItems)
        {
            if (Bot.Player.Username != player.Key)
                continue;

            foreach (var Item in player.Value)
                if (!Bot.Inventory.Contains(Item))
                    Core.Unbank(Item);
        }
        #endregion  Unbanking Required items

        #region Enhancment Unlock Checking

        // Quest IDs and Enhancement Names
        int[] questIDs =
        {
            2937, 8738, 8739, 8740, 8741, 8742, 8743, 8744, 8745, 8758,
            8821, 8820, 8822, 8823, 8824, 8825, 8828, 8827, 8826, 9172,
            9171, 9560
        };

        string[] enhancementNames =
            {
            "Health Vamp/AweBlast", "ForgeWeapon", "Lacerate", "Smite", "Valiance", "ArcanasConcerto",
            "Absolution", "Vainglory", "Avarice", "ForgeCape", "Elysium", "Acheron",
            "Penitence", "Lament", "Vim", "Examen", "ForgeHelm", "Pneuma", "Anima",
            "Dauntless", "Praxis", "Ravenous"
            };

        // List to store missing enhancements
        List<string> missingEnhancements = new();

        // Display quest completion status and track missing enhancements
        foreach (var (questID, enhancementName) in questIDs.Zip(enhancementNames, (q, n) => (questID: q, enhancementName: n)))
        {
            string completionStatus = Core.isCompletedBefore(questID) ? "✅" : "❌";
            Core.Logger($"{enhancementName} - {completionStatus}");

            // Add missing enhancement to the list
            if (!Core.isCompletedBefore(questID))
            {
                missingEnhancements.Add(enhancementName);
            }
        }

        // Dictionary to map enhancement names to their respective methods
        var enhancementActions = new Dictionary<string, Action>
            {
                // AweBlast & Health Vamp:
                { "Awe Enhancments", Farm.UnlockBoA },

                // Forge:
                { "ForgeWeapon", UnlockForgeEnhancements.ForgeWeaponEnhancement },
                { "Lacerate", UnlockForgeEnhancements.Lacerate },
                { "Smite", UnlockForgeEnhancements.Smite },
                { "Valiance", UnlockForgeEnhancements.HerosValiance },
                { "ArcanasConcerto", UnlockForgeEnhancements.ArcanasConcerto },
                { "Absolution", UnlockForgeEnhancements.Absolution },
                { "Vainglory", UnlockForgeEnhancements.Vainglory },
                { "Avarice", UnlockForgeEnhancements.Avarice },
                { "ForgeCape", UnlockForgeEnhancements.ForgeCapeEnhancement },
                { "Elysium", UnlockForgeEnhancements.Elysium },
                { "Acheron", UnlockForgeEnhancements.Acheron },
                { "Penitence", UnlockForgeEnhancements.Penitence },
                { "Lament", UnlockForgeEnhancements.Lament },
                { "Vim", UnlockForgeEnhancements.Vim },
                { "Examen", UnlockForgeEnhancements.Examen },
                { "ForgeHelm", UnlockForgeEnhancements.ForgeHelmEnhancement },
                { "Pneuma", UnlockForgeEnhancements.Pneuma },
                { "Anima", UnlockForgeEnhancements.Anima },
                { "Dauntless", UnlockForgeEnhancements.DauntLess },
                { "Praxis", UnlockForgeEnhancements.Praxis },
                { "Ravenous", UnlockForgeEnhancements.Ravenous }
            };

        // Handle each missing enhancement using the dictionary
        foreach (var missingEnhancement in missingEnhancements)
        {
            if (enhancementActions.TryGetValue(missingEnhancement, out var action))
            {
                action.Invoke();
            }
            else
            {
                Core.Logger($"Unhandled enhancement: {missingEnhancement}");
            }
        }

        // Use missingEnhancements list later as needed
        // Example usage:
        foreach (var missingEnhancement in missingEnhancements)
        {
            Core.Logger($"Missing Enhancement: {missingEnhancement}");
        }


        // Wspecial x8 (x9 with Hvamp)
        Adv.EnhanceItem(Bot.Config!.Get<string>("Dauntless"), EnhancementType.Lucky, wSpecial: WeaponSpecial.Dauntless);
        Adv.EnhanceItem(Bot.Config!.Get<string>("Valiance"), EnhancementType.Lucky, wSpecial: WeaponSpecial.Valiance);
        Adv.EnhanceItem(Bot.Config!.Get<string>("Arcanas_Concerto"), EnhancementType.Lucky, wSpecial: WeaponSpecial.Arcanas_Concerto);
        Adv.EnhanceItem(Bot.Config!.Get<string>("Awe_Blast"), EnhancementType.Lucky, wSpecial: WeaponSpecial.Awe_Blast);
        Adv.EnhanceItem(Bot.Config!.Get<string>("Praxis"), EnhancementType.Lucky, wSpecial: WeaponSpecial.Praxis);
        Adv.EnhanceItem(Bot.Config!.Get<string>("Ravenous"), EnhancementType.Lucky, wSpecial: WeaponSpecial.Ravenous);
        Adv.EnhanceItem(Bot.Config!.Get<string>("Elysium"), EnhancementType.Lucky, wSpecial: WeaponSpecial.Elysium);
        Adv.EnhanceItem(Bot.Config!.Get<string>("Lacerate"), EnhancementType.Lucky, wSpecial: WeaponSpecial.Lacerate);
        Adv.EnhanceItem(Bot.Config!.Get<string>("HealthVamp"), EnhancementType.Lucky, wSpecial: WeaponSpecial.Health_Vamp);


        // Hspecial x3
        Adv.EnhanceItem(Bot.Config!.Get<string>("Wizard"), EnhancementType.Wizard, hSpecial: HelmSpecial.None);
        Adv.EnhanceItem(Bot.Config!.Get<string>("Luck"), EnhancementType.Lucky, hSpecial: HelmSpecial.None);
        Adv.EnhanceItem(Bot.Config!.Get<string>("ForgeHelm"), EnhancementType.Lucky, hSpecial: HelmSpecial.Forge);

        // Cspecial x5
        Adv.EnhanceItem(Bot.Config!.Get<string>("Absolution"), EnhancementType.Lucky, cSpecial: CapeSpecial.Absolution);
        Adv.EnhanceItem(Bot.Config!.Get<string>("Avarice"), EnhancementType.Lucky, cSpecial: CapeSpecial.Avarice);
        Adv.EnhanceItem(Bot.Config!.Get<string>("Penitence"), EnhancementType.Lucky, cSpecial: CapeSpecial.Penitence);
        Adv.EnhanceItem(Bot.Config!.Get<string>("Vainglory"), EnhancementType.Lucky, cSpecial: CapeSpecial.Vainglory);
        Adv.EnhanceItem(Bot.Config!.Get<string>("Lament"), EnhancementType.Lucky, cSpecial: CapeSpecial.Lament);
        #endregion Enhancment Unlock Checking

        #region  Potions & Scrolls
        // Buy potions and scrolls
        PotionBuyer.INeedYourStrongestPotions(new[] { "Potent Malevolence Elixir" }, new bool[] { true }, 300, true, true);
        Scroll.BuyScroll(Scrolls.Enrage, 1000);
        Adv.BuyItem("terminatemple", 2328, "Scroll of Life Steal", 99);
        #endregion  Potions & Scrolls
    }


    public void RemoveLineIfContains(string fileName, string lineToRemove)
    {
        // Construct the full file path
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Skua", fileName);

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            // Find the index of the line to remove
            int indexToRemove = Array.FindIndex(lines, line => line.Contains(lineToRemove));

            // If the line exists, remove it
            if (indexToRemove != -1)
            {
                // Create a new array without the line to remove
                string[] newLines = new string[lines.Length - 1];
                int j = 0;
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i != indexToRemove)
                    {
                        newLines[j++] = lines[i];
                    }
                }

                // Write the modified contents back to the file
                File.WriteAllLines(filePath, newLines);
                Core.Logger($"Line containing '{lineToRemove}' removed from the file. This will make the OTM Option Reappear.");
            }
            else
            {
                Core.Logger($"Line containing '{lineToRemove}' not found in the file");
            }
        }
        else
        {
            Core.Logger($"File '{filePath}' not found.");
        }
    }
}