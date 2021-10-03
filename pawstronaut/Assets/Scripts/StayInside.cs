using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInside : MonoBehaviour
{
    private float halfObjectSize;
    private float halfObjectHeight;
    
    void Start()
    {
        halfObjectSize = (GetComponent<SpriteRenderer>().bounds.size.x) / 2;
        halfObjectHeight = (GetComponent<SpriteRenderer>().bounds.size.y) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -GameManager.screenBounds.x+halfObjectSize, GameManager.screenBounds.x-halfObjectSize),
                                         Mathf.Clamp(transform.position.y, -GameManager.screenBounds.y+ halfObjectHeight, GameManager.screenBounds.y- halfObjectHeight), 
                                         transform.position.z);
    }
}
