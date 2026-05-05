using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject mainMenuUI;
    public GameObject inGameUI;
    public GameObject gameOverUI;
    public static int pointsCount;
    public static float currentTime;
    public static float totalTime;
    [SerializeField] private TextMeshProUGUI pointDisplay;
    [SerializeField] private TextMeshProUGUI timeDisplay;
    [SerializeField] private TextMeshProUGUI totalTimeDisplay;
    [SerializeField] private TextMeshProUGUI totalPointsDisplay;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 0;
        if (mainMenuUI != null)
        {
            mainMenuUI.SetActive(true);
            SoundManager.PlaySoundLoop(SoundType.STARTMUSIC);
        }
    }

    // Update is called once per frame
    void Update()
    {
        pointDisplay.text = pointsCount.ToString();

        currentTime += Time.deltaTime;  
        int minutes = (int)(currentTime / 60);
        int seconds = (int)(currentTime % 60);
        timeDisplay.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        if (mainMenuUI != null)
        {
            mainMenuUI.SetActive(false);
        }
        inGameUI.SetActive(true);
        SoundManager.StopMusic();
        SoundManager.PlaySoundLoop(SoundType.BGM);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        inGameUI.SetActive(false);
        gameOverUI.SetActive(true);
        SoundManager.StopMusic();
        int minutes = (int)(totalTime / 60);
        int seconds = (int)(totalTime % 60);
        totalTimeDisplay.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        totalPointsDisplay.text = pointsCount.ToString();
    }

    public void RestartGame()
    {
        pointsCount = 0;
        currentTime = 0;
        gameOverUI.SetActive(false);
        mainMenuUI.SetActive(true);
        SoundManager.StopMusic();
        SceneManager.LoadScene("Gameplay");
    }
}
