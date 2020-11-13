using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
   
    public List<GameObject> panelList = new List<GameObject>();
    public GameObject MainPanel,CreateNewRoomPanel,CurrentRoomPanel;
    public Text RoomName;
    public Text PlayerLimit;
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
        CloseAllPanels();
        MainPanel.SetActive(true);
        //int value = UnityEngine.Random.Range(0, 9999);
        //print(value)
    }


    public void CreateNewRoomButton()
    {
        CloseAllPanels();
        CreateNewRoomPanel.SetActive(true);
    }
    public void CreateNewRoom()
    {
        PhotonManager.instance.CreateRoom(RoomName.text,byte.Parse(PlayerLimit.text));
        CloseAllPanels();
        CurrentRoomPanel.SetActive(true);
    }
    public void LeaveRoom()
    {
        PhotonManager.instance.LeaveRoom();
        CloseAllPanels();
        MainPanel.SetActive(true);
    }
    public void CloseAllPanels()
    {
        foreach (var panel in panelList)
        {
            panel.gameObject.SetActive(false);
        }
    }
    public void BackButton()
    {
        CloseAllPanels();
        MainPanel.SetActive(true);
    }
    public void StartButton()
    {
        CloseAllPanels();
        PlayerManager.instance.SpawnPlayer();
    }
}
