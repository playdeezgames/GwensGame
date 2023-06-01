Module Boilerplate
    Sub QuitGame()
        ClearPrompts()
        ClearActions()
    End Sub


    Sub ConfirmQuit()
        ClearPrompts()
        AddPrompt("Are you sure you want to quit?")
        ClearActions()
        AddAction("No", AddressOf MainMenu.Run, ConsoleColor.Gray)
        AddAction("Yes", AddressOf QuitGame, ConsoleColor.Gray)
    End Sub

    Sub StartGame()
        InPlay.StartGame()
        CurrentArea()
    End Sub

End Module
