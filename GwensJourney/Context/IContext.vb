Public Interface IContext
    Property Flag(flagName As String) As Boolean
    Property Counter(counterName As String) As Integer
    Function IsInPlay() As Boolean
    Sub SetGameOver()
    Sub Initialize()
    ReadOnly Property Abilities As IAbilities
    ReadOnly Property DistanceRemaining As IDistanceRemaining
    ReadOnly Property HungerCounter As IHungerCounter
    ReadOnly Property HungerState As IHungerState
    ReadOnly Property Pace As IPace
    ReadOnly Property Snax As ISnax
    ReadOnly Property Foraging As IForaging
End Interface
