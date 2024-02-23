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
                inputField.ActivateInputField();//ä���� �Է��� �Ŀ��� �̾ �Է��� �� �ֵ��� ����, �Է¶� Ŀ�� Ȱ��ȭ?
                return;
            }

            string chatting = PhotonNetwork.NickName + " : " + inputField.text;
            //photonView.RPC(���� ��ų �Լ��� �̸�, ȣ���� �Լ��� ���� �� �ִ� ���, ������ �Լ��� �Ű�����)
            photonView.RPC("Chatting", RpcTarget.All, chatting);
        }
    }

    [PunRPC] //RPC �Լ��� ���
    public void Chatting(string message) {
        GameObject content = Instantiate(Resources.Load<GameObject>("String"));
        content.GetComponent<Text>().text = message;
        content.transform.SetParent(contentTransform);
        
        inputField.text = ""; //�ؽ�Ʈ �ʱ�ȭ -> ���ۿ� ���� �����ֱ⿡ �ʱ�ȭ�� ���ش�.
    }
}
