Friend Module CurrentArea
    Friend Sub Run(engine As IEngine)
        engine.Prompts.Clear()
        engine.Prompts.Add($"You are on the way to yer destination, and have {DistanceRemaining.Read()} miles left to go.")
        If IsHungry() Then
            engine.Prompts.Add($"You are {HungerState.Name()}.")
        End If
        If Count() > 0 Then
            engine.Prompts.Add($"You have {Count()} snax.")
        End If
        engine.ActionItems.Clear()
        engine.ActionItems.Add("Keep Going", Sub() KeepGoing.Run(engine))
        engine.ActionItems.Add("Change Pace", Sub() ChangePace.Run(engine))
        If IsHungry() Then
            engine.ActionItems.Add("Eat", Sub() Eat.Run(engine))
        End If
        engine.ActionItems.Add("Main Menu", Sub() MainMenu.Run(engine))
    End Sub
End Module
