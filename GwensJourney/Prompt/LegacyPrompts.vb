Module LegacyPrompts
    Sub Add(engine As IEngine, prompt As String)
        engine.Prompts.Add(prompt)
    End Sub
End Module
