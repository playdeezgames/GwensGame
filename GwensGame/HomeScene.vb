Public Class HomeScene
    Implements IScene

    Public ReadOnly Property ShortStatus As Action Implements IScene.ShortStatus
        Get
            Return Sub()
                       AddPrompt("Yer in yer house.")
                   End Sub
        End Get
    End Property

    Public ReadOnly Property LongStatus As Action Implements IScene.LongStatus
        Get
            Return Sub()
                       AddPrompt("There's no place like home.")
                   End Sub
        End Get
    End Property

    Public Function CanMove(direction As Direction) As Boolean Implements IScene.CanMove
        Return direction = Direction.East
    End Function
End Class
