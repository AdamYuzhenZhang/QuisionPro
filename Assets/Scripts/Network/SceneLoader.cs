using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private bool m_IsQuest;
    [SerializeField] private string m_QuestSceneName;
    [SerializeField] private string m_IOSSceneName;

    [SerializeField] private NetworkController m_NetworkController;

    [SerializeField] private GameObject m_PersistentCamera;
    [SerializeField] private GameObject m_PersistentEventSystem;
    [SerializeField] private GameObject m_PersistentCanvas;
    public void LoadScene()
    {
        Debug.Log("Load Scene");
        if (NetworkManager.Singleton.IsHost) SceneManager.LoadSceneAsync(m_QuestSceneName, LoadSceneMode.Additive);
        else
        {
            //SceneManager.UnloadSceneAsync(m_QuestSceneName);
            SceneManager.LoadSceneAsync(m_IOSSceneName, LoadSceneMode.Additive);
        }

        Debug.Log("Scene Loaded");
        //StartCoroutine(DestroyAfterDelay());
        Destroy(m_PersistentCamera);
        Destroy(m_PersistentCanvas);
        Destroy(m_PersistentEventSystem);;
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(m_PersistentCamera);
        Destroy(m_PersistentCanvas);
        Destroy(m_PersistentEventSystem);
    }

    private void Start()
    {
        Debug.Log("Start");
        if (m_IsQuest)
        {
            Debug.Log("Is Quest");
            // automatically start host
            NetworkManager.Singleton.StartHost();
            Debug.Log("Host Started");
            // automatically load scene
            LoadScene();
            
            // display ip
            // handled in quest scripts
        }
    }
}
