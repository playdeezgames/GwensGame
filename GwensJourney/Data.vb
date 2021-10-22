Module Data
    Private _distanceRemaining As Integer
    Private _inPlay As Boolean
    Function GetDistanceRemaining() As Integer
        Return _distanceRemaining
    End Function
    Sub ResetData()
        _distanceRemaining = 2000
        _inPlay = True
    End Sub
    Function IsInPlay() As Boolean
        Return _inPlay
    End Function
End Module
