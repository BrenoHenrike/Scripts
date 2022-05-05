//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/DarkonGarden.cs
using RBot;

public class PrinceDarkonsPoleaxePreReqs
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDarkon Darkon = new CoreDarkon();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions(); 
        
        FarmPreReqs();

        Core.SetOptions(false);
    }
    public void FarmPreReqs()
    {
        if (Core.CheckInventory("Prince Darkon's Poleaxe"))
            return;

        Darkon.AMelody(22);
        Darkon.FarmReceipt(22);
        Darkon.Teeth(22);
        Darkon.LasGratitude(22);
        Darkon.AstravianMedal(22);
        Darkon.InstantNoodle(22);
        //BuyPoleaxe();
    }

    private void BuyPoleaxe()
    {
        if (!Core.CheckInventory("Prince Darkon's Poleaxe"))
        {
            if (!Core.CheckInventory("Algie's Bow") && Core.CheckInventory("Ultra Drago Insignia", 5))
            {
                Core.BuyItem("ultradrago", 2066, "Algie's Bow");
                Bot.Wait.ForItemBuy();
            }
                
            if (!Core.CheckInventory("Dene's Axe") && Core.CheckInventory("Ultra Drago Insignia", 5))
            {
                Core.BuyItem("ultradrago", 2066, "Dene's Axe");
                Bot.Wait.ForItemBuy();
            }

            if (Core.CheckInventory("Ultra Drago Insignia", 10))
            {
                Core.BuyItem("ultradrago", 2066, "Prince Darkon's Poleaxe");
                Bot.Wait.ForItemBuy();
            }
        }
    }
}
