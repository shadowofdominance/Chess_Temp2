using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript2 : MonoBehaviour
{
    public float speed = 6f;
    // Start is called before the first frame update
    void Start()
    {
        transform.(Vector3.up * Time.deltaTime * speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
