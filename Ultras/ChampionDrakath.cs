//cs_include Scripts/Ultras/CoreUltras.cs

using System;
using System.Dynamic;
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ChampionDrakath
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreUltras Core = new();

    public string taunterClass;
    public bool DontPreconfigure = true;
    public string OptionsStorage = "ChampionDrakath";
    public List<IOption> Options = new() {
        new Option<string>("taunterClass", "Taunter Class", "Insert the name of the class that will taunt", "")
    };

    public void ScriptMain(IScriptInterface bot)
    {
        taunterClass = Bot.Config.Get<string>("taunterClass") ?? string.Empty;
        if (string.IsNullOrEmpty(taunterClass))
        {
            Bot.Log("Taunter not filled in! Please edit Script Options.");
            Bot.Stop();
        }

        Core.Boot();

        PreparePotions(taunterClass);
        Kill(taunterClass);

        Bot.Stop();
    }

    void PreparePotions(string taunterClass)
    {
        Core.UseAlchemyPotions(Core.GetBestTonicPotion());
        Core.UseAlchemyPotions(Core.GetBestElixirPotion());

        Core.BuyAlchemyPotion("Potent Honor Potion");
        Core.EquipConsumable("Potent Honor Potion");

        if (Core.HasClassEquipped(taunterClass))
            Core.GetScrollOfEnrage();
    }

    void Kill(string taunterClass)
    {
        Core.Join("championdrakath");
        Core.WaitForArmy(3);
        Core.ChooseBestCell("Champion Drakath");
        Core.EnableSkills();

        while (Core.MonsterAlive("Champion Drakath") && !Bot.ShouldExit)
        {
            if (Core.HasClassEquipped(taunterClass))
                Core.DrakathTaunter();
            else
            {
                Core.Kill("Champion Drakath");
                if (Core.GetTargetHealthPercentage() < 10)
                    Bot.Skills.UseSkill(5);
            }
        }
    }
}

