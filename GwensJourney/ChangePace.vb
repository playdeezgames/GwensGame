Friend Module ChangePace
    Friend Sub Run(engine As IEngine)
        engine.Prompts.Clear()
        engine.Prompts.Add($"Yer current pace: {LegacyPace.Name()}")
        engine.Prompts.Add("What would you like to change yer pace to?")
        engine.ActionItems.Clear()
        For paceValue = LegacyPace.MinimumPace To LegacyPace.MaximumPace
            Dim v = paceValue
            engine.ActionItems.Add(
                LegacyPace.Name(paceValue),
                SetNewPace(engine, v))
        Next
        engine.ActionItems.Add("Never mind", AddressOf CurrentArea.Run)
    End Sub

    Private Function SetNewPace(engine As IEngine, paceValue As Integer) As Action(Of IEngine)
        Return Sub(e)
                   e.Prompts.Clear()
                   Write(paceValue)
                   e.Prompts.Add($"Yer pace is now {GwensJourney.LegacyPace.Name()}.")
                   e.ActionItems.Clear()
                   e.ActionItems.Add("Good", AddressOf CurrentArea.Run)
               End Sub
    End Function
End Module
