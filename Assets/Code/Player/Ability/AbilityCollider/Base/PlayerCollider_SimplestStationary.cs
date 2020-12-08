using UnityEngine;
using System.Collections;


namespace MyNameSpace
{
    public class PlayerCollider_SimplestStationary : MonoBehaviour
    {
        [SerializeField] float lifeTime = 0.2f;
        [SerializeField] int damage = 5;

        protected LayerMask enemyLayer;

        protected void Awake()
        {
            enemyLayer = CharacterSettings.instance.EnemyLayer;
        }

        IEnumerator Start()
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy(transform.parent.gameObject);
        }


        void OnTriggerEnter(Collider other)
        {
            if (enemyLayer == (enemyLayer | 1 << other.gameObject.layer))
            {
                other.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
}