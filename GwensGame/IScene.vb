Public Interface IScene
    ReadOnly Property ShortStatus As Action
    ReadOnly Property LongStatus As Action
    Function CanMove(direction As Direction) As Boolean
End Interface
