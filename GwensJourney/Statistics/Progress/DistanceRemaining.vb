Friend Class DistanceRemaining
    Implements IDistanceRemaining
    Private Const INITIAL_DISTANCE_REMAINING = 2000
    Private ReadOnly _context As IContext
    Sub New(context As IContext)
        _context = context
    End Sub
    Public Sub Change(delta As Integer) Implements IDistanceRemaining.Change
        _context.Counter(CounterNames.DistanceRemaining) += delta
    End Sub
    Public Sub Reset() Implements IDistanceRemaining.Reset
        Change(INITIAL_DISTANCE_REMAINING - Read())
    End Sub
    Public Function Read() As Integer Implements IDistanceRemaining.Read
        Return _context.Counter(CounterNames.DistanceRemaining)
    End Function
    Public Function HasArrived() As Boolean Implements IDistanceRemaining.HasArrived
        Return Read() <= 0
    End Function
End Class
