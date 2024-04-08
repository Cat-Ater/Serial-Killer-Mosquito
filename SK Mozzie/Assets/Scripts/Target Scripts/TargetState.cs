public struct TargetState
{
    public bool idle, attacked, dead;

    public TargetState(bool idle, bool attacked, bool dead)
    {
        this.idle = idle;
        this.attacked = attacked;
        this.dead = dead;
    }
}
