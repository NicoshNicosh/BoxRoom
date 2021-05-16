public class FoodButton : ClosetButton
{
    public PotentialPoop PoopSettings;

    public override void OnPurchase()
    { 
        Manager.Eat(PoopSettings);
    }
}