Friend Module Pace
    Private Const INITIAL_PACE As Integer = 3
    Private Const MINIMUM_PACE = 1
    Private Const MAXIMUM_PACE = 5
    Private ReadOnly paceNames As IReadOnlyDictionary(Of Integer, String) =
        New Dictionary(Of Integer, String) From
        {
            {1, "[green]Slowest[/]"},
            {2, "[aqua]Slow[/]"},
            {3, "[blue]Medium[/]"},
            {4, "[purple]Fast[/]"},
            {5, "[fuchsia]Fastest[/]"}
        }
    Friend Function Read() As Integer
        Return Context.Counter(CounterNames.Pace)
    End Function
    Friend Sub Write(pace As Integer)
        Context.Counter(CounterNames.Pace) = Math.Clamp(pace, MINIMUM_PACE, MAXIMUM_PACE)
    End Sub
    Friend Function Name() As String
        Return paceNames(Context.Counter(CounterNames.Pace))
    End Function
    Friend Function Name(index As Integer) As String
        Return paceNames(Math.Clamp(index, MINIMUM_PACE, MAXIMUM_PACE))
    End Function
    Friend Sub Reset()
        Write(INITIAL_PACE)
    End Sub
End Module
