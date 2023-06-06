Friend Module LegacyHungerCounter
    Friend Sub Change(delta As Integer)
        LegacyContext.Counter(CounterNames.HungerCounter) += delta
        If Not CheckAbility(GetConstitution(), LegacyContext.Counter(CounterNames.HungerCounter)) Then
            LegacyHungerState.Change(1)
        End If
    End Sub

    Friend Sub Reset()
        LegacyContext.Counter(CounterNames.HungerCounter) = 0
    End Sub
End Module
