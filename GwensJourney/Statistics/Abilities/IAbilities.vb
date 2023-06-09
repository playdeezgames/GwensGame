Public Interface IAbilities
    Function GetConstitution() As Integer
    Function GetWisdom() As Integer
    Function CheckAbility(abilityScore As Integer, difficulty As Integer) As Boolean
    Sub RollAbilities()
End Interface
