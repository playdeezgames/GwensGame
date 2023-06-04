Friend Module MainMenu
    Friend Sub Run(engine As IEngine)
        engine.Prompts.Clear()
        engine.Prompts.Add("Main Menu:")
        engine.ActionItems.Clear()
        If IsInPlay() Then
            engine.ActionItems.Add("Continue", AddressOf CurrentArea.Run)
        Else
            engine.ActionItems.Add("Embark!", AddressOf Embark.Run)
        End If
        engine.ActionItems.Add("Quit", AddressOf ConfirmQuit.Run)
    End Sub
End Module
