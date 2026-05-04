using System;
using Mirror;

public interface INetworkMessageService
{

    // Сервер: отправить конкретному соединению (если оно подписано)
    void SendToConnection<T>(NetworkConnectionToClient conn, T message)
        where T : struct, NetworkMessage;

    // Клиент: подписаться на сообщения типа T с обработчиком
    void Subscribe<T>(Action<T> handler) where T : struct, NetworkMessage;

    // Клиент: отписаться
    void Unsubscribe<T>() where T : struct, NetworkMessage;
    
    event Action<NetworkConnectionToClient, int> OnSubscribed;

    // Инициализация (вызывается при старте сервера/клиента)
    void InitServer();
    void InitClient();
}