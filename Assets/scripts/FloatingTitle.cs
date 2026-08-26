using UnityEngine;

public class FloatingTitle : MonoBehaviour
{
    [Header("둥둥 뜨는 설정")]
    [Tooltip("원이 그려지는 반지름 (움직임의 크기)")]
    public float radius = 15f;

    [Tooltip("회전 속도 (낮을수록 천천히 돕니다)")]
    public float speed = 1.5f;

    [Header("타원 모양 조절 (선택사항)")]
    [Tooltip("가로 세로 비율을 다르게 하고 싶을 때 조정")]
    public float widthMultiplier = 1f;
    public float heightMultiplier = 1f;

    private Vector2 startPosUI;
    private Vector3 startPosWorld;
    private RectTransform rectTransform;

    void Start()
    {
        // UI 엘리먼트(Canvas 안의 Image나 TextMeshPro)인지 확인
        rectTransform = GetComponent<RectTransform>();

        if (rectTransform != null)
        {
            startPosUI = rectTransform.anchoredPosition;
        }
        else
        {
            // 일반 2D Sprite 오브젝트일 경우
            startPosWorld = transform.localPosition;
        }
    }

    void Update()
    {
        // Cos과 Sin을 이용한 원운동 위치 계산
        float x = Mathf.Cos(Time.time * speed) * radius * widthMultiplier;
        float y = Mathf.Sin(Time.time * speed) * radius * heightMultiplier;

        if (rectTransform != null)
        {
            // UI의 경우 anchoredPosition 조정
            rectTransform.anchoredPosition = startPosUI + new Vector2(x, y);
        }
        else
        {
            // 일반 Sprite의 경우 localPosition 조정
            transform.localPosition = startPosWorld + new Vector3(x, y, 0);
        }
    }
}