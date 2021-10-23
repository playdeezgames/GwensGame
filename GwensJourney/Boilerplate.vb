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
            AddAction("No", MainMenu)
            AddAction("Yes", QuitGame)
        End Sub

    ReadOnly StartGame As Action =
        Sub()
            ResetData()
            CurrentArea()
        End Sub

    Public ReadOnly MainMenu As Action =
        Sub()
            ClearPrompts()
            AddPrompt("Main Menu:")
            ClearActions()
            If IsInPlay() Then
                AddAction("Continue", CurrentArea)
            Else
                AddAction("Start", StartGame)
            End If
            AddAction("Quit", ConfirmQuit)
        End Sub

End Module
