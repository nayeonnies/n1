using UnityEngine;
using UnityEngine.UI;

public class ShopRatingManager : MonoBehaviour
{
    public Text ratingText;

    private float totalScore = 0f;
    private int reviewCount = 0;

    public void AddReview(float score)
    {
        totalScore += score;
        reviewCount++;
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        if (reviewCount == 0)
        {
            ratingText.text = "0.0";
            return;
        }

        float average = totalScore / reviewCount; // 0~1 »çÀÌ Æò±Õ
        float starsOutOf5 = average * 5f;
        int fullStars = Mathf.Clamp(Mathf.RoundToInt(starsOutOf5), 0, 5);

        string stars = new string('¡Ú', fullStars) + new string('¡Ù', 5 - fullStars);
        ratingText.text = stars;
    }
}