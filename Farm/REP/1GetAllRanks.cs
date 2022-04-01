//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ThroneofDarkness/05bSekt(FourthDimensionalPyramid).cs
//cs_include Scripts/Story/ThroneofDarkness/03aZiri(BaconCatFortress).cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using RBot;
public class GetAllRanks
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public FourthDimensionalPyramid FDP = new();
    public FlyingBaconCatFortress BCF = new();
    public Core13LoC LOC => new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        FDP.FourthDimensionalPyramidSaga();
        BCF.FlyingBaconCatFortressSaga();
        LOC.Wolfwing();
        LOC.Kimberly();
        LOC.Lionfang();
        UnlockDiabolical();
        UnlockLoreMaster();

        //Farm.UseBoost(ChangeToBoostID, RBot.Items.BoostType.Reputation, true);

        Farm.GetAllRanks();

        Core.SetOptions(false);
    }

    private void UnlockDiabolical()
    {
        if (!Bot.Quests.IsUnlocked(7877))
        {
            Story.KillQuest(7875, "timevoid", "Unending Avatar");
            Story.KillQuest(7876, "twilightedge", "ChaosWeaver Warrior");
        }
    }

    private void UnlockLoreMaster()
    {
        if (Core.IsMember && !Bot.Quests.IsUnlocked(3032))
        {
            // Rosetta Stones
            Story.KillQuest(3029, "druids", new[] { "Void Bear", "Void Larva", "Void Ghast" }, false);
            Story.KillQuest(3030, "druids", "Void Larva");
            Story.KillQuest(3031, "druids", "Void Ghast");
        }
    }
}