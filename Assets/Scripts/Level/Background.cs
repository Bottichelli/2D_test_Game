using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject bg;
    [SerializeField] private Camera cam;
    [SerializeField] private float followSpeed = 0.6f;

    // Update is called once per frame
    void FixedUpdate()
    {
        bg.transform.position = new Vector2 (cam.transform.position.x * followSpeed ,
        cam.transform.position.y * followSpeed / 3f );
    }
}
