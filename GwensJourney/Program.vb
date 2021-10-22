Module Program
    Sub Welcome()
        Console.WriteLine("Welcome to Gwen's Journey")
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
