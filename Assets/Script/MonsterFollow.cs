using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterFollow : MonoBehaviour
{
   public Transform target;
   public float speed = 3f;
   public float rotateSpeed = 0.01f;
   private Rigidbody2D rb;

   private void Start()
   {
      rb = GetComponent<Rigidbody2D>();
   }

   private void Update()
   {
      if (!target)
      {
         GetTarget();
      }
      else
      {
         RotateTowardsTarget();
      }
   }

   private void RotateTowardsTarget()
   {
      Vector2 targetDirection = target.position - transform.position;
      float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90;
      Quaternion q = Quaternion.LookRotation(Vector3.forward, targetDirection);
      transform.rotation = Quaternion.Slerp(transform.rotation, q, rotateSpeed);
   }

   private void FixedUpdate()
   {
      rb.velocity = transform.up * speed;
   }

   // Null check and assignment in GetTarget()
   private void GetTarget()
   {
      target = GameObject.FindGameObjectWithTag("Player")?.transform;
   }

   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.tag == "Player")
      {
         Destroy(other.gameObject);
      }
   }
}