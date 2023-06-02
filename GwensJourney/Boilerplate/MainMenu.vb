Friend Module MainMenu
    Friend Sub Run()
        Prompts.Clear()
        Prompts.Add("Main Menu:")
        ActionItems.Clear()
        If IsInPlay() Then
            ActionItems.Add("Continue", AddressOf CurrentArea.Run)
        Else
            ActionItems.Add("Start", AddressOf Boilerplate.StartGame)
        End If
        ActionItems.Add("Quit", AddressOf ConfirmQuit)
    End Sub
End Module
