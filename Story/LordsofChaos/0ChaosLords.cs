//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Story/LordsofChaos/0ChaosLords.cs
//cs_include Scripts/Story/LordsofChaos/1Prologue.cs
//cs_include Scripts/Story/LordsofChaos/2Escherion(ChiralValley).cs
//cs_include Scripts/Story/LordsofChaos/3Vath(Dwarfhold).cs
//cs_include Scripts/Story/LordsofChaos/4Kitsune(Yokai).cs
//cs_include Scripts/Story/LordsofChaos/5Wolfwing(Darkovia).cs
//cs_include Scripts/Story/LordsofChaos/6Kimberly(Mythsong).cs
//cs_include Scripts/Story/LordsofChaos/7Ledgermayne(Arcangrove).cs
//cs_include Scripts/Story/LordsofChaos/8Tibicenas(Sandsea).cs
//cs_include Scripts/Story/LordsofChaos/9Khasaanda(Horc).cs
//cs_include Scripts/Story/LordsofChaos/10Iadoa(TheSpan).cs
//cs_include Scripts/Story/LordsofChaos/11Lionfang(Darkblood).cs
//cs_include Scripts/Story/LordsofChaos/12Xiang(MirrorRealm).cs
//cs_include Scripts/Story/LordsofChaos/13Alteon(Swordhaven).cs
//cs_include Scripts/Story/LordsofChaos/14Hero(ChaosFinale).cs
//cs_include Scripts/Story/LordsofChaos/15Extra.cs
using RBot;
using System;
using System.Collections.Generic;

public class ImLordNow
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public SagaPrologue prologue = new SagaPrologue();
    public SagaChiralValley Escherion = new SagaChiralValley();
    public SagaDwarfhold Vath = new SagaDwarfhold();
    public SagaYokai Kitsune = new SagaYokai();
    public SagaDarkovia Wolfwing = new SagaDarkovia();
    public SagaMythsong Kimberly = new SagaMythsong();
    public SagaArcangrove Ledgermayne = new SagaArcangrove();
    public SagaSandsea Tibicenas = new SagaSandsea();
    public SagaHorc Khasaanda = new SagaHorc();
    public SagaTheSpan Iadoa = new SagaTheSpan();
    public SagaDarkblood LionFang = new SagaDarkblood();
    public SagaMirrorRealm Xiang = new SagaMirrorRealm();
    public SagaSwordhaven Alteon = new SagaSwordhaven();
    public SagaChaosFinale Hero = new SagaChaosFinale();
    public SagaExtra Extra = new SagaExtra();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        Core.AcceptandCompleteTries = 5;

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        prologue.CompleteSaga();
        Escherion.CompleteSaga();
        Vath.CompleteSaga();
        Kitsune.CompleteSaga();
        Wolfwing.CompleteSaga();
        Kimberly.CompleteSaga();
        Ledgermayne.CompleteSaga();
        Tibicenas.CompleteSaga();
        Khasaanda.CompleteSaga();
        Iadoa.CompleteSaga();
        LionFang.CompleteSaga();
        Xiang.CompleteSaga();
        Alteon.CompleteSaga();
        Hero.CompleteSaga();
        Extra.CompleteSaga();
    }
}