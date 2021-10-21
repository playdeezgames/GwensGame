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

End Module
