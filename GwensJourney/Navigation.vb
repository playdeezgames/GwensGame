﻿Module Navigation
    Sub KeepGoing()
        DistanceRemaining.Change(-GetPace())
        HungerCounter.Change(GetPace())
        If HasArrived() Then
            TheEnd()
        Else
            CurrentArea()
        End If
    End Sub
    Sub TheEnd()
        Clear()
        AddPrompt("You made it!")
        SetGameOver()
        ClearActions()
        AddAction("Huzzah!", AddressOf MainMenu.Run)
    End Sub
    Public Function DoSetPace(pace As Integer) As Action
        Return Sub()
                   Clear()
                   SetPace(pace)
                   AddPrompt($"Yer pace is now {GwensJourney.Pace.Name()}.")
                   ClearActions()
                   AddAction("Good", AddressOf CurrentArea)
               End Sub
    End Function
    Sub ChangePace()
        Clear()
        AddPrompt($"Yer current pace: {Pace.Name()}")
        AddPrompt("What would you like to change yer pace to?")
        ClearActions()
        AddAction("Slowest", DoSetPace(1))
        AddAction("Slow", DoSetPace(2))
        AddAction("Medium", DoSetPace(3))
        AddAction("Fast", DoSetPace(4))
        AddAction("Fastest", DoSetPace(5))
        AddAction("Never mind", AddressOf CurrentArea)
    End Sub
    Sub Eat()
        Clear()
        ClearActions()
        If Count() > 0 Then
            AddPrompt("You eat one of yer snax.")
            Snax.Change(-1)
            HungerState.Change(-1)
            AddPrompt($"Yer now {HungerState.Name()}.")
            AddPrompt($"You have {Count()} snax left.")
            If IsHungry() Then
                AddAction("Eat more", AddressOf Eat)
            End If
            AddAction("Yum!", AddressOf CurrentArea)
        Else
            AddPrompt("Yer all out of snax.")
            AddAction("Oh well...", AddressOf CurrentArea)
        End If
    End Sub
    Sub CurrentArea()
        Clear()
        AddPrompt($"You are on the way to yer destination, and have {Read()} miles left to go.")
        If IsHungry() Then
            AddPrompt($"You are {HungerState.Name()}.")
        End If
        If Count() > 0 Then
            AddPrompt($"You have {Count()} snax.")
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
