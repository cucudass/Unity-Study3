using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using PlayFab.ClientModels;
using UnityEngine.UI;
using PlayFab;
using System;

public class PlayfabManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField email;
    [SerializeField] InputField password;

    //로그인 성공 시 호출
    public void Success(LoginResult result) {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.GameVersion = "1.0f";
        PhotonNetwork.LoadLevel("Photon Lobby");
    }

    public void Success(RegisterPlayFabUserResult result) {
        Alarm.Show(result.ToString(), AlarmType.Alarm);
    }
    
    //로그인 실패
    public void Failure(PlayFabError error) {
        Alarm.Show(error.GenerateErrorReport(), AlarmType.Alarm);
    }

    public void SignUp() {
        var request = new RegisterPlayFabUserRequest {
            Email = email.text,
            Password = password.text,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, Success, Failure);  
    }

    public void Login() {
        var request = new LoginWithEmailAddressRequest {
            Email = email.text,
            Password = password.text
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, Success, Failure);
    }
}
