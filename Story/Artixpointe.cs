/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Artixpointe
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        OmniArtifact();

        Core.SetOptions(false);
    }

    public void OmniArtifact()
    {
        if (Core.CheckInventory("Omni Artifact"))
            return;

        Story.PreLoad(this);

        if (!Core.CheckInventory("Unholy Wasabi"))
        {
            //Find the Unholy Wasabi 3800
            Core.AddDrop("Unholy Wasabi");
            Core.EnsureAccept(3800);
            Core.HuntMonster("artixpointe", "Corrupted Sushi Chef", "Find Unholy Wasabi");
            Core.EnsureComplete(3800);
            Bot.Wait.ForPickup("Unholy Wasabi");
        }

        if (!Core.CheckInventory("Cysero's Doom Sock"))
        {
            //Find Cysero's Doom Sock 3801
            Core.AddDrop("Cysero's Doom Sock");
            Core.EnsureAccept(3801);
            Core.GetMapItem(2911, 1, "artixpointe");
            Core.EnsureComplete(3801);
            Bot.Wait.ForPickup("Cysero's Doom Sock");
        }

        if (!Core.CheckInventory("Chickencow Claw"))
        {
            //Recover Cursed ChickenCow Claw 3802
            Core.AddDrop("Chickencow Claw");
            Core.EnsureAccept(3802);
            Core.HuntMonster("battlefowl", "Chickencow", "Cursed Claw Found");
            Core.EnsureComplete(3802);
            Bot.Wait.ForPickup("Chickencow Claw");
        }

        if (!Core.CheckInventory("Zorbak's Staff Skull"))
        {
            //Find Zorbak’s Haunted Staff 3803
            Core.AddDrop("Zorbak's Staff Skull");
            Core.EnsureAccept(3803);
            Core.HuntMonster("graveyard", "Big Jack Sprat", "Zorbak's Skull");
            Core.EnsureComplete(3803);
            Bot.Wait.ForPickup("Zorbak's Staff Skull");
        }

        if (!Core.CheckInventory("Dragon Khan's Corrupt Scepter"))
        {
            //The Dragon Khan’s Corrupt Scepter 3804
            Core.AddDrop("Dragon Khan's Corrupt Scepter");
            Core.EnsureAccept(3804);
            Core.HuntMonster("vendorbooths", "Dragon Khan", "Corrupt Scepter Found");
            Core.EnsureComplete(3804);
            Bot.Wait.ForPickup("Dragon Khan's Corrupt Scepter");
        }

        if (!Core.CheckInventory("Sir Ver's Broken Power Button"))
        {
            //Locate Sir Ver’s Power Button 3805
            Core.AddDrop("Sir Ver's Broken Power Button");
            Core.EnsureAccept(3805);
            Core.HuntMonster("undergroundlabb", "Rabid Server Hamster", "Cursed Power Button");
            Core.EnsureComplete(3805);
            Bot.Wait.ForPickup("Sir Ver's Broken Power Button");
        }

        if (!Core.CheckInventory("Death's Cursed Hourglass"))
        {
            if (!Core.isCompletedBefore(3807))
            {
                //Battle Gravefang 3806
                Core.EnsureAccept(3806);
                Core.HuntMonster("gravefang", "Gravefang", "Gravefang Defeated");
                Core.EnsureComplete(3806);
            }
            //Recover Death's Hourglass 3807
            Core.AddDrop("Death's Cursed Hourglass");
            Core.EnsureAccept(3807);
            Core.GetMapItem(2912, 1, "artixpointe");
            Core.EnsureComplete(3807);
            Bot.Wait.ForPickup("Death's Cursed Hourglass");
        }
        Core.BuyItem("artixpointe", 1002, "Omni Artifact");
    }
}
