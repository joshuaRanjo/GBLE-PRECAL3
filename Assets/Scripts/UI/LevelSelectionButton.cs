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
        number = 0;
        if(number <= 0)
        {
            moveCount.text = "";
        }
        else
        {
             moveCount.text = number.ToString();
        }
       
        
    }
    public void EnableCheckMark()
    {
        checkMarkimg.SetActive(true);
    }    
}
