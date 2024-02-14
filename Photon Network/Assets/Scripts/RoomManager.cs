using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime; //어느 서버에 접속했을 때 이벤트를 호출하는 라이브러리
using UnityEngine.UI;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Button roomCreateButton;
    public TMP_InputField roomNameInputField;
    public TMP_InputField roomPersonnelInputField;
    public Transform roomParentTransform;

    //룸 목록을 저장하기 위한 자료구조
    Dictionary<string, RoomInfo> roomDictionary = new Dictionary<string, RoomInfo>();

    void Update()
    {
        if(roomNameInputField.text.Length > 0 && roomPersonnelInputField.text.Length > 0) {
            roomCreateButton.interactable = true;
        } else {
            roomCreateButton.interactable = false;
        }
    }

    //룸에 입장한 후 호출되는 콜백 함수
    public override void OnJoinedRoom() {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void InstantiateRoom() {
        //룸 옵션을 설정
        RoomOptions roomOptions = new RoomOptions();

        //최대 접속 인원 설정
        roomOptions.MaxPlayers = byte.Parse(roomPersonnelInputField.text);

        //룸의 오픈 여부 설정
        roomOptions.IsOpen = true;

        //로비에서 룸 목록을 노출시킬 지 설정
        roomOptions.IsVisible = true;

        //룸을 생성
        PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions);
    }

    //해당 로비에 방 목록의 변경 사항이 있으면 호출(추가, 삭제, 참가)되는 콜백함수
    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        //1. 룸을 삭제
        //2. 룸을 업데이트
        //3. 룸을 생성
    }

    public void RemoveRoom() {
        foreach (Transform roomTransform in roomParentTransform) {
            Destroy(roomTransform.gameObject);
        }
    }

    public void UpdateRoom(List<RoomInfo> roomList) {
        for (int i = 0; i < roomList.Count; i++) {
            //해당 이름이 roomDictionary의 Key값으로 설정되어있다면
            if (roomDictionary.ContainsKey(roomList[i].Name)) {
                //RemoveFromList : (true) 룸에서 삭제 되었을 때
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
