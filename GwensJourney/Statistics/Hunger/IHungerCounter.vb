Public Interface IHungerCounter
    Sub Change(delta As Integer, Optional constitutionCheck As Boolean = True)
    Sub Reset()
End Interface
