using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    WaitForSeconds waitForSeconds = new WaitForSeconds(5f);

    void Start()
    {
        if (PhotonNetwork.IsMasterClient) {
            StartCoroutine(SpawnObject());
        }
    }

    public IEnumerator SpawnObject() {
        while (true) {
            PhotonNetwork.InstantiateRoomObject("Metalon", RandomPosition(15), Quaternion.identity);

            yield return waitForSeconds;
        }
    }

    public Vector3 RandomPosition(float distance) {
        //(1,1,1)의 범위 내에서 랜덤으로 위치 설정
        Vector3 direction = Random.insideUnitSphere;
        direction.Normalize();
        direction *= distance;
        direction.y = 0;

        return direction;
    }
}
