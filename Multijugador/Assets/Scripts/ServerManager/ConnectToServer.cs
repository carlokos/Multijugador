using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField txtName;
    [SerializeField] private Button btnConnect;
    [SerializeField] private TextMeshProUGUI btnText;
    private Color color;

    private void Start()
    {
        color = btnConnect.image.color;
    }
    public void OnClickConnect()
    {
        if (txtName.text.Length >= 1)
        {
            PhotonNetwork.NickName = txtName.text;
            btnText.text = "Connecting...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Se ha conectado al servidor");
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        if(txtName.text.Length <= 0)
        {
            btnConnect.interactable = false;
            color.a = 0.5f;
        }
        else
        {
            btnConnect.interactable = true;
            color.a = 1f;
        }
        btnConnect.image.color = color;
    }
}
