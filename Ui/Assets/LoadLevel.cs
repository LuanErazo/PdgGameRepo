using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoozyUI;

public class LoadLevel : MonoBehaviour {

    public const string LOADED_LEVEL = "lOADED_LEVEL";
    public const int NO_LEVEL_LOADED = -1;

    public LevelManagementData data;

    [SerializeField]
    private int currentLoadedLevel = NO_LEVEL_LOADED;

    private void Awake()
    {
        currentLoadedLevel = PlayerPrefs.GetInt(LOADED_LEVEL, NO_LEVEL_LOADED);

    }

    private void updateCurrentLoadedLevel()
    {
        currentLoadedLevel = PlayerPrefs.GetInt(LOADED_LEVEL, NO_LEVEL_LOADED);

    }

    public void restart() {
        LevelManagementData.pjPuntos = -500;
        data.turnos = 0;
        data.resetTime(8);
    }



    public void loadLevel(int levelNumber)
    {
        UIManager.SendGameEvent("LoadLvl" + levelNumber);
        PlayerPrefs.SetInt(LOADED_LEVEL, levelNumber);
        updateCurrentLoadedLevel();
    }

    public void unloadLevel(int levelNumber)
    {
        UIManager.SendGameEvent("UnloadLvl" + levelNumber);
        PlayerPrefs.SetInt(LOADED_LEVEL, NO_LEVEL_LOADED);
        PlayerPrefs.Save();
        updateCurrentLoadedLevel();

    }

    public void UnloadCurrentLevel() {
        int levelNumber = PlayerPrefs.GetInt(LOADED_LEVEL, NO_LEVEL_LOADED);
        if (levelNumber == NO_LEVEL_LOADED)
        {
            return;
        }

        unloadLevel(levelNumber);

    }
}
