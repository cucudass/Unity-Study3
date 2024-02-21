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

    public Vector3 RandomPosition(float distance) {//�÷��̾� ���� ��ġ
        //(1,1,1)�� ���� ������ �������� ��ġ ����
        Vector3 direction = Random.insideUnitSphere;
        direction.Normalize();
        direction *= distance;
        direction.y = 1;

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
