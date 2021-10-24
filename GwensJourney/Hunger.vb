Module Hunger
    Private _hungerCounter As Integer
    Private _hungerState As Integer
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
    Sub ChangeHungerState(delta As Integer)
        _hungerState += delta
        _hungerCounter = 0
    End Sub
    Sub ChangeHunger(delta As Integer)
        _hungerCounter += delta
        If Not CheckAbility(GetConstitution(), _hungerCounter) Then
            _hungerState += 1
            _hungerCounter = 0
        End If
    End Sub

End Module
