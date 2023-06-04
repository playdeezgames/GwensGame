Friend Module ChangePace
    Friend Sub Run()
        LegacyPrompts.Clear()
        LegacyPrompts.Add($"Yer current pace: {Pace.Name()}")
        LegacyPrompts.Add("What would you like to change yer pace to?")
        LegacyActionItems.Clear()
        For paceValue = Pace.MinimumPace To Pace.MaximumPace
            Dim v = paceValue
            LegacyActionItems.Add(
                Pace.Name(paceValue),
                Sub()
                    LegacyPrompts.Clear()
                    Write(v)
                    LegacyPrompts.Add($"Yer pace is now {GwensJourney.Pace.Name()}.")
                    LegacyActionItems.Clear()
                    LegacyActionItems.Add("Good", AddressOf CurrentArea.Run)
                End Sub)
        Next
        LegacyActionItems.Add("Never mind", AddressOf CurrentArea.Run)
    End Sub
End Module
