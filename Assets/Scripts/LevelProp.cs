using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProp : MonoBehaviour
{
    public string prefabName;
    public string objectId;
    public void OnEnable() {
        if(transform.parent.gameObject.name != null)
        if(transform.parent.gameObject.name != "LevelProps" )
        {
            Transform levelprops = GameObject.Find("LevelProps").transform;
            transform.parent = levelprops;
        }
            //
    }
}
