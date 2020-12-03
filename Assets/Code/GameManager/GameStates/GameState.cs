using UnityEngine;
using System.Collections;


public abstract class GameState : MonoBehaviour
{
    [SerializeField] GameStateTypes stateType;

    protected SceneManager gm;

    public GameStateTypes StateType => stateType;


    public virtual void OnStateEnter() { }
    public abstract void Tick();
    public virtual void OnStateExit() { }

    public abstract void SetPause(bool value);

}