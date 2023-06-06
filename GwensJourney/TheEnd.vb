Friend Module TheEnd
    Friend Sub Run(engine As IEngine, context As IContext)
        engine.Prompts.Clear()
        engine.Prompts.Add("You made it!")
        context.SetGameOver()
        engine.ActionItems.Clear()
        engine.ActionItems.Add("Huzzah!", AddressOf MainMenu.Run)
    End Sub
End Module
