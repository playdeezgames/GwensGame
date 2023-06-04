Friend Module TheEnd
    Friend Sub Run(engine As IEngine)
        engine.Prompts.Clear()
        engine.Prompts.Add("You made it!")
        SetGameOver()
        engine.ActionItems.Clear()
        engine.ActionItems.Add("Huzzah!", AddressOf MainMenu.Run)
    End Sub
End Module
