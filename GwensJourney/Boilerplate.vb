Module Boilerplate
    Sub QuitGame()
        ClearPrompts()
        ClearActions()
    End Sub


    Sub ConfirmQuit()
        ClearPrompts()
        AddPrompt("Are you sure you want to quit?")
        ClearActions()
        AddAction("No", AddressOf MainMenu.Run)
        AddAction("Yes", AddressOf QuitGame)
    End Sub

    Sub StartGame()
        InPlay.StartGame()
        CurrentArea()
    End Sub

End Module
