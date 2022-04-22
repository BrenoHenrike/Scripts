//cs_include Scripts/CoreBots.cs
using RBot;

public class CandyshellMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetMergeItems();

        Core.SetOptions(false);
    }

    public void GetMergeItems()
    {
        //Needed AddDrop
        Core.AddDrop("Caramel Eggshells", "Anti-Neggshells", "Shadow Eggshells", "Creme Eggshells", "Chocolate Eggshells", "Rainbow Eggshells", "Chaotic Eggshells", "Golden Eggshells");

        while (!Core.CheckInventory(new[] { "Caramel Eggshells", "Anti-Neggshells", "Shadow Eggshells", "Creme Eggshells", "Chocolate Eggshells", "Rainbow Eggshells", "Chaotic Eggshells", "Golden Eggshells" }, 150))
        {
            Core.EquipClass(ClassType.Farm);

            //Caramel Eggshells
            if (!Core.CheckInventory("Caramel Eggshells", 150))
            {
                Core.Join("GreenguardWest");
                for (int i = 0; i < 10; i++)
                    Bot.Player.Hunt("Kittarian");
            }

            //Anti-Neggshells
            if (!Core.CheckInventory("Anti-Neggshells", 150) && Core.IsMember)
            {
                Core.Join("Greymoor");
                for (int i = 0; i < 10; i++)
                    Bot.Player.Hunt("Spooky Treeant");
            }

            //Creme Eggshells
            if (!Core.CheckInventory("Creme Eggshells", 150))
            {
                Core.Join("GreenShell");
                for (int i = 0; i < 10; i++)
                    Bot.Player.Hunt("Tsukumogami");
            }

            //Chocolate Eggshells
            if (!Core.CheckInventory("Chocolate Eggshells", 150))
            {
                Core.Join("GreenguardEast");
                for (int i = 0; i < 10; i++)
                    Bot.Player.Hunt("Gurushroom");
            }

            //Rainbow Eggshells
            if (!Core.CheckInventory("Rainbow Eggshells", 150))
            {
                Core.Join("Greendragon");
                for (int i = 0; i < 10; i++)
                    Bot.Player.Hunt("Greenguard Dragon");
            }

            //Chaotic Eggshells
            if (!Core.CheckInventory("Chaotic Eggshells", 150))
            {
                Core.Join("Grenstory");
                for (int i = 0; i < 10; i++)
                    Bot.Player.Hunt("Imposter Egg");
            }

            //Golden Eggshells
            if (!Core.CheckInventory("Golden Eggshells", 150))
            {
                Core.Join("Greed");
                for (int i = 0; i < 10; i++)
                    Bot.Player.Hunt("Treasure Pile");
            }

            //Shadow Eggshells
            if (!Core.CheckInventory("Shadow Eggshells", 150))
            {
                Core.Join("Grenwog", publicRoom: true);
                Core.EquipClass(ClassType.Solo);
                for (int i = 0; i < 10; i++)
                    Bot.Player.Hunt("Grenwog");
            }
        }
    }
}



