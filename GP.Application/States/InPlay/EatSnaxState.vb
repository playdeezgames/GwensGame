Friend Class EatSnaxState
    Inherits BaseState
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub
    Public Overrides Sub Run()
        Dim avatar = Engine.World.Avatar
        With FrameBuffer
            .WriteLine(, Gray, Black)
            .WriteLine("You eat snax.")
            avatar.EatSnax()
            GoToState(Neutral)
        End With
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Throw New NotImplementedException()
    End Function
End Class
