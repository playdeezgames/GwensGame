Friend Class HungerState
    Implements IHungerState
    Private ReadOnly _context As Context
    Public Sub New(context As Context)
        Me._context = context
    End Sub
    Private Const MinimumHungerState = 0
    Private Const MaximumHungerState = 4
    Private ReadOnly hungerStateNames As IReadOnlyDictionary(Of Integer, String) =
        New Dictionary(Of Integer, String) From
        {
            {0, "[lime]not hungry[/]"},
            {1, "[yellow]hungry[/]"},
            {2, "[olive]very hungry[/]"},
            {3, "[red]starving[/]"},
            {4, "[grey]starved to death[/]"}
        }
    Function IsHungry() As Boolean Implements IHungerState.IsHungry
        Return _context.Counter(CounterNames.HungerState) > 0
    End Function
    Function Name() As String Implements IHungerState.Name
        Return hungerStateNames(Math.Clamp(_context.Counter(CounterNames.HungerState), MinimumHungerState, MaximumHungerState))
    End Function
    Sub Change(delta As Integer) Implements IHungerState.Change
        _context.Counter(CounterNames.HungerState) += delta
        _context.Counter(CounterNames.HungerCounter) = 0
    End Sub
    Function IsDead() As Boolean Implements IHungerState.IsDead
        Return _context.Counter(CounterNames.HungerState) >= MaximumHungerState
    End Function
    Sub Reset() Implements IHungerState.Reset
        _context.Counter(CounterNames.HungerState) = MinimumHungerState
    End Sub
End Class
