using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// 
///  Creates a second tilemap on the scene
///  The second tilemap only has its Composite Collider setup with Polygon type geometry
///  and is used as the collider to detect if objects are inside ground objects
/// </summary>
public class GroundDoublerScript : MonoBehaviour
{
    public bool duplicateBool = false;
    private void OnEnable()
    {
        if(duplicateBool == false)
        {
            duplicateBool = true;
            GameObject duplicate = Instantiate(gameObject, transform.position, transform.rotation);
            duplicateBool = false;


            duplicate.GetComponent<CompositeCollider2D>().isTrigger = true;
            duplicate.GetComponent<GroundDoublerScript>().duplicateBool = true;
            duplicate.GetComponent<TilemapRenderer>().enabled = false;
            duplicate.transform.SetParent(transform);
            duplicate.GetComponent<CompositeCollider2D>().geometryType = CompositeCollider2D.GeometryType.Polygons;

            // Remove children from the duplicate
            foreach (Transform child in duplicate.transform)
            {
                Destroy(child.gameObject);
            }
            
            gameObject.GetComponent<CompositeCollider2D>().edgeRadius = 0.015f;
            
        }
        
    }

   private void OnDisable() {
        if(duplicateBool)
        {
            Destroy(gameObject);
        }
   }
}