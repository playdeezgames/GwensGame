Public Class DefaultScene
    Implements IScene

    Public ReadOnly Property ShortStatus As Action Implements IScene.ShortStatus
        Get
            Return Sub()
                       AddPrompt($"You are at ({Data.GetX()}, {Data.GetY()}) and facing {GetDirectionName(Data.GetFacing())}.")
                   End Sub
        End Get
    End Property

    Public ReadOnly Property LongStatus As Action Implements IScene.LongStatus
        Get
            Return Sub()
                       AddPrompt($"You are at ({Data.GetX()}, {Data.GetY()}) and facing {GetDirectionName(Data.GetFacing())}.")
                   End Sub
        End Get
    End Property

    Public Function CanMove(direction As Direction) As Boolean Implements IScene.CanMove
        Return True
    End Function
End Class
