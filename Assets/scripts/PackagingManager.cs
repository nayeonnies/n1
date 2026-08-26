using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PackagingManager : MonoBehaviour
{
    public bool hasBagOnTable = false;
    public bool bagFilledWithWater = false;
    public List<Fish> fishInBag = new List<Fish>();

    [Header("Visuals")]
    public GameObject bagOnTableVisual;
    public Transform fishHolder;
    public GameObject fishIconPrefab;
    private Vector3 originalPosition;   // 추가
    private Vector3 originalScale;      // 추가

    [Header("Managers")]
    public ShopRatingManager shopRating;
    public OrderManager orderManager;
    public ReviewPopup reviewPopup;
    public MoneyManager moneyManager;

    [Header("Ship Animation")]
    public float shipDistance = 15f;
    public float shipDuration = 0.5f; // 값을 키우면 더 천천히 사라짐

    public void OnBagClicked()
    {
        hasBagOnTable = true;
        bagFilledWithWater = true;
        fishInBag.Clear();
        bagOnTableVisual.SetActive(true);

        bagOnTableVisual.transform.localPosition = originalPosition; // 추가
        bagOnTableVisual.transform.localScale = originalScale;       // 추가

        SpriteRenderer[] renderers = bagOnTableVisual.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in renderers)
        {
            Color c = sr.color;
            c.a = 1f;
            sr.color = c;
        }

        ClearFishIcons();
        Debug.Log("Bag placed, filled with water");
    }

    void Start()
    {
        originalPosition = bagOnTableVisual.transform.localPosition;
        originalScale = bagOnTableVisual.transform.localScale;
    }

    public void OnTankClicked(Fish clickedFish)
    {
        if (hasBagOnTable && bagFilledWithWater)
        {
            fishInBag.Add(clickedFish);

            GameObject icon = Instantiate(fishIconPrefab, fishHolder);
            icon.GetComponent<SpriteRenderer>().sprite = clickedFish.fishImage;

            RepositionFishIcons(); // 매번 전체 재배치

            Debug.Log(clickedFish.fishname + " added. Total: " + fishInBag.Count);
        }
        else
        {
            Debug.Log("No bag on table, cannot add fish");
        }
    }

    public void ShipOrder()
    {
        if (!hasBagOnTable || fishInBag.Count == 0)
        {
            Debug.Log("배송할 물고기가 없어요");
            return;
        }

        float score = orderManager.EvaluateOrder(fishInBag);
        StartCoroutine(ShipAnimation(score));
    }

    private IEnumerator ShipAnimation(float score)
    {
        Vector3 startPos = bagOnTableVisual.transform.localPosition;
        Vector3 startScale = bagOnTableVisual.transform.localScale;
        Vector3 endPos = startPos + new Vector3(0, -shipDistance, 0);

        int totalPrice = 0;
        foreach (Fish f in fishInBag) totalPrice += f.price;
        int earned = Mathf.RoundToInt(totalPrice * score);

        SpriteRenderer[] allRenderers = bagOnTableVisual.GetComponentsInChildren<SpriteRenderer>();

        float elapsed = 0f;
        while (elapsed < shipDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / shipDuration;
            float easedT = t * t; // 서서히 가속하는 곡선

            bagOnTableVisual.transform.localPosition = Vector3.Lerp(startPos, endPos, easedT);
            bagOnTableVisual.transform.localScale = Vector3.Lerp(startScale, startScale * 0.3f, easedT);

            foreach (SpriteRenderer sr in allRenderers)
            {
                Color c = sr.color;
                c.a = Mathf.Lerp(1f, 0f, easedT);
                sr.color = c;
            }

            yield return null;
        }

        ResetPackaging();
        bagOnTableVisual.transform.localPosition = startPos;
        bagOnTableVisual.transform.localScale = startScale;

        reviewPopup.Show(score);
        shopRating.AddReview(score);
        moneyManager.AddMoney(earned);
        orderManager.GenerateOrder();
    }

    public void ResetPackaging()
    {
        hasBagOnTable = false;
        bagFilledWithWater = false;
        fishInBag.Clear();
        bagOnTableVisual.SetActive(false);
        ClearFishIcons();
    }

    private void ClearFishIcons()
    {
        foreach (Transform child in fishHolder)
        {
            Destroy(child.gameObject);
        }
    }

    private void RepositionFishIcons()
    {
        int count = fishHolder.childCount;
        if (count == 0) return;

        float spacing = 0.4f;
        int perRow = 3;
        int rows = Mathf.CeilToInt((float)count / perRow);

        for (int i = 0; i < count; i++)
        {
            int row = i / perRow;
            int itemsInRow = Mathf.Min(perRow, count - row * perRow);
            int col = i % perRow;

            float rowWidth = (itemsInRow - 1) * spacing;
            float x = -rowWidth / 2f + col * spacing;
            float y = (rows - 1) * spacing / 2f - row * spacing;

            fishHolder.GetChild(i).localPosition = new Vector3(x, y, 0);
        }
    }
}