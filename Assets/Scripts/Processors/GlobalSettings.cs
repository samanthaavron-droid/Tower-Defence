using UnityEngine;

public static class GlobalSettings
{
    private static float difficulty = 1f;
    public static float GetSetDifficulty { get { return difficulty; } set { difficulty = value; } }

    private static float timeScaleSpeed = 1f;
    public static float SetTimeScale { get { return timeScaleSpeed; } set { timeScaleSpeed = value; } } 
}