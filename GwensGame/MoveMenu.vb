Module MoveMenu
    Function MakeMove(direction As Direction, message As String) As Boolean
        Dim scene As IScene = GetScene(GetX(), GetY())
        If scene.CanMove(direction) Then
            AddPrompt(message)
            Dim nextX = GetNextX(GetX(), GetY(), direction)
            Dim nextY = GetNextY(GetX(), GetY(), direction)
            SetXY(nextX, nextY)
            ShowStatus()
            Return True
        Else
            AddPrompt("You can't go that way.")
        End If
    End Function
    Public MoveAhead As Action =
        Sub()
            ClearPrompts()
            MakeMove(GetFacing(), "You move ahead.")
        End Sub
    Public MoveRight As Action =
        Sub()
            ClearPrompts()
            MakeMove(Directions.GetNextDirection(GetFacing()), "You move right.")
        End Sub
    Public MoveBack As Action =
        Sub()
            ClearPrompts()
            MakeMove(Directions.GetOppositeDirection(GetFacing()), "You move back.")
        End Sub
    Public MoveLeft As Action =
        Sub()
            ClearPrompts()
            MakeMove(Directions.GetPreviousDirection(GetFacing()), "You move left.")
        End Sub
    Public ShowMoveMenu As Action =
        Sub()
            ClearPrompts()
            ClearInputItems()
            AddPrompt("Which way do you want to move?")
            AddInputItem("Ahead", MoveAhead)
            AddInputItem("Left", MoveLeft)
            AddInputItem("Right", MoveRight)
            AddInputItem("Back", MoveBack)
            AddInputItem("Never Mind", ShowStatus)
        End Sub
End Module
