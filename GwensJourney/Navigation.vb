Module Navigation
    ReadOnly KeepGoing As Action =
        Sub()
            ChangeDistanceRemaining(-1)
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
            AddAction("Huzzah!", MainMenu)
        End Sub
    Public ReadOnly CurrentArea As Action =
        Sub()
            ClearPrompts()
            AddPrompt($"You are on the way to yer destination, and have {GetDistanceRemaining()} miles left to go.")
            ClearActions()
            AddAction("Keep Going", KeepGoing)
            AddAction("Main Menu", MainMenu)
        End Sub
End Module
