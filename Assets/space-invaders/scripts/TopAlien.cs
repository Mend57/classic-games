using UnityEngine;
using System;

public class TopAlien : Alien {
    public event Action finishedMovingDown;

    protected override void Awake() {
        base.Awake();
    }

    protected override void Update() {
        base.Update();
    }

    protected override void move() {
        Vector3 pos = transform.position;
        bool movingDown = gameManager.movingDown;
        if (movingDown) {
            pos.y -= Mathf.Abs(moveAmount);
            changeDirection();
            finishedMovingDown?.Invoke();
        }
        else if (Mathf.Abs(pos.x + moveAmount) <= gameManager.mapLimit) {
            pos.x += moveAmount;
        }
        else {
            if (movingDown == false) gameManager.movingDown = true;
        }
        transform.position = pos;
    }

    protected override void setProjectile() {
        projectile = gameManager.normalProjectile;
    }

    protected override void setMoveCooldown() {
        _moveCooldown = gameManager.moveCooldown + 0.1f;
    }

    protected override void setScoreValue() {
        scoreValue = 30;
    }
}
