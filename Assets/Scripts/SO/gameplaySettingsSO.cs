using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


[CreateAssetMenu(fileName = "GameplaySettings", menuName = "ScriptableObjects/gameplaySettings", order = 1)]

public class gameplaySettingsSO : ScriptableObject
{
	public progressionLevel[] playerProgression;
	public progressionLevel[] priceProgression;
}

[System.Serializable]
public class progressionLevel
{
	public int value;
	public int price;
}


