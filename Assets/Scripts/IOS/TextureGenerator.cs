using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureGenerator : MonoBehaviour
{
    [SerializeField] private int width = 2048;
    [SerializeField] private int height = 2048;
    [SerializeField] private float scale = 20;
    public Texture2D Texture1;
    public Texture2D Texture2;
    public Texture2D Texture3;
    [SerializeField] private float offset1 = 0f;
    [SerializeField] private float offset2 = 50f;
    [SerializeField] private float offset3 = 80f;

    private void Awake()
    {
        Texture1 = GenerateTexture(offset1);
        Texture2 = GenerateTexture(offset2);
        Texture3 = GenerateTexture(offset3);
    }

    Texture2D GenerateTexture(float offset)
    {
        Texture2D texture = new Texture2D(width, height);
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y, offset);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }
    Color CalculateColor(int x, int y, float offset)
    {
        float xCoord = (float) x / width * scale + offset;
        float yCoord = (float) y / height * scale + offset;
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample, sample);
    }
}
