using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    public FishDatabase fishDatabase;
    public Text orderText;

    public string currentAttrKey;
    public string currentValue;
    public int currentCount;

    void Start()
    {
        GenerateOrder();
    }

    public void GenerateOrder()
    {
        string[] attrKeys = { "season", "size", "color", "type", "scent" };
        currentAttrKey = attrKeys[Random.Range(0, attrKeys.Length)];

        List<string> values = new List<string>();
        foreach (Fish f in fishDatabase.allFish)
        {
            string v = GetAttrValue(f, currentAttrKey);
            if (!values.Contains(v)) values.Add(v);
        }
        currentValue = values[Random.Range(0, values.Count)];

        int matchingCount = 0;
        foreach (Fish f in fishDatabase.allFish)
        {
            if (GetAttrValue(f, currentAttrKey) == currentValue) matchingCount++;
        }
        currentCount = Mathf.Clamp(Random.Range(1, 3), 1, matchingCount);

        if (orderText != null)
        {
            orderText.text = TranslateValue(currentAttrKey, currentValue) + " " + currentCount + "마리";
        }
    }

    string GetAttrValue(Fish f, string key)
    {
        switch (key)
        {
            case "season": return f.season;
            case "size": return f.size;
            case "color": return f.color;
            case "type": return f.type;
            case "scent": return f.scent;
        }
        return "";
    }

    // 코드값(s, calm, fl 등)을 사람이 읽기 좋은 한국어 문구로 변환
    string TranslateValue(string key, string value)
    {
        switch (key)
        {
            case "season":
                return SeasonKor(value) + "에 어울리는";

            case "size":
                if (value == "s") return "작은 물고기";
                if (value == "m") return "중간 물고기";
                if (value == "l") return "큰 물고기";
                return value;

            case "type":
                if (value == "calm") return "차분한 느낌의";
                if (value == "cute") return "귀여운 느낌의";
                if (value == "elegant") return "우아한 느낌의";
                if (value == "gorgeous") return "화려한 느낌의";
                if (value == "mysterious") return "신비로운 느낌의";
                return value + " 느낌의";

            case "scent":
                if (value == "fl") return "꽃향기 나는";
                if (value == "fr") return "과일향이 나는";
                if (value == "ocean") return "바다향이 나는";
                return value;

            case "color":
                return value + " 색의";

            default:
                return value;
        }
    }

    string SeasonKor(string season)
    {
        switch (season)
        {
            case "summer": return "여름";
            case "winter": return "겨울";
            case "spring": return "봄";
            case "autumn": return "가을";
        }
        return season;
    }

    public float EvaluateOrder(List<Fish> fishInBag)
    {
        if (fishInBag.Count == 0) return 0f;

        int correctCount = 0;
        foreach (Fish f in fishInBag)
        {
            if (GetAttrValue(f, currentAttrKey) == currentValue) correctCount++;
        }

        float attrScore = (float)correctCount / fishInBag.Count;
        bool countMatches = fishInBag.Count == currentCount;
        float countScore = countMatches ? 1f : 0.5f;

        return attrScore * countScore;
    }
}