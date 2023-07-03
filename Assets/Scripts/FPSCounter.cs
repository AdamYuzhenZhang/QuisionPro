using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private float timer;
    private int frames;
    private float refreshRate = 0.5f; // Refresh rate in seconds
    private float fps;

    [SerializeField] private TextMeshProUGUI m_FPSText;
    
    private void Update()
    {
        timer += Time.unscaledDeltaTime;
        frames++;

        if (timer > refreshRate)
        {
            fps = frames / timer;
            timer = 0;
            frames = 0;

            m_FPSText.text = "FPS: " + fps;
        }
    }
}
