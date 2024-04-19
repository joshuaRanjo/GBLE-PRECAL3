using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Player Data", menuName = "PlayerData")]
public class PlayerData: ScriptableObject
{

    public string currentLevel;
    public int currentLevelNum;
    public bool tutorial;



}