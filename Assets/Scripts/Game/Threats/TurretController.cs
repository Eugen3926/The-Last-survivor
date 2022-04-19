using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurretController
{
    public void TurretUprise(Transform turret) {
        turret.GetChild(1).DOLocalMoveY(1f, 1f);
    }    
}
