Friend Module ChangePace
    Friend Sub Run(engine As IEngine, context As IContext)
        engine.Prompts.Clear()
        engine.Prompts.Add($"Yer current pace: {context.Pace.Name()}")
        engine.Prompts.Add("What would you like to change yer pace to?")
        engine.ActionItems.Clear()
        For paceValue = Pace.MinimumPace To Pace.MaximumPace
            Dim v = paceValue
            engine.ActionItems.Add(
                context.Pace.Name(paceValue),
                SetNewPace(v))
        Next
        engine.ActionItems.Add("Never mind", AddressOf Neutral.Run)
    End Sub

    Private Function SetNewPace(paceValue As Integer) As Action(Of IEngine, IContext)
        Return Sub(e, c)
                   e.Prompts.Clear()
                   c.Pace.Write(paceValue)
                   e.Prompts.Add($"Yer pace is now {c.Pace.Name()}.")
                   e.ActionItems.Clear()
                   e.ActionItems.Add("Good", AddressOf Neutral.Run)
               End Sub
    End Function
End Module
