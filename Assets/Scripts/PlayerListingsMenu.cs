using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private PlayerListing _playerListing;

    private List<PlayerListing> _listings = new List<PlayerListing>();


    private void Start()
    {
        //   GetCurrentRoomPlayer();
    }

    public void GetCurrentRoomPlayer()
    {
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }
    private void AddPlayerListing(Player player)
    {
        PlayerListing listing = Instantiate(_playerListing, _content);
        if (listing != null)
        {
            listing.SetPlayerInfo(player);
            _listings.Add(listing);
        }
    }

    public override void OnLeftRoom()
    {
        
        print("Lefted room");
        base.OnLeftRoom();
    }
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
       
        AddPlayerListing(newPlayer);
    }
   
    public override void OnJoinedRoom()
    {
        
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }
}
