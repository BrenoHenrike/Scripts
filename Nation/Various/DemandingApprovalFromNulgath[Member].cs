//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs

using Skua.Core.Interfaces;

public class DemandingApprovalFromNulgath
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Nation.GemStoneReceiptOfNulgath();

        Core.SetOptions(false);
    }
}