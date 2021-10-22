Module Program
    Sub Welcome()
        Console.WriteLine("Welcome to Gwen's Journey")
    End Sub

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
    Sub MainLoop()
        While HasActions()
            Do
                ShowPrompts()
            Loop Until SelectAction()
        End While
    End Sub
    Sub GoodBye()
        Console.WriteLine()
        Console.WriteLine("Thank you for playing Gwen's Journey")
    End Sub
    Sub Main(args As String())
        Console.Title = "Gwen's Journey"
        Welcome()
        MainMenu()
        MainLoop()
        GoodBye()
    End Sub
End Module
