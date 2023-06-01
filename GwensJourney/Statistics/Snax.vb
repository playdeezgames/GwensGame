Friend Module Snax
    Private Const INITIAL_SNAX As Integer = 10
    Friend Function GetSnax() As Integer
        Return Context.Counter(CounterNames.Snax)
    End Function
    Friend Sub ChangeSnax(delta As Integer)
        Context.Counter(CounterNames.Snax) += delta
    End Sub
    Friend Sub ResetSnax()
        ChangeSnax(INITIAL_SNAX - GetSnax())
    End Sub
End Module
