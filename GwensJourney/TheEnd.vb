Friend Module TheEnd
    Friend Sub Run(engine As IEngine)
        engine.Prompts.Clear()
        engine.Prompts.Add("You made it!")
        SetGameOver()
        engine.ActionItems.Clear()
        engine.ActionItems.Add("Huzzah!", Sub() MainMenu.Run(engine))
    End Sub
End Module
