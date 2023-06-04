Friend Module CurrentArea
    Friend Sub Run()
        LegacyPrompts.Clear()
        LegacyPrompts.Add($"You are on the way to yer destination, and have {DistanceRemaining.Read()} miles left to go.")
        If IsHungry() Then
            LegacyPrompts.Add($"You are {HungerState.Name()}.")
        End If
        If Count() > 0 Then
            LegacyPrompts.Add($"You have {Count()} snax.")
        End If
        LegacyActionItems.Clear()
        LegacyActionItems.Add("Keep Going", AddressOf KeepGoing.Run)
        LegacyActionItems.Add("Change Pace", AddressOf ChangePace.Run)
        If IsHungry() Then
            LegacyActionItems.Add("Eat", AddressOf Eat.Run)
        End If
        LegacyActionItems.Add("Main Menu", AddressOf MainMenu.Run)
    End Sub
End Module
