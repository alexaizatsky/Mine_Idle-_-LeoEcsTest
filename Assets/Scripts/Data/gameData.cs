using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameData
{
    public saveData mySaveData;
    public uiManagerView uiManager;
    public levelManagerView levelManager;
    public gameplaySettingsSO gameplaySettings;
    public dataLoader myDataLoader;
    public gameData(saveData _save, uiManagerView _ui, levelManagerView _level, gameplaySettingsSO _gameplay, dataLoader _loader)
    {
        mySaveData = _save;
        uiManager = _ui;
        levelManager = _level;
        gameplaySettings = _gameplay;
        myDataLoader = _loader;
    }
}
