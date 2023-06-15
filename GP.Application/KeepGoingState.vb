Friend Class KeepGoingState
    Inherits BaseState
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub
    Public Overrides Sub Run()
        With _frameBuffer
            .BackgroundColor = Black
            .ForegroundColor = Gray
            .WriteLine()
            .WriteLine("You keep going!")
        End With
        Dim avatar = Engine.World.Avatar
        avatar.KeepGoing(1)
        GoToState(Neutral)
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Throw New NotImplementedException()
    End Function
End Class
