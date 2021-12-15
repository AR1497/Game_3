public sealed class PlayerBall : Player
{
    private void Update()
    {
        Execute();
    }

    private void FixedUpdate()
    {
        Move();
        GetBonus();
        CheckedSpeedP();
    }
}
