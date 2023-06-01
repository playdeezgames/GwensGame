Friend Module MainMenu
    Friend Sub Run()
        ClearPrompts()
        AddPrompt("Main Menu:")
        ClearActions()
        If IsInPlay() Then
            AddAction("Continue", CurrentArea, ConsoleColor.Gray)
        Else
            AddAction("Start", AddressOf Boilerplate.StartGame, ConsoleColor.Gray)
        End If
        AddAction("Quit", AddressOf ConfirmQuit, ConsoleColor.Gray)
    End Sub
End Module
