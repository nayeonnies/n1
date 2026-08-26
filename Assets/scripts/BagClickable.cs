using UnityEngine;

public class BagClickable : MonoBehaviour
{
    public PackagingManager packagingManager;

    void OnMouseDown()
    {
        packagingManager.OnBagClicked();
    }
}