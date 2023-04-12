using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PullGunBack : MonoBehaviour
{
   
    public void MoveVisualAway()
    {
        transform.DOLocalMoveX(-0.9f, 1).SetEase(Ease.OutBounce);
        MoveVisualFront();
    }

    public void MoveVisualFront()
    {
        transform.DOLocalMoveX(0.9f, 1).SetDelay(2f).SetEase(Ease.OutFlash);
    }
}
