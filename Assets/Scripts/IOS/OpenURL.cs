using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public void OpenGivenURL(string url)
    {
        Application.OpenURL(url);
    }
}
