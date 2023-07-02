using System.Collections;
using System.Collections.Generic;
using Oculus.Movement.UI;
using Unity.Netcode;
using UnityEditor.PackageManager;
using UnityEngine;

public class BlendShapeSender : NetworkBehaviour
{
    [SerializeField] private SkinnedMeshRenderer m_SkinnedMeshSource;
    [SerializeField] private SkinnedMeshRenderer m_SkinnedMeshTarget;
    [SerializeField] private int blendShapeNum = 0;
    [SerializeField] private float[] blendShapeWeight = new []{1f};
    // network variable
    private BlendShapeData m_BlendShapeData = new BlendShapeData();

    [SerializeField] private bool m_HostConnected;
    [SerializeField] private bool m_ClientConnected;

    private void Start()
    {
        
    }
    
    public override void OnNetworkSpawn()
    {
        //Debug.Log("Network !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        StartCoroutine(DelayAfterNetworkSpawn());
    }

    IEnumerator DelayAfterNetworkSpawn()
    {
        yield return new WaitForSeconds(2f);
        if (IsServer)
        {
            // find the blendshapesource in scene
            m_SkinnedMeshSource =
                GameObject.FindGameObjectWithTag("BlendShapeSource").GetComponent<SkinnedMeshRenderer>();
            if (m_SkinnedMeshSource)
            {
                // initialize blendshape value
                SetBlendShapeValues(m_SkinnedMeshSource);
                m_BlendShapeData.blendShapeNum = blendShapeNum;
                m_BlendShapeData.blendShapeWeight = blendShapeWeight;
                // this is the host so can change the value of blendshapedata
                m_HostConnected = true;
            }
        }
        else
        {
            // find the blendshapetarget in scene
            m_SkinnedMeshTarget =
                GameObject.FindGameObjectWithTag("BlendShapeTarget").GetComponent<SkinnedMeshRenderer>();
            if (m_SkinnedMeshTarget) m_ClientConnected = true;
        }
    }

    private void SetBlendShapeValues(SkinnedMeshRenderer skinnedMesh)
    {
        blendShapeNum = skinnedMesh.sharedMesh.blendShapeCount;

        // Initialize the blend shape weights array
        float[] weights = new float[blendShapeNum];

        // Retrieve and store the blend shape weights
        for (int i = 0; i < blendShapeNum; i++)
        {
            weights[i] = skinnedMesh.GetBlendShapeWeight(i);
        }

        blendShapeWeight = weights;
    }

    private void Update()
    {
        if (m_HostConnected)
        {
            // change blendshape value
            SetBlendShapeValues(m_SkinnedMeshSource);
            m_BlendShapeData.blendShapeWeight = blendShapeWeight;
            
            //SendBlendShapeMessage();
        }
        else if (m_ClientConnected)
        {
            // assign blendshapedata value to blendshapetarget
            AssignBlendShapeValues(m_SkinnedMeshTarget);
        }
    }
    
    private void AssignBlendShapeValues(SkinnedMeshRenderer skinnedMesh)
    {
        for (int i = 0; i < m_BlendShapeData.blendShapeNum; i++)
        {
            skinnedMesh.SetBlendShapeWeight(i, m_BlendShapeData.blendShapeWeight[i]);
        }
    }

    /*
    private void SendBlendShapeMessage()
    {
        BlendShapeData blendShapeData = new BlendShapeData()
        {
            blendShapeNum = blendShapeNum,
            blendShapeWeight = blendShapeWeight
        };

        // Send the custom network message to other clients
        NetworkManager.Singleton.SendMessage(blendShapeData, DeliveryMethod.ReliableSequenced);
    }
    */
}
