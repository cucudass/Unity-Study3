using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectServer : MonoBehaviourPunCallbacks
{
    [SerializeField] Canvas canvasRoom;
    [SerializeField] Canvas canvasLobby;
    [SerializeField] TMP_Dropdown server;
    
    private void Awake() {
        server.options[0].text = "Asia";
        server.options[1].text = "Europe";
        server.options[2].text = "America";

        if (PhotonNetwork.IsConnected) { 
            //���� �����̸� ���� Ȱ��ȭ, �κ�� ��Ȱ��ȭ
            //������ ������ ���� ������ �̵� �� �ش� �������� �κ� �ƴ� �� UI�� ����Ѵ�.
            canvasLobby.gameObject.SetActive(false);
        }
    }
    
    public void SelectServer() {
        //���� �����ϴ� �Լ�
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedLobby() {
        canvasRoom.sortingOrder = 1;
    }

    public override void OnConnectedToMaster() {
        //OnJoinedLobby : Ư�� �κ� �����Ͽ� �����ϴ� ���
        PhotonNetwork.JoinLobby(
            new TypedLobby(
                server.options[server.value].text, 
                LobbyType.Default
            )
        );
    }
}
