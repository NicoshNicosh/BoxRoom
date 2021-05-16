public class FoodButton : ClosetButton
{
    public PotentialPoop PoopSettings;

    protected override void OnPurchase()
    { 
        Manager.Eat(PoopSettings);
    }
}