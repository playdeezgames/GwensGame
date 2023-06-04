Friend Module MainMenu
    Friend Sub Run(engine As IEngine)
        LegacyPrompts.Clear(engine)
        LegacyPrompts.Add(engine, "Main Menu:")
        LegacyActionItems.Clear()
        If IsInPlay() Then
            LegacyActionItems.Add("Continue", Sub() CurrentArea.Run(engine))
        Else
            LegacyActionItems.Add("Start", Sub() Boilerplate.StartGame(engine))
        End If
        LegacyActionItems.Add("Quit", Sub() ConfirmQuit(engine))
    End Sub
End Module
