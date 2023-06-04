Module Boilerplate
    Sub QuitGame()
        LegacyPrompts.Clear()
        LegacyActionItems.Clear()
    End Sub


    Sub ConfirmQuit()
        LegacyPrompts.Clear()
        LegacyPrompts.Add("Are you sure you want to quit?")
        LegacyActionItems.Clear()
        LegacyActionItems.Add("No", AddressOf MainMenu.Run)
        LegacyActionItems.Add("Yes", AddressOf QuitGame)
    End Sub

    Sub StartGame()
        Context.Initialize()
        CurrentArea.Run()
    End Sub

End Module
