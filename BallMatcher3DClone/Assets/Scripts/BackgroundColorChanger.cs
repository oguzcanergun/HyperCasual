using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundColorChanger : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color[] colorPalette = new Color[6];

    // Start is called before the first frame update
    void Start()
    {
        ChangeColor();
    }

    public void ChangeColor()
    {
        backgroundImage.color = colorPalette[Random.Range(0, colorPalette.Length)];
    }
}
