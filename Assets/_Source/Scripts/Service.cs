using UnityEngine;

public static class Service
{
    public static float Calculate(float level, float baseValue, float degree)
    {
        float value = baseValue * Mathf.Pow(degree, level);
        value = Mathf.Round(value);
        return value;
    }

    public static float IncreaseValue(float value, float grade)
    {
        float result = value * (grade * 0.1f + 1f);
        return result;
    }

    public static float Income
    {
        get => Game.Data.Saves.IncomeLevel * 0.1f + 1;
    }

    public static float Power
    {
        get => Game.Data.Saves.PowerLevel * 0.5f + 1;
    }
}