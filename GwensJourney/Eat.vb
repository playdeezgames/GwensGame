Friend Module Eat
    Friend Sub Run(engine As IEngine)
        engine.Prompts.Clear()
        engine.ActionItems.Clear()
        If Count() <= 0 Then
            engine.Prompts.Add("Yer all out of snax.")
            engine.ActionItems.Add("Oh well...", Sub() CurrentArea.Run(engine))
            Return
        End If
        engine.Prompts.Add("You eat one of yer snax.")
        Snax.Change(-1)
        HungerState.Change(-1)
        engine.Prompts.Add($"Yer now {HungerState.Name()}.")
        engine.Prompts.Add($"You have {Count()} snax left.")
        If IsHungry() Then
            engine.ActionItems.Add("Eat more", Sub() Run(engine))
        End If
        engine.ActionItems.Add("Yum!", Sub() CurrentArea.Run(engine))
    End Sub
End Module
