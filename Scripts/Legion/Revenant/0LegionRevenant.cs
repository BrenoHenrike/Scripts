//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/Revenant/LegionFealty1.cs
//cs_include Scripts/Legion/Revenant/LegionFealty2.cs
//cs_include Scripts/Legion/Revenant/LegionFealty3.cs
//cs_include Scripts/Legion/JoinLegion[UndeadWarrior].cs
//cs_include Scripts/Legion/InfiniteLegionDarkCaster.cs
//cs_include Scripts/Story/NecroDungeon.cs
using RBot;

public class LegionFealty4
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreLegion Legion = new CoreLegion();
    public LegionFealty1 LF1 = new LegionFealty1();
    public LegionFealty2 LF2 = new LegionFealty2();
    public LegionFealty3 LF3 = new LegionFealty3();
    public JoinLegion JoinLegion = new JoinLegion();
    public NecroDungeon NecroDung = new NecroDungeon();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetLR();

        Core.SetOptions(false);
    }

    public void GetLR()
    {
        if (Core.CheckInventory("Legion Revenant"))
            return;

        JoinLegion.JoinLegionQuests();

        Core.AddDrop("Legion Token");
        Core.AddDrop(Legion.legionMedals);
        Core.AddDrop(Legion.LR);
        Core.AddDrop(Legion.LF1);
        Core.AddDrop(Legion.LF2);
        Core.AddDrop(Legion.LF3);
        
        NecroDung.NecrodungeonStoryLine();
        LF1.RevenantSpellscroll();
        LF2.ConquestWreath();
        LF3.ExaltedCrown();

        Core.ChainComplete(6900);
    }
}

