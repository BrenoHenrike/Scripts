//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
//cs_include Scripts/Darkon/CoreDarkon.cs

using Skua.Core.Interfaces;

public class DarkonDebris2ReconstructedPrerequisites
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreDarkon Darkon = new();


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        FarmAll();

        Core.SetOptions(false);
    }

    public void FarmAll()
    {
        if (!Core.CheckInventory("Darkon's Debris 2 (Reconstructed)"))
        {
            if (!Core.CheckInventory("Darkon's Debris 2 (Recovered)"))
            {
                Darkon.UnfinishedMusicalScore(22);
                Core.BuyItem("theworld", 2141, "Darkon's Debris 2 (Recovered)");
            }
            Darkon.BanditsCorrespondence(22);
            Darkon.SukisPrestiege(22);
            Darkon.AncientRemnant(22);
            Darkon.MourningFlower(22);
            if (!Core.CheckInventory("Darkon Insignia", 20))
            {
                Core.Logger(" x20 \"Darkon Insignia\" is Required to continue quest, our Bots cannot *currently* kill this mob Untill CoreArmy is Released and a script is made.", messageBox: true);
                return;
            }
            else Core.BuyItem("ultradarkon", 2147, "Darkon's Debris 2 (Reconstructed)");
        }
    }
}
