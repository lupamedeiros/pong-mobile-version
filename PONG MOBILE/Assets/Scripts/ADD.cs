using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADD : MonoBehaviour
{
    public void ADDp()
    {
        ClienteServer.Instance.AddPoints(10);
    }

    public void Remover()
    {
        ClienteServer.Instance.SubtractPoints(10);
    }
}
