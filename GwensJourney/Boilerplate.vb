Module Boilerplate
    ReadOnly QuitGame As Action =
        Sub()
            ClearPrompts()
            ClearActions()
        End Sub

    ReadOnly ConfirmQuit As Action =
        Sub()
            ClearPrompts()
            AddPrompt("Are you sure you want to quit?")
            ClearActions()
            AddAction("No", AddressOf MainMenu, ConsoleColor.Gray)
            AddAction("Yes", QuitGame, ConsoleColor.Gray)
        End Sub

    ReadOnly StartGame As Action =
        Sub()
            InPlay.StartGame()
            CurrentArea()
        End Sub

    Sub MainMenu()
        ClearPrompts()
        AddPrompt("Main Menu:")
        ClearActions()
        If IsInPlay() Then
            AddAction("Continue", CurrentArea, ConsoleColor.Gray)
        Else
            AddAction("Start", StartGame, ConsoleColor.Gray)
        End If
        AddAction("Quit", ConfirmQuit, ConsoleColor.Gray)
    End Sub

End Module
