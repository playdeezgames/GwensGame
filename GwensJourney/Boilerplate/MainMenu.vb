Friend Module MainMenu
    Friend Sub Run(engine As IEngine)
        engine.Prompts.Clear()
        engine.Prompts.Add("Main Menu:")
        engine.ActionItems.Clear()
        If IsInPlay() Then
            engine.ActionItems.Add("Continue", Sub() CurrentArea.Run(engine))
        Else
            engine.ActionItems.Add("Start", Sub() Boilerplate.StartGame(engine))
        End If
        engine.ActionItems.Add("Quit", Sub() ConfirmQuit(engine))
    End Sub
End Module
