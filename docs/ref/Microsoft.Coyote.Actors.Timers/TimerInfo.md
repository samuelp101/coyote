# TimerInfo class

Stores information about a timer that can send timeout events to its owner actor.

```csharp
public class TimerInfo : IEquatable<TimerInfo>
```

## Public Members

| name | description |
| --- | --- |
| readonly [CustomEvent](TimerInfo/CustomEvent.md) | The optional custom event to raise instead of the default TimerElapsedEvent. |
| readonly [DueTime](TimerInfo/DueTime.md) | The amount of time to wait before sending the first timeout event. |
| readonly [OwnerId](TimerInfo/OwnerId.md) | The id of the actor that owns the timer. |
| readonly [Period](TimerInfo/Period.md) | The time interval between timeout events. |
| override [Equals](TimerInfo/Equals.md)(…) | Determines whether the specified object is equal to the current object. |
| [Equals](TimerInfo/Equals.md)(…) | Indicates whether the specified [`TimerInfo`](TimerInfo.md) is equal to the current [`TimerInfo`](TimerInfo.md). |
| override [GetHashCode](TimerInfo/GetHashCode.md)() | Returns the hash code for this instance. |
| override [ToString](TimerInfo/ToString.md)() | Returns a string that represents the current instance. |

## Remarks

See [Using timers in actors](/coyote/concepts/actors/timers) for more information.

## See Also

* namespace [Microsoft.Coyote.Actors.Timers](../Microsoft.Coyote.Actors.TimersNamespace.md)
* assembly [Microsoft.Coyote](../Microsoft.Coyote.md)

<!-- DO NOT EDIT: generated by xmldocmd for Microsoft.Coyote.dll -->