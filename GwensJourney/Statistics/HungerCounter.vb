Friend Module HungerCounter
    Friend Sub Change(delta As Integer)
        Context.Counter(CounterNames.HungerCounter) += delta
        If Not CheckAbility(GetConstitution(), Context.Counter(CounterNames.HungerCounter)) Then
            HungerState.Change(1)
        End If
    End Sub
End Module
