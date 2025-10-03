//cs_include Scripts/Ultras/CoreUltras.cs

using System;
using System.Dynamic;
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class UltraEngineer
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreUltras Core = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.Boot();

        Kill();

        Bot.Stop();
    }

    void Kill()
    {
        Core.UseAlchemyPotions(Core.GetBestTonicPotion());
        Core.UseAlchemyPotions(Core.GetBestElixirPotion());
        Core.BuyAlchemyPotion("Potent Honor Potion");
        Core.EquipConsumable("Potent Honor Potion");

        Core.Join("ultraengineer");
        Core.WaitForArmy(3);
        Core.ChooseBestCell("Ultra Engineer");
        Core.EnableSkills();

        while (Core.MonsterAlive("Ultra Engineer") && !Bot.ShouldExit)
        {
            Core.KillWithPriority("Ultra Engineer", 3, "Defense Drone", 2, "Attack Drone", 1);
            Bot.Skills.UseSkill(5);
        }
    }
}
