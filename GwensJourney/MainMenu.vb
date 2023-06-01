Friend Module MainMenu
    Friend Sub Run()
        Clear()
        AddPrompt("Main Menu:")
        ClearActions()
        If IsInPlay() Then
            AddAction("Continue", AddressOf CurrentArea)
        Else
            AddAction("Start", AddressOf Boilerplate.StartGame)
        End If
        AddAction("Quit", AddressOf ConfirmQuit)
    End Sub
End Module
