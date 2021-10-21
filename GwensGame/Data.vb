Module Data
    Private x As Integer = 0
    Private y As Integer = 0
    Private facing As Direction = Direction.North
    Private inPlay As Boolean = False
    Public Function GetX() As Integer
        Return x
    End Function
    Public Function GetY() As Integer
        Return y
    End Function
    Public Sub SetXY(newX As Integer, newY As Integer)
        x = newX
        y = newY
    End Sub
    Public Function GetFacing() As Direction
        Return facing
    End Function
    Public Sub SetFacing(newFacing As Direction)
        facing = newFacing
    End Sub
    Public Sub ResetData()
        x = 0
        y = 0
        facing = Direction.North
        inPlay = True
    End Sub
    Public Function IsInPlay() As Boolean
        Return inPlay
    End Function
End Module
