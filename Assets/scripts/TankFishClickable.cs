using UnityEngine;

public class TankFishClickable : MonoBehaviour
{
    public int fishIndex;
    public FishDatabase fishDatabase;
    public PackagingManager packagingManager;

    void OnMouseDown()
    {
        Fish clicked = fishDatabase.GetFishByIndex(fishIndex);
        packagingManager.OnTankClicked(clicked);
    }
}