using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] int attack = 20;
    [SerializeField] LayerMask layerMask;

    Ray ray;
    RaycastHit raycastHit;

    void Update()
    {
        Launch();
    }

    public void Launch() {
        if (Input.GetButtonDown("Fire1")) {
            ray = camera.ViewportPointToRay(new Vector2(0.5f, 0.5f)); //뷰 포트 공간은 0~1의 영역을 가지기에 0.5f, 0.5f가 중점이다.
           
            if(Physics.Raycast(ray, out raycastHit, Mathf.Infinity, layerMask)) {
                PhotonView photonView = raycastHit.collider.GetComponent<PhotonView>();
                photonView.GetComponent<Metalon>().Health -= attack;

                if (photonView.GetComponent<Metalon>().Health <= 0) {
                    if (photonView.IsMine) {
                        PhotonNetwork.Destroy(photonView.gameObject);
                    }
                }
            }
        }
    }
}
