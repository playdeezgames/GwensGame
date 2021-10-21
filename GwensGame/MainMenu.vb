Module MainMenu
    Public ShowMainMenu As Action =
        Sub()
            ClearPrompts()
            AddPrompt("Main Menu:")
            ClearInputItems()
            AddInputItem("Start Game", StartGame)
            AddInputItem("Quit", QuitGame)
        End Sub
End Module
