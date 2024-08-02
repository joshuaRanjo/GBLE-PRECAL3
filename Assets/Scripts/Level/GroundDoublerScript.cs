using UnityEngine;
using UnityEngine.Tilemaps;
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