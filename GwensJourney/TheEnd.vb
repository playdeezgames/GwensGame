Friend Module TheEnd
    Friend Sub Run(engine As IEngine)
        LegacyPrompts.Clear(engine)
        LegacyPrompts.Add(engine, "You made it!")
        SetGameOver()
        LegacyActionItems.Clear()
        LegacyActionItems.Add("Huzzah!", Sub() MainMenu.Run(engine))
    End Sub
End Module
