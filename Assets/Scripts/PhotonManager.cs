using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PhotonManager : MonoBehaviourPunCallbacks
{
    public static PhotonManager instance;
    public Transform _contentPlayerListing;
    public Transform _contentRoomListing;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        
    }
    //Bağlanma
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to the server!");
        PhotonNetwork.NickName = PlayerManager.instance._playerName;
        PhotonNetwork.JoinLobby(); // Lobiye katıl
        base.OnConnectedToMaster();
    }
    public override void OnJoinedLobby()
    {
        print("Joined the lobby!");
        PlayerManager.instance._playerName = PhotonNetwork.LocalPlayer.NickName;
       // PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions { MaxPlayers = 20, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        base.OnJoinedLobby();
    }
    public override void OnJoinedRoom()
    {
        print("Joined the room!");
        GameObject.Find("PlayerListings-ScrollView").GetComponent<PlayerListingsMenu>().GetCurrentRoomPlayer();
        DestroyChild(_contentRoomListing.gameObject);
    }
    public override void OnCreatedRoom()
    {
        print("Room created!");
    }

    public override void OnLeftLobby()
    {
        print("Left the lobby!");
    }
    public override void OnLeftRoom()
    {
        
        print("Left the room!");
        DestroyChild(_contentRoomListing.gameObject);
        PhotonNetwork.Disconnect();
        PhotonNetwork.ConnectUsingSettings();
    }

    //Hata
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("Error the room!");
        CanvasManager.instance.CloseAllPanels();
        CanvasManager.instance.MainPanel.SetActive(true);
        DestroyChild(_contentRoomListing.gameObject);
        base.OnJoinRoomFailed(returnCode, message);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("Error the joinrandomfailed!");
        base.OnJoinRandomFailed(returnCode, message);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("Error the create room!");
        base.OnCreateRoomFailed(returnCode, message);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
    }
    public void CreateRoom(string roomName,byte playerLimit)
    {
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = playerLimit});
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        DestroyChild(_contentPlayerListing.gameObject);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnect because "+cause);
    }
    //Custom
    public void DestroyChild(GameObject gox)
    {
        foreach (Transform chield in gox.transform)
        {
            Destroy(chield.gameObject);
        }
    }

}
