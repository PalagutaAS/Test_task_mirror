using System.Collections.Generic;
using Mirror;
using UnityEngine;
using VContainer;

public class ServerGreetingManager : MonoBehaviour
{
    [Inject] private INetworkMessageService _messages;

    private readonly HashSet<int> _greetedConnections = new();

    private void Start()
    {
        _messages.InitServer();
        _messages.OnSubscribed += OnClientSubscribed;
        NetworkServer.OnDisconnectedEvent += OnServerDisconnected;
    }

    private void OnDestroy()
    {
        if (_messages != null)
            _messages.OnSubscribed -= OnClientSubscribed;
        NetworkServer.OnDisconnectedEvent -= OnServerDisconnected;
    }

    private void OnClientSubscribed(NetworkConnectionToClient conn, int typeHash)
    {
        if (typeHash == typeof(HelloMessage).FullName!.GetHashCode())
        {
            if (!_greetedConnections.Contains(conn.connectionId))
            {
                _greetedConnections.Add(conn.connectionId);
                _messages.SendToConnection(conn, new HelloMessage { Text = "Hello Client!" });
                Debug.Log($"[Server] Sent HelloMessage to conn {conn.connectionId}");
            }
            else
            {
                Debug.Log($"[Server] Greeting already sent to conn {conn.connectionId}, skipping.");
            }
        }
    }

    private void OnServerDisconnected(NetworkConnectionToClient conn)
    {
        _greetedConnections.Remove(conn.connectionId);
    }
}