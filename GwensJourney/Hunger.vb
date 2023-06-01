Friend Module Hunger
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
    Friend Function GetHungerStateName() As String
        Return hungerStateNames(Math.Clamp(Context.Counter(CounterNames.HungerState), HUNGER_STATE_MINIMUM, HUNGER_STATE_MAXIMUM))
    End Function
    Friend Sub ChangeHungerState(delta As Integer)
        Context.Counter(CounterNames.HungerState) += delta
        Context.Counter(CounterNames.HungerCounter) = 0
    End Sub
    Friend Sub ChangeHunger(delta As Integer)
        Context.Counter(CounterNames.HungerCounter) += delta
        If Not CheckAbility(GetConstitution(), Context.Counter(CounterNames.HungerCounter)) Then
            Context.Counter(CounterNames.HungerState) += 1
            Context.Counter(CounterNames.HungerCounter) = 0
        End If
    End Sub
End Module
