# Task.Delay method (1 of 4)

Creates a [`Task`](../Task.md) that completes after a time delay.

```csharp
public static Task Delay(int millisecondsDelay)
```

| parameter | description |
| --- | --- |
| millisecondsDelay | The number of milliseconds to wait before completing the returned task, or -1 to wait indefinitely. |

## Return Value

Task that represents the time delay.

## See Also

* class [Task](../Task.md)
* namespace [Microsoft.Coyote.Tasks](../Task.md)
* assembly [Microsoft.Coyote](../../Microsoft.Coyote.md)

---

# Task.Delay method (2 of 4)

Creates a [`Task`](../Task.md) that completes after a specified time interval.

```csharp
public static Task Delay(TimeSpan delay)
```

| parameter | description |
| --- | --- |
| delay | The time span to wait before completing the returned task, or TimeSpan.FromMilliseconds(-1) to wait indefinitely. |

## Return Value

Task that represents the time delay.

## See Also

* class [Task](../Task.md)
* namespace [Microsoft.Coyote.Tasks](../Task.md)
* assembly [Microsoft.Coyote](../../Microsoft.Coyote.md)

---

# Task.Delay method (3 of 4)

Creates a [`Task`](../Task.md) that completes after a time delay.

```csharp
public static Task Delay(int millisecondsDelay, CancellationToken cancellationToken)
```

| parameter | description |
| --- | --- |
| millisecondsDelay | The number of milliseconds to wait before completing the returned task, or -1 to wait indefinitely. |
| cancellationToken | Cancellation token that can be used to cancel the delay. |

## Return Value

Task that represents the time delay.

## See Also

* class [Task](../Task.md)
* namespace [Microsoft.Coyote.Tasks](../Task.md)
* assembly [Microsoft.Coyote](../../Microsoft.Coyote.md)

---

# Task.Delay method (4 of 4)

Creates a [`Task`](../Task.md) that completes after a specified time interval.

```csharp
public static Task Delay(TimeSpan delay, CancellationToken cancellationToken)
```

| parameter | description |
| --- | --- |
| delay | The time span to wait before completing the returned task, or TimeSpan.FromMilliseconds(-1) to wait indefinitely. |
| cancellationToken | Cancellation token that can be used to cancel the delay. |

## Return Value

Task that represents the time delay.

## See Also

* class [Task](../Task.md)
* namespace [Microsoft.Coyote.Tasks](../Task.md)
* assembly [Microsoft.Coyote](../../Microsoft.Coyote.md)

<!-- DO NOT EDIT: generated by xmldocmd for Microsoft.Coyote.dll -->
