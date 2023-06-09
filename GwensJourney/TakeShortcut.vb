Friend Module TakeShortcut
    Friend Sub Run(engine As IEngine, context As IContext)
        engine.Prompts.Clear()
        engine.ActionItems.Clear()
        engine.Prompts.Add("You take the shortcut!")
        context.HungerCounter.Change(1)
        context.DistanceRemaining.Change(context.Shortcut.Distance)
        If context.Shortcut.Distance > 0 Then
            engine.Prompts.Add("Winds up taking you further away from yer goal!")
        End If
        NewArea.Run(context)
        engine.ActionItems.Add("Ok", AddressOf Neutral.Run)
    End Sub
End Module
