using Mirror;
using UnityEngine;

public class HostBootstrap : MonoBehaviour
{
    private void Start()
    {
        NetworkManager.singleton.autoCreatePlayer = false;
        NetworkManager.singleton.StartHost();
        Debug.Log("Host started");
    }
}