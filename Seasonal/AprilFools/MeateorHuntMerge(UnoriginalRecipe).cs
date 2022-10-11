//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/AprilFools/MeateorHunt.cs
using Skua.Core.Interfaces;
public class MeateorHuntMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public MeateorHunt MH = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetUnoriginalRecipe();

        Core.SetOptions(false);
    }

    public void GetUnoriginalRecipe(int Quant = 300)
    {
        if (!Core.isSeasonalMapActive("MeateorTown"))
            return;
        //Item Check
        if (Core.CheckInventory("Unoriginal Recipe", Quant))
            return;

        //Add Drop
        Core.AddDrop("Unoriginal Recipe");

        //Quest PreReqs
        MH.StoryLine();

        Core.EquipClass(ClassType.Solo);

        while (!Bot.ShouldExit && !Core.CheckInventory("Unoriginal Recipe", Quant))
        {
            Core.EnsureAccept(8629);

            Core.HuntMonster("BattleFowl", "Egg-stremis", "Egg-Streme Drumstick", publicRoom: true);
            Core.HuntMonster("BattleonTown", "ChickenCow", "ChickenCow Wings");
            Core.HuntMonster("BattleFowl", "Zeuster Projection", "Zeuster’s Thigh");
            Core.HuntMonster("BattleFowl", "Sabertooth Chicken", "Chick-N-Nuggets", 10);

            Core.EnsureComplete(8629);
        }
    }
}