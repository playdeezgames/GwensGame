Friend Module ChangePace
    Friend Sub Run(engine As IEngine)
        engine.Prompts.Clear()
        LegacyPrompts.Add(engine, $"Yer current pace: {Pace.Name()}")
        LegacyPrompts.Add(engine, "What would you like to change yer pace to?")
        LegacyActionItems.Clear()
        For paceValue = Pace.MinimumPace To Pace.MaximumPace
            Dim v = paceValue
            LegacyActionItems.Add(
                Pace.Name(paceValue),
                Sub()
                    engine.Prompts.Clear()
                    Write(v)
                    LegacyPrompts.Add(engine, $"Yer pace is now {GwensJourney.Pace.Name()}.")
                    LegacyActionItems.Clear()
                    LegacyActionItems.Add("Good", Sub() CurrentArea.Run(engine))
                End Sub)
        Next
        LegacyActionItems.Add("Never mind", Sub() CurrentArea.Run(engine))
    End Sub
End Module
