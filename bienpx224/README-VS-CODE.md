# VS Code hỗ trợ c# cho Unity : 
- Cài các Extension : c#, c# extension, Unity Snippet...
- Nên cài .Net 6. trở lên, tốt nhất nên cài bản stable mới nhất là 7.0
- Changing that omnisharp.useModernNet setting to false. Đợi update, install. 
    ```
    .NET Framework builds of OmniSharp no longer ship with Mono or the MSBuild tooling (See announcement omnisharp-roslyn#2339). To ensure that the C# extension remains usable out of the box for .NET SDK projects, we have changed the default value of omnisharp.useModernNet to true.

    If you still need Unity or .NET Framework support, you can set omnisharp.useModernNet to false in your VS Code settings and restart OmniSharp.
    ```
- Cài đặt Mono on MacOS : https://www.mono-project.com/docs/getting-started/install/mac/#uninstalling-mono-on-macos 
- Tắt VS code đi và khởi động lại.