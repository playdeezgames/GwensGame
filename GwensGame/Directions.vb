Module Directions
    Function GetDirectionName(direction As Direction) As String
        Select Case direction
            Case Direction.North
                Return "north"
            Case Direction.East
                Return "east"
            Case Direction.South
                Return "south"
            Case Direction.West
                Return "west"
            Case Else
                Return "????"
        End Select
    End Function
    Function GetNextX(x As Integer, y As Integer, direction As Direction) As Integer
        Select Case direction
            Case Direction.East
                Return x + 1
            Case Direction.West
                Return x - 1
            Case Else
                Return x
        End Select
    End Function
    Function GetNextY(x As Integer, y As Integer, direction As Direction) As Integer
        Select Case direction
            Case Direction.South
                Return y + 1
            Case Direction.North
                Return y - 1
            Case Else
                Return y
        End Select
    End Function
    Function GetNextDirection(direction As Direction) As Direction
        Select Case direction
            Case Direction.North
                Return Direction.East
            Case Direction.East
                Return Direction.South
            Case Direction.South
                Return Direction.West
            Case Else
                Return Direction.North
        End Select
    End Function
    Function GetPreviousDirection(direction As Direction) As Direction
        Select Case direction
            Case Direction.North
                Return Direction.West
            Case Direction.East
                Return Direction.North
            Case Direction.South
                Return Direction.East
            Case Else
                Return Direction.South
        End Select
    End Function
    Function GetOppositeDirection(direction As Direction) As Direction
        Select Case direction
            Case Direction.North
                Return Direction.South
            Case Direction.East
                Return Direction.West
            Case Direction.South
                Return Direction.North
            Case Else
                Return Direction.East
        End Select
    End Function
End Module
