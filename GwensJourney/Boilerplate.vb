Module Boilerplate
    Sub QuitGame()
        Prompts.Clear()
        ActionItems.Clear()
    End Sub


    Sub ConfirmQuit()
        Prompts.Clear()
        Prompts.Add("Are you sure you want to quit?")
        ActionItems.Clear()
        ActionItems.Add("No", AddressOf MainMenu.Run)
        ActionItems.Add("Yes", AddressOf QuitGame)
    End Sub

    Sub StartGame()
        InPlay.Initialize()
        CurrentArea()
    End Sub

End Module
