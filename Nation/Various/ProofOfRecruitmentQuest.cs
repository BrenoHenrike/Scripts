/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs

using Skua.Core.Interfaces;

public class ProofOFRecruitmentQuest
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();

    string[] RequiredItems = { "ArchToken I", "ArchToken II", "Portal Key", "ArchToken III", "ArchToken IV", "ArchToken V", "ArchToken VI", "ArchToken VII", "ArchToken VIII", "ArchToken IX", "ArchToken X", "ArchToken XI" };
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        Core.BankingBlackList.AddRange(RequiredItems);

        CompleteQuests();

        Core.SetOptions(false);
    }

    public void CompleteQuests()
    {
        string[] Rewards = { "Tainted Gem", "Dark Crystal Shard", "Diamond of Nulgath", "Gem of Nulgath", "Unidentified 13" };

        Core.AddDrop(RequiredItems);
        Core.AddDrop(Rewards);

        for (int i = 0; i < Rewards.Length; i++)
        {
            if (Bot.Inventory.IsMaxStack(Rewards[i]))
                Core.Logger($"{Rewards[i]} is max stack Checking next item in the \"Proof of Recruitment\" Quest's Rewards");
            else
            {
                Core.Logger($"farming {Rewards[i]} till max stack from  \"Proof of Recruitment Quest\"");

                if (!Core.CheckInventory("ArchToken XI"))
                {
                    if (!Core.CheckInventory("ArchToken X"))
                    {
                        if (!Core.CheckInventory("ArchToken IX"))
                        {
                            if (!Core.CheckInventory("ArchToken VIII"))
                            {
                                if (!Core.CheckInventory("ArchToken VII"))
                                {
                                    if (!Core.CheckInventory("ArchToken VI"))
                                    {
                                        if (!Core.CheckInventory("ArchToken V"))
                                        {
                                            if (!Core.CheckInventory("ArchToken IV"))
                                            {
                                                if (!Core.CheckInventory("ArchToken III"))
                                                {
                                                    if (!Core.CheckInventory("ArchToken II"))
                                                    {
                                                        if (!Core.CheckInventory("ArchToken I"))
                                                        {
                                                            //Dirtlicker's Test 4751 [ArchToken I]
                                                            Core.EnsureAccept(4751);
                                                            Core.HuntMonster("ArchPortal", "Skull Warrior", "Monsters Slain", 8);
                                                            Core.EnsureComplete(4751);
                                                        }
                                                        //Souls for the Nation 4752 [ArchToken II]
                                                        Core.EnsureAccept(4752);
                                                        if (!Core.CheckInventory("Doomwood Token", 5))
                                                            Core.Logger($"Can't Finish The quest as it requires \"Doomwood Token x5 [temp item]\"  which can only be obtained from DoomWood PVP Arena", messageBox: true, stopBot: true);
                                                        Core.EnsureComplete(4752);
                                                    }
                                                    //More Souls! 4753 [ArchToken III]
                                                    Core.EnsureAccept(4753);
                                                    Farm.BludrutBrawlBoss(quant: 30);
                                                    Core.EnsureComplete(4753);
                                                }
                                                //Legion Scum 4754 [ArchToken IV]
                                                Core.EnsureAccept(4754);
                                                Core.HuntMonster("ArchPortal", "Legion Spy", "Defeated Legion Spies", 8);
                                                Core.EnsureComplete(4754);
                                            }
                                            //Traitors 4755 [ArchToken V]
                                            Core.EnsureAccept(4755);
                                            Core.HuntMonster("Tercessuinotlim", "Death's Head", "Death's Head Head");
                                            Core.HuntMonster("EvilWarDage ", "Infernalfiend", "Infernal Fiend Head");
                                            Core.HuntMonster("EvilWarNul", "Nulgath's Redemption", "Nulgath's Redemption Head");
                                            Core.EnsureComplete(4755);
                                        }
                                        //The Evil War 4756 [ArchToken VI]
                                        Core.EnsureAccept(4756);
                                        Core.HuntMonster("EvilWarNul", "Blade Master", "Blade Master's Blood");
                                        Core.HuntMonster("EvilWarNul", "Undead Legend", "Legion's Crown");
                                        Core.HuntMonster("EvilWarNul", "Laken", "Saber of the Traveler");
                                        Core.EnsureComplete(4756);
                                    }
                                    //Eye Spy 4757 [ArchToken VII]
                                    Core.EnsureAccept(4757);
                                    Core.GetMapItem(4178, 8, "ArchPortal");
                                    Core.HuntMonster("Void", "Void Elemental", "Void Energy", 8);
                                    Core.EnsureComplete(4757);
                                }
                                //Souls of the Legion 4758 [ArchToken VIII]
                                Core.EnsureAccept(4758);
                                Core.HuntMonster("ArchPortal", "Skull Warrior", "Dusty Bone Marrow", 5);
                                Core.HuntMonster("ArchPortal", "Legion Guard", "Mummified Gray Matter", 5);
                                Core.EnsureComplete(4758);
                            }
                            //Put the Lime in the Coconut 4759 [ArchToken IX]
                            Core.EnsureAccept(4759);
                            Core.GetMapItem(4179, 12, "ArchPortal");
                            Core.HuntMonster("ArchPortal", "Skull Warrior", "Legion Skull");
                            Core.EnsureComplete(4759);
                        }
                        //Hungry, Hungry Portals 4760 [ArchToken X]
                        Core.EnsureAccept(4760);
                        Core.HuntMonster("ArchPortal", "Legion Guard", "Undead Guards Souls", 8);
                        Core.HuntMonster("ArchPortal", "Skull Warrior", "Skull Warriors Souls", 10);
                        Core.HuntMonster("ArchPortal", "Legion Spy", "Hidden Legion Spy Souls", 6);
                        Core.HuntMonster("Tercessuinotlim", "Death's Head", "Soul of the Death's Head");
                        Core.EnsureComplete(4760);
                    }
                    //Protect the Portal! 4761 [ArchToken XI]
                    Core.EnsureAccept(4761);
                    Core.HuntMonster("ArchPortal", "High Legion Inquisitor", "High Legion Inquisitor Defeated");
                    Core.EnsureComplete(4761);
                }
                //Proof of Recruitment 4785 [Rewards quest]
                while (!Bot.Inventory.IsMaxStack(Rewards[i]))
                {
                    Core.EnsureAccept(4785);
                    Core.KillMonster("shadowblast", "r13", "Left", "*", "Fiend Seal", 15, false);
                    Nation.EmblemofNulgath(1);
                    Core.HuntMonster("ArchPortal", "Skull Warrior", "Defeat the TR8T0R", 15);
                    Core.EnsureComplete(4785);
                }
            }
        }
    }
}
