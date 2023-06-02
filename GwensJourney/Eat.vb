Friend Module Eat
    Friend Sub Run()
        Prompts.Clear()
        ActionItems.Clear()
        If Count() <= 0 Then
            Prompts.Add("Yer all out of snax.")
            ActionItems.Add("Oh well...", AddressOf CurrentArea.Run)
            Return
        End If
        Prompts.Add("You eat one of yer snax.")
        Snax.Change(-1)
        HungerState.Change(-1)
        Prompts.Add($"Yer now {HungerState.Name()}.")
        Prompts.Add($"You have {Count()} snax left.")
        If IsHungry() Then
            ActionItems.Add("Eat more", AddressOf Run)
        End If
        ActionItems.Add("Yum!", AddressOf CurrentArea.Run)
    End Sub
End Module
