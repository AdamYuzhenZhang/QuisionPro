using UnityEngine;

public class SceneController : MonoBehaviour
{
    private void Awake()
    {
        // Disable screen timeout
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    
    // stores the left eye and right eye
    private GameObject m_LeftEye;
    private GameObject m_RightEye;
    public GameObject LeftEye
    {
        get => m_LeftEye;
        set => m_LeftEye = value;
    }
    public GameObject RightEye
    {
        get => m_RightEye;
        set => m_RightEye = value;
    }

    [SerializeField] private GameObject m_ViewerCamRoot;

    private NetworkSystemController m_NetworkSystemController;

    private bool m_AutoResetWhenNoEyes = true;
    private bool m_AutoOffScreen = false;
    private int m_ScreenBeforeOff;
    private bool m_ScreenTurnedOff;
    [SerializeField] private ModelController m_ModelController;
    [SerializeField] private int m_OffScreenIdx;

    [SerializeField] private Transform m_ViewerResetTransform;

    private void Start()
    {
        m_NetworkSystemController = FindObjectOfType<NetworkSystemController>();
    }

    private void Update()
    {
        
    }

    public void UpdateWithARFace(bool visible)
    {
        // move camera to left eye if it exists
        if (LeftEye && visible)
        {
            Vector3 eyePos = LeftEye.transform.position;
            m_ViewerCamRoot.transform.position = new Vector3(-eyePos.x, eyePos.y, eyePos.z);
            //m_ViewerCamRoot.transform.position = LeftEye.transform.position;
            //m_ViewerCamRoot.transform.rotation = LeftEye.transform.rotation;
            //if (m_NetworkSystemController) 
            //    m_NetworkSystemController.UpdateViewerPosition(m_ViewerCamRoot.transform.localPosition);
            if (m_ScreenTurnedOff)
            {
                m_ScreenTurnedOff = false;
                m_ModelController.ChangeToModel(m_ScreenBeforeOff);
            }
            
        }
        
        if (!visible && m_AutoResetWhenNoEyes)
        {
            m_ViewerCamRoot.transform.position = m_ViewerResetTransform.position;

            if (m_AutoOffScreen)
            {
                if (!m_ScreenTurnedOff)
                {
                    m_ScreenTurnedOff = true;
                    m_ScreenBeforeOff = m_ModelController.GetActiveIndex();
                    m_ModelController.ChangeToModel(m_OffScreenIdx);
                }
                
            }

            //if (m_NetworkSystemController) 
            //    m_NetworkSystemController.UpdateViewerPosition(m_ViewerCamRoot.transform.localPosition);
        }
        
        MatchSceneRotation();
    }

    public void ToggleAutoReset()
    {
        m_AutoResetWhenNoEyes = !m_AutoResetWhenNoEyes;
    }
    public void ToggleAutoOffScreen()
    {
        m_AutoOffScreen = !m_AutoOffScreen;
    }


    // match scene rotation
    [SerializeField] private GameObject m_SceneRoot;
    [SerializeField] private GameObject m_ARCam;

    private void MatchSceneRotation()
    {
        Vector3 eulerRotation = m_ARCam.transform.rotation.eulerAngles;
        m_SceneRoot.transform.rotation = Quaternion.Euler(new Vector3(eulerRotation.x, -eulerRotation.y, -eulerRotation.z));
    }
}
