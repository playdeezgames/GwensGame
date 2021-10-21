Module MoveMenu
    Sub MakeMove(direction As Direction)
        Dim nextX = GetNextX(GetX(), GetY(), direction)
        Dim nextY = GetNextY(GetX(), GetY(), direction)
        SetXY(nextX, nextY)
        ShowStatus()
    End Sub
    Public MoveAhead As Action =
        Sub()
            ClearPrompts()
            AddPrompt("You move ahead.")
            MakeMove(GetFacing())
        End Sub
    Public MoveRight As Action =
        Sub()
            ClearPrompts()
            AddPrompt("You move right.")
            MakeMove(Directions.GetNextDirection(GetFacing()))
        End Sub
    Public MoveBack As Action =
        Sub()
            ClearPrompts()
            AddPrompt("You move back.")
            MakeMove(Directions.GetOppositeDirection(GetFacing()))
        End Sub
    Public MoveLeft As Action =
        Sub()
            ClearPrompts()
            AddPrompt("You move left.")
            MakeMove(Directions.GetPreviousDirection(GetFacing()))
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
