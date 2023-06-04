Module Boilerplate
    Sub QuitGame(engine As IEngine)
        engine.Prompts.Clear()
        LegacyActionItems.Clear()
    End Sub


    Sub ConfirmQuit(engine As IEngine)
        engine.Prompts.Clear()
        engine.Prompts.Add("Are you sure you want to quit?")
        LegacyActionItems.Clear()
        LegacyActionItems.Add("No", Sub() MainMenu.Run(engine))
        LegacyActionItems.Add("Yes", Sub() QuitGame(engine))
    End Sub

    Sub StartGame(engine As IEngine)
        Context.Initialize()
        CurrentArea.Run(engine)
    End Sub

End Module
