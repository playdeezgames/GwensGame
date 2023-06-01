Friend Module HungerState
    Private Const HUNGER_STATE_MINIMUM = 0
    Private Const HUNGER_STATE_MAXIMUM = 4
    Private ReadOnly hungerStateNames As IReadOnlyDictionary(Of Integer, String) =
        New Dictionary(Of Integer, String) From
        {
            {0, "not hungry"},
            {1, "hungry"},
            {2, "very hungry"},
            {3, "starving"},
            {4, "starved to death"}
        }
    Friend Function IsHungry() As Boolean
        Return Context.Counter(CounterNames.HungerState) > 0
    End Function
    Friend Function Name() As String
        Return hungerStateNames(Math.Clamp(Context.Counter(CounterNames.HungerState), HUNGER_STATE_MINIMUM, HUNGER_STATE_MAXIMUM))
    End Function
    Friend Sub Change(delta As Integer)
        Context.Counter(CounterNames.HungerState) += delta
        Context.Counter(CounterNames.HungerCounter) = 0
    End Sub
End Module
