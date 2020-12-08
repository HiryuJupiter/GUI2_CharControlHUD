using UnityEngine;
using System.Collections;

namespace MyNameSpace
{
    //A class just for storing key environmental positions
    public class EnvironmentlPositions : MonoBehaviour
    {
        public static EnvironmentlPositions Instance;

        [SerializeField] Transform respawnPoint;

        public Transform RespawnPoint => respawnPoint;

        void Awake()
        {
            Instance = this;
        }
    }
}