Friend Module CurrentArea
    Friend Sub Run()
        Prompts.Clear()
        Prompts.Add($"You are on the way to yer destination, and have {DistanceRemaining.Read()} miles left to go.")
        If IsHungry() Then
            Prompts.Add($"You are {HungerState.Name()}.")
        End If
        If Count() > 0 Then
            Prompts.Add($"You have {Count()} snax.")
        End If
        ActionItems.Clear()
        ActionItems.Add("Keep Going", AddressOf KeepGoing.Run)
        ActionItems.Add("Change Pace", AddressOf ChangePace.Run)
        If IsHungry() Then
            ActionItems.Add("Eat", AddressOf Eat.Run)
        End If
        ActionItems.Add("Main Menu", AddressOf MainMenu.Run)
    End Sub
End Module
