Friend Module DistanceRemaining
    Private Const INITIAL_DISTANCE_REMAINING = 2000
    Friend Function Read() As Integer
        Return Context.Counter(CounterNames.DistanceRemaining)
    End Function
    Friend Sub Change(delta As Integer)
        Context.Counter(CounterNames.DistanceRemaining) += delta
    End Sub
    Friend Function HasArrived() As Boolean
        Return Read() <= 0
    End Function
    Friend Sub Reset()
        Change(INITIAL_DISTANCE_REMAINING - Read())
    End Sub
End Module
