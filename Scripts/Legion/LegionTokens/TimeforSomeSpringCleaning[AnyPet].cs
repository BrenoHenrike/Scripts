//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Legion/CoreLegion.cs
using RBot;
public class TimeforSomeSpringCleaning_AnyPet_
{
    public CoreBots Core => CoreBots.Instance;
    public CoreLegion Legion = new CoreLegion();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Legion.LTShogunParagon();

        Core.SetOptions(false);
    }
}