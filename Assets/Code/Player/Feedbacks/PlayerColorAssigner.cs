using UnityEngine;
using System.Collections;

public class PlayerColorAssigner : MonoBehaviour
{
    [SerializeField] MeshRenderer hair;
    [SerializeField] MeshRenderer eyeL;
    [SerializeField] MeshRenderer eyeR;
    [SerializeField] MeshRenderer head;
    [SerializeField] MeshRenderer body;
    
    public void UpdateColor (GameData data)
    {
        hair.material.color = data.hairColor;
        eyeL.material.color  = data.eyeColor;
        eyeR.material.color  = data.eyeColor;
        head.material.color = data.headColor;
        body.material.color = data.bodyColor;
    }
}