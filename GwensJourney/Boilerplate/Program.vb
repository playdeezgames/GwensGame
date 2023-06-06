Module Program
    Sub Main(args As String())
        Console.Title = "Gwen's Journey"
        Dim engine As IEngine = New Engine()
        Dim context As IContext = New Context(New ContextData)
        Welcome.Run()
        MainMenu.Run(engine, context)
        MainLoop.Run(engine, context)
        GoodBye.Run()
    End Sub
End Module
