//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/Nulgath/AFDL/WillpowerExtraction.cs
//cs_include Scripts/Nulgath/AFDL/NulgathDemandsWork.cs
//cs_include Scripts/Nulgath/AFDL/EnoughDOOMforanArchfiend.cs
//cs_include Scripts/Nulgath/Various/GoldenHanzoVoid.cs
using RBot;

public class ADFL
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNulgath Nulgath = new();
    public WillpowerExtraction WillpowerExtraction = new();
    public NulgathDemandsWork NulgathDemandsWork = new();
    public EnoughDOOMforanArchfiend DOOM = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nulgath.bagDrops);
        Core.BankingBlackList.AddRange(new[] {"ArchFiend DoomLord", "Undead Essence", "Chaorruption Essence",
            "Essence Potion", "Essence of Klunk", "Living Star Essence", "Bone Dust", "Undead Energy"});
        Core.SetOptions();

        DOOM.AFDL();

        Core.SetOptions(false);
    }
}