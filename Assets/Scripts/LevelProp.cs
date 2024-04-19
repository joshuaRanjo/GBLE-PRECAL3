using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProp : MonoBehaviour
{
    public string prefabName;
    public string objectId;
    private int i = 0;
    public void Start() {
        if(transform.parent.gameObject.name != null)
        if(transform.parent.gameObject.name != "LevelProps" )
        {
            Transform levelprops = GameObject.Find("LevelProps").transform;
            transform.SetParent(levelprops);
        }
            
    }
}
