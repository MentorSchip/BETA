using UnityEngine;

public class ExecuteOnEnable : MonoBehaviour
{
    SequenceUtility sequence = new SequenceUtility();

    public OrderedEvent[] OnEnableSequence;

    void OnEnable()
    {
        //Debug.Log(gameObject.name + " enabled");
        sequence.Begin(OnEnableSequence);
    }
}
