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
            AddAction("Huzzah!", AddressOf MainMenu.Run, ConsoleColor.Gray)
        End Sub
    Public Function DoSetPace(pace As Integer) As Action
        Return Sub()
                   ClearPrompts()
                   SetPace(pace)
                   AddPrompt($"Yer pace is now {GetPaceName()}.")
                   ClearActions()
                   AddAction("Good", CurrentArea, ConsoleColor.Gray)
               End Sub
    End Function
    Public ReadOnly ChangePace As Action =
        Sub()
            ClearPrompts()
            AddPrompt($"Yer current pace: {GetPaceName()}")
            AddPrompt("What would you like to change yer pace to?")
            ClearActions()
            AddAction("Slowest", DoSetPace(1), ConsoleColor.Gray)
            AddAction("Slow", DoSetPace(2), ConsoleColor.Gray)
            AddAction("Medium", DoSetPace(3), ConsoleColor.Gray)
            AddAction("Fast", DoSetPace(4), ConsoleColor.Gray)
            AddAction("Fastest", DoSetPace(5), ConsoleColor.Gray)
            AddAction("Never mind", CurrentArea, ConsoleColor.Gray)
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
                    AddAction("Eat more", Eat, ConsoleColor.Gray)
                End If
                AddAction("Yum!", CurrentArea, ConsoleColor.Gray)
            Else
                AddPrompt("Yer all out of snax.")
                AddAction("Oh well...", CurrentArea, ConsoleColor.Gray)
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
            AddAction("Keep Going", KeepGoing, ConsoleColor.Gray)
            AddAction("Change Pace", ChangePace, ConsoleColor.Gray)
            If IsHungry() Then
                AddAction("Eat", Eat, ConsoleColor.Gray)
            End If
            AddAction("Main Menu", AddressOf MainMenu.Run, ConsoleColor.Gray)
        End Sub
End Module
