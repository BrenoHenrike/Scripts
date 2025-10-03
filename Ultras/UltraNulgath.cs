//cs_include Scripts/Ultras/CoreUltras.cs

using System;
using System.Dynamic;
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class UltraNulgath
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreUltras Core = new();

    public string primaryTaunter;
    public string secondaryTaunter;
    public bool DontPreconfigure = true;
    public string OptionsStorage = "UltraNulgath";
    public List<IOption> Options = new()
    {
        new Option<string>("primaryTaunter", "First Taunter Class", "Insert the name of the class that will taunt", ""),
        new Option<string>("secondaryTaunter", "Second Taunter Class", "Insert the name of the class that will taunt", ""),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        primaryTaunter = Bot.Config.Get<string>("primaryTaunter") ?? string.Empty;
        if (string.IsNullOrEmpty(primaryTaunter))
        {
            Bot.Log("First taunter not filled in! Please edit Script Options.");
            Bot.Stop();
        }

        secondaryTaunter = Bot.Config.Get<string>("secondaryTaunter") ?? string.Empty;
        if (string.IsNullOrEmpty(secondaryTaunter))
        {
            Bot.Log("Second taunter not filled in! Please edit Script Options.");
            Bot.Stop();
        }

        Core.Boot();

        PreparePotions(primaryTaunter, secondaryTaunter);
        Kill(primaryTaunter, secondaryTaunter);

        Bot.Stop();
    }

    void PreparePotions(string primaryTaunter, string secondaryTaunter)
    {
        Core.UseAlchemyPotions(Core.GetBestTonicPotion());
        Core.UseAlchemyPotions(Core.GetBestElixirPotion());

        if (Core.HasClassEquipped(primaryTaunter) || Core.HasClassEquipped(secondaryTaunter))
            Core.GetScrollOfEnrage();
        else
        {
            Core.BuyAlchemyPotion("Potent Honor Potion");
            Core.EquipConsumable("Potent Honor Potion");
        }
    }

    void Kill(string primaryTaunter, string secondaryTaunter)
    {
        if (Core.HasClassEquipped(primaryTaunter) || Core.HasClassEquipped(secondaryTaunter))
            Core.GetScrollOfEnrage();

        Core.Join("ultranulgath");
        Core.WaitForArmy(3);
        if (!Core.HasClassEquipped(primaryTaunter) || !Core.HasClassEquipped(secondaryTaunter))
            Bot.Sleep(5000);
        Core.ChooseBestCell("Nulgath the Archfiend");
        Core.EnableSkills();

        while (Core.MonsterAlive("Nulgath the Archfiend") && !Bot.ShouldExit)
            if (Core.HasClassEquipped(primaryTaunter))
                Core.TauntCycle(primaryTaunter, "Nulgath the Archfiend", "Focus", 250);
            else if (Core.HasClassEquipped(secondaryTaunter))
                Core.TauntCycle(secondaryTaunter, "Nulgath the Archfiend", "Focus", 700);
            else
            {
                Core.KillWithPriority("Nulgath the Archfiend", "Overfiend Blade");
                Bot.Skills.UseSkill(5);
            }

    }
}


