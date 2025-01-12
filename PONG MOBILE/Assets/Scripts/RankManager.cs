using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankDisplay : MonoBehaviour
{
    [SerializeField] private Text rankText; // Referência ao Text do Canvas para exibir o ranking

    private float updateInterval = 5f; // Tempo em segundos entre as atualizações
    private float timer; 
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            UpdateRankFromServer(); // Atualiza o ranking
            timer = 0; // Reinicia o timer
        }
    }
    private void Start()
    {
        if (ClienteServer.Instance != null)
        {
            ClienteServer.Instance.ConnectToServer();
            RequestRankUpdate();
        }
        else
        {
            Debug.LogError("ClienteServer não está inicializado.");
        }
    }

    public void UpdateRankDisplay(string rankData)
    {
        // O servidor enviará os dados no formato: "1:John:500|2:Jane:450|3:Mike:400"
        string[] players = rankData.Split('|');
        rankText.text = "Ranking:\n";

        foreach (string player in players)
        {
            string[] details = player.Split(':');
            if (details.Length == 3)
            {
                rankText.text += $"#{details[0]} {details[1]} - {details[2]} pts\n";
            }
        }
    }
    public void UpdateRankFromServer()
    {
        // Verifica se o ClienteServer está disponível
        if (ClienteServer.Instance != null)
        {
            // Envia solicitação ao servidor para obter o ranking
            ClienteServer.Instance.SendRequest("GET_RANK");
        }
        else
        {
            Debug.LogError("ClienteServer não encontrado! Certifique-se de que ele está ativo.");
        }
    }


    public void RequestRankUpdate()
    {
        // Solicita ao servidor uma atualização do ranking
        ClienteServer.Instance.SendRequest("GET_RANK");
    }
}