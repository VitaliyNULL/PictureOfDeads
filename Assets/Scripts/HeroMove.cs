using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMove : MonoBehaviour
{
    private Vector2 startPos;
    private Quaternion startQuaternion;
    private Animator animator;
    public bool hasStayed;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startPos = transform.position;
        startQuaternion = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStayed)
        {
            transform.position = startPos;
            transform.rotation = startQuaternion;
        }
    }
}
