using System.Collections;
using System.Collections.Generic;
using FlappyBirdClone.Scripts;
using UnityEngine;

public class BackgroundLayer : MonoBehaviour
{
    private float xOffset = 21f;
    private int numSections = 3;
    private Vector3 originalPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        if (position.x <= -GameEvents.Current.ScreenBounds.x - xOffset)
        {
            transform.position = new Vector3(position.x + (xOffset * numSections), position.y, position.z);
        }
    }
}
