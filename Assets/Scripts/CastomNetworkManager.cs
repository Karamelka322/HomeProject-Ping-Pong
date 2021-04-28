using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CastomNetworkManager : NetworkManager
{
    private void InstantiateBall()
    {
        GameObject ball = Instantiate(spawnPrefabs[0]);
        NetworkServer.Spawn(ball);
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        var currentPlayerCount = NetworkServer.connections.Count;

        if(currentPlayerCount <= startPositions.Count)
        {
            if (currentPlayerCount == 2)
                InstantiateBall();

            GameObject player = Instantiate(playerPrefab, startPositions[currentPlayerCount - 1].position, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player);
        }
        else
        {
            conn.Disconnect();
        }
    }
}
