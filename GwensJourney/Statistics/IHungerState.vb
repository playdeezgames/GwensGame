Public Interface IHungerState
    Function IsHungry() As Boolean
    Function Name() As String
    Sub Change(delta As Integer)
    Function IsDead() As Boolean
    Sub Reset()
End Interface
