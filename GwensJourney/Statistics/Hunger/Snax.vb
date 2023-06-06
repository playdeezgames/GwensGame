Friend Class Snax
    Implements ISnax
    Private ReadOnly _context As Context
    Public Sub New(context As Context)
        Me._context = context
    End Sub
    Private Const INITIAL_SNAX As Integer = 10
    Function Count() As Integer Implements ISnax.Count
        Return _context.Counter(CounterNames.Snax)
    End Function
    Sub Change(delta As Integer) Implements ISnax.Change
        _context.Counter(CounterNames.Snax) += delta
    End Sub
    Sub Reset() Implements ISnax.Reset
        Change(INITIAL_SNAX - Count())
    End Sub
End Class
