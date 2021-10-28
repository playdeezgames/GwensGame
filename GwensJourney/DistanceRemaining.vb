Module DistanceRemaining
    Private Const INITIAL_DISTANCE_REMAINING = 2000
    Private _distanceRemaining As Integer
    Function GetDistanceRemaining() As Integer
        Return _distanceRemaining
    End Function
    Sub ChangeDistanceRemaining(delta As Integer)
        _distanceRemaining += delta
    End Sub
    Function HasArrived() As Boolean
        Return _distanceRemaining <= 0
    End Function
    Sub ResetDistanceRemaining()
        ChangeDistanceRemaining(INITIAL_DISTANCE_REMAINING - GetDistanceRemaining())
    End Sub
End Module
