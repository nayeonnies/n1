using UnityEngine;

public class SwimmingFish : MonoBehaviour
{
    public float horizontalDistance = 0.3f;
    public float verticalDistance = 0.15f;
    public float speed = 1f;

    private Vector3 startPos;
    private float timeOffset; // 물고기마다 서로 다른 시작 타이밍을 저장할 변수

    void Start()
    {
        startPos = transform.localPosition;
        // 0부터 100 사이의 랜덤한 오프셋 값을 지정하여 모든 물고기의 시작 위상을 다르게 함
        timeOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        // Time.time 대신 (Time.time + timeOffset)을 사용
        float currentTime = Time.time + timeOffset;

        float offsetX = Mathf.Sin(currentTime * speed) * horizontalDistance;
        float offsetY = Mathf.Sin(currentTime * speed * 1.7f) * verticalDistance;

        transform.localPosition = startPos + new Vector3(offsetX, offsetY, 0);

        float scaleX = Mathf.Abs(transform.localScale.x);
        transform.localScale = new Vector3(offsetX > 0 ? scaleX : -scaleX, transform.localScale.y, transform.localScale.z);
    }
}