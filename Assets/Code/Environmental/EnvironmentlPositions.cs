using UnityEngine;
using System.Collections;

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