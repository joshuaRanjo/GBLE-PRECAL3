using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TestScript : MonoBehaviour
{
    public GameObject object1;
    private void Start() {
        GameObject obj =Instantiate(object1,transform.position, Quaternion.identity);
        obj.transform.SetParent(this.transform);
    }
}
