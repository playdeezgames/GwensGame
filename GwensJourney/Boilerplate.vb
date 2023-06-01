Module Boilerplate
    Sub QuitGame()
        Clear()
        ClearActions()
    End Sub


    Sub ConfirmQuit()
        Clear()
        Add("Are you sure you want to quit?")
        ClearActions()
        AddAction("No", AddressOf MainMenu.Run)
        AddAction("Yes", AddressOf QuitGame)
    End Sub

    Sub StartGame()
        InPlay.Initialize()
        CurrentArea()
    End Sub

End Module
