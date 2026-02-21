public class TopAlien : Alien {
    protected override void Awake() {
        base.Awake();
    }

    protected override void Update() {
        base.Update();
    }

    protected override void setProjectile() {
        projectile = gameManager.normalProjectile;
    }

    protected override void setScoreValue() {
        scoreValue = 30;
    }
}
