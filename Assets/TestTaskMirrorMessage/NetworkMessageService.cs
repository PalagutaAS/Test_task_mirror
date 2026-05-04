using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetworkMessageService : INetworkMessageService
{
    // ===== СЕРВЕРНАЯ ЧАСТЬ =====
    private readonly Dictionary<int, HashSet<int>> _subscribers = new();

    public event Action<NetworkConnectionToClient, int> OnSubscribed;
    
    // ===== КЛИЕНТСКАЯ ЧАСТЬ =====
    private readonly Dictionary<int, Action<NetworkReader>> _clientHandlers = new();

    private static int HashOf(Type t) => t.FullName!.GetHashCode();

    // ---------- INIT SERVER ----------
    public void InitServer()
    {
        NetworkServer.RegisterHandler<SubscribeMessage>(OnSubscribe, false);
        NetworkServer.OnDisconnectedEvent += OnServerDisconnected;

        Debug.Log("[NetworkMessageService] Server initialized");
    }

    private void OnSubscribe(NetworkConnectionToClient conn, SubscribeMessage msg)
    {
        if (!_subscribers.TryGetValue(msg.TypeHash, out var set))
        {
            set = new HashSet<int>();
            _subscribers[msg.TypeHash] = set;
        }

        if (msg.Subscribe)
        {
            set.Add(conn.connectionId);
            Debug.Log($"[Server] connection ID: {conn.connectionId} subscribed to {msg.TypeHash}");
            OnSubscribed?.Invoke(conn, msg.TypeHash);
        }
        else
        {
            set.Remove(conn.connectionId);
        }
    }

    private void OnServerDisconnected(NetworkConnectionToClient conn)
    {
        foreach (var set in _subscribers.Values)
            set.Remove(conn.connectionId);
    }

    // ---------- INIT CLIENT ----------
    public void InitClient()
    {
        NetworkClient.RegisterHandler<EnvelopeMessage>(OnEnvelope, false);

        Debug.Log("[NetworkMessageService] Client initialized");
    }

    private void OnEnvelope(EnvelopeMessage env)
    {
        if (!_clientHandlers.TryGetValue(env.TypeHash, out var handler))
        {
            Debug.LogWarning($"[Client] An unknown type was received {env.TypeHash}");
            return;
        }

        using var reader = NetworkReaderPool.Get(env.Payload);
        handler(reader);
    }

    // ---------- CLIENT API ----------
    public void Subscribe<T>(Action<T> userHandler) where T : struct, NetworkMessage
    {
        int hash = HashOf(typeof(T));
        _clientHandlers[hash] = reader => {
            T msg = reader.Read<T>();
            userHandler(msg);
        };
        NetworkClient.Send(new SubscribeMessage { TypeHash = hash, Subscribe = true });
        Debug.Log($"[Client] Subscribe sent for {typeof(T).Name} with hash {hash}");
    }

    public void Unsubscribe<T>() where T : struct, NetworkMessage
    {
        int hash = HashOf(typeof(T));
        _clientHandlers.Remove(hash);
        if (NetworkClient.active)
            NetworkClient.Send(new SubscribeMessage { TypeHash = hash, Subscribe = false });
    }

    // ---------- SERVER API ----------

    public void SendToConnection<T>(NetworkConnectionToClient conn, T message)
        where T : struct, NetworkMessage
    {
        int hash = HashOf(typeof(T));
        if (!_subscribers.TryGetValue(hash, out var set) || !set.Contains(conn.connectionId))
        {
            Debug.LogWarning($"[Server] connection {conn.connectionId} не подписан на {typeof(T).Name}");
            return;
        }
        conn.Send(BuildEnvelope(hash, message));
    }

    private EnvelopeMessage BuildEnvelope<T>(int hash, T message) where T : struct, NetworkMessage
    {
        using var writer = NetworkWriterPool.Get();
        writer.Write(message);
        return new EnvelopeMessage
        {
            TypeHash = hash,
            Payload = writer.ToArray()
        };
    }
}