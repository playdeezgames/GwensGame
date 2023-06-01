Friend Module Pace
    Private Const INITIAL_PACE As Integer = 3
    Private Const MINIMUM_PACE = 1
    Private Const MAXIMUM_PACE = 5
    Private ReadOnly paceNames As IReadOnlyDictionary(Of Integer, String) =
        New Dictionary(Of Integer, String) From
        {
            {1, "Slowest"},
            {2, "Slow"},
            {3, "Medium"},
            {4, "Fast"},
            {5, "Fastest"}
        }
    Friend Function GetPace() As Integer
        Return Context.Counter(CounterNames.Pace)
    End Function
    Friend Sub SetPace(pace As Integer)
        Context.Counter(CounterNames.Pace) = Math.Clamp(pace, MINIMUM_PACE, MAXIMUM_PACE)
    End Sub
    Friend Function Name() As String
        Return paceNames(Context.Counter(CounterNames.Pace))
    End Function
    Friend Sub Reset()
        SetPace(INITIAL_PACE)
    End Sub
End Module
