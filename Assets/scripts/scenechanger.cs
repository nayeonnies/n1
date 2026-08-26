using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string nextSceneName = "MainGame";

    void Awake()
    {
        // 게임이 켜지는 순간 무조건 1920x1080 창 모드로 해상도 고정
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);

        // 만약 전체 화면으로 1920x1080을 고정하고 싶다면 아래 주석을 사용하세요.
        // Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullscreen);
    }

    public void ChangeToMainGame()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}