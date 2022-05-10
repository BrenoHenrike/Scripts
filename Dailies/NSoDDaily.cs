//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class NSODDaily
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();
    public CoreFarms Farm = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        // Daily.NSODDaily()
        DoDaily();

        Core.SetOptions(false);
    }


    public void DoDaily()
    {
        if (Core.CheckInventory(new[] { "Necrotic Sword of Doom", "Dual Necrotic Swords of Doom" }, any: true))
            return;

        if (Core.CheckInventory("Void Aura", 7500))
            return;

        Core.EquipClass(ClassType.Solo);
        Core.AddDrop("Void Aura", "(Necro) Scroll of Dark Arts");

        // Glimpse Into the Dark[Mem] - 8652
        if (Core.IsMember)
        {
            if (Daily.CheckDaily(8653))
            {
                Core.EnsureAccept(8653);
                if (Core.isCompletedBefore(3119))
                {
                    Core.AddDrop("Kraken Doubloon");
                    Core.RegisterQuests(3119);
                    while (!Core.CheckInventory("Kraken Doubloon", 13))
                    {
                        Core.HuntMonster("chaoskraken", "Chaos Kraken", "Kraken Keelhauled");
                    }
                    Core.CancelRegisteredQuests();
                }
                else Core.HuntMonster("chaoskraken", "Chaos Kraken", "Kraken Doubloon", 13, isTemp: false, publicRoom: true);
                Core.HuntMonster($"ancienttrigoras", "Ancient Trigoras", "Ancient Trigora’s Horns", 3, isTemp: false);
                Core.KillMonster("gravechallenge", "r19", "Left", "Graveclaw the Defiler", "Graveclaw's Broken Axe", isTemp: false);
                Core.EnsureComplete(8653);
                Bot.Wait.ForPickup("Void Aura");
            }
        }
        // The Encroaching Shadows - 8653
        if (Daily.CheckDaily(8653))
        {
            Core.EnsureAccept(8653);
            Core.HuntMonster("icestormarena", "Warlord Icewing", "Glacial Pinion", isTemp: false, publicRoom: true);
            Core.HuntMonster("hydrachallenge", "Hydra Head 90", "Hydra Eyeball", 3, isTemp: false);
            Core.HuntMonster("thevoid", "Flibbitiestgibbet", "Flibbitigiblets", isTemp: false, publicRoom: true);
            Core.EnsureComplete(8653);
            Bot.Wait.ForPickup("Void Aura");
        }
    }
}


