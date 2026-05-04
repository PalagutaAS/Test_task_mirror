using Mirror;

public struct HelloMessage : NetworkMessage
{
    public string Text;
}

public struct SubscribeMessage : NetworkMessage
{
    public int TypeHash;
    public bool Subscribe;
}

public struct EnvelopeMessage : NetworkMessage
{
    public int TypeHash;
    public byte[] Payload;
}