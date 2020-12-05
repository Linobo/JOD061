using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameController : MonoBehaviourPunCallbacks
{
    void Start()
    {
        if (!PhotonNetwork.IsConnected) {
            PhotonNetwork.LoadLevel("LobbyScene");
            return;
        }

        PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-15f, 15f), 0f, 0f), Quaternion.identity, 0);        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log(
                "Jogador " + PhotonNetwork.LocalPlayer.NickName + 
                " saiu da sala " + PhotonNetwork.CurrentRoom.Name +
                " (" + PhotonNetwork.CurrentRoom.PlayerCount + ")"
            );            
            PhotonNetwork.LeaveRoom();
        }
    }

    override public void OnLeftRoom() {    
        PhotonNetwork.Disconnect();
    }

    override public void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Desconectado!");
        PhotonNetwork.LoadLevel("LobbyScene");
    }    
}
