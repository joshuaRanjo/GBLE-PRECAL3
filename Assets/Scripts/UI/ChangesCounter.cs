using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangesCounter : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private GameObject precisionMode;
    [SerializeField] private GameObject notPrecisionMode;
    [SerializeField] private GameObject changeButton;
    
    private void OnEnable() {
        EventManager.StartListening("ChangeCounterUpdate", UpdateChangeText);
        EventManager.StartListening("LevelLoaded", UpdateChangeText);
    }
    private void OnDisable() {
        EventManager.StopListening("ChangeCounterUpdate", UpdateChangeText);
    }

    private void UpdateChangeText()
    {
            text.text = playerData.changeCount.ToString();
            precisionMode.SetActive(true);
            changeButton.SetActive(false);
            notPrecisionMode.SetActive(false);
    }
}
