Module Data
    Private x As Integer = 0
    Private y As Integer = 0
    Private facing As Direction = Direction.North
    Public Function GetX() As Integer
        Return x
    End Function
    Public Function GetY() As Integer
        Return y
    End Function
    Public Function SetXY(newX As Integer, newY As Integer)
        x = newX
        y = newY
    End Function
    Public Function GetFacing() As Direction
        Return facing
    End Function
    Public Sub ResetData()
        x = 0
        y = 0
        facing = Direction.North
    End Sub
End Module
