using UnityEngine;
using UnityEngine.UI;

public class ManualPopup : MonoBehaviour
{
    public FishDatabase fishDatabase;

    public Text nameText;
    public Text seasonText;
    public Text sizeText;
    public Text colorText;
    public Image fishImage;

    private int currentIndex = 0;

    public void Show()
    {
        currentIndex = 0;
        gameObject.SetActive(true);
        Refresh();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Next()
    {
        currentIndex = (currentIndex + 1) % fishDatabase.allFish.Count;
        Refresh();
    }

    public void Prev()
    {
        currentIndex = (currentIndex - 1 + fishDatabase.allFish.Count) % fishDatabase.allFish.Count;
        Refresh();
    }

    private void Refresh()
    {
        Fish f = fishDatabase.GetFishByIndex(currentIndex);
        if (f == null) return;

        nameText.text = f.fishname;
        seasonText.text = f.season;
        sizeText.text = f.size;
        colorText.text = f.color;
        fishImage.sprite = f.fishImage;
    }
}