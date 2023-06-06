Friend Class HungerCounter
    Implements IHungerCounter
    Private ReadOnly _context As Context
    Public Sub New(context As Context)
        Me._context = context
    End Sub
    Public Sub Change(delta As Integer) Implements IHungerCounter.Change
        _context.Counter(CounterNames.HungerCounter) += delta
        If Not _context.Abilities.CheckAbility(_context.Abilities.GetConstitution(), _context.Counter(CounterNames.HungerCounter)) Then
            _context.HungerState.Change(1)
        End If
    End Sub
    Public Sub Reset() Implements IHungerCounter.Reset
        _context.Counter(CounterNames.HungerCounter) = 0
    End Sub
End Class
