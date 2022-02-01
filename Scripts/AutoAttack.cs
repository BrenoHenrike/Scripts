using RBot;

public class AutoAttack
{
    public string[] DropItems = 
    {
        // Add items here like: "Legion Blade", "Treasure Chest" **no comma after the last item**

    };

    public void ScriptMain(ScriptInterface bot)
    {
        bot.Options.InfiniteRange = true;
        bot.Lite.UntargetSelf = true;
        
        if (DropItems.Length != 0)
        {
            bot.Drops.RejectElse = false;
            foreach (string item in DropItems)
            {
                bot.Drops.Add(item);
            }
            bot.Drops.Start();
        }

        bot.Skills.StartAdvanced(bot.Inventory.CurrentClass.Name, false);
        bot.Player.SetSpawnPoint();
        
        while (!bot.ShouldExit())
            bot.Player.Kill("*");
    }
}