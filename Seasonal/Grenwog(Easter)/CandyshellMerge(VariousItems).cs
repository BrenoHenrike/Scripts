/*
name: Candyshell Merge Materials
description: Farms all of the candyshell materials at max quantity.
tags: candyshell-merge, seasonal, easter, materials, mats
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class CandyshellMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetMergeItems();

        Core.SetOptions(false);
    }

    public void GetMergeItems()
    {
        //Needed AddDrop
        Core.AddDrop("Caramel Eggshells", "Anti-Neggshells", "Shadow Eggshells", "Creme Eggshells", "Chocolate Eggshells", "Rainbow Eggshells", "Chaotic Eggshells", "Golden Eggshells");

        while (!Bot.ShouldExit && !Core.CheckInventory(new[] { "Caramel Eggshells", "Anti-Neggshells", "Shadow Eggshells", "Creme Eggshells", "Chocolate Eggshells", "Rainbow Eggshells", "Chaotic Eggshells", "Golden Eggshells" }, 150))
        {
            Core.EquipClass(ClassType.Farm);

            //Caramel Eggshells
            if (!Core.CheckInventory("Caramel Eggshells", 150))
            {
                Core.Join("GreenguardWest");
                for (int i = 0; i < 10; i++)
                    Bot.Hunt.Monster("Kittarian");
            }

            //Anti-Neggshells
            if (!Core.CheckInventory("Anti-Neggshells", 150) && Core.IsMember)
            {
                Core.Join("Greymoor");
                for (int i = 0; i < 10; i++)
                    Bot.Hunt.Monster("Spooky Treeant");
            }

            //Creme Eggshells
            if (!Core.CheckInventory("Creme Eggshells", 150))
            {
                Core.Join("GreenShell");
                for (int i = 0; i < 10; i++)
                    Bot.Hunt.Monster("Tsukumogami");
            }

            //Chocolate Eggshells
            if (!Core.CheckInventory("Chocolate Eggshells", 150))
            {
                Core.Join("GreenguardEast");
                for (int i = 0; i < 10; i++)
                    Bot.Hunt.Monster("Gurushroom");
            }

            //Rainbow Eggshells
            if (!Core.CheckInventory("Rainbow Eggshells", 150))
            {
                Core.Join("Greendragon");
                for (int i = 0; i < 10; i++)
                    Bot.Hunt.Monster("Greenguard Dragon");
            }

            //Chaotic Eggshells
            if (!Core.CheckInventory("Chaotic Eggshells", 150))
            {
                Core.Join("Grenstory");
                for (int i = 0; i < 10; i++)
                    Bot.Hunt.Monster("Imposter Egg");
            }

            //Golden Eggshells
            if (!Core.CheckInventory("Golden Eggshells", 150))
            {
                Core.Join("Greed");
                for (int i = 0; i < 10; i++)
                    Bot.Hunt.Monster("Treasure Pile");
            }

            //Shadow Eggshells
            if (!Core.CheckInventory("Shadow Eggshells", 150))
            {
                Core.Join("Grenwog", publicRoom: true);
                Core.EquipClass(ClassType.Solo);
                for (int i = 0; i < 10; i++)
                    Bot.Hunt.Monster("Grenwog");
            }
        }
    }
}



