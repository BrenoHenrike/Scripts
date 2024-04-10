/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Quests;

public class DefaultTemplate
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    private CoreAdvanced Adv = new();
    private CoreFarms Farm = new();
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Example();

        Core.SetOptions(false);
    }

    public void Example()
    {
        if (Core.CheckInventory("item", 1))
            return;


        Core.RegisterQuests(000);
        while (!Bot.ShouldExit && Core.CheckInventory("item", 1))
        {
            Core.HuntMonster("map", "mob", "item", 1, isTemp: false, log: false);
            Core.HuntMonsterMapID("map", 1, "item", 1, isTemp: false, log :false);
            Core.KillMonster("map", "cell", "pad", "mob", "item", 1, isTemp: false, log: false);
        }
        Core.CancelRegisteredQuests();
    }

    //vv keeping this hre for an example vv
    // public void IsMonsterAliveConversion()
    // {
    //     //vv required for CellKill vv
    //     Core.Join("map", "cell", "pad");
    //     //^^ required for CellKill ^^

    //     Quest? quest = Bot.Quests.EnsureLoad(0000);
    //     ItemBase? Item = quest!.Requirements.FirstOrDefault(x => x.Name == "Desired item");

    //     #region Map Wide (sort of a Suedo-Hunt)
    //     while (!Bot.ShouldExit && !Core.CheckInventory(Item!.Name, Item.Quantity))
    //     {
    //         foreach (Monster mob in Bot.Monsters.MapMonsters)
    //         {
    //             while (!Bot.ShouldExit && Bot.Player.Cell != mob.Cell)
    //             {
    //                 Core.Jump(mob.Cell, "Left");
    //                 Core.Sleep();
    //             }

    //             Bot.Kill.Monster(mob.MapID);

    //             if (Core.CheckInventory(Item!.Name, Item.Quantity))
    //                 break;
    //         }
    //     }
    //     #endregion Map Wide (sort of a Suedo-Hunt)


    //     #region CellKill (strict)
    //     while (!Bot.ShouldExit && !Core.CheckInventory(Item!.Name, Item.Quantity))
    //     {
    //         foreach (Monster mob in Bot.Monsters.CurrentAvailableMonsters)
    //         {
    //             while (!Bot.ShouldExit && Bot.Player.Cell != mob.Cell)
    //             {
    //                 Core.Jump("cell", "Left");
    //                 Core.Sleep();
    //             }

    //             Bot.Kill.Monster(mob.MapID);

    //             if (Core.CheckInventory(Item!.Name, Item.Quantity))
    //                 break;
    //         }
    //     }
    //     #endregion CellKill (strict)
    // }
    // //^^ keeping this hre for an example ^^

}
