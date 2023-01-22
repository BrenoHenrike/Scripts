/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class FindingFragments_Any
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreBLOD BLOD = new CoreBLOD();
    public CoreStory Story = new CoreStory();

    public string OptionStorage = "Finding_Fragments";
    public List<IOption> Options = new List<IOption>()
    {
        new Option<FindingFragmentsIDs>("questID", "Quest ID", "ID of the desired Finding Fragments quest to do.", FindingFragmentsIDs.Blade)
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        FindingFragments();

        Core.SetOptions(false);
    }

    public void FindingFragments()
    {
        Core.EquipClass(ClassType.Farm);
        int i = 1;
        int questID = (int)Bot.Config.Get<FindingFragmentsIDs>("questID");
        while (!Bot.ShouldExit)
        {
            BLOD.FindingFragments(questID);
            Core.Logger($"Completed x{i++}");
        }
    }
}

public enum FindingFragmentsIDs
{
    Bow = 2174,
    Dagger = 2175,
    Mace = 2176,
    Scythe = 2177,
    Broadsword = 2178,
    Blade = 2179
}
