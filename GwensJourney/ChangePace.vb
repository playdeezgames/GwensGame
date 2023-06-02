Friend Module ChangePace
    Friend Sub Run()
        Prompts.Clear()
        Prompts.Add($"Yer current pace: {Pace.Name()}")
        Prompts.Add("What would you like to change yer pace to?")
        ActionItems.Clear()
        For paceValue = Pace.MinimumPace To Pace.MaximumPace
            Dim v = paceValue
            ActionItems.Add(
                Pace.Name(paceValue),
                Sub()
                    Prompts.Clear()
                    Write(v)
                    Prompts.Add($"Yer pace is now {GwensJourney.Pace.Name()}.")
                    ActionItems.Clear()
                    ActionItems.Add("Good", AddressOf CurrentArea.Run)
                End Sub)
        Next
        ActionItems.Add("Never mind", AddressOf CurrentArea.Run)
    End Sub
End Module
