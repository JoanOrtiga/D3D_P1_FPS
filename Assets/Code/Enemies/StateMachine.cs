public class StateMachine
{
    IState currentState;

    public void ChangeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    public void UpdateMachine()
    {
        if(currentState != null)
        {
            currentState.Execute();
        }
    }

}
