Module Data
    Private _random As New Random()
    Private _distanceRemaining As Integer
    Private _inPlay As Boolean
    Private _snax As Integer
    Private _constitution As Integer
    Private _pace As Integer
    Private _hungerCounter As Integer
    Private _hungerState As Integer
    Private Function D6() As Integer
        Return _random.Next(1, 7)
    End Function
    Private Function RollAbility() As Integer
        Return D6() + D6() + D6()
    End Function
    Function GetDistanceRemaining() As Integer
        Return _distanceRemaining
    End Function
    Function GetSnax() As Integer
        Return _snax
    End Function
    Function GetPace() As Integer
        Return _pace
    End Function
    Sub SetPace(pace As Integer)
        _pace = pace
    End Sub
    Function GetPaceName() As String
        Select Case GetPace()
            Case 1
                Return "Slowest"
            Case 2
                Return "Slow"
            Case 4
                Return "Fast"
            Case 5
                Return "Fastest"
            Case Else
                Return "Medium"
        End Select
    End Function
    Public Function IsHungry() As Boolean
        Return _hungerState > 0
    End Function
    Public Function GetHungerStateName() As String
        Select Case _hungerState
            Case 0
                Return "not hungry"
            Case 1
                Return "hungry"
            Case 2
                Return "very hungry"
            Case 3
                Return "starving"
            Case Else
                Return "starved to death"
        End Select
    End Function
    Sub ChangeSnax(delta As Integer)
        _snax += delta
    End Sub
    Sub ChangeHungerState(delta As Integer)
        _hungerState += delta
        _hungerCounter = 0
    End Sub
    Function GetConstitution() As Integer
        Return _constitution
    End Function
    Sub ChangeDistanceRemaining(delta As Integer)
        _distanceRemaining += delta
    End Sub
    Function HasArrived() As Boolean
        Return _distanceRemaining <= 0
    End Function
    Function GetAbilityBonus(abilityScore As Integer)
        Return (abilityScore - abilityScore Mod 2) / 2 - 5
    End Function
    Function D20() As Integer
        Return _random.Next(1, 21)
    End Function
    Function CheckAbility(abilityScore As Integer, difficulty As Integer) As Boolean
        Return D20() + GetAbilityBonus(abilityScore) >= difficulty
    End Function
    Sub ChangeHunger(delta As Integer)
        _hungerCounter += delta
        If Not CheckAbility(_constitution, _hungerCounter) Then
            _hungerState += 1
            _hungerCounter = 0
        End If
    End Sub
    Sub ResetData()
        _constitution = RollAbility()
        _snax = 10
        _distanceRemaining = 2000
        _inPlay = True
        _pace = 3
    End Sub
    Function IsInPlay() As Boolean
        Return _inPlay
    End Function
    Sub SetGameOver()
        _inPlay = False
    End Sub
End Module
