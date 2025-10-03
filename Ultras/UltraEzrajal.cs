//cs_include Scripts/Ultras/CoreUltras.cs

using System;
using System.Dynamic;
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class UltraEzrajal
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
        //Core.UseAlchemyPotions(Core.GetBestTonicPotion());
        //Core.UseAlchemyPotions(Core.GetBestElixirPotion());
        //Core.BuyAlchemyPotion("Potent Honor Potion");
        //Core.EquipConsumable("Potent Honor Potion");

        Core.Join("ultraezrajal");
        //Core.WaitForArmy(3);
        Core.ChooseBestCell("Ultra Ezrajal");
        Core.EnableSkills();

        while (Core.MonsterAlive("Ultra Ezrajal") && !Bot.ShouldExit)
        {
            if (Core.HasAura("Counter Attack"))
                Core.DontAttack();
            Core.Kill("Ultra Ezrajal");
        }
    }
}
