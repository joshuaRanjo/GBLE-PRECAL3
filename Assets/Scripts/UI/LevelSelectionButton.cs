using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelSelectionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveCount;
    [SerializeField] private GameObject checkMarkimg;

    public void SetMoveCount(int number)
    {
        //moveCount.text = number.ToString();
        moveCount.text = "";
    }
    public void EnableCheckMark()
    {
        checkMarkimg.SetActive(true);
    }    
}
