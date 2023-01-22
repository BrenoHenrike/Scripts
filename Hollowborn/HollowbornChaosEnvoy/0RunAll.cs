/*
name: null
description: null
tags: null
*/
//cs_include Scripts/Chaos/ChaosAvengerPreReqs.cs
//cs_include Scripts/Chaos/DrakathsArmor.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Chaos/AscendedDrakathGear.cs
//cs_include Scripts/Chaos/EternalDrakathSet.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQOM.cs
//cs_include Scripts/Story/StarSinc.cs
//cs_include Scripts/Story/TitanAttack.cs
//cs_include Scripts/Story/TowerOfDoom.cs
//cs_include Scripts/Other/MergeShops/TitanGearIIMerge.cs
//cs_include Scripts/Hollowborn/HollowbornChaosEnvoy/CoreHollowbornChaosEnvoy.cs
//cs_include Scripts/Other/Badges/ChaosPuppetMaster.cs
using Skua.Core.Interfaces;

public class HBCE0
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreHollowbornChaosEnvoy HBCE = new();
    private static CoreHollowbornChaosEnvoy sHBCE = new();

    public string OptionsStorage = sHBCE.OptionsStorage;
    public bool DontPreconfigure = true;
    public List<IOption> Options = sHBCE.Options;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        HBCE.GetAll(Bot.Config.Get<bool>("getAll"));

        Core.SetOptions(false);
    }
}
