using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PhotonPlayer : MonoBehaviourPun, IPunObservable
{
    [SerializeField] float score;
    [SerializeField] float mouseX;
    [SerializeField] float rotateSpeed;
    [SerializeField] float speed;

    [SerializeField] Vector3 direction;
    [SerializeField] Camera temporaryCamera;

    void Start()
    {
        //���� �÷��̾ �� �ڽ��̶��
        if (photonView.IsMine) {
            //Camera.main -> �±װ� MainCamera�� ī�޶� �����Ѵ�.
            Camera.main.gameObject.SetActive(false);
        } else {
            temporaryCamera.enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
    }

    void Update()
    {
        //�ڱ� �ڽŸ� �����̰� �Ѵ�.
        if (!photonView.IsMine) return;

        Movement();
        Rotation();
    }

    public void Movement() {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");
        direction.Normalize();

        //transform.TransformDirection(direction) -> �ڱⰡ �ٶ󺸰� �ִ� ������ �̵��ϴ� �Լ�
        transform.position += transform.TransformDirection(direction) * speed * Time.deltaTime;
    }

    public void Rotation() {
        mouseX += Input.GetAxisRaw("Mouse X") * rotateSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, mouseX, 0);
    }

    // ������ ����ȭ�� �� �� �ִ�.
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        //���� ������Ʈ��� ���� �κ��� �����Ѵ�.
        if (stream.IsWriting) {
            //��Ʈ��ũ�� ���� �����͸� ������.
            stream.SendNext(score);
        } else { //���� ������Ʈ��� �б� �κ��� �����Ѵ�.
            //��Ʈ��ũ�� ���ؼ� �����͸� �޴´�.
            try {
                score = (float)stream.ReceiveNext();
            } catch {
            }
        }
    }
}
