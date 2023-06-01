Friend Module Snax
    Private Const INITIAL_SNAX As Integer = 10
    Friend Function Count() As Integer
        Return Context.Counter(CounterNames.Snax)
    End Function
    Friend Sub Change(delta As Integer)
        Context.Counter(CounterNames.Snax) += delta
    End Sub
    Friend Sub Reset()
        Change(INITIAL_SNAX - Count())
    End Sub
End Module
