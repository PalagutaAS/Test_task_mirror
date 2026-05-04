using Mirror;
using UnityEngine;
using VContainer;

public class ClientGreetingManager : MonoBehaviour
{
    [Inject] private INetworkMessageService _messages;

    private void Start()
    {
        _messages.InitClient();

        if (NetworkClient.isConnected)
        {
            Subscribe();
        }
        else
        {
            NetworkClient.OnConnectedEvent += OnConnected;
        }
    }

    private void OnDestroy()
    {
        NetworkClient.OnConnectedEvent -= OnConnected;
    }

    private void OnConnected()
    {
        NetworkClient.OnConnectedEvent -= OnConnected;
        Subscribe();
    }

    private void Subscribe()
    {
        _messages.Subscribe<HelloMessage>(OnHelloReceived);
        Debug.Log("[Client] Subscribed to HelloMessage");
    }

    private void OnHelloReceived(HelloMessage msg)
    {
        Debug.Log($"[Client] Received: {msg.Text}");
    }
}