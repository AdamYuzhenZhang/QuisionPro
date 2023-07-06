using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    [SerializeField] private int width = 256;
    [SerializeField] private int height = 256;
    [SerializeField] private float scale = 20;

    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    [SerializeField] private float rateX;
    [SerializeField] private float rateY;

    [SerializeField] private Color colorA;
    [SerializeField] private Color colorB;
    [SerializeField] private Color colorC;
    [SerializeField] private float blendDuration = 2f;
    private float timer;
    
    private Renderer renderer;
    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (offsetX > 10000f) offsetX = 0;
        if (offsetY > 10000f) offsetY = 0;
        offsetX += rateX * Time.deltaTime;
        offsetY += rateY * Time.deltaTime;
        renderer.material.mainTexture = GenerateTexture();
        LerpColor();
    }

    private void LerpColor()
    {
        timer += Time.deltaTime;

        // Calculate the interpolation factor
        float t = Mathf.PingPong(timer / blendDuration, 1f);

        // Blend the colors using Color.Lerp
        Color blendedColor;
        if (t < 0.5f)
        {
            blendedColor = Color.Lerp(colorA, colorB, t * 2f);
        }
        else
        {
            blendedColor = Color.Lerp(colorB, colorC, (t - 0.5f) * 2f);
        }

        // Assign the blended color to the GameObject's material or color property
        if (renderer != null)
        {
            renderer.material.color = blendedColor;
        }
    }
    
    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }

    Color CalculateColor(int x, int y)
    {
        float xCoord = (float) x / width * scale + offsetX;
        float yCoord = (float) y / height * scale + offsetY;
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample, sample);
    }
}
