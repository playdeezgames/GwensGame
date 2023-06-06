Friend Module CurrentArea
    Friend Sub Run(engine As IEngine)
        engine.Prompts.Clear()
        engine.Prompts.Add($"You are on the way to yer destination, and have {LegacyDistanceRemaining.Read()} miles left to go.")
        engine.Prompts.Add($"Yer pace is {LegacyPace.Name()}.")
        If IsHungry() Then
            engine.Prompts.Add($"You are {LegacyHungerState.Name()}.")
        End If
        If Count() > 0 Then
            engine.Prompts.Add($"You have {Count()} snax.")
        End If
        engine.ActionItems.Clear()
        engine.ActionItems.Add("Keep Going", AddressOf KeepGoing.Run)
        engine.ActionItems.Add("Change Pace", AddressOf ChangePace.Run)
        If IsHungry() Then
            engine.ActionItems.Add("Eat", AddressOf Eat.Run)
        End If
        engine.ActionItems.Add("Main Menu", AddressOf MainMenu.Run)
    End Sub
End Module
