Module Boilerplate
    Sub QuitGame(engine As IEngine)
        engine.Prompts.Clear()
        engine.ActionItems.Clear()
    End Sub


    Sub ConfirmQuit(engine As IEngine)
        engine.Prompts.Clear()
        engine.Prompts.Add("Are you sure you want to quit?")
        engine.ActionItems.Clear()
        engine.ActionItems.Add("No", Sub() MainMenu.Run(engine))
        engine.ActionItems.Add("Yes", Sub() QuitGame(engine))
    End Sub

    Sub StartGame(engine As IEngine)
        Context.Initialize()
        CurrentArea.Run(engine)
    End Sub

End Module
