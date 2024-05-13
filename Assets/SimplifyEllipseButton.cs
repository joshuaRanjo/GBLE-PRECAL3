using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimplifyEllipseButton : MonoBehaviour
{
    private Button simpButton;

    private void Start() {
        simpButton = GetComponent<Button>();
        
    }

    public void SimplifyEllipseSwitch()
    {
        EventManager.TriggerEvent("SwitchSimplifiedEllipse");
    }
}
