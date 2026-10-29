
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
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
    }
    public void OnButtonLoad()
    {
        SceneManager.LoadScene(1);
    }
}
