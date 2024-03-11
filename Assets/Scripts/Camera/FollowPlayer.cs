using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Transform target;
    public Transform target2;
    private Transform savedTarget2;
    [SerializeField] private LineData ld;
    [SerializeField] private QuestionData qd;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float cameraSpeed = 0.1f;

    private Vector3 minValues, maxValues;
    private GameObject bounds;

    public bool followingPlayer = true;
    public bool following = true;


    private void OnEnable() {
        EventManager.StartListening("EnterPuzzle", OtherFocus);
        EventManager.StartListening("ExitPuzzle", ExitPuzzle);
    }

    private void OnDisable() {
        EventManager.StopListening("EnterPuzzle", OtherFocus);
        EventManager.StopListening("ExitPuzzle", ExitPuzzle);
    }

    private void Awake() {
        /*
        float n,s,e,w;
        bounds = GameObject.FindGameObjectWithTag("Border");
        n = bounds.transform.Find("N").gameObject.transform.position.y;
        s = bounds.transform.Find("S").gameObject.transform.position.y;
        e = bounds.transform.Find("E").gameObject.transform.position.x;
        w = bounds.transform.Find("W").gameObject.transform.position.x;
        minValues = new Vector3(w,s,-10);
        maxValues = new Vector3(e,n,-10);
        */
    }
    private void Start() {
        ExitPuzzle();
    }

    void FixedUpdate()
    {
        /*
        float n,s,e,w;
        n = bounds.transform.Find("N").gameObject.transform.position.y;
        s = bounds.transform.Find("S").gameObject.transform.position.y;
        e = bounds.transform.Find("E").gameObject.transform.position.x;
        w = bounds.transform.Find("W").gameObject.transform.position.x;
        minValues = new Vector3(w,s,-10);
        maxValues = new Vector3(e,n,-10);
        */
            if(following)
            Follow();
        
        
        
    }

    private void Follow()
    {
        if(target2 != null)
        {
            target = target2;
        }
        else{
            if(followingPlayer)
                target = player;
            
        }
            Vector3 finalPosition = target.position + cameraOffset;

            ///
            /*
            Vector3 boundPosition = new Vector3(
                Mathf.Clamp(finalPosition.x, minValues.x, maxValues.x),
                Mathf.Clamp(finalPosition.y, minValues.y, maxValues.y),
                Mathf.Clamp(finalPosition.z, minValues.z, maxValues.z));
            */
            Vector3 lerpPosition = Vector3.Lerp(transform.position, finalPosition, cameraSpeed);
            transform.position = lerpPosition;
    
    }


    private void OtherFocus()
    {
        target = ld.workArea;

        Vector3 offset = cameraOffset;
        offset.y = qd.yOffset;
        offset.x = -3.2f + qd.xOffset;
        cameraOffset = offset;
        followingPlayer = false;
        if(target2 != null)
        {
            savedTarget2 = target2;
            target2 = null;
        }
    }

    public void FocusOnTransform(Transform newTarget)
    {
        Debug.Log("Focusing");
        target2 = newTarget;
    }
    private void ExitPuzzle()
    {
        target = player;
        followingPlayer = true;
        Vector3 offset = cameraOffset;
        offset.y = 5f;
        offset.x = 0f;
        cameraOffset = offset;

        if(savedTarget2 != null)
        {
            target2 = savedTarget2;
            savedTarget2 = null;
        }
    }

    public void StartFollowing()
    {
        following = true;
    }

    public void StopFollowing()
    {
        following = false;
    }
}   
