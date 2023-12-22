using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;




public class DestroyController : MonoBehaviour
{
    public RuleTile wallTile;
    public Tilemap floorTile;

    public float castDistance = 1.0f;
    public Transform raycastPoint;
    public LayerMask layer;
    private float blockDestroyTime = 0.2f;
    
    private Vector3 direction;
    private RaycastHit2D hit;

    private bool destroyingBlock = false;
  
    
  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C) )
        {
            RaycastDirection();
        }
        
    }

    private void RaycastDirection()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            direction.x = Input.GetAxis("Horizontal");
            direction.y = Input.GetAxis("Vertical");
        }

        hit = Physics2D.Raycast(raycastPoint.position, direction, castDistance, layer.value);

        Vector2 endpos = raycastPoint.position + direction;
        
        Debug.DrawLine(raycastPoint.position, endpos, Color.red);

        if (Input.GetKey(KeyCode.B))
        {
            if (hit.collider && !destroyingBlock)
            {
                destroyingBlock = true;
                StartCoroutine(DestroyBlock(hit.collider.gameObject.GetComponent<Tilemap>(), endpos));
            }
            
        }

    }

   private IEnumerator DestroyBlock(Tilemap map, Vector2 pos)
   {
       yield return new WaitForSeconds(blockDestroyTime);

       pos.y = Mathf.Floor(pos.y);
       pos.x = Mathf.Floor(pos.x);
       
       map.SetTile(new Vector3Int((int)pos.x, (int)pos.y,0),null);
       destroyingBlock = false;
   }
}