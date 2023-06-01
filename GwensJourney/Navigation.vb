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
        Clear()
        Add("You made it!")
        SetGameOver()
        ClearActions()
        AddAction("Huzzah!", AddressOf MainMenu.Run)
    End Sub
    Public Function DoSetPace(pace As Integer) As Action
        Return Sub()
                   Clear()
                   Write(pace)
                   Add($"Yer pace is now {GwensJourney.Pace.Name()}.")
                   ClearActions()
                   AddAction("Good", AddressOf CurrentArea)
               End Sub
    End Function
    Sub ChangePace()
        Clear()
        Add($"Yer current pace: {Pace.Name()}")
        Add("What would you like to change yer pace to?")
        ClearActions()
        AddAction(Pace.Name(1), DoSetPace(1))
        AddAction(Pace.Name(2), DoSetPace(2))
        AddAction(Pace.Name(3), DoSetPace(3))
        AddAction(Pace.Name(4), DoSetPace(4))
        AddAction(Pace.Name(5), DoSetPace(5))
        AddAction("Never mind", AddressOf CurrentArea)
    End Sub
    Sub Eat()
        Clear()
        ClearActions()
        If Count() > 0 Then
            Add("You eat one of yer snax.")
            Snax.Change(-1)
            HungerState.Change(-1)
            Add($"Yer now {HungerState.Name()}.")
            Add($"You have {Count()} snax left.")
            If IsHungry() Then
                AddAction("Eat more", AddressOf Eat)
            End If
            AddAction("Yum!", AddressOf CurrentArea)
        Else
            Add("Yer all out of snax.")
            AddAction("Oh well...", AddressOf CurrentArea)
        End If
    End Sub
    Sub CurrentArea()
        Clear()
        Add($"You are on the way to yer destination, and have {DistanceRemaining.Read()} miles left to go.")
        If IsHungry() Then
            Add($"You are {HungerState.Name()}.")
        End If
        If Count() > 0 Then
            Add($"You have {Count()} snax.")
        End If
        ClearActions()
        AddAction("Keep Going", AddressOf KeepGoing)
        AddAction("Change Pace", AddressOf ChangePace)
        If IsHungry() Then
            AddAction("Eat", AddressOf Eat)
        End If
        AddAction("Main Menu", AddressOf MainMenu.Run)
    End Sub
End Module
