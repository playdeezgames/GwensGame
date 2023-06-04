Friend Module MainMenu
    Friend Sub Run()
        LegacyPrompts.Clear()
        LegacyPrompts.Add("Main Menu:")
        LegacyActionItems.Clear()
        If IsInPlay() Then
            LegacyActionItems.Add("Continue", AddressOf CurrentArea.Run)
        Else
            LegacyActionItems.Add("Start", AddressOf Boilerplate.StartGame)
        End If
        LegacyActionItems.Add("Quit", AddressOf ConfirmQuit)
    End Sub
End Module
