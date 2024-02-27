using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOUSETYPE { LOCK, FREE }
public class MouseManager : MonoBehaviour
{
    void Start()
    {
        SetMouse(MOUSETYPE.LOCK);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SetMouse(MOUSETYPE.FREE);
            Alarm.Show(AlarmType.PausePanel);
        }
    }

    public void SetMouse(MOUSETYPE mouseType) {
        switch (mouseType) {
            case MOUSETYPE.LOCK:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked; //���콺 ���
                break;
            case MOUSETYPE.FREE:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None; //��� ����
                break;
        }
    }
}
