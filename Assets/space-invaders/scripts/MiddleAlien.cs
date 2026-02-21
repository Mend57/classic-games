public class MiddleAlien : Alien
{
    protected override void Awake() {
        base.Awake();
    }

    protected override void Update() {
        base.Update();
    }

    protected override void setProjectile() {
        projectile = gameManager.slowProjectile;
    }

    protected override void setScoreValue() {
        scoreValue = 20;
    }
}
