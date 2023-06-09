Friend Module Eat
    Friend Sub Run(engine As IEngine, context As IContext)
        engine.Prompts.Clear()
        engine.ActionItems.Clear()
        If context.Snax.Count() <= 0 Then
            engine.Prompts.Add("Yer all out of snax.")
            engine.ActionItems.Add("Oh well...", AddressOf Neutral.Run)
            Return
        End If
        engine.Prompts.Add("You eat one of yer snax.")
        context.Snax.Change(-1)
        context.HungerState.Change(-1)
        engine.Prompts.Add($"Yer now {context.HungerState.Name()}.")
        engine.Prompts.Add($"You have {context.Snax.Count()} snax left.")
        If context.HungerState.IsHungry() Then
            engine.ActionItems.Add("Eat more", AddressOf Run)
        End If
        engine.ActionItems.Add("Yum!", AddressOf Neutral.Run)
    End Sub
End Module
