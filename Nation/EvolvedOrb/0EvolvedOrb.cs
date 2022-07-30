//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/EvolvedOrb/EvolvedBloodOrb.cs
//cs_include Scripts/Nation/EvolvedOrb/EvolvedHexOrb.cs
//cs_include Scripts/Nation/EvolvedOrb/EvolvedShadowOrb[Mem].cs
using Skua.Core.Interfaces;

public class EvolvedOrb
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();
    public EvolvedBloodOrb EBO = new EvolvedBloodOrb();
    public EvolvedHexOrb EHO = new EvolvedHexOrb();
    public EvolvedShadowOrb ESO = new EvolvedShadowOrb();

    public void ScriptMain(IScriptInterface bot)
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