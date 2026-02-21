public class LowAlien : Alien
{

    protected override void Awake() {
        base.Awake();
    }

    protected override void Update() {
        base.Update();
    }

    protected override void setProjectile() {
        projectile = gameManager.fastProjectile;
    }

    protected override void setScoreValue() {
        scoreValue = 10;
    }
}
