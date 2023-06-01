Module Navigation
    Sub KeepGoing()
        ChangeDistanceRemaining(-GetPace())
        ChangeHunger(GetPace())
        If HasArrived() Then
            TheEnd()
        Else
            CurrentArea()
        End If
    End Sub
    Sub TheEnd()
        ClearPrompts()
        AddPrompt("You made it!")
        SetGameOver()
        ClearActions()
        AddAction("Huzzah!", AddressOf MainMenu.Run)
    End Sub
    Public Function DoSetPace(pace As Integer) As Action
        Return Sub()
                   ClearPrompts()
                   SetPace(pace)
                   AddPrompt($"Yer pace is now {GetPaceName()}.")
                   ClearActions()
                   AddAction("Good", AddressOf CurrentArea)
               End Sub
    End Function
    Sub ChangePace()
        ClearPrompts()
        AddPrompt($"Yer current pace: {GetPaceName()}")
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
        ClearPrompts()
        ClearActions()
        If GetSnax() > 0 Then
            AddPrompt("You eat one of yer snax.")
            ChangeSnax(-1)
            ChangeHungerState(-1)
            AddPrompt($"Yer now {GetHungerStateName()}.")
            AddPrompt($"You have {GetSnax()} snax left.")
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
        ClearPrompts()
        AddPrompt($"You are on the way to yer destination, and have {GetDistanceRemaining()} miles left to go.")
        If IsHungry() Then
            AddPrompt($"You are {GetHungerStateName()}.")
        End If
        If GetSnax() > 0 Then
            AddPrompt($"You have {GetSnax()} snax.")
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
