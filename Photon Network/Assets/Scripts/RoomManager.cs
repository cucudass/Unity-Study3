using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime; //��� ������ �������� �� �̺�Ʈ�� ȣ���ϴ� ���̺귯��
using UnityEngine.UI;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Button roomCreateButton;
    public TMP_InputField roomNameInputField;
    public TMP_InputField roomPersonnelInputField;
    public Transform roomParentTransform;

    //�� ����� �����ϱ� ���� �ڷᱸ��
    Dictionary<string, RoomInfo> roomDictionary = new Dictionary<string, RoomInfo>();

    void Update()
    {
        if(roomNameInputField.text.Length > 0 && roomPersonnelInputField.text.Length > 0) {
            roomCreateButton.interactable = true;
        } else {
            roomCreateButton.interactable = false;
        }
    }

    //�뿡 ������ �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnJoinedRoom() {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void InstantiateRoom() {
        //�� �ɼ��� ����
        RoomOptions roomOptions = new RoomOptions();

        //�ִ� ���� �ο� ����
        roomOptions.MaxPlayers = byte.Parse(roomPersonnelInputField.text);

        //���� ���� ���� ����
        roomOptions.IsOpen = true;

        //�κ񿡼� �� ����� �����ų �� ����
        roomOptions.IsVisible = true;

        //���� ����
        PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions);
    }

    //�ش� �κ� �� ����� ���� ������ ������ ȣ��(�߰�, ����, ����)�Ǵ� �ݹ��Լ�
    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        //1. ���� ����
        //2. ���� ������Ʈ
        //3. ���� ����
    }

    public void RemoveRoom() {
        foreach (Transform roomTransform in roomParentTransform) {
            Destroy(roomTransform.gameObject);
        }
    }

    public void UpdateRoom(List<RoomInfo> roomList) {
        for (int i = 0; i < roomList.Count; i++) {
            //�ش� �̸��� roomDictionary�� Key������ �����Ǿ��ִٸ�
            if (roomDictionary.ContainsKey(roomList[i].Name)) {
                //RemoveFromList : (true) �뿡�� ���� �Ǿ��� ��
                if (roomList[i].RemovedFromList) {
                    roomDictionary.Remove(roomList[i].Name);
                } else {
                    roomDictionary[roomList[i].Name] = roomList[i];
                }
            } else {
                roomDictionary[roomList[i].Name] = roomList[i];
            }
        }
    }
}
