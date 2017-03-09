using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NetworkManager : MonoBehaviour
{
    /*--------------------------------------------------------------------------------
     * @enum ネットワーク状態　列挙
    ---------------------------------------------------------------------------------*/
    public enum NETWORK_STATE : int
    {
        NETWORK_STATE_DISCONNET = 0,        // 接続無し
        NETWORK_STATE_CONNETED,             // 接続有り
        NETWORK_STATE_JOIN_ROOM,            // ルーム参加
        NETWORK_STATE_CREATE_ROOM,          // ルーム作成

        MAX_NETWORK_STATE                     // 最大値
    }

    /*--------------------------------------------------------------------------------
     * メンバ変数
    ---------------------------------------------------------------------------------*/
    private NETWORK_STATE   network_state_ = NETWORK_STATE.NETWORK_STATE_DISCONNET;     //! ネットワーク状態
    private string          join_room_name_ = "";
    private string          create_room_name_ = "";


    /*--------------------------------------------------------------------------------
     * @brief 初期化
    ---------------------------------------------------------------------------------*/
    void Start()
    {
        DontDestroyOnLoad(this);
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    /*--------------------------------------------------------------------------------
     * @brief 更新
    ---------------------------------------------------------------------------------*/
    void Update()
    {
        if (network_state_ == NETWORK_STATE.NETWORK_STATE_JOIN_ROOM
            || network_state_ == NETWORK_STATE.NETWORK_STATE_CREATE_ROOM)
        {
            if (Input.GetMouseButton(0))
            {
                float rand_x = Random.Range(-50.0f, 50.0f);
                PhotonNetwork.Instantiate("Prefabs/Cube", new Vector3(rand_x, 9.0f, 0.0f), Quaternion.identity, 0);
            }
        }
    }

    /*--------------------------------------------------------------------------------
     * @brief Photon Callback：サーバに接続
    ---------------------------------------------------------------------------------*/
    void OnConnectedToPhoton()
    {
        network_state_ = NETWORK_STATE.NETWORK_STATE_CONNETED;
        SceneManager.LoadScene("Menu");
    }

    /*--------------------------------------------------------------------------------
     * @brief Photon Callback：ルームに接続
    ---------------------------------------------------------------------------------*/
    void OnJoinedRoom()
    {
        network_state_ = NETWORK_STATE.NETWORK_STATE_JOIN_ROOM;
        SceneManager.LoadScene("Main");
    }

    /*--------------------------------------------------------------------------------
     * @brief Photon Callback：ルーム参加失敗
    ---------------------------------------------------------------------------------*/
    void OnPhotonJoinRoomFailed()
    {
        network_state_ = NETWORK_STATE.NETWORK_STATE_CONNETED;
        SceneManager.LoadScene("Menu");
    }

    /*--------------------------------------------------------------------------------
     * @brief Photon Callback：ルームを作成
    ---------------------------------------------------------------------------------*/
    void OnCreatedRoom()
    {
        network_state_ = NETWORK_STATE.NETWORK_STATE_CREATE_ROOM;
        SceneManager.LoadScene("Main");
    }

    /*--------------------------------------------------------------------------------
     * @brief Photon Callback：ルーム作成失敗
    ---------------------------------------------------------------------------------*/
    void OnPhotonCreateRoomFailed()
    {
        network_state_ = NETWORK_STATE.NETWORK_STATE_CONNETED;
        SceneManager.LoadScene("Menu");
    }

    /*--------------------------------------------------------------------------------
     * @brief Photon Callback：ルームから退室
    ---------------------------------------------------------------------------------*/
    void OnLeftRoom()
    {
        network_state_ = NETWORK_STATE.NETWORK_STATE_CONNETED;
        SceneManager.LoadScene("Menu");
    }




    /*--------------------------------------------------------------------------------
     * @brief ルームに接続
    ---------------------------------------------------------------------------------*/
    public void JoinRoom()
    {
        if (join_room_name_ != null && join_room_name_.Length > 0)
        {
            PhotonNetwork.JoinRoom(join_room_name_);
            SceneManager.LoadScene("Load");
        }
    }

    /*--------------------------------------------------------------------------------
     * @brief 接続するルーム名を設定
     * @param string JoinRoomName       ：接続するルーム名
    ---------------------------------------------------------------------------------*/
    public void SetJoinRoomName(string JoinRoomName)
    {
        join_room_name_ = string.Copy(JoinRoomName);
    }
    
    /*--------------------------------------------------------------------------------
     * @brief ルームを作成
    ---------------------------------------------------------------------------------*/
    public void CreateRoom()
    {
        if (create_room_name_ != null && create_room_name_.Length > 0)
        {
            PhotonNetwork.CreateRoom(create_room_name_);
            SceneManager.LoadScene("Load");
        }
    }

    /*--------------------------------------------------------------------------------
    * @brief 作成するルーム名を設定
    * @param string CreateRoomName     ：作成するルーム名
    ---------------------------------------------------------------------------------*/
    public void SetCreateRoomName(string CreateRoomName)
    {
        create_room_name_ = string.Copy(CreateRoomName);
    }

    /*--------------------------------------------------------------------------------
         * @brief ルームを作成
    ---------------------------------------------------------------------------------*/
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Load");
    }


    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
