using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLayer : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 screenBounds;
    private float xOffset = 21f;
    private int numSections = 3;
    private Vector3 originalPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        screenBounds =
            mainCamera
                ? mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z)) 
                : Vector3.zero;
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        if (position.x <= -screenBounds.x - xOffset)
        {
            transform.position = new Vector3(position.x + (xOffset * numSections), position.y, position.z);
        }
    }
}
