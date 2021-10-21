Module Quit
    Public ConfirmQuit As Action =
        Sub()
            ClearInputItems()
        End Sub
    Public QuitGame As Action =
        Sub()
            ClearPrompts()
            ClearInputItems()
            AddPrompt("Are you sure you want to quit?")
            AddInputItem("No", ShowMainMenu)
            AddInputItem("Yes", ConfirmQuit)
        End Sub
End Module
