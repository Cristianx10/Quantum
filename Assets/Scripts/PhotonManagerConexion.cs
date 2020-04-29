using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManagerConexion : MonoBehaviourPunCallbacks, Photon.Pun.IPunObservable
{

    public string Version = "v1";
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        //Photon.ConnectUsingSettings();
    }

    // ...
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN.");
        PhotonNetwork.JoinOrCreateRoom("Global2", new RoomOptions() { MaxPlayers = 2 }, null);
    }
    // ...

    // ...
    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("Player", transform.position, transform.rotation, 0);
    }
    // ...

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo message)
    {

    }


}

