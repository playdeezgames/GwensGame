Friend Module CurrentArea
    Friend Sub Run(engine As IEngine, context As IContext)
        engine.Prompts.Clear()
        engine.Prompts.Add($"You are on the way to yer destination, and have {context.DistanceRemaining.Read()} miles left to go.")
        engine.Prompts.Add($"Yer pace is {context.Pace.Name()}.")
        If context.HungerState.IsHungry() Then
            engine.Prompts.Add($"You are {context.HungerState.Name()}.")
        End If
        If context.Snax.Count() > 0 Then
            engine.Prompts.Add($"You have {context.Snax.Count()} snax.")
        End If
        engine.ActionItems.Clear()
        engine.ActionItems.Add("Keep Going", AddressOf KeepGoing.Run)
        engine.ActionItems.Add("Change Pace", AddressOf ChangePace.Run)
        If context.HungerState.IsHungry() Then
            engine.ActionItems.Add("Eat", AddressOf Eat.Run)
        End If
        engine.ActionItems.Add("Main Menu", AddressOf MainMenu.Run)
    End Sub
End Module
