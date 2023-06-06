Friend Module LegacyAbilities
    Friend Function GetConstitution() As Integer
        Return LegacyContext.Counter(CounterNames.Constitution)
    End Function
    Private Function GetAbilityBonus(abilityScore As Integer) As Integer
        Return (abilityScore - abilityScore Mod 2) \ 2 - 5
    End Function
    Friend Function CheckAbility(abilityScore As Integer, difficulty As Integer) As Boolean
        Return D20() + GetAbilityBonus(abilityScore) >= difficulty
    End Function
    Private Function RollAbility() As Integer
        Return D6() + D6() + D6()
    End Function
    Friend Sub RollAbilities()
        LegacyContext.Counter(CounterNames.Constitution) = RollAbility()
    End Sub
End Module
