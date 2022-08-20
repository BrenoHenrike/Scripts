//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Evil/NSoD/CoreNSOD.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class NecroticSwordOfDoom
{
    public IScriptInterface Bot { get; set; }
    public CoreBots Core => CoreBots.Instance;
    public CoreNSOD NSoD = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "NSoD_OptionsV1";
    public List<IOption> Options = new()
    {
        new Option<bool>("SkipOption", "Skip this window next time", "You will be able to return to this screen via [Options] -> [Script Options] if you wish to change anything.", false),
        new Option<bool>("getSDKA", "Get SDKA first [Mem]", "If true, the bot will attempt to get SDKA first, so that it can use the fastest Void Aura farm available\nMember-Only", true),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        if (!Bot.Config.Get<bool>("SkipOption") && !Core.CheckInventory(14474, toInv: false) && Core.IsMember)
            Bot.Config.Configure();

        if ((!Bot.Config.Get<bool>("getSDKA") && !Core.IsMember) || (!Core.CheckInventory(14474, toInv: false) && !Core.IsMember))
            Core.BankingBlackList.AddRange(NSoD.Essences);

        Core.SetOptions();

        NSoD.GetNSOD();

        Core.SetOptions(false);
    }
}
