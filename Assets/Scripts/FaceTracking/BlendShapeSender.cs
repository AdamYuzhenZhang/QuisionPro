using System.Collections;
using System.Collections.Generic;
using Oculus.Movement.UI;
using Unity.Collections;
using Unity.Netcode;
using UnityEditor.PackageManager;
using UnityEngine;

public class BlendShapeSender : NetworkBehaviour
{
    [SerializeField] private SkinnedMeshRenderer m_SkinnedMeshSource;
    [SerializeField] private SkinnedMeshRenderer m_SkinnedMeshTarget;
    [SerializeField] private int blendShapeNum = 0;
    [SerializeField] private float[] blendShapeWeight = new []{1f};
    //private NativeArray<float> blendShapeWeightsArray;
    
    // network variable
    public NetworkVariable<BlendShapeData> m_BlendShapeData = new NetworkVariable<BlendShapeData>(
        new BlendShapeData(),
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
        );
    
    /*
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref blendShapeWeight);
    }
    */

    public NetworkVariable<int> Num = new(0, NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server);

    //public NetworkVariable<NativeArray<float>> Weights = new NetworkVariable<NativeArray<float>>();

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
        yield return new WaitForSeconds(5f);
        if (IsServer)
        {
            // find the blendshapesource in scene
            m_SkinnedMeshSource =
                GameObject.FindGameObjectWithTag("BlendShapeSource").GetComponent<SkinnedMeshRenderer>();
            if (m_SkinnedMeshSource)
            {
                // initialize blendshape value
                SetBlendShapeValues(m_SkinnedMeshSource);
                BlendShapeData data = new BlendShapeData();
                data.blendShapeNum = blendShapeNum;
                data.blendShapeWeight = blendShapeWeight;
                m_BlendShapeData.Value = data;
                
                Num.Value = blendShapeNum;
                //Weights.Value = blendShapeWeightsArray;
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
        //NativeArray<float> weightsArray = new NativeArray<float>(blendShapeNum, Allocator.Persistent);
        
        // Retrieve and store the blend shape weights
        for (int i = 0; i < blendShapeNum; i++)
        {
            weights[i] = skinnedMesh.GetBlendShapeWeight(i);
            //weightsArray[i] = skinnedMesh.GetBlendShapeWeight(i);
        }

        blendShapeWeight = weights;
        //blendShapeWeightsArray = weightsArray;
    }

    private void Update()
    {
        if (m_HostConnected)
        {
            // change blendshape value
            SetBlendShapeValues(m_SkinnedMeshSource);
            //m_BlendShapeData.blendShapeWeight = blendShapeWeight;
            //Weights.Value = blendShapeWeightsArray;

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
        blendShapeNum = m_BlendShapeData.Value.blendShapeNum;
        blendShapeWeight = m_BlendShapeData.Value.blendShapeWeight;
        for (int i = 0; i < blendShapeNum; i++)
        {
            //skinnedMesh.SetBlendShapeWeight(i, m_BlendShapeData.blendShapeWeight[i]);
            skinnedMesh.SetBlendShapeWeight(i, blendShapeWeight[i]);
            //skinnedMesh.SetBlendShapeWeight(i, Weights.Value[i]);
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
