# TaskCompletionSource&lt;TResult&gt;.TrySetException method

Attempts to transition the underlying task into the Faulted state and binds it to the specified exception.

```csharp
public virtual bool TrySetException(Exception exception)
```

| parameter | description |
| --- | --- |
| exception | The exception to bind to this task. |

## Return Value

True if the operation was successful; otherwise, false.

## See Also

* class [TaskCompletionSource&lt;TResult&gt;](../TaskCompletionSource-1.md)
* namespace [Microsoft.Coyote.Tasks](../TaskCompletionSource-1.md)
* assembly [Microsoft.Coyote](../../Microsoft.Coyote.md)

<!-- DO NOT EDIT: generated by xmldocmd for Microsoft.Coyote.dll -->
