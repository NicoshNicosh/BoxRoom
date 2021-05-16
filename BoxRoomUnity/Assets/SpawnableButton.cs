public class SpawnableButton : ClosetButton
{
    public int SpawnableNum = 0;

    public override void OnPurchase()
    {
        var obj = SpawnableManager.Instance.SpawnableRefs[SpawnableNum-1];
        obj.SetActive(obj.activeSelf);
    }


}