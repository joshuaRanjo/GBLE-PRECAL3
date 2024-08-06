using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// All objects within the level prefab are instantiated 
/// they become children of the LevelProps object. This is done 
/// mostly for object organization.
/// 
/// </summary>
public class LevelProp : MonoBehaviour
{
    public string prefabName;
    public string objectId;
    private int i = 0;
    public void Start() {
        {
            Transform levelprops = GameObject.Find("LevelProps").transform;
            transform.SetParent(levelprops);
        }
            
    }
}
