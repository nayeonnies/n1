using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReviewPopup : MonoBehaviour
{
    public Text reviewText;
    public Text starsText;

    public float slideDuration = 0.4f;
    private RectTransform rect;
    private Vector2 shownPos;
    private Vector2 hiddenPos;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        shownPos = rect.anchoredPosition;
        hiddenPos = shownPos + new Vector2(-1200f, 0f); // 왼쪽 화면 밖
    }

    public void Show(float score)
    {
        gameObject.SetActive(true);

        int stars = Mathf.Clamp(Mathf.RoundToInt(score * 5f), 1, 5);
        starsText.text = new string('★', stars) + new string('☆', 5 - stars);

        switch (stars)
        {
            case 5:
                reviewText.text = "완벽해요! 딱 원하던 물고기예요.";
                break;
            case 4:
                reviewText.text = "좋아요! 거의 다 맞았어요.";
                break;
            case 3:
                reviewText.text = "나쁘지 않네요, 그래도 아쉬운 점이 있어요.";
                break;
            case 2:
                reviewText.text = "음... 제가 원한 건 이게 아닌데요.";
                break;
            default:
                reviewText.text = "이건 완전히 잘못된 주문이에요!";
                break;
        }

        gameObject.SetActive(true);
        StopAllCoroutines();
        rect.anchoredPosition = hiddenPos; // 항상 왼쪽 바깥에서 시작하도록 강제 고정
        StartCoroutine(SlideTo(hiddenPos, shownPos));
    }
    

    public void Close()
    {
        StopAllCoroutines();
        StartCoroutine(SlideOutAndHide());
    }

    private IEnumerator SlideOutAndHide()
    {
        yield return SlideTo(rect.anchoredPosition, hiddenPos);
        gameObject.SetActive(false);
    }

    private IEnumerator SlideTo(Vector2 from, Vector2 to)
    {
        float elapsed = 0f;
        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / slideDuration;
            rect.anchoredPosition = Vector2.Lerp(from, to, t);
            yield return null;
        }
        rect.anchoredPosition = to;
    }
}