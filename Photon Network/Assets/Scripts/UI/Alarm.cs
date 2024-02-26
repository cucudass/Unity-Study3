using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AlarmType { Alarm, PasuePanel }

public class Alarm : MonoBehaviourPunCallbacks
{
    [SerializeField] Text content;

    public static void Show(string message, AlarmType alarmType) {
        GameObject window = Instantiate(Resources.Load<GameObject>(alarmType.ToString()));
        window.GetComponent<Alarm>().content.text = message;
        window.GetComponent<Alarm>().content.fontSize = 65;
        window.GetComponent<Alarm>().content.alignment = TextAnchor.MiddleCenter;
    }

    public static void Show(AlarmType alarmType) {
        GameObject window = Instantiate(Resources.Load<GameObject>(alarmType.ToString()));
    }

    public void Continue() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; //마우스 잠금

        Destroy(gameObject);
    }

    public void Exit() {
        PhotonNetwork.LeaveRoom();
    }

    //방을 나갈 때 호출
    public override void OnLeftRoom() {
        PhotonNetwork.LoadLevel("Photon Lobby");
    }

    public void Close() {
        Destroy(gameObject);
    }
}
