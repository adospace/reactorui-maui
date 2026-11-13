; Unshipped analyzer release
; https://github.com/dotnet/roslyn-analyzers/blob/main/src/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

### New Rules

Rule ID | Category | Severity | Notes
--------|-------------------|----------|----------------------------------------------------
MRH001  | MauiReactor.Hooks | Error    | Hook called outside Render, HookRulesAnalyzer
MRH002  | MauiReactor.Hooks | Error    | Hook called conditionally, HookRulesAnalyzer
MRH003  | MauiReactor.Hooks | Error    | Hook called in a loop, HookRulesAnalyzer
MRH004  | MauiReactor.Hooks | Error    | Hook called inside a nested function, HookRulesAnalyzer
