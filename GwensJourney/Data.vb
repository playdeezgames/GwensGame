Module Data
    Private _distanceRemaining As Integer
    Private _inPlay As Boolean
    Function GetDistanceRemaining() As Integer
        Return _distanceRemaining
    End Function
    Sub ChangeDistanceRemaining(delta As Integer)
        _distanceRemaining += delta
    End Sub
    Function HasArrived() As Boolean
        Return _distanceRemaining <= 0
    End Function
    Sub ResetData()
        _distanceRemaining = 20
        _inPlay = True
    End Sub
    Function IsInPlay() As Boolean
        Return _inPlay
    End Function
    Sub SetGameOver()
        _inPlay = False
    End Sub
End Module
