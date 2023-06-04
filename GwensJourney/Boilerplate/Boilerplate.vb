Module Boilerplate
    Sub QuitGame(engine As IEngine)
        engine.Prompts.Clear()
        engine.ActionItems.Clear()
    End Sub


    Sub ConfirmQuit(engine As IEngine)
        engine.Prompts.Clear()
        engine.Prompts.Add("Are you sure you want to quit?")
        engine.ActionItems.Clear()
        engine.ActionItems.Add("No", AddressOf MainMenu.Run)
        engine.ActionItems.Add("Yes", AddressOf QuitGame)
    End Sub

    Sub Embark(engine As IEngine)
        Context.Initialize()
        CurrentArea.Run(engine)
    End Sub

End Module
