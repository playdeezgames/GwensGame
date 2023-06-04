Module Program
    Sub Main(args As String())
        Console.Title = "Gwen's Journey"
        Dim engine As IEngine = New Engine()
        Welcome.Run()
        MainMenu.Run(engine)
        MainLoop.Run(engine)
        GoodBye.Run()
    End Sub
End Module
