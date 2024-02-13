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
            //연결 상태이면 룸을 활성화, 로비는 비활성화
            //게임이 끝나고 게임 씬에서 이동 시 해당 로직으로 로비가 아닌 룸 UI를 출력한다.
            canvasLobby.gameObject.SetActive(false);
        }
    }
    
    public void SelectServer() {
        //서버 접속하는 함수
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedLobby() {
        canvasRoom.sortingOrder = 1;
    }

    public override void OnConnectedToMaster() {
        //OnJoinedLobby : 특정 로비를 생성하여 진입하는 방법
        PhotonNetwork.JoinLobby(
            new TypedLobby(
                server.options[server.value].text, 
                LobbyType.Default
            )
        );
    }
}
