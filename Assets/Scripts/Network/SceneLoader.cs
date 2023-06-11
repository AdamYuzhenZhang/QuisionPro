using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string m_TargetSceneName;

    private void Awake()
    {
        //SceneManager.LoadSceneAsync(m_TargetSceneName, LoadSceneMode.Additive);
    }
}
