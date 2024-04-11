using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProp : MonoBehaviour
{
    public void OnEnable() {
        if(transform.parent.gameObject.name != "LevelProps")
            transform.parent = GameObject.Find("LevelProps").transform;
    }
}
