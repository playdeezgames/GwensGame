Public Interface IDistanceRemaining
    Function Read() As Integer
    Sub Change(delta As Integer)
    Function HasArrived() As Boolean
    Sub Reset()
End Interface
