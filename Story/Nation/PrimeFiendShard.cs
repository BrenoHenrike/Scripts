/*
name: PrimeFiendShardQuests
description: This script will complete the storyline from the prime fiend shard
tags: prime fiend shard, nation, ravenous, fiend shard, nulgath, voidchasm, void chasm
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
//cs_include Scripts/Nation/Various/ArchfiendDeathLord.cs
//cs_include Scripts/Nation/MergeShops/VoidChasmMerge.cs
//cs_include Scripts/Story/Nation/VoidChasm.cs
//cs_include Scripts/Nation/MergeShops/NationMerge.cs
//cs_include Scripts/Nation\NationLoyaltyRewarded.cs
using Skua.Core.Interfaces;


public class PrimeFiendShard
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    private CoreAdvanced Adv = new();
    public Originul_Story Originul = new();
    public CoreVHL VHL = new();
    public CoreNation Nation = new();
    public VoidRefugeMerge VoidRefugeMerge = new();
    public TempleDelveMerge TempleDelveMerge = new();
    public DirtlickersMerge DirtlickersMerge = new();
    public VoidPaladin VoidPaladin = new();
    public NulgathDiamondMerge NulgathDiamondMerge = new();
    public VoidSpartan VoidSpartan = new();
    public SwirlingTheAbyss SwirlingTheAbyss = new();
    public TradingandStuffSingle TradingandStuffSingle = new();
    public VoidAvengerScythe VoidAvengerScythe = new();
    public NulgathDemandsWork NulgathDemandsWork = new();
    public ArchfiendDeathLord ArchfienddDeathLord = new();
    public WrathofNulgath WrathofNulgath = new();
    public DilligasMerge DilligasMerge = new();
    public VoidChasmMerge VoidChasmMerge = new();
    public NationMerge NationMerge = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(9559))
            return;

        Core.Logger("If you're going to use insingias for the roents\n" +
        "please do so before starting the script, as it will stop otherwise if dont have them.");

        VHL.GetVHL();

        // Prime Fiend Shard [required to accept]

        VoidChasmMerge.BuyAllMerge("Prime Fiend Shard");

        // Feed the Fiend Shard 9555
        if (!Story.QuestProgression(9555))
        {
            Core.EnsureAccept(9555);
            if (!Core.CheckInventory("Dual Dragonbone Axe of Nulgath"))
            {
                Core.AddDrop("Dual Dragonbone Axe of Nulgath");
                Core.Logger("Farming Dual Dragonbone Axe of Nulgath.");
                // Combat Style: Dragonbone Axe 629
                Core.EnsureAccept(629);
                Nation.FarmUni13(1);
                Nation.FarmTaintedGem(13);
                Nation.FarmDarkCrystalShard(13);
                Nation.FarmDiamondofNulgath(13);
                Nation.Supplies(Nation.Uni(21));
                Core.HuntMonster("evilmarsh", "Tainted Elemental", "Tainted Rune of Evil", log: false);
                Core.EnsureComplete(629);
            }
            Nation.EssenceofNulgath(20);
            NationMerge.BuyAllMerge("Nation Soulstealer");
            TempleDelveMerge.BuyAllMerge("Void Nation Caster");
            DirtlickersMerge.BuyAllMerge("Iron Dreadsaw");
            Core.EnsureComplete(9555);
        }

        // Grow the Fiend Shard 9556
        if (!Story.QuestProgression(9556))
        {
            Core.EnsureAccept(9556);
            Nation.EssenceofNulgath(40);
            NulgathDiamondMerge.BuyAllMerge("Abyssal Priest of Nulgath");
            NulgathDiamondMerge.BuyAllMerge("Storm Knight");
            VoidPaladin.DeeperandDeeperintoDarkness();
            VoidSpartan.GetSpartan("Void Spartan");
            Core.EnsureComplete(9556);
        }

        // Evolve the Fiend Shard 9557
        if (!Story.QuestProgression(9557))
        {
            Core.EnsureAccept(9557);
            SwirlingTheAbyss.STA("Void Soul of Nulgath");
            TradingandStuffSingle.ArchFiendEnchantedOrbs();
            NulgathDiamondMerge.BuyAllMerge("Blood Ranger");
            VoidRefugeMerge.BuyAllMerge("Envenomed Edge of Nulgath");
            Nation.EssenceofNulgath(60);
            Core.EnsureComplete(9557);
        }

        // The Fiend Shard's Demand 9558
        if (!Story.QuestProgression(9558))
        {
            Core.EnsureAccept(9558);
            VoidAvengerScythe.SkewsQuests();
            NulgathDemandsWork.NDWQuest(new[] { "Doomblade of Destruction" });
            ArchfienddDeathLord.GetArm(true, ArchfiendDeathLord.RewardChoice.Archfiend_DeathLord);
            WrathofNulgath.GetSword();
            DilligasMerge.BuyAllMerge("Ancient Shogun Armor");
            Core.EnsureComplete(9558);
        }

        // The Fiend Shard's Finale 9559
        if (!Story.QuestProgression(9559))
        {
            Core.EnsureAccept(9559);
            Nation.FarmUni13(1);
            Nation.FarmTaintedGem(750);
            Nation.FarmDarkCrystalShard(400);
            Nation.FarmDiamondofNulgath(1000);
            Nation.FarmTotemofNulgath(60);
            Nation.FarmGemofNulgath(300);
            Nation.FarmBloodGem(100);
            while (!Bot.ShouldExit && (!Core.CheckInventory("Roentgenium of Nulgath", 10) && Core.CheckInventory("Elders' Blood")))
                VHL.VHLChallenge(10);
            Core.EnsureAccept(9559);
        }
    }
}