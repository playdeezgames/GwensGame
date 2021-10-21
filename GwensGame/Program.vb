Module Program
    Sub Main(args As String())
        ShowWelcome()
        ShowMainMenu()
        While HasInputItems()
            Console.WriteLine()
            ShowPrompts()
            ReadInput()
        End While
        CloseConnection()
        ShowGoodBye()
    End Sub
End Module
