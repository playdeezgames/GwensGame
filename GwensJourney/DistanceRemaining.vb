Module DistanceRemaining
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

End Module
