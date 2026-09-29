using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        
    }
    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1;
        }
    }
    public void EndGame()
    {
#if UNITY_EDITOR
        // stops Play Mode inside the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        //interface
        Time.timeScale = 0;
    }
}
