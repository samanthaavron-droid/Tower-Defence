using UnityEngine;

public static class GlobalSettings
{
    private static float modifier = 1f;
    public static float GetSetModifier {  get { return modifier; } set { modifier = value; } }
    private static float difficulty = 1f;
    public static float GetSetDifficulty { get { return difficulty; } set { difficulty = value; } }
}