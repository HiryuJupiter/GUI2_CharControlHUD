﻿using UnityEngine;
using System.Collections;


namespace MyNameSpace
{//A simple abstract class that specifies the variables and methods this object must contain
    public abstract class PoolObject : MonoBehaviour
    {
        protected Pool pool;

        public void SetPool(Pool pool)
        {
            this.pool = pool;
        }

        public virtual void Respawned() { }
        public virtual void Respawned(Vector3 pos)
        {
            transform.position = pos;
            Respawned();
        }

        protected void ReturnToPool()
        {
            pool.Despawn(gameObject);
        }


    }
}