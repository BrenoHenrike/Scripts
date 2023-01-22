/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class ExtraCreditAC
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        ExtraFunAC();

        Core.SetOptions(false);
    }

    public string[] Dogear =
    {
        "Book of Lore",
        "Casual Black Backpack",
        "Casual Light Backpack",
        "Dogged Professor"
    };

    public string[] Bully =
    {
        "Meanest Bully",
        "Burning Book"
    };

    public string[] Locker =
    {
        "Bagged Lunch",
        "Car Pencil Case Blue",
        "Car Pencil Case Orange",
        "Chair Mace",
        "Green Backpack",
        "Number 2 Pencil",
        "Sleek Chic Locks",
        "Slick Spikes Hair",
        "Weekend Fun Pigtails Locks",
        "Winter Uniform"
    };

    public void ExtraFunAC()
    {
        if (!Core.isSeasonalMapActive("extracredit"))
            return;

        if (Core.CheckInventory(Dogear) && Core.CheckInventory(Bully) && Core.CheckInventory(Locker))
            return;

        Core.AddDrop(Dogear);
        Core.AddDrop(Bully);
        Core.AddDrop(Locker);
        Core.EquipClass(ClassType.Solo);
        while (!Bot.ShouldExit && (!Core.CheckInventory(Dogear)))
            Core.HuntMonster("extracredit", "Dogear");
        while (!Bot.ShouldExit && (!Core.CheckInventory(Bully)))
            Core.HuntMonster("extracredit", "Meanest Girl");
        while (!Bot.ShouldExit && (!Core.CheckInventory(Locker)))
            Core.HuntMonster("extracredit", "Supply Locker");
    }
}
