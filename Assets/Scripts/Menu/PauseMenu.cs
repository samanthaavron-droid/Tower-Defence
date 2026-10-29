using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    private static bool gameActive;
    public static bool getGameActive {  get { return gameActive; } }
    [SerializeField] private GameObject EndUi;
    [SerializeField] private GameObject pauseUi;
    void Awake()
    {
        gameActive = true;
        instance = this;
        if (EndUi != null) EndUi.SetActive(false);
        if (pauseUi != null) pauseUi.SetActive(false);
    }
    void Update()
    {
        if (gameActive == false) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Pause();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
        {
            if (pauseUi != null) pauseUi.SetActive(false);
            EndGame();
        }
    }
    private void Pause()
    {
        if (Time.timeScale != 0)
        {
            if (pauseUi != null) pauseUi.SetActive(true);
            Time.timeScale = 0;
        } else
        {
            if (pauseUi != null) pauseUi.SetActive(false);
            Time.timeScale = GlobalSettings.SetTimeScale;
        }
    }
    public void EndGame()
    {
        gameActive = false;
        if (EndUi != null) EndUi.SetActive(true);
    }
    public void LoadScene(int id)
    {
        Time.timeScale = GlobalSettings.SetTimeScale;
        gameActive = true;
        SceneManager.LoadScene(id);
    }
    public void UpdateSpeedScale(float f)
    {
        GlobalSettings.SetTimeScale = f;
    }
}
