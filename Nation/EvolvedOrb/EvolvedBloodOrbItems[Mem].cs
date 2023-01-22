/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/EvolvedOrb/EvolvedBloodOrb.cs
//cs_include Scripts/Nation/Various/JuggernautItems.cs
using Skua.Core.Interfaces;

public class EvolvedBloodOrbItems
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv = new CoreAdvanced();
    private CoreFarms Farm = new CoreFarms();
    private CoreNation Nation = new();
    private EvolvedBloodOrb EBO = new();
    public JuggernautItemsofNulgath juggernaut = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetItems();

        Core.SetOptions(false);
    }

    public void GetItems()
    {
        if (!Core.IsMember || Core.CheckInventory(Rewards, toInv: false))
            return;

        EBO.GetEvolvedBloodOrb();
        if (Core.CheckInventory(33196)) //recheck
            return;

        FlowLikeBlood();
        BloodTears();

        //yet to be added:
        //Flow Like Blood(AC)
        //Flow Like Blood(None-AC)
    }

    public void FlowLikeBlood()
    {
        #region Required Items
        if (!Core.CheckInventory("1v1 PvP Trophy", 10))
            Core.Logger("You do not own \"10x 1v1 PvP Trophies\", stopping", stopBot: true);

        Core.AddDrop("Evolved Blood of Nulgath");
        if (!Core.CheckInventory("Platinum Coin of Nulgath: 2500") || !Core.CheckInventory(33196))
            return;

        if (!Core.CheckInventory("RustBucket"))
        {
            Core.EnsureAccept(126);
            Core.HuntMonster("crashsite", "ProtoSatorium", "ProtoSartorium  Parts", 5);
            Core.EnsureComplete(126);
            Bot.Wait.ForPickup("RustBucket");
        }
        Adv.rankUpClass("RustBucket");

        #endregion

        // Flow Like Blood - 4780
        Core.EnsureAccept(4780);

        if (!Core.CheckInventory("Crimson Plate of Nulgath"))
        {
            Core.EnsureAccept(765);
            Nation.FarmTotemofNulgath(3);
            Core.HuntMonster("underworld", "Skull Warrior", "Skull Warrior Rune");
            Core.EnsureComplete(765, 4961);
        }
        Nation.FarmUni13(3);
        Nation.TheAssistant("Unidentified 25");
        juggernaut.JuggItems(reward: JuggernautItemsofNulgath.RewardsSelection.Dimensional_Championof_Nulgath);
        if (Core.IsMember)
            Nation.ForgeBloodGems(20);
        else Nation.FarmBloodGem(20);

        Core.EnsureComplete(4780);
        Bot.Wait.ForPickup("Evolved Blood of Nulgath");
    }

    public void BloodTears()
    {
        //Void Emotion 4774
        if (!Core.CheckInventory("Platinum Coin of Nulgath: 300") || !Core.CheckInventory("Evolved Blood Orb"))
            return;


        Core.EnsureAccept(4783);

        Nation.FarmDiamondofNulgath(10);
        Nation.FarmVoucher(false);
        Nation.FarmBloodGem(5);
        Nation.FarmTotemofNulgath(1);
        Nation.FarmBloodGem(5);

        Core.EnsureComplete(4783);
        Bot.Wait.ForPickup("Evolved Blood Guard");
    }

    private string[] Rewards =
    {
        "Evolved Blood of Nulgath",
        "Evolved Blood Guard",
    };
}
