using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    public RoomInfo RoomInfo { get; private set; }

    public void SetRoomInfo(RoomInfo roomInfo)

    {
        RoomInfo = roomInfo;
        _text.text = roomInfo.Name +" | "+ roomInfo.PlayerCount+ " / " + roomInfo.MaxPlayers;
    }
    public void OnClick_Button()
    {
        PhotonNetwork.JoinRoom(RoomInfo.Name);
        CanvasManager.instance.CloseAllPanels();
        CanvasManager.instance.CurrentRoomPanel.SetActive(true);
    }
}
