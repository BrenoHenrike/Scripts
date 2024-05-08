/*
name: Gravelyn's DoomFire Token
description: This script will get items from Gravelyn's DoomFire shop.
tags: gravelyn, doomfire, token, doomfire of gravelyn, horned doomfire helm, evil
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class GravelynDoomFire
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BuyItems();

        Core.SetOptions(false);
    }

    public string[] GravelynsDoomFireTokenItems =
    {
        "Empowered Essence",
        "Gravelyn's Blessing",
        "Painful Memory Bubble",
        "Burning Passion Flame",
        "Father's Sorrowful Tear",
    };

    public void BuyItems()
    {
        // Items to buy from the shop
        string[] shopItems = { "DOOMFire OF Gravelyn", "Horned DOOMFire Helm" };

        // If we already have the shop items, return
        if (Core.CheckInventory(shopItems))
            return;

        // Add the drop items to inventory
        Core.AddDrop(GravelynsDoomFireTokenItems.Concat(shopItems).ToArray());

        while (!Bot.ShouldExit && !Core.CheckInventory(shopItems))
        {
            // Check for "Gravelyn's DoomFire Token"
            if (!Core.CheckInventory(37033))
            {
                // Check for "Gravelyn's Blessing"
                if (!Core.CheckInventory(37034))
                {
                    if (Core.CheckInventory("Necrotic Sword of Doom"))
                        Core.ChainComplete(5455);
                    else if (Core.CheckInventory("Sepulchure's DoomKnight Armor"))
                        Core.ChainComplete(5456);
                    else
                    {
                        // Handle the case when none of the above conditions match
                        Farm.EvilREP(10);
                        Core.EnsureAccept(5457);
                        Core.HuntMonsterMapID("necrodungeon", 9, "Essence of the Doomlord");
                        Core.EnsureComplete(5457);
                        Bot.Wait.ForPickup(37034);
                    }
                }

                // Check for "Painful Memory Bubble"
                if (!Core.CheckInventory("Painful Memory Bubble"))
                {
                    Core.EnsureAcceptmultiple(false, new[] { 5458, 5459, 5460, 5461 });
                    Core.KillMonster("swordhavenfalls", "r10", "Left", 1295, "Doomed Memories");
                    Core.EnsureComplete(5458);
                    Bot.Wait.ForPickup("Painful Memory Bubble");
                }

                // Check for "Burning Passion Flame"
                if (!Core.CheckInventory("Burning Passion Flame"))
                {
                    Core.HuntMonster("shadowstrike", "Sepulchuroth", "Sepulchuroth's Undying Flame");
                    Core.EnsureComplete(5459);
                    Bot.Wait.ForPickup("Burning Passion Flame");
                }

                // Check for "Father's Sorrowful Tear"
                if (!Core.CheckInventory("Father's Sorrowful Tear"))
                {
                    Core.HuntMonster("Shadowfall", "Shadow of the Past", "Father's Anger");
                    Core.EnsureComplete(5460);
                    Bot.Wait.ForPickup("Father's Sorrowful Tear");
                }

                // Kill monsters for "Empowered Essence"
                Core.KillMonster("shadowrealmpast", "Enter", "Spawn", "*", "Empowered Essence", 13, isTemp: false);

                // Ensure completion of the main quest
                if (GravelynsDoomFireTokenItems.Take(6).All(item => Core.CheckInventory(item)) && Core.CheckInventory("Empowered Essence", 13))
                    Core.EnsureComplete(5461);
                Bot.Wait.ForPickup(37033);
            }

            // Buy missing shop items and send them to the bank
            foreach (string item in shopItems)
            {
                if (!Core.CheckInventory(item) && Core.CheckInventory(37033))
                    Core.BuyItem("darkthronehub", 1307, item);
                //Loop it back if no Doomfire Tokens
                else break;
            }

            Core.ToBank(shopItems);
        }
    }
}

