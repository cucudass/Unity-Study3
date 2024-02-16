using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    private void Awake() {
        CreatePlayer();
    }

    private void CreatePlayer() {
        PhotonNetwork.Instantiate("Player", RandomPosition(5), Quaternion.identity);
    }

    public Vector3 RandomPosition(float distance) {
        //(1,1,1)�� ���� ������ �������� ��ġ ����
        Vector3 direction = Random.insideUnitSphere;
        direction.Normalize();
        direction *= distance;
        direction.y = 0;

        return direction;
    }

    public void ExitGame() {
        PhotonNetwork.LeaveRoom();
    }
    //���� ���� �� ȣ��
    public override void OnLeftRoom() {
        PhotonNetwork.LoadLevel("Photon Lobby");
    }

    //������ ���� ��, ȣ��Ǵ� �Լ� ������ ������ �ٸ� Ŭ���̾�Ʈ���� �ѱ�
    public override void OnMasterClientSwitched(Player newMasterClient) {
        PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[0]);
    }
}
