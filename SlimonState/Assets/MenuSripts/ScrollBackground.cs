using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBackground : MonoBehaviour
{
    [SerializeField] private RawImage backgroundImage;
    [SerializeField] private float x, y;

    // Update is called once per frame to scroll background image
    void Update()
    {
        backgroundImage.uvRect = new Rect(backgroundImage.uvRect.position + new Vector2(x, y) * Time.deltaTime, backgroundImage.uvRect.size);
    }
}
