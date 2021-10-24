Module Abilities
    Private _constitution As Integer
    Function GetConstitution() As Integer
        Return _constitution
    End Function
    Function GetAbilityBonus(abilityScore As Integer)
        Return (abilityScore - abilityScore Mod 2) / 2 - 5
    End Function
    Function CheckAbility(abilityScore As Integer, difficulty As Integer) As Boolean
        Return D20() + GetAbilityBonus(abilityScore) >= difficulty
    End Function
    Private Function RollAbility() As Integer
        Return D6() + D6() + D6()
    End Function
    Sub RollAbilities()
        _constitution = RollAbility()
    End Sub
End Module
