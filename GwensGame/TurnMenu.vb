Module TurnMenu
    Sub SetFacingDirection(direction As Direction)
        SetFacing(direction)
        ShowStatus()
    End Sub
    Public TurnLeft As Action =
        Sub()
            ClearPrompts()
            AddPrompt("You turn left.")
            SetFacingDirection(GetPreviousDirection(GetFacing()))
        End Sub
    Public TurnRight As Action =
        Sub()
            ClearPrompts()
            AddPrompt("You turn right.")
            SetFacingDirection(GetNextDirection(GetFacing()))
        End Sub
    Public TurnAround As Action =
        Sub()
            ClearPrompts()
            AddPrompt("You turn around.")
            SetFacingDirection(GetOppositeDirection(GetFacing()))
        End Sub
    Public ShowTurnMenu As Action =
        Sub()
            ClearPrompts()
            AddPrompt("Which way do you want to turn?")
            ClearInputItems()
            AddInputItem("Right", TurnRight)
            AddInputItem("Left", TurnLeft)
            AddInputItem("Around", TurnAround)
            AddInputItem("Never Mind", ShowStatus)
        End Sub
End Module
