Friend Module Eat
    Friend Sub Run(engine As IEngine)
        engine.Prompts.Clear()
        LegacyActionItems.Clear()
        If Count() <= 0 Then
            LegacyPrompts.Add(engine, "Yer all out of snax.")
            LegacyActionItems.Add("Oh well...", Sub() CurrentArea.Run(engine))
            Return
        End If
        LegacyPrompts.Add(engine, "You eat one of yer snax.")
        Snax.Change(-1)
        HungerState.Change(-1)
        LegacyPrompts.Add(engine, $"Yer now {HungerState.Name()}.")
        LegacyPrompts.Add(engine, $"You have {Count()} snax left.")
        If IsHungry() Then
            LegacyActionItems.Add("Eat more", Sub() Run(engine))
        End If
        LegacyActionItems.Add("Yum!", Sub() CurrentArea.Run(engine))
    End Sub
End Module
