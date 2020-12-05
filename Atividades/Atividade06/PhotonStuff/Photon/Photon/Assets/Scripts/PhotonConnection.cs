using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PhotonConnection : MonoBehaviourPunCallbacks
{
    public InputField nomeJogador;

    public Button btnConectar;

    public Button btnIniciar;

    public void Conectar()
    {
        Debug.Log("Conectando na nuvem....");
        nomeJogador.interactable = false;
        btnConectar.interactable = false;

        // Conecta na Nuvem Photon
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.NickName = nomeJogador.text;
        PhotonNetwork.ConnectUsingSettings();        
    }

    public void IniciarJogo()
    {
        // Entra em uma sala
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 20;
        PhotonNetwork.JoinOrCreateRoom("JOD061", options, TypedLobby.Default);
    }

    override public void OnConnectedToMaster() 
    {       
        Debug.Log("Conectado!");
        btnIniciar.interactable = true;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    override public void OnJoinedRoom()
    {
        Debug.Log("Jogador " + PhotonNetwork.LocalPlayer.NickName + 
            " entrou na sala " + PhotonNetwork.CurrentRoom.Name +
            " (" + PhotonNetwork.CurrentRoom.PlayerCount + ")"
        );

        PhotonNetwork.LoadLevel("GameScene");
    }

    override public void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Deu ruim ao entrar na sala! Motivo: " + message);
    }

}
