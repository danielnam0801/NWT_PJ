using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public List<ThemeInfo> ThemeList;
    [SerializeField]
    private int currentStageIndex;
    [SerializeField]
    private int currentThemeIndex;
    [SerializeField]
    private Stage currentStage;

    public Portal ProtalObj;

    private void Start()
    {
        currentStage = ThemeList[0].StageList[0];
        Instantiate(currentStage);
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ChangeStage()
    {
        Destroy(currentStage);

        currentStageIndex++;

        if(currentStageIndex == ThemeList[currentThemeIndex].StageList.Count)
        {
            currentStageIndex = 0;
            currentThemeIndex++;
        }

        currentStage = ThemeList[currentThemeIndex].StageList[currentStageIndex];

        Instantiate(currentStage);
    }
}
