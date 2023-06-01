Friend Module Hunger
    Friend Function IsHungry() As Boolean
        Return Context.Counter(CounterNames.HungerState) > 0
    End Function
    Friend Function GetHungerStateName() As String
        Select Case Context.Counter(CounterNames.HungerState)
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
    Friend Sub ChangeHungerState(delta As Integer)
        Context.Counter(CounterNames.HungerState) += delta
        Context.Counter(CounterNames.HungerCounter) = 0
    End Sub
    Friend Sub ChangeHunger(delta As Integer)
        Context.Counter(CounterNames.HungerCounter) += delta
        If Not CheckAbility(GetConstitution(), Context.Counter(CounterNames.HungerCounter)) Then
            Context.Counter(CounterNames.HungerState) += 1
            Context.Counter(CounterNames.HungerCounter) = 0
        End If
    End Sub
End Module
