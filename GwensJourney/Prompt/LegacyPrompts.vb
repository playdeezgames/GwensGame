Module LegacyPrompts
    Sub Clear(engine As IEngine)
        engine.Prompts.Clear()
    End Sub
    Sub Add(engine As IEngine, prompt As String)
        engine.Prompts.Add(prompt)
    End Sub
    Sub Show(engine As IEngine)
        engine.Prompts.Show()
    End Sub
End Module
