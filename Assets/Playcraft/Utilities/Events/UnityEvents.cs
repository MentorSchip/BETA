using System;
using UnityEngine;

// Primitives
[Serializable] public class BoolEvent: UnityEngine.Events.UnityEvent<bool> { }
[Serializable] public class IntEvent : UnityEngine.Events.UnityEvent<int> { }
[Serializable] public class FloatEvent : UnityEngine.Events.UnityEvent<float> { }
[Serializable] public class Vector2Event : UnityEngine.Events.UnityEvent<Vector2> { }
[Serializable] public class Vector3Event : UnityEngine.Events.UnityEvent<Vector3> { }

// Multi-arguments
[Serializable] public class Vector3x2Event : UnityEngine.Events.UnityEvent<Vector3, Vector3> { }
[Serializable] public class Vector3IntEvent : UnityEngine.Events.UnityEvent<Vector3, int> { }
[Serializable] public class GameObjectVector3Event : UnityEngine.Events.UnityEvent<GameObject, Vector3> { }

