Friend Module HungerCounter
    Friend Sub Change(delta As Integer)
        Context.Counter(CounterNames.HungerCounter) += delta
        If Not CheckAbility(GetConstitution(), Context.Counter(CounterNames.HungerCounter)) Then
            Context.Counter(CounterNames.HungerState) += 1
            Context.Counter(CounterNames.HungerCounter) = 0
        End If
    End Sub
End Module
