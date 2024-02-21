using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject nickNamePanel;
    [SerializeField] TMP_InputField nickNameInputField;
    
    private void Awake() {
        CreatePlayer();
        CheckNickName();
    }

    private void CreatePlayer() {
        PhotonNetwork.Instantiate("Player", RandomPosition(5), Quaternion.identity);
    }

    public Vector3 RandomPosition(float distance) {//플레이어 생성 위치
        //(1,1,1)의 범위 내에서 랜덤으로 위치 설정
        Vector3 direction = Random.insideUnitSphere;
        direction.Normalize();
        direction *= distance;
        direction.y = 1;

        return direction;
    }

    public void ExitGame() {
        PhotonNetwork.LeaveRoom();
    }
    //방을 나갈 때 호출
    public override void OnLeftRoom() {
        PhotonNetwork.LoadLevel("Photon Lobby");
    }

    //방장이 나갈 시, 호출되는 함수 마스터 권한을 다른 클라이언트한테 넘김
    public override void OnMasterClientSwitched(Player newMasterClient) {
        PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[0]);
    }

    public void CreateNickName() {
        PlayerPrefs.SetString("Nick Name", nickNameInputField.text);
        PhotonNetwork.NickName = nickNameInputField.text;
        nickNamePanel.SetActive(false);
    }

    public void CheckNickName() {
        string nickName = PlayerPrefs.GetString("Nick Name");
        PhotonNetwork.NickName = nickName;

        nickNamePanel.SetActive(string.IsNullOrEmpty(nickName) ? true : false);
    }
}
