using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Information : MonoBehaviourPunCallbacks
{
    [SerializeField] string roomName;
    public TextMeshProUGUI roomInformation;

    public void ConnectRoom() {
        //JoinRoom() -> �뿡 �����Ѵ�.
        PhotonNetwork.JoinRoom(roomName);
    }

    public void RoomData(string name, int currentStaff, int maxStaff) {
        roomName = name;
        roomInformation.fontSize = 50;
        roomInformation.text = name + " ( " + currentStaff + " / " + maxStaff + " ) ";
    }
}
