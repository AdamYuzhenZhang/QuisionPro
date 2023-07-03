using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Screen = UnityEngine.Device.Screen;

public class NetworkSystemController : NetworkBehaviour
{
    /// <summary>
    /// Which character to use
    /// </summary>
    public NetworkVariable<int> m_FaceIndex = new(0, NetworkVariableReadPermission.Everyone,
    NetworkVariableWritePermission.Server);
    /// <summary>
    /// Which model and environment is active
    /// </summary>
    public NetworkVariable<int> m_SceneIndex = new(0, NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server);
    /// <summary>
    /// Special elements by the screen
    /// </summary>
    public NetworkVariable<int> m_ScreenIndex = new(0, NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server);

    /// <summary>
    /// Relative position of the observer
    /// </summary>
    public NetworkVariable<Vector3> m_ViewerPosition = new(Vector3.zero,
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private bool m_HostConnected;
    private bool m_ClientConnected;

    private MainCharacterController m_FaceController;
    private ModelController m_SceneController;
    private ScreenController m_ScreenController;

    private Transform m_ServerViewerTransform;
    private Transform m_ClientViewerTransform;
    
    public override void OnNetworkSpawn()
    {
        StartCoroutine(DelayAfterNetworkSpawn());
    }

    IEnumerator DelayAfterNetworkSpawn()
    {
        yield return new WaitForSeconds(5f);
        if (IsServer)
        {
            m_HostConnected = true;
            m_ServerViewerTransform = GameObject.FindGameObjectWithTag("ServerViewer").transform;
            if (m_ServerViewerTransform) m_ViewerPosition.OnValueChanged += OnViewerPositionChangedServer;
        }
        else
        {
            m_ClientConnected = true;
            m_FaceController = FindObjectOfType<MainCharacterController>();
            m_SceneController = FindObjectOfType<ModelController>();
            m_ScreenController = FindObjectOfType<ScreenController>();
            if (m_FaceController) m_FaceIndex.OnValueChanged += OnFaceValueChanged;
            if (m_ScreenController) m_SceneIndex.OnValueChanged += OnSceneValueChanged;
            if (m_ScreenController) m_ScreenIndex.OnValueChanged += OnScreenValueChanged;
            m_ClientViewerTransform = GameObject.FindGameObjectWithTag("ClientViewer").transform;
            if (m_ClientViewerTransform) m_ViewerPosition.OnValueChanged += OnViewerPositionChangedClient;
        }
    }

    public void SetFaceValue(int val)
    {
        if (m_HostConnected) m_FaceIndex.Value = val;
    }
    public void SetSceneValue(int val)
    {
        if (m_HostConnected) m_SceneIndex.Value = val;
    }
    public void SetScreenValue(int val)
    {
        if (m_HostConnected) m_ScreenIndex.Value = val;
    }

    public void UpdateViewerPosition(Vector3 pos)
    {
        m_ViewerPosition.Value = pos;
    }

    
    private void OnFaceValueChanged(int previousVal, int newVal)
    {
        m_FaceController.ChangeCharacter(newVal);
    }
    private void OnSceneValueChanged(int previousVal, int newVal)
    {
        m_SceneController.ChangeToModel(newVal);
    }
    private void OnScreenValueChanged(int previousVal, int newVal)
    {
        m_ScreenController.ChangeToScreen(newVal);
    }
    
    private void OnViewerPositionChangedServer(Vector3 previousVal, Vector3 newVal)
    {
        m_ServerViewerTransform.localPosition = newVal;
    }
    
    private void OnViewerPositionChangedClient(Vector3 previousVal, Vector3 newVal)
    {
        m_ClientViewerTransform.localPosition = newVal;
    }
}
