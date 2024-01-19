using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject message;
    public Vector2 instantiationPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("DEBUG::Instantiated a message");
            GameObject.Instantiate(message, instantiationPosition, Quaternion.identity);
        }
        
    }

}
