/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class BlazeBeard
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        Core.BankingBlackList.AddRange(RequiredItems);

        TokenQuests();

        Core.SetOptions(false);
    }

    string[] RequiredItems = { "Pirate Booty I", "Pirate Booty II", "Pirate Booty III", "Pirate Booty IV", "Pirate Booty V", "Pirate Booty VI", "Pirate Booty VII", "Pirate Booty VIII", "Pirate Booty IX", "Pirate Booty X", "Pirate Booty XI", "Pirate Booty XII", "Pirate Booty XIII", "Pirate Booty XIV", "Pirate Booty XV" };

    public void TokenQuests()
    {
        if (!Core.isSeasonalMapActive("BlazeBeard"))
            return;

        if (Core.CheckInventory("Pirate Booty XV", toInv: false))
            return;


        Story.PreLoad(this);

        Core.AddDrop(RequiredItems);


        if (!Core.CheckInventory("Pirate Booty XV"))
        {
            if (!Core.CheckInventory("Pirate Booty XIV"))
            {
                if (!Core.CheckInventory("Pirate Booty XIII"))
                {
                    if (!Core.CheckInventory("Pirate Booty XII"))
                    {
                        if (!Core.CheckInventory("Pirate Booty XI"))
                        {
                            if (!Core.CheckInventory("Pirate Booty X"))
                            {
                                if (!Core.CheckInventory("Pirate Booty IX"))
                                {
                                    if (!Core.CheckInventory("Pirate Booty VIII"))
                                    {
                                        if (!Core.CheckInventory("Pirate Booty VII"))
                                        {
                                            if (!Core.CheckInventory("Pirate Booty VI"))
                                            {
                                                if (!Core.CheckInventory("Pirate Booty V"))
                                                {
                                                    if (!Core.CheckInventory("Pirate Booty IV"))
                                                    {
                                                        if (!Core.CheckInventory("Pirate Booty III"))
                                                        {
                                                            if (!Core.CheckInventory("Pirate Booty II"))
                                                            {
                                                                if (!Core.CheckInventory("Pirate Booty I"))
                                                                {
                                                                    //Scrap Metal 4513 [Pirate Booty I]
                                                                    Core.EnsureAccept(4513);
                                                                    Core.HuntMonster("CrashSite", "Barrier Bot|Dwakel Blaster", "Dwakel Scrap Metal", 10);
                                                                    Core.EnsureComplete(4513);
                                                                }
                                                                //Mana Holder 4514 [Pirate Booty II]
                                                                Core.EnsureAccept(4514);
                                                                Core.HuntMonster("Mafic", "Living Fire", "Mysterious Molten Metal", 12);
                                                                Core.EnsureComplete(4514);
                                                            }
                                                            //A Scoop Full of Mana 4515 [Pirate Booty III]
                                                            Core.EnsureAccept(4515);
                                                            Core.HuntMonster("Elemental", "Mana Falcon", "Mana Droplets", 12);
                                                            Core.EnsureComplete(4515);
                                                        }
                                                        //Test it for… Magic? 4516 [Pirate Booty IV]
                                                        Core.EnsureAccept(4516);
                                                        Core.HuntMonster("EarthStorm", "Earth Elemental", "No Explosion", 15);
                                                        Core.EnsureComplete(4516);
                                                    }
                                                    //Shadow Shroud Potion 4517 [Pirate Booty V]
                                                    Core.EnsureAccept(4517);
                                                    Core.HuntMonster("BlazeBeard", "Undead Pirate", "Darkness Energy", 8);
                                                    Core.EnsureComplete(4517);
                                                }
                                                //Plunder Their Supplies 4518 [Pirate Booty VI]
                                                Core.EnsureAccept(4518);
                                                Core.GetMapItem(3725, 12, "BlazeBeard");
                                                Core.EnsureComplete(4518);
                                            }
                                            //Crew Crushing 4519 [Pirate Booty VII]
                                            Core.EnsureAccept(4519);
                                            Core.HuntMonster("BlazeBeard", "Pirate Crew", "Pirate Crew Defeated", 15);
                                            Core.EnsureComplete(4519);
                                        }
                                        //Plunder Plans 4520 [Pirate Booty VIII]
                                        Core.EnsureAccept(4520);
                                        Core.HuntMonster("BlazeBeard", "Pirate Plunderer", "Blazebeard's Plans", 5);
                                        Core.EnsureComplete(4520);
                                    }
                                    //Keep On Battling 4521 [Pirate Booty IX]
                                    Core.EnsureAccept(4521);
                                    Core.HuntMonster("BlazeBeard", "Pirate Crew", "Pirate Crew Defeated", 10);
                                    Core.HuntMonster("BlazeBeard", "Pirate Plunderer", "Plunderer Defeated", 5);
                                    Core.EnsureComplete(4521);
                                }
                                //Kill the Captain 4522 [Pirate Booty X]
                                Core.EnsureAccept(4522);
                                Core.HuntMonster("BlazeBeard", "Pirate Captain", "Captain Defeated");
                                Core.EnsureComplete(4522);
                            }
                            //Hand Cannon Heist 4523 [Pirate Booty XI]
                            Core.EnsureAccept(4523);
                            Core.HuntMonster("ManaCannon", "Pirate Gunner", "Hand Cannons", 6);
                            Core.EnsureComplete(4523);
                        }
                        //Interrogation 4524 [Pirate Booty XII]
                        Core.EnsureAccept(4524);
                        Core.HuntMonster("ManaCannon", "Pirate Plunderer", "Location Researched", 10);
                        Core.EnsureComplete(4524);
                    }
                    //Conquer His Crew 4525 [Pirate Booty XIII] 
                    Core.EnsureAccept(4525);
                    Core.HuntMonster("ManaCannon", "Pirate Crew", "Crew Defeated", 15);
                    Core.EnsureComplete(4525);
                }
                //Pirates With Magic? 4526 [Pirate Booty XIV]
                Core.EnsureAccept(4526);
                Core.HuntMonster("ManaCannon", "Pirate Caster", "Pirate Caster Defeated", 15);
                Core.EnsureComplete(4526);
            }
            //Douse Blazebeard 4527 [Pirate Booty XV]
            Core.EnsureAccept(4527);
            Core.HuntMonster("ManaCannon", "Blazebeard", "Blazebeard Doused");
            Core.EnsureComplete(4527);
        }
    }

}
