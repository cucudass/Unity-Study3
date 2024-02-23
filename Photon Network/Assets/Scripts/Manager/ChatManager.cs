using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ChatManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField inputField;
    [SerializeField] Transform contentTransform;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (inputField.text.Length == 0) {
                inputField.ActivateInputField();//채팅을 입력한 후에도 이어서 입력할 수 있도록 설정, 입력란 커서 활성화?
                return;
            }

            string chatting = PhotonNetwork.NickName + " : " + inputField.text;
            //photonView.RPC(실행 시킬 함수의 이름, 호출할 함수를 받을 수 있는 대상, 실행할 함수의 매개변수)
            photonView.RPC("Chatting", RpcTarget.All, chatting);
        }
    }

    [PunRPC] //RPC 함수로 등록
    public void Chatting(string message) {
        GameObject content = Instantiate(Resources.Load<GameObject>("String"));
        content.GetComponent<Text>().text = message;
        content.transform.SetParent(contentTransform);
        
        inputField.text = ""; //텍스트 초기화 -> 버퍼에 값이 남아있기에 초기화를 해준다.
    }
}
