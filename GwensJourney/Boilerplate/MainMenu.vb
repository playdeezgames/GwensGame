Friend Module MainMenu
    Friend Sub Run(engine As IEngine)
        engine.Prompts.Clear()
        engine.Prompts.Add("Main Menu:")
        engine.ActionItems.Clear()
        If IsInPlay() Then
            engine.ActionItems.Add("Continue", AddressOf CurrentArea.Run)
        Else
            engine.ActionItems.Add("Embark!", AddressOf Boilerplate.Embark)
        End If
        engine.ActionItems.Add("Quit", AddressOf ConfirmQuit)
    End Sub
End Module
