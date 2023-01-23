using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CatchPlayer : MonoBehaviour
{
    private TextMeshProUGUI txtScore;
    private PhotonView view;
    private float points = 0;

    private void Start()
    {
        view = GetComponentInParent<PhotonView>();
        txtScore = FindObjectOfType<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitbox") && view.IsMine)
        {
            points++;
            txtScore.text = "Your score: " + points.ToString();
            if(points >= 10)
            {
                Debug.Log("La partida ha finalizado");
            }
            Debug.Log("Jugador capturado");
        }
    }
}
