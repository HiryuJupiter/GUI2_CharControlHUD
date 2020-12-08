using UnityEngine;
using System.Collections;


namespace MyNameSpace
{
    public class PlayerCollider_Projectile : PlayerCollider_SimplestStationary
    {
        [SerializeField] float movespeed = 29;

        protected virtual void FixedUpdate()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * movespeed);
            Debug.DrawRay(transform.position, transform.forward, Color.yellow);
        }
    }
}