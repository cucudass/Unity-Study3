using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using PlayFab.ClientModels;

public class NickName : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI nickName;
    [SerializeField] Camera playerCamera;

    void Start()
    {
        nickName.text = photonView.Owner.NickName;
    }

    void Update()
    {
        transform.forward = playerCamera.transform.forward;
    }
}
