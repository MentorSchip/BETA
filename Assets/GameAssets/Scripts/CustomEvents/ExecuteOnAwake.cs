using UnityEngine;

public class ExecuteOnAwake : MonoBehaviour
{
    SequenceUtility sequence = new SequenceUtility();

    public OrderedEvent[] OnStartSequence;

    void Awake()
    {
        sequence.Begin(OnStartSequence);
    }
}
