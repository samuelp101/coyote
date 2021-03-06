# RewritingOptions.IgnoredAssemblies property

The regular expressions used to match against assembly names to determine which assemblies to ignore when rewriting dependencies or a whole directory.

```csharp
public IList<string> IgnoredAssemblies { get; set; }
```

## Remarks

The list automatically includes the following expressions: Microsoft\.Coyote.* Microsoft\.TestPlatform.* Microsoft\.VisualStudio\.TestPlatform.* Newtonsoft\.Json.* System\.Private\.CoreLib mscorlib.

## See Also

* class [RewritingOptions](../RewritingOptions.md)
* namespace [Microsoft.Coyote.Rewriting](../RewritingOptions.md)
* assembly [Microsoft.Coyote.Test](../../Microsoft.Coyote.Test.md)

<!-- DO NOT EDIT: generated by xmldocmd for Microsoft.Coyote.Test.dll -->
