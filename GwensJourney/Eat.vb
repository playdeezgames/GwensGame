Friend Module Eat
    Friend Sub Run()
        LegacyPrompts.Clear()
        LegacyActionItems.Clear()
        If Count() <= 0 Then
            LegacyPrompts.Add("Yer all out of snax.")
            LegacyActionItems.Add("Oh well...", AddressOf CurrentArea.Run)
            Return
        End If
        LegacyPrompts.Add("You eat one of yer snax.")
        Snax.Change(-1)
        HungerState.Change(-1)
        LegacyPrompts.Add($"Yer now {HungerState.Name()}.")
        LegacyPrompts.Add($"You have {Count()} snax left.")
        If IsHungry() Then
            LegacyActionItems.Add("Eat more", AddressOf Run)
        End If
        LegacyActionItems.Add("Yum!", AddressOf CurrentArea.Run)
    End Sub
End Module
