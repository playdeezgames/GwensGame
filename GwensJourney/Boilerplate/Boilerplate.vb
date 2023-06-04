Module Boilerplate
    Sub QuitGame(engine As IEngine)
        LegacyPrompts.Clear(engine)
        LegacyActionItems.Clear()
    End Sub


    Sub ConfirmQuit(engine As IEngine)
        LegacyPrompts.Clear(engine)
        LegacyPrompts.Add(engine, "Are you sure you want to quit?")
        LegacyActionItems.Clear()
        LegacyActionItems.Add("No", Sub() MainMenu.Run(engine))
        LegacyActionItems.Add("Yes", Sub() QuitGame(engine))
    End Sub

    Sub StartGame(engine As IEngine)
        Context.Initialize()
        CurrentArea.Run(engine)
    End Sub

End Module
