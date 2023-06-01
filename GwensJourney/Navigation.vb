Module Navigation
    Sub KeepGoing()
        DistanceRemaining.Change(-Pace.Read())
        HungerCounter.Change(Pace.Read())
        If HasArrived() Then
            TheEnd()
        Else
            CurrentArea()
        End If
    End Sub
    Sub TheEnd()
        Prompts.Clear()
        Prompts.Add("You made it!")
        SetGameOver()
        ActionItems.Clear()
        ActionItems.Add("Huzzah!", AddressOf MainMenu.Run)
    End Sub
    Public Function DoSetPace(pace As Integer) As Action
        Return Sub()
                   Prompts.Clear()
                   Write(pace)
                   Prompts.Add($"Yer pace is now {GwensJourney.Pace.Name()}.")
                   ActionItems.Clear()
                   ActionItems.Add("Good", AddressOf CurrentArea)
               End Sub
    End Function
    Sub ChangePace()
        Prompts.Clear()
        Prompts.Add($"Yer current pace: {Pace.Name()}")
        Prompts.Add("What would you like to change yer pace to?")
        ActionItems.Clear()
        ActionItems.Add(Pace.Name(1), DoSetPace(1))
        ActionItems.Add(Pace.Name(2), DoSetPace(2))
        ActionItems.Add(Pace.Name(3), DoSetPace(3))
        ActionItems.Add(Pace.Name(4), DoSetPace(4))
        ActionItems.Add(Pace.Name(5), DoSetPace(5))
        ActionItems.Add("Never mind", AddressOf CurrentArea)
    End Sub
    Sub Eat()
        Prompts.Clear()
        ActionItems.Clear()
        If Count() > 0 Then
            Prompts.Add("You eat one of yer snax.")
            Snax.Change(-1)
            HungerState.Change(-1)
            Prompts.Add($"Yer now {HungerState.Name()}.")
            Prompts.Add($"You have {Count()} snax left.")
            If IsHungry() Then
                ActionItems.Add("Eat more", AddressOf Eat)
            End If
            ActionItems.Add("Yum!", AddressOf CurrentArea)
        Else
            Prompts.Add("Yer all out of snax.")
            ActionItems.Add("Oh well...", AddressOf CurrentArea)
        End If
    End Sub
    Sub CurrentArea()
        Prompts.Clear()
        Prompts.Add($"You are on the way to yer destination, and have {DistanceRemaining.Read()} miles left to go.")
        If IsHungry() Then
            Prompts.Add($"You are {HungerState.Name()}.")
        End If
        If Count() > 0 Then
            Prompts.Add($"You have {Count()} snax.")
        End If
        ActionItems.Clear()
        ActionItems.Add("Keep Going", AddressOf KeepGoing)
        ActionItems.Add("Change Pace", AddressOf ChangePace)
        If IsHungry() Then
            ActionItems.Add("Eat", AddressOf Eat)
        End If
        ActionItems.Add("Main Menu", AddressOf MainMenu.Run)
    End Sub
End Module
