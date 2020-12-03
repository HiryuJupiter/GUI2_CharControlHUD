using UnityEngine;
using System.Collections;

namespace Assets.Code._usefulButUnused
{
    public class _randomFromClass : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

/*
 using UnityEngine;
using UnityEngine.UI;
 
public class ButtonCreator : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject panelToAttachButtonsTo;
    void Start()//Creates a button and sets it up
    {
        GameObject button = (GameObject)Instantiate(buttonPrefab);
        button.transform.SetParent(panelToAttachButtonsTo.transform);//Setting button parent
        button.GetComponent<Button>().onClick.AddListener(OnClick);//Setting what button does when clicked
//Next line assumes button has child with text as first gameobject like button created from GameObject->UI->Button
        button.transform.GetChild(0).GetComponent<Text>().text = "This is button text";//Changing text
    }
    void OnClick()
    {
        Debug.Log("clicked!");
    }
}
 */


/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerDefense
{
    public class OrbitCamera : MonoBehaviour
    {
        public Camera attachedCamera;
        public float minYAngle = 30f, maxYAngle = 90f;
        public float ySpeed = 120f, xSpeed = 120f;
        void FixedUpdate()
        {
            if (Input.GetMouseButton(1))
            {
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");
                Vector3 euler = transform.eulerAngles;
                euler.x -= mouseY * ySpeed * Time.deltaTime;
                euler.y += mouseX * xSpeed * Time.deltaTime;
                euler.x = Mathf.Clamp(euler.x, minYAngle, maxYAngle);
                transform.eulerAngles = euler;
            }
        }
    }
}
 */