using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.SerializationRate = 60f;

        // 나의 플레이어 생성
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
