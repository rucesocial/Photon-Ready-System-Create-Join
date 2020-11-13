using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance = new PlayerManager();

    public string _playerName;

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
    private void Start()
    {
        if (_playerName == null && _playerName == "")
        {
            int value = Random.Range(0, 9999);
            _playerName = "Player" + value;
        }
    }
    public void SpawnPlayer()
    {
        GameObject Player = PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-4, 4), 0.5f, 0), Quaternion.identity);
        Camera.main.GetComponent<CameraFollow>().target = Player.gameObject.transform;
    }

}
