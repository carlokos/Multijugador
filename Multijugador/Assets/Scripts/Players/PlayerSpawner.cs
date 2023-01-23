using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        Transform spawPoint;
        int randomNumber = Random.Range(0, spawnPoints.Length);
        if (PhotonNetwork.IsMasterClient)
        {
             spawPoint = spawnPoints[0];
        }
        else
        {
            spawPoint = spawnPoints[1];
        }
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerAvatar"]];
        PhotonNetwork.Instantiate(playerToSpawn.name, spawPoint.position, Quaternion.identity);
    }
}
