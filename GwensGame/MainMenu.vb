Module MainMenu
    Public ShowMainMenu As Action =
        Sub()
            ClearPrompts()
            AddPrompt("Main Menu:")
            ClearInputItems()
            If IsInPlay() Then
                AddInputItem("Continue Game", ShowStatus)
            Else
                AddInputItem("Start Game", AddressOf StartGame)
            End If
            AddInputItem("Quit", QuitGame)
        End Sub
End Module
