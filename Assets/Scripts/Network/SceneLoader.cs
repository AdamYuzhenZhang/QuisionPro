using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private bool m_IsQuest;
    [SerializeField] private string m_QuestSceneName;
    [SerializeField] private string m_IOSSceneName;

    public void LoadScene()
    {
        if (NetworkManager.Singleton.IsHost) SceneManager.LoadSceneAsync(m_QuestSceneName, LoadSceneMode.Additive);
        else
        {
            //SceneManager.UnloadSceneAsync(m_QuestSceneName);
            SceneManager.LoadSceneAsync(m_IOSSceneName, LoadSceneMode.Additive);
        }
    }
}
