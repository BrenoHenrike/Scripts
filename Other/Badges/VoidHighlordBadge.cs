//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class VoidHighlordBadge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (Core.HasWebBadge(badge))
        {
            Core.Logger($"Already have the {badge} badge");
            return;
        }

        if (!Core.CheckInventory("Void Highlord"))
            return;

        Core.Logger($"Doing the quest for {badge} badge");
        Core.EnsureAccept(7651);
        Core.HuntMonster("shadowblast", "Legion Fenrir", "Fiend Seal", 1, isTemp: false);
        Core.EnsureComplete(7651);
    }

    private string badge = "Void Highlord";
}
