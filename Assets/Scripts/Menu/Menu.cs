
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject mainCanvas;
    private void Start()
    {
        if (score != null)
            score.text = "High Score: " + PlayerPrefs.GetFloat("score", 0);
    }
    public void OnSliderDifficulty(float newValue)
    {
        GlobalSettings.GetSetDifficulty = newValue;
    }
    public void OnSliderSpeed(float newValue)
    {
        GlobalSettings.SetTimeScale = newValue;
        Time.timeScale = newValue;
    }
    public void OnButtonLoad()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    public void LoadControls()
    {
        if (controls.activeSelf == false)
        {
            controls.SetActive(true);
            mainCanvas.SetActive(false);
        } else
        {
            controls.SetActive(false);
            mainCanvas.SetActive(true);
        }
    }
}
