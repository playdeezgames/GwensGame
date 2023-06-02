Friend Module HungerState
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
    Friend Function IsHungry() As Boolean
        Return Context.Counter(CounterNames.HungerState) > 0
    End Function
    Friend Function Name() As String
        Return hungerStateNames(Math.Clamp(Context.Counter(CounterNames.HungerState), MinimumHungerState, MaximumHungerState))
    End Function
    Friend Sub Change(delta As Integer)
        Context.Counter(CounterNames.HungerState) += delta
        Context.Counter(CounterNames.HungerCounter) = 0
    End Sub
    Friend Function IsDead() As Boolean
        Return Context.Counter(CounterNames.HungerState) >= MaximumHungerState
    End Function

    Friend Sub Reset()
        Context.Counter(CounterNames.HungerState) = MinimumHungerState
    End Sub
End Module
