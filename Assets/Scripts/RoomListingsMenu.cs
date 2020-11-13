using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public Transform _content;
    [SerializeField]
    private RoomListing _roomListing;

    public List<RoomListing> _listings = new List<RoomListing>();
    public override void OnJoinedRoom()
    {
        //_content.DestroyChiledren();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {

        
        foreach (RoomInfo info in roomList)
        {
            //Removed from rooms list.
            if (info.RemovedFromList)
            {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index!=-1 && _listings[index]!=null)
                {
                   // PhotonManager.instance.DestroyChild(_content.gameObject);
                  
                     //print(_listings[index]);
                   Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
                else if(_listings[index] == null)
                {
                    PhotonManager.instance.DestroyChild(PhotonManager.instance._contentRoomListing.gameObject);
                    PhotonNetwork.Disconnect();
                    PhotonNetwork.ConnectUsingSettings();
                }

            }
            //Added to rooms list
            else
            {
               // PhotonManager.instance.DestroyChild(_content.gameObject);
                RoomListing listing = Instantiate(_roomListing, _content);
                if (listing != null)
                {
                    listing.SetRoomInfo(info);
                    _listings.Add(listing);
                }
                   
            }
           
            
        }
       
    }
 
}
