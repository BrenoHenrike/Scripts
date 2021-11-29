//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Legion/CoreLegion.cs
using RBot;

public class LegionFealty1
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreLegion Legion = new CoreLegion();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        Core.AddDrop("Legion Token");
        Core.AddDrop(Legion.LR);
        Core.AddDrop(Legion.LF1);

        RevenantSpellscroll();

        Core.SetOptions(false);
    }

    public void RevenantSpellscroll(int quant = 20)
    {
        if (Core.CheckInventory("Revenant's Spellscroll", quant))
            return;

        int i = 1;
        Core.Logger($"Farming {quant} Revenant's Spellscroll");
        while (!Core.CheckInventory("Revenant's Spellscroll", quant))
        {
            Core.EnsureAccept(6897);

            Core.EquipClass(ClassType.Solo);
            Core.KillMonster("judgement", "r10a", "Left", "Ultra Aeacus", "Aeacus Empowered", 50, false);

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("revenant", "r2", "Left", "*", "Tethered Soul", 300, false);
            Core.KillMonster("shadowrealmpast", "Enter", "Spawn", "*", "Darkened Essence", 500, false);
            Core.KillMonster("necrodungeon", "r22", "Down", "*", "Dracolich Contract", 1000, false);

            Core.EnsureComplete(6897);
            Bot.Player.Pickup("Revenant's Spellscroll");
            Core.Logger($"Completed x{i++}");
        }
    }
}