using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform player; // Reference to the player's transform
    public float maxAngle = 30f; // Maximum angle to follow the player
    public float minAngle = -30f; // Minimum angle to follow the player
    public float rotationSpeed = 5f; // Speed of rotation
    public bool facingLeft = false;
    public float angle;
    public float angleCheck;

    void Start()
    {
        // Find the player object by tag and get its transform
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found. Make sure it has the tag 'Player'.");
        }
    }

    void Update()
    {
        // Check if the player has been found
        if (player != null)
        {
            // Calculate the direction from the object to the player
            Vector3 direction = player.position - transform.position;
            direction.z = 0; // Keep the z-axis value at 0 for 2D
            
            // Calculate the angle in degrees
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
             angleCheck = angle;
            if(facingLeft)
            {
                if(angleCheck > 90f)
                {
                    angleCheck = 180 - angleCheck;
                }
                if(angleCheck < -90f)
                {
                    angleCheck = Mathf.Abs(-180 - angleCheck) * -1 ;
                }
            }

            Quaternion targetRotation;

            // Check if the angle is within the specified range
            if (angleCheck >= minAngle && angleCheck <= maxAngle )
            {
                // Calculate the target rotation to face the player
                float angleUse = angle;
                if(facingLeft)
                    angleUse = -(180-angleUse);

                targetRotation = Quaternion.Euler(new Vector3(0, 0, angleUse));
                
                if(facingLeft && (angle == angleCheck))
                    targetRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                // Calculate the target rotation to return to an angle of 0 degrees
                targetRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }

            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
