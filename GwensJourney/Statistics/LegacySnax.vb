Friend Module LegacySnax
    Private Const INITIAL_SNAX As Integer = 10
    Friend Function Count() As Integer
        Return LegacyContext.Counter(CounterNames.Snax)
    End Function
    Friend Sub Change(delta As Integer)
        LegacyContext.Counter(CounterNames.Snax) += delta
    End Sub
    Friend Sub Reset()
        Change(INITIAL_SNAX - Count())
    End Sub
End Module
