using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float speed = 0.1f;
    private float offset = 0f;
    Renderer rend;
    public GameObject player;
    float pos = 0;
  
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        offset = player.GetComponent<Rigidbody2D>().velocity.x/1500f;
        print(offset);
        pos += offset;


        rend.material.mainTextureOffset = new Vector2(pos, 0);
    }
}
