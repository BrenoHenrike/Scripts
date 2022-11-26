//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class UltraOmniKnightBlade
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        FreeRare();

        Core.SetOptions(false);
    }

    public void FreeRare()
    {
        if (Core.CheckInventory("Ultra OmniKnight Blade"))
            return;

        Core.AddDrop("Legend of Vordred");
        Core.EnsureAccept(4565);
        Core.HuntMonster("vordredboss", "Vordred", "Vordred's Skull");
        Core.EnsureComplete(4565);

        Core.BuyItem("battleon", 1163, "Ultra OmniKnight Blade");
    }
}