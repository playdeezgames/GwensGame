Friend Class Context
    Implements IContext
    Private ReadOnly _flags As New HashSet(Of String)
    Private ReadOnly _counters As New Dictionary(Of String, Integer)
    Public Property Flag(flagName As String) As Boolean Implements IContext.Flag
        Get
            Return _flags.Contains(flagName)
        End Get
        Set(value As Boolean)
            If value Then
                _flags.Add(flagName)
            Else
                _flags.Remove(flagName)
            End If
        End Set
    End Property
    Public Property Counter(counterName As String) As Integer Implements IContext.Counter
        Get
            If _counters.ContainsKey(counterName) Then
                Return _counters(counterName)
            End If
            Return 0
        End Get
        Set(value As Integer)
            _counters(counterName) = value
        End Set
    End Property

    Public ReadOnly Property Abilities As IAbilities Implements IContext.Abilities
        Get
            Return New Abilities(Me)
        End Get
    End Property

    Public ReadOnly Property HungerCounter As IHungerCounter Implements IContext.HungerCounter
        Get
            Return New HungerCounter(Me)
        End Get
    End Property

    Public ReadOnly Property HungerState As IHungerState Implements IContext.HungerState
        Get
            Return New HungerState(Me)
        End Get
    End Property

    Public ReadOnly Property Pace As IPace Implements IContext.Pace
        Get
            Return New Pace(Me)
        End Get
    End Property

    Public ReadOnly Property Snax As ISnax Implements IContext.Snax
        Get
            Return New Snax(Me)
        End Get
    End Property

    Public ReadOnly Property DistanceRemaining As IDistanceRemaining Implements IContext.DistanceRemaining
        Get
            Return New DistanceRemaining(Me)
        End Get
    End Property

    Public Sub SetGameOver() Implements IContext.SetGameOver
        Flag(FlagNames.InPlay) = False
    End Sub
    Public Sub Initialize() Implements IContext.Initialize
        Flag(FlagNames.InPlay) = True
        Snax.Reset()
        DistanceRemaining.Reset()
        Pace.Reset()
        Abilities.RollAbilities()
        HungerState.Reset()
        HungerCounter.Reset()
    End Sub
    Public Function IsInPlay() As Boolean Implements IContext.IsInPlay
        Return Flag(FlagNames.InPlay)
    End Function
End Class
