using Unity.Netcode;

public struct BlendShapeData : INetworkSerializable
{
    public int blendShapeNum;
    public float[] blendShapeWeight;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref blendShapeNum);
        for (int n = 0; n < blendShapeNum; ++n)
        {
            serializer.SerializeValue(ref blendShapeWeight[n]);
        }
        //serializer.SerializeValue(ref blendShapeWeight);
    }
}
