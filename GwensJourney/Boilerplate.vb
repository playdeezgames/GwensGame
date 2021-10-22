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

    ReadOnly MainMenu As Action =
        Sub()
            ClearPrompts()
            AddPrompt("Main Menu:")
            ClearActions()
            AddAction("Quit", ConfirmQuit)
        End Sub

End Module
