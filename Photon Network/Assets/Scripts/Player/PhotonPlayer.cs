using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PhotonPlayer : MonoBehaviourPun
{
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
}
