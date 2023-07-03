using Unity.Netcode;

public struct BlendShapeData : INetworkSerializable
{
    public int blendShapeNum;
    public float[] blendShapeWeight;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref blendShapeNum);
        serializer.SerializeValue(ref blendShapeWeight);
    }
}
