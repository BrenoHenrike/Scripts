/*
name: null
description: null
tags: null
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
    public string[] GravelynsDoomFireTokenItems = { "Empowered Essence", "Gravelyn's Blessing", "Painful Memory Bubble", "Burning Passion Flame", "Father's Sorrowful Tear", "Gravelyn's DoomFire Token", "Necrotic Sword of Doom", "Sepulchure's DoomKnight Armor" };
    public void BuyItems()
    {
        string[] ShopItems = { "DOOMFire OF Gravelyn", "Horned DOOMFire Helm" };

        if (Core.CheckInventory(ShopItems))
            return;

        Core.AddDrop(GravelynsDoomFireTokenItems);
        if (!Core.CheckInventory("Gravelyn's DoomFire Token"))
        {
            while (!Bot.ShouldExit && !Core.CheckInventory("Gravelyn's Blessing"))
            {
                if (Core.CheckInventory("Necrotic Sword of Doom"))
                    //A Loyal Servant: Necrotic Sword of Doom 5455
                    Core.ChainComplete(5455);
                else if (Core.CheckInventory("Sepulchure's DoomKnight Armor"))
                    //A Loyal Servant: Sepulchure’s DoomKnight 5456
                    Core.ChainComplete(5456);
                else
                {
                    //Let Darkness Enter your Heart 5457 
                    Farm.EvilREP(10);
                    Core.EnsureAccept(5457);
                    Core.HuntMonster("necrodungeon", "Doom Overlord", "Essence of the Doomlord");
                    Core.EnsureComplete(5457);
                }
                Bot.Wait.ForPickup("Gravelyn's Blessing");

            }
            //Find me some Doom 5458
            Core.EnsureAccept(5458, 5459, 5460, 5461);
            Core.KillMonster("swordhavenfalls", "r10", "Left", 1295, "Doomed Memories");
            Core.EnsureComplete(5458);

            //Abyssal 5459
            Bot.Wait.ForPickup("Painful Memory Bubble");
            Core.HuntMonster("shadowstrike", "Sepulchuroth", "Sepulchuroth's Undying Flame");
            Core.EnsureComplete(5459);
            Bot.Wait.ForPickup("Burning Passion Flame");

            //Memories of The Past 5460
            Core.HuntMonster("Shadowfall", "Shadow of the Past", "Father's Anger");
            Core.EnsureComplete(5460);
            Bot.Wait.ForPickup("Father's Sorrowful Tear");

            //The Summoning 5461
            Core.HuntMonster("shadowrealmpast", "*", "Empowered Essence", 13, isTemp: false);
            Core.EnsureComplete(5461);
            Bot.Wait.ForPickup("Gravelyn's DoomFire Token");
        }

        Core.BuyItem("darkthronehub", 1307, "DOOMFire OF Gravelyn");
        Core.BuyItem("darkthronehub", 1307, "Horned DOOMFire Helm");
        Core.ToBank(ShopItems);

    }
}
