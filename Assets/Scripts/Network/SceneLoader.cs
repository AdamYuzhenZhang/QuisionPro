using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private bool m_IsQuest;
    [SerializeField] private string m_QuestSceneName;
    [SerializeField] private string m_IOSSceneName;

    private void Awake()
    {
        if (m_IsQuest) SceneManager.LoadSceneAsync(m_QuestSceneName, LoadSceneMode.Additive);
        else SceneManager.LoadSceneAsync(m_IOSSceneName, LoadSceneMode.Additive);
    }
}
