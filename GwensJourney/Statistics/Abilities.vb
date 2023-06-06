Friend Class Abilities
    Implements IAbilities
    Private ReadOnly _context As IContext
    Sub New(context As IContext)
        _context = context
    End Sub
    Public Sub RollAbilities() Implements IAbilities.RollAbilities
        LegacyContext.Counter(CounterNames.Constitution) = RollAbility()
    End Sub
    Public Function GetConstitution() As Integer Implements IAbilities.GetConstitution
        Return _context.Counter(CounterNames.Constitution)
    End Function
    Public Function CheckAbility(abilityScore As Integer, difficulty As Integer) As Boolean Implements IAbilities.CheckAbility
        Return D20() + GetAbilityBonus(abilityScore) >= difficulty
    End Function
    Private Function RollAbility() As Integer
        Return D6() + D6() + D6()
    End Function
    Private Function GetAbilityBonus(abilityScore As Integer) As Integer
        Return (abilityScore - abilityScore Mod 2) \ 2 - 5
    End Function
End Class
