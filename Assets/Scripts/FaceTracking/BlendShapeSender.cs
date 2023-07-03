using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

public struct NetworkedString : INetworkSerializable, System.IEquatable<NetworkedString>
{
    public string str;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        if (serializer.IsReader)
        {
            var reader = serializer.GetFastBufferReader();
            reader.ReadValueSafe(out str);
        }
        else
        {
            var writer = serializer.GetFastBufferWriter();
            writer.WriteValueSafe(str);
        }
    }

    public bool Equals(NetworkedString other)
    {
        if (String.Equals(other.str, str)) return true;
        return false;
    }
}

public class BlendShapeSender : NetworkBehaviour
{
    [SerializeField] private SkinnedMeshRenderer m_SkinnedMeshSource;
    [SerializeField] private SkinnedMeshRenderer m_SkinnedMeshTarget;
    [SerializeField] private int blendShapeNum = 0;
    [SerializeField] private float[] blendShapeWeight = new []{1f};
    
    public NetworkVariable<int> Num = new(0, NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server);

    public NetworkVariable<NetworkedString> Weights = new NetworkVariable<NetworkedString>(
        new NetworkedString()
        {
            str = ""
        });

    [SerializeField] private bool m_HostConnected;
    [SerializeField] private bool m_ClientConnected;

    public override void OnNetworkSpawn()
    {
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
                Weights.Value = new NetworkedString() {str = string.Join(" ", blendShapeWeight)};
                
                Num.Value = blendShapeNum;
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
            Weights.Value = new NetworkedString() {str = string.Join(" ", blendShapeWeight)};
            //Debug.Log(Weights.Value.str);
        }
        else if (m_ClientConnected)
        {
            // assign blendshapedata value to blendshapetarget
            AssignBlendShapeValues(m_SkinnedMeshTarget);
        }
    }

    private void AssignBlendShapeValues(SkinnedMeshRenderer skinnedMesh)
    {
        string[] s = Weights.Value.str.Split(' ');
        for (int i = 0; i < Num.Value; i++)
        {
            skinnedMesh.SetBlendShapeWeight(i, float.Parse(s[i]));
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
