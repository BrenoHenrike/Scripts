//cs_include Scripts/CoreBots.cs

using Skua.Core.Interfaces;
using Skua.Core.Options;

public class TimeStreamRavagersSigil
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<bool>("CanSolo", "Can solo boss?", "Can you solo Eternal Dragon?", false),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetRavagerSigil();

        Core.SetOptions(false);
    }

    public void GetRavagerSigil()
    {
        if(Core.CheckInventory("Timestream Ravager's Sigil")){
            Core.Logger("You already have the item.");
            return;
        }

        Core.EquipClass(ClassType.Solo);

        if(!Bot.Config.Get<bool>("CanSolo"))
            Core.HuntMonster("deadlines", "Eternal Dragon", "Timestream Ravager's Sigil", isTemp: false, publicRoom: true);
        else Core.HuntMonster("deadlines", "Eternal Dragon", "Timestream Ravager's Sigil", isTemp: false);
    }
}