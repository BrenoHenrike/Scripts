//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/Nulgath/EvolvedOrb/EvolvedBloodOrb.cs
//cs_include Scripts/Nulgath/EvolvedOrb/EvolvedHexOrb.cs
//cs_include Scripts/Nulgath/EvolvedOrb/EvolvedShadowOrb[Mem].cs
using RBot;

public class EvolvedOrb
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNulgath Nulgath = new CoreNulgath();
    public EvolvedBloodOrb EBO = new EvolvedBloodOrb();
    public EvolvedHexOrb EHO = new EvolvedHexOrb();
    public EvolvedShadowOrb ESO = new EvolvedShadowOrb();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        GetAllOrb();
        Core.SetOptions(false);
    }

    public void GetAllOrb()
    {
        EBO.GetEvolvedBloodOrb();
        EHO.GetEvolvedHexOrb();
        ESO.GetEvolvedShadowOrb();
        Core.Logger($"Done, you have the balls");
    }

}