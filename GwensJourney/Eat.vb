Friend Module Eat
    Friend Sub Run(engine As IEngine)
        engine.Prompts.Clear()
        engine.ActionItems.Clear()
        If Count() <= 0 Then
            engine.Prompts.Add("Yer all out of snax.")
            engine.ActionItems.Add("Oh well...", AddressOf CurrentArea.Run)
            Return
        End If
        engine.Prompts.Add("You eat one of yer snax.")
        LegacySnax.Change(-1)
        LegacyHungerState.Change(-1)
        engine.Prompts.Add($"Yer now {LegacyHungerState.Name()}.")
        engine.Prompts.Add($"You have {Count()} snax left.")
        If IsHungry() Then
            engine.ActionItems.Add("Eat more", AddressOf Run)
        End If
        engine.ActionItems.Add("Yum!", AddressOf CurrentArea.Run)
    End Sub
End Module
