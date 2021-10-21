Module Program
    Sub Main(args As String())
        ShowWelcome()
        ShowMainMenu()
        While HasInputItems()
            ShowPrompts()
            ReadInput()
        End While
        ShowGoodBye()
    End Sub
End Module
