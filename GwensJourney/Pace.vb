Module Pace
    Private _pace As Integer
    Function GetPace() As Integer
        Return _pace
    End Function
    Sub SetPace(pace As Integer)
        _pace = pace
    End Sub
    Function GetPaceName() As String
        Select Case GetPace()
            Case 1
                Return "Slowest"
            Case 2
                Return "Slow"
            Case 4
                Return "Fast"
            Case 5
                Return "Fastest"
            Case Else
                Return "Medium"
        End Select
    End Function

End Module
