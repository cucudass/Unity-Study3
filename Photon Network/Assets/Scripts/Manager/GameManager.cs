using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Text timerText;
    [SerializeField] GameObject nickNamePanel;
    [SerializeField] TMP_InputField nickNameInputField;

    [SerializeField] int playerCount = 1;

    [SerializeField] float timer = 180;
    private int minute;
    private int second;

    private void Awake() {
        CreatePlayer();
        CheckNickName();
        checkPlayer();
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

    public void checkPlayer() {
        if(PhotonNetwork.PlayerList.Length >= playerCount) {
            photonView.RPC("RemoteTimer", RpcTarget.All);
        }
    }

    [PunRPC]
    public void RemoteTimer() {
        StartCoroutine(StartTimer());
    }

    /*����� �ڵ�
    IEnumerator StartTimer() {
        while (true) {
            timer -= Time.deltaTime;
            minute = (int)timer / 60;
            second = (int)timer % 60;

            timerText.text = minute.ToString("00") + " : " + second.ToString("00");

            yield return null;

            if (timer <= 0) yield break;
        }
    }
    */
    IEnumerator StartTimer() {
        while(timer >= 0) {
            minute = (int)timer / 60;
            second = (int)timer % 60;

            timerText.text = minute.ToString("00") + " : " + second.ToString("00");
            timer -= 1;

            yield return new WaitForSeconds(1f);
        }
    }
}
