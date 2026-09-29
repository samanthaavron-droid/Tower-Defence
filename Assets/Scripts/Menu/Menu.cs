
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    private void Start()
    {
        score.text = "High Score: " + PlayerPrefs.GetFloat("score", 0);
    }
    public void OnSliderDifficulty(float newValue)
    {
        GlobalSettings.difficulty = newValue;
    }
    public void OnSliderSpeed(float newValue)
    {
        GlobalSettings.modifier = newValue;
    }
    public void OnButtonLoad()
    {
        SceneManager.LoadScene(1);
    }
}
