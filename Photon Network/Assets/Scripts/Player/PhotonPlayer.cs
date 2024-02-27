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
        //현재 플레이어가 나 자신이라면
        if (photonView.IsMine) {
            //Camera.main -> 태그가 MainCamera인 카메라를 감지한다.
            Camera.main.gameObject.SetActive(false);
        } else {
            temporaryCamera.enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
    }

    void Update()
    {
        //자기 자신만 움직이게 한다.
        if (!photonView.IsMine) return;

        Movement();
        Rotation();
    }

    public void Movement() {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");
        direction.Normalize();

        //transform.TransformDirection(direction) -> 자기가 바라보고 있는 방향을 이동하는 함수
        transform.position += transform.TransformDirection(direction) * speed * Time.deltaTime;
    }

    public void Rotation() {
        mouseX += Input.GetAxisRaw("Mouse X") * rotateSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, mouseX, 0);
    }

    // 빠르게 동기화를 할 수 있다.
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        //로컬 오브젝트라면 쓰기 부분을 실행한다.
        if (stream.IsWriting) {
            //네트워크를 통해 데이터를 보낸다.
            stream.SendNext(score);
        } else { //원격 오브젝트라면 읽기 부분을 실행한다.
            //네트워크를 통해서 데이터를 받는다.
            try {
                score = (float)stream.ReceiveNext();
            } catch {
            }
        }
    }
}
