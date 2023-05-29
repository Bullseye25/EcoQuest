
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // [System.Serializable]
    // public class Level
    // {
    //     public string levelText;
    //     public string[] stageTexts;
    // }

    // public Level[] levels;

    // private int currentLevelIndex = 0;
    // private int currentStageIndex = 0;

    // private void Start()
    // {
    //     ShowLevelAndStage(1, 1);
    // }

    // private void ShowLevelAndStage(int levelIndex, int stageIndex)
    // {
    //     if (levelIndex >= 1 && levelIndex <= levels.Length && stageIndex >= 1 && stageIndex <= levels[levelIndex - 1].stageTexts.Length)
    //     {
    //         // Show the level text
    //         string levelText = GetLevelText(levelIndex);
    //         Debug.Log("Level " + levelIndex + ": " + levelText);

    //         // Show the stage text
    //         string stageText = GetStageText(levelIndex, stageIndex);
    //         Debug.Log("Stage " + stageIndex + ": " + stageText);
    //     }
    //     else
    //     {
    //         Debug.LogError("Invalid level or stage index!");
    //     }
    // }

    // public string GetLevelText(int levelIndex)
    // {
    //     if (levelIndex >= 1 && levelIndex <= levels.Length)
    //     {
    //         return levels[levelIndex - 1].levelText;
    //     }

    //     Debug.LogError("Invalid level index!");
    //     return string.Empty;
    // }

    // public string GetStageText(int levelIndex, int stageIndex)
    // {
    //     if (levelIndex >= 1 && levelIndex <= levels.Length && stageIndex >= 1 && stageIndex <= levels[levelIndex - 1].stageTexts.Length)
    //     {
    //         return levels[levelIndex - 1].stageTexts[stageIndex - 1];
    //     }

    //     Debug.LogError("Invalid level or stage index!");
    //     return string.Empty;
    // }
}
