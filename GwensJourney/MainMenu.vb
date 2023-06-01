Friend Module MainMenu
    Friend Sub Run()
        ClearPrompts()
        AddPrompt("Main Menu:")
        ClearActions()
        If IsInPlay() Then
            AddAction("Continue", CurrentArea)
        Else
            AddAction("Start", AddressOf Boilerplate.StartGame)
        End If
        AddAction("Quit", AddressOf ConfirmQuit)
    End Sub
End Module
