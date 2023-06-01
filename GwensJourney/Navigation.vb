Module Navigation
    ReadOnly KeepGoing As Action =
        Sub()
            ChangeDistanceRemaining(-GetPace())
            ChangeHunger(GetPace())
            If HasArrived() Then
                TheEnd.Invoke()
            Else
                CurrentArea.Invoke()
            End If
        End Sub
    ReadOnly TheEnd As Action =
        Sub()
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
                   AddAction("Good", CurrentArea)
               End Sub
    End Function
    Public ReadOnly ChangePace As Action =
        Sub()
            ClearPrompts()
            AddPrompt($"Yer current pace: {GetPaceName()}")
            AddPrompt("What would you like to change yer pace to?")
            ClearActions()
            AddAction("Slowest", DoSetPace(1))
            AddAction("Slow", DoSetPace(2))
            AddAction("Medium", DoSetPace(3))
            AddAction("Fast", DoSetPace(4))
            AddAction("Fastest", DoSetPace(5))
            AddAction("Never mind", CurrentArea)
        End Sub
    Public ReadOnly Eat As Action =
        Sub()
            ClearPrompts()
            ClearActions()
            If GetSnax() > 0 Then
                AddPrompt("You eat one of yer snax.")
                ChangeSnax(-1)
                ChangeHungerState(-1)
                AddPrompt($"Yer now {GetHungerStateName()}.")
                AddPrompt($"You have {GetSnax()} snax left.")
                If IsHungry() Then
                    AddAction("Eat more", Eat)
                End If
                AddAction("Yum!", CurrentArea)
            Else
                AddPrompt("Yer all out of snax.")
                AddAction("Oh well...", CurrentArea)
            End If
        End Sub
    Public ReadOnly CurrentArea As Action =
        Sub()
            ClearPrompts()
            AddPrompt($"You are on the way to yer destination, and have {GetDistanceRemaining()} miles left to go.")
            If IsHungry() Then
                AddPrompt($"You are {GetHungerStateName()}.")
            End If
            If GetSnax() > 0 Then
                AddPrompt($"You have {GetSnax()} snax.")
            End If
            ClearActions()
            AddAction("Keep Going", KeepGoing)
            AddAction("Change Pace", ChangePace)
            If IsHungry() Then
                AddAction("Eat", Eat)
            End If
            AddAction("Main Menu", AddressOf MainMenu.Run)
        End Sub
End Module
